using Microsoft.EntityFrameworkCore;

namespace ControleDeEstoque.Models
{
  public class Contexto : DbContext
  {
    public DbSet<Usuario> Usuario { get; set; }
    public DbSet<Produto> Produto { get; set; }
    public DbSet<SaidaEstoque> SaidaEstoque { get; set; }
    
    public Contexto(DbContextOptions<Contexto> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder construtorDeModelo)
    {
        construtorDeModelo.Entity<Usuario>().HasIndex(x => x.Username).IsUnique();
    }
  }
}