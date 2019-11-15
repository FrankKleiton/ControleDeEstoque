using Microsoft.EntityFrameworkCore;

namespace ControleDeEstoque.Models
{
  public class Contexto : DbContext
  {
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Produto> Produtos { get; set; }
    public DbSet<Historico> Historicos { get; set; }
    public DbSet<TotalVenda> TotalVendas { get; set; }
    public Contexto(DbContextOptions<Contexto> options) : base(options)
    {
    }
  }
}