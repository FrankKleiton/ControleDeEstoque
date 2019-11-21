using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControleDeEstoque.Models
{
    public class TotalVenda
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        public decimal ValorVendido { get; set; }
        public DateTime Data { get; set; }


        [ForeignKey("TotalVenda")]
        public int ProdutoId { get; set; }
        public virtual Produto Produto { get; set; }
    }
}