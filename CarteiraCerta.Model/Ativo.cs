namespace CarteiraCerta.Model
{
    public class Ativo
    {
        [Key]
        public int IdAtivo { get; set; }
        public required string Ticker { get; set; }
        public required string NomeEmpresa { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal PrecoAtual { get; set; }
    }
}