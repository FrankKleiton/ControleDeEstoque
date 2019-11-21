using Microsoft.EntityFrameworkCore;

namespace ControleDeEstoque.Models
{
  public class Contexto : DbContext
  {
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Produto> Produtos { get; set; }
    public DbSet<TotalVenda> TotalVendas { get; set; }
    
    public Contexto(DbContextOptions<Contexto> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder construtorDeModelo)
    {
        construtorDeModelo.Entity<Usuario>().HasIndex(x => x.Username).IsUnique();
    }
  }
}