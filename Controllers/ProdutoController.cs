using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ControleDeEstoque.Models;
using ControleDeEstoque.Servicos.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeEstoque.Controllers
{
    [ApiController]
    [Route("api/produto")]
    public class ProdutoController : ControllerBase
    {
        private readonly IQueryDeUsuario _QueryDeUsuario;
        private readonly IQueryDeProduto _QueryDeProduto;

        public ProdutoController(IQueryDeUsuario queryDeUsuario, IQueryDeProduto queryDeProduto)
        {
            _QueryDeUsuario = queryDeUsuario;
            _QueryDeProduto = queryDeProduto;
        }

        [Authorize]
        [HttpPost("inserir")]
        public async Task<ActionResult> Inserir(Produto produto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { message = "Erro durante inserção" });

            try
            {
                produto.Nome = TratarString(produto.Nome);
                var usuarioId = GetIdDoUsuario();
                if (await _QueryDeProduto.InserirProduto(produto, usuarioId))
                    return Ok(new { message = "Produtos inseridos com sucesso." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Erro ao salvar: " + ex.Message });
            }
            return BadRequest(new { message = "Não foi possível salvar os dados..." });
        }

        [Authorize]
        [HttpPatch("adicionar/{nomeDoProduto}/{quantidade}")]
        public async Task<ActionResult> AdicionarProdutos(string nomeDoProduto, 
                                                              int quantidade)
        {
            if (nomeDoProduto == null || nomeDoProduto.Trim() == string.Empty ||                quantidade == 0)
            {
                return BadRequest(
                    new { message = "Insira o nome do produto" }
                );
            }

            nomeDoProduto = TratarString(nomeDoProduto);
            var usuarioId = GetIdDoUsuario();

            try
            {
                await _QueryDeProduto.IncrementarQuantidade(
                    nomeDoProduto, quantidade, usuarioId
                );
                return Ok(new { message = "Quantidade incrementada com sucesso..." });
            }
            catch(Exception ex)
            {
                return BadRequest(new { message= "Erro ao incrementar..." });
            }
        }

        private string TratarString(string texto)
        {
            var padroes = new string[] { @"\s+", @"ç", @"Ç", @"Ã", @"ã" };
            var substitutos = new string[] { "", "c", "C", "A", "a" };
            var contador = 0;
            foreach (var padrao in padroes)
            {
                texto = Regex.Replace(texto, padrao, substitutos[contador]);
                contador++;
            }
            return texto;
        }

        [Authorize]
        [HttpGet("retirar/{nome}/{quantidade}")]
        public async Task<ActionResult> RetirarProduto(string nome, int quantidade)
        {
            if (nome == null || nome.Trim() == string.Empty || quantidade == 0)
            {
                return BadRequest(
                    new { message = "Insira o nome do produto" }
                );
            }
            nome = TratarString(nome);
            var usuarioId = GetIdDoUsuario();
            try
            {
                if (await _QueryDeProduto.RetirarProduto(nome, quantidade, usuarioId))
                    return Ok(new { message = "Quantia decrementada com sucesso!" });
            }
            catch (Exception ex)
            {
                return BadRequest(
                    new { message = "Quantidade insuficiente: " + ex.Message }
                );
            }
            return BadRequest(
                new { message = "Quantia indisponível, insira nova quantia..." }
            );
        }

        private int GetIdDoUsuario()
        {
            return Convert.ToInt32(User.Identity.Name);
        }

        [Authorize]
        [HttpGet("listar")]
        public async Task<ICollection<Produto>> GetProdutos()
        {
            var usuarioId = GetIdDoUsuario();
            return await _QueryDeProduto.ListarProdutos(usuarioId);
        }

        [Authorize]
        [HttpGet("listar/{nome}")]
        public async Task<Produto> GetProduto(int id)
        {
            var usuarioId = GetIdDoUsuario();
            Produto produto = await _QueryDeProduto.BuscarPorId(id, usuarioId);
            return produto;
        }
        
        [Authorize]
        [HttpPut("atualizar/{id}")]
        public async Task<ActionResult<Produto>> AtualizarProduto(Produto produto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { message = "Erro durante inserção" });

            try
            {
                await _QueryDeProduto.AtualizarProduto(produto);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Erro ao Atualizar: " + ex.Message });
            }
            return BadRequest(new { message = "Não foi possível atualizar os dados..." });
        }

    }
}