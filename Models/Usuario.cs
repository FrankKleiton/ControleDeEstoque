using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ControleDeEstoque.Models
{
  public class Usuario
  {
    [Key]
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Este campo é obrigatório")]
    [MaxLength(100, ErrorMessage = "Este campo deve ter no máximo 100 caracteres.")]
    public string Username { get; set; }
    
    [Required(ErrorMessage = "Este campo é obrigatório")]
    [MaxLength(100, ErrorMessage = "Este campo deve ter no máximo 100 caracteres.")]
    public string Password { get; set; }

    public virtual ICollection<Produto> Produtos { get; set; }
  }
}