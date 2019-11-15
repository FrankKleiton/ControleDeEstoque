using System;
using System.ComponentModel.DataAnnotations;

namespace ControleDeEstoque.Models
{
  public class Historico
  {
    [Key]
    public int Id { get; set; }
    
    [Required]
    public int ProdutoId { get; set; }
    
    [Required]
    [StringLength(100)]
    public int UsuarioId { get; set; }
    
    [Required]
    public DateTime Data { get; set; }
    
    [Required]
    [StringLength(100)]
    public string NomeProduto { get; set; }
    
    [Required]
    public int Quantidade { get; set; }
    
    [Required]
    public decimal ValorTotalDaVenda { get; set; }
    public string Tipo { get; set; }
  }
}