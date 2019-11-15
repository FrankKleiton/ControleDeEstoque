using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControleDeEstoque.Models
{
  public class Produto
  {
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Nome { get; set; }

    [Required]
    public decimal Preco { get; set; }

    [Required]
    [StringLength(50)]
    public string Tipo { get; set; }

    [Required]
    public int Quantidade { get; set; }

    [ForeignKey("Usuario")]
    public int UsuarioId { get; set; }
    public virtual Usuario Usuario { get; set; }

    [ForeignKey("TotalVenda")]
    public int TotalVendaId { get; set; }
    public virtual TotalVenda TotalVenda { get; set; }
  }
}