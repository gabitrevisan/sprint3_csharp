namespace CarteiraCerta.Model
{
    public class Usuario
    {
        [Key]
        public int IdUsuario { get; set; }
        public required string Nome { get; set; }
        public required string Email { get; set; }
    }
}