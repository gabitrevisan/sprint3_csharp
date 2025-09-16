namespace CarteiraCerta.Model
{
    public class Carteira
    {
        [Key]
        public int IdCarteira { get; set; }
        public int IdUsuario { get; set; }
        [ForeignKey("IdUsuario")]
        [JsonIgnore]
        public Usuario? Usuario { get; set; }
        public required string NomeCarteira { get; set; }
        public List<Ativo> Ativos { get; set; } = new List<Ativo>();
    }
}