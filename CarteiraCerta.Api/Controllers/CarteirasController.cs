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
    }
}