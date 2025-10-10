using CarteiraCerta.Data;
using CarteiraCerta.Model; 
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Text.Json;

namespace CarteiraCerta.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarteirasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CarteirasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Carteiras
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Carteira>>> GetCarteiras()
        {
            // .Include para carregar os ativos relacionados
            return await _context.Carteiras.Include(c => c.Ativos).ToListAsync();
        }

        // GET: api/Carteiras/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Carteira>> GetCarteira(int id)
        {
            var carteira = await _context.Carteiras
                .Include(c => c.Ativos)
                .FirstOrDefaultAsync(c => c.IdCarteira == id);

            if (carteira == null)
            {
                return NotFound();
            }

            return carteira;
        }

        // POST: api/Carteiras
        [HttpPost]
        public async Task<ActionResult<Carteira>> PostCarteira(Carteira carteira)
        {
            // validação para garantir que o usuário existe
            var usuario = await _context.Usuarios.FindAsync(carteira.IdUsuario);
            if (usuario == null)
            {
                return BadRequest("Usuário especificado não existe.");
            }

            _context.Carteiras.Add(carteira);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCarteira), new { id = carteira.IdCarteira }, carteira);
        }

        // PUT: api/Carteiras/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCarteira(int id, Carteira carteira)
        {
            if (id != carteira.IdCarteira)
            {
                return BadRequest();
            }

            _context.Entry(carteira).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Carteiras/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCarteira(int id)
        {
            var carteira = await _context.Carteiras.FindAsync(id);
            if (carteira == null)
            {
                return NotFound();
            }

            _context.Carteiras.Remove(carteira);
            await _context.SaveChangesAsync();
            return NoContent();
        }


        [HttpGet("{id}/exportar")]
        public async Task<IActionResult> ExportarCarteiraJson(int id)
        {
            var carteira = await _context.Carteiras
                .Include(c => c.Ativos)
                .FirstOrDefaultAsync(c => c.IdCarteira == id);

            if (carteira == null)
            {
                return NotFound("Carteira não encontrada.");
            }

            var options = new JsonSerializerOptions { WriteIndented = true };
            var jsonString = JsonSerializer.Serialize(carteira, options);
            var bytes = Encoding.UTF8.GetBytes(jsonString);

            return File(bytes, "application/json", $"carteira_{id}.json");
        }
        
        // GET: api/Carteiras/por-usuario/1
        [HttpGet("por-usuario/{usuarioId}")]
        public async Task<ActionResult<IEnumerable<Carteira>>> GetCarteirasPorUsuario(int usuarioId)
        {
            var carteiras = await _context.Carteiras
                .Where(c => c.IdUsuario == usuarioId)
                .Include(c => c.Ativos) // inclui os ativos de cada carteira
                .ToListAsync();

            if (!carteiras.Any())
            {
                return NotFound("Nenhuma carteira encontrada para este usuário.");
            }

            return Ok(carteiras);
        }
    }
}