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
        // O CONTEXTO DO BANCO FOI REMOVIDO PARA O TESTE
        // private readonly ApplicationDbContext _context; 
        private readonly IHttpClientFactory _httpClientFactory;

        // A INJEÇÃO DE DEPENDÊNCIA DO BANCO FOI REMOVIDA DO CONSTRUTOR
        public AtivosController(/*ApplicationDbContext context,*/ IHttpClientFactory httpClientFactory)
        {
            // _context = context;
            _httpClientFactory = httpClientFactory;
        }

        // OS ENDPOINTS DE CRUD FORAM DESABILITADOS TEMPORARIAMENTE
        [HttpGet]
        public ActionResult<IEnumerable<Ativo>> GetAtivos()
        {
            return Ok("Endpoint desabilitado temporariamente devido à conexão do banco de dados.");
        }

        [HttpGet("{id}")]
        public ActionResult<Ativo> GetAtivo(int id)
        {
            return Ok("Endpoint desabilitado temporariamente devido à conexão do banco de dados.");
        }

        [HttpPost]
        public ActionResult<Ativo> PostAtivo(Ativo ativo)
        {
            return Ok("Endpoint desabilitado temporariamente devido à conexão do banco de dados.");
        }

        [HttpPut("{id}")]
        public IActionResult PutAtivo(int id, Ativo ativo)
        {
            return Ok("Endpoint desabilitado temporariamente devido à conexão do banco de dados.");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAtivo(int id)
        {
            return Ok("Endpoint desabilitado temporariamente devido à conexão do banco de dados.");
        }
        
        // ESTE ENDPOINT DEVE FUNCIONAR, POIS NÃO USA O BANCO DE DADOS
        [HttpGet("cotacao/{ticker}")]
        public async Task<IActionResult> GetCotacao(string ticker)
        {
            var tickerParaBusca = ticker.EndsWith(".SA") ? ticker : $"{ticker}.SA";
            var httpClient = _httpClientFactory.CreateClient("Finnhub");
            try
            {
                var response = await httpClient.GetAsync($"quote?symbol={tickerParaBusca.ToUpper()}");

                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var quote = JsonSerializer.Deserialize<QuoteDto>(jsonString);
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