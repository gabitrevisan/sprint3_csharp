namespace CarteiraCerta.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AtivosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AtivosController(ApplicationDbContext context)
        {
            _context = context;
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
    }
}