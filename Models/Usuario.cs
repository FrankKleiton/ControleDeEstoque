using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ControleDeEstoque.Models
{
  public class Usuario
  {
    [Key]
    public int Id { get; set; }
    
    [Required]
    [StringLength(100)]
    public string Username { get; set; }
    
    [Required]
    [StringLength(100)]
    public string Password { get; set; }

    public virtual ICollection<Produto> Produtos { get; set; }
  }
}