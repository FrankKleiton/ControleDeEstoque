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
    [Route("api/saidaestoque")]
    public class SaidaEstoqueController : ControllerBase
    {
        private readonly Contexto _Contexto;
        private readonly IQueryDeProduto _QueryDeProduto;
        
        public SaidaEstoqueController(Contexto contexto, IQueryDeProduto queryDeProduto)
        {
            _Contexto = contexto;
            _QueryDeProduto = queryDeProduto;
        }

        [Authorize]
        [HttpGet("listar/{idProduto}")]
        public async Task<ICollection<SaidaEstoque>> GetSaidasEstoques(int idProduto)
        {
            int usuarioId = Convert.ToInt32(User.Identity.Name);
            var produto = _QueryDeProduto.BuscarPorId(idProduto, usuarioId);
            return await _Contexto.SaidaEstoque.Where(t => t.Id == produto.Id).ToListAsync();
        }
    }
}