using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControleDeEstoque.Models;
using ControleDeEstoque.Servicos.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ControleDeEstoque.Controllers
{
    [ApiController]
    [Route("api/totalvenda")]
    public class TotalVendaController : ControllerBase
    {
        private readonly Contexto _Contexto;
        private readonly IQueryDeProduto _QueryDeProduto;
        
        public TotalVendaController(Contexto contexto, IQueryDeProduto queryDeProduto)
        {
            _Contexto = contexto;
            _QueryDeProduto = queryDeProduto;
        }

        [Authorize]
        [HttpGet("listar/{nomeProduto}")]
        public async Task<ICollection<TotalVenda>> GetTotalVenda(string nomeProduto)
        {
            int usuarioId = Convert.ToInt32(User.Identity.Name);
            var produto = _QueryDeProduto.BuscarPorNome(nomeProduto, usuarioId);
            return await _Contexto.TotalVendas.Where(t => t.ProdutoId == produto.Id).ToListAsync();
        }
    }
}