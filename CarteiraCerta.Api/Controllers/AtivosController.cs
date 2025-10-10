using CarteiraCerta.Data;
using CarteiraCerta.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace CarteiraCerta.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AtivosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpClientFactory _httpClientFactory;

        public AtivosController(ApplicationDbContext context, IHttpClientFactory httpClientFactory)
        {
            _context = context;
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ativo>>> GetAtivos()
        {
            return await _context.Ativos.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Ativo>> GetAtivo(int id)
        {
            var ativo = await _context.Ativos.FindAsync(id);
            if (ativo == null) return NotFound();
            return ativo;
        }

        [HttpPost]
        public async Task<ActionResult<Ativo>> PostAtivo(Ativo ativo)
        {
            _context.Ativos.Add(ativo);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetAtivo), new { id = ativo.IdAtivo }, ativo);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAtivo(int id, Ativo ativo)
        {
            if (id != ativo.IdAtivo) return BadRequest();
            _context.Entry(ativo).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAtivo(int id)
        {
            var ativo = await _context.Ativos.FindAsync(id);
            if (ativo == null) return NotFound();
            _context.Ativos.Remove(ativo);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // GET: api/Ativos/cotacao/PETR4
        [HttpGet("cotacao/{ticker}")]
        public async Task<IActionResult> GetCotacao(string ticker)
        {
            // no mercado brasileiro, tickers geralmente terminam com ".SA" na Finnhub
            var tickerParaBusca = ticker.EndsWith(".SA") ? ticker : $"{ticker}.SA";

            var httpClient = _httpClientFactory.CreateClient("Finnhub");
            try
            {
                var response = await httpClient.GetAsync($"quote?symbol={tickerParaBusca.ToUpper()}");

                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var quote = JsonSerializer.Deserialize<QuoteDto>(jsonString);

                    // API da Finnhub retorna 0 para cotações não encontradas
                    if (quote.CurrentPrice == 0)
                    {
                        return NotFound($"Cotação para o ticker '{tickerParaBusca}' não encontrada.");
                    }

                    return Ok(quote);
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    return StatusCode((int)response.StatusCode, $"Não foi possível obter a cotação. Resposta da API: {errorContent}");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno ao chamar a API de cotações: {ex.Message}");
            }
        }
    }
}