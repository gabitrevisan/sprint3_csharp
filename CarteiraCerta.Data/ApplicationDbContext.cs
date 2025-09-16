using CarteiraCerta.Model;
using Microsoft.EntityFrameworkCore;

namespace CarteiraCerta.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Ativo> Ativos { get; set; }
        public DbSet<Carteira> Carteiras { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Usuario>().ToTable("CarteiraCerta_Usuarios");
            modelBuilder.Entity<Ativo>().ToTable("CarteiraCerta_Ativos");
            modelBuilder.Entity<Carteira>().ToTable("CarteiraCerta_Carteiras");
        }
    }
}