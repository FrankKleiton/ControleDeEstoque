using System;
using System.ComponentModel.DataAnnotations;

namespace ControleDeEstoque.Models
{
  public class Historico
  {
    [Key]
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Este campo é obrigatório")]
    public int ProdutoId { get; set; }
    
    [Required(ErrorMessage = "Este campo é obrigatório")]
    [MaxLength(100, ErrorMessage = "Este campo deve ter no máximo 100 caracteres.")]
    public int UsuarioId { get; set; }
    
    [Required(ErrorMessage = "Este campo é obrigatório")]
    public DateTime Data { get; set; }
    
    [Required(ErrorMessage = "Este campo é obrigatório")]
    [MaxLength(100, ErrorMessage = "Este campo deve ter no máximo 100 caracteres.")]
    public string NomeProduto { get; set; }
    
    [Required(ErrorMessage = "Este campo é obrigatório")]
    public int Quantidade { get; set; }
    
    [Required(ErrorMessage = "Este campo é obrigatório")]
    public string Tipo { get; set; }

    [Required(ErrorMessage = "Este campo é obrigatório")]
    public decimal ValorTotalDaVenda { get; set; }
  }
}