using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControleDeEstoque.Models
{
  public class Produto
  {
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Este campo é obrigatório")]
    [MaxLength(100, ErrorMessage = "Este campo deve ter no máximo 100 caracteres.")]
    public string Nome { get; set; }

    [Required(ErrorMessage = "Este campo é obrigatório")]
    public decimal Preco { get; set; }

    [Required(ErrorMessage = "Este campo é obrigatório")]
    [MaxLength(50, ErrorMessage = "Este campo deve ter no máximo 50 caracteres.")]
    public string Tipo { get; set; }

    [Required(ErrorMessage = "Este campo é obrigatório")]
    public int Quantidade { get; set; }

    [ForeignKey("Usuario")]
    public int UsuarioId { get; set; }
    public virtual Usuario Usuario { get; set; }


    public int? SaidaEstoqueId { get; set; }
    public virtual ICollection<SaidaEstoque> SaidaEstoque { get; set; }
  }
}