using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControleDeEstoque.Models;
using ControleDeEstoque.Servicos.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ControleDeEstoque.Servicos.QueryService
{
    public class QueryDeProduto : IQueryDeProduto
    {
        private readonly Contexto _Contexto;
        private readonly IQueryDeUsuario _QueryDeUsuario;

        public QueryDeProduto(Contexto contexto,
                              IQueryDeUsuario queryDeUsuario)
        {
            _Contexto = contexto;
            _QueryDeUsuario = queryDeUsuario;
        }

        public async Task<bool> InserirProduto(Produto produto, int usuarioId)
        {
            var produtoBuscado = BuscarPorNome(produto.Nome, usuarioId);

            if (produtoBuscado == null)
            {
                return await SalvarNovosProdutos(produto, usuarioId);
            }
            else
            {
                return await IncrementarQuantidadeDeProdutos(produto, produtoBuscado);
            }
        }

        public Produto BuscarPorNome(string nome, int usuarioId)
        {
            return _Contexto.Produto.Where(x =>
                x.Nome == nome && x.UsuarioId == usuarioId
            ).FirstOrDefault();
        }

        private async Task<bool> SalvarNovosProdutos(Produto produto, int usuarioId)
        {
            produto.UsuarioId = usuarioId;
            await AdicionarProduto(produto);
            return true;
        }

        public async Task AdicionarProduto(Produto produto)
        {
            await _Contexto.Produto.AddAsync(produto);
            await SalvarMudancas();
        }

        public async Task<dynamic> SalvarMudancas()
        {
            return await _Contexto.SaveChangesAsync();
        }

        private async Task<bool> IncrementarQuantidadeDeProdutos(Produto produto,
                                                                 Produto produtoBuscado)
        {
            produtoBuscado.Quantidade += produto.Quantidade;
            await SalvarMudancas();
            return true;
        }

        public async Task<ICollection<Produto>> ListarProdutos(int usuarioId)
        {
            return await _Contexto.Produto.Where(
                u => u.UsuarioId == usuarioId
            ).ToListAsync();
        }

        public async Task<bool> RetirarProduto(int id, int quantidade, int usuarioId)
        {
            var produto = await BuscarPorId(id, usuarioId);
            if (produto.Quantidade < quantidade)
                return false;

            await ComputarSaida(quantidade, produto);
            return true;
        }

        private async Task ComputarSaida(int quantidade, Produto produto)
        {
            SaidaEstoque venda = new SaidaEstoque();
            produto.Quantidade -= quantidade;
            venda.ValorVendido = quantidade * produto.Preco;
            venda.ProdutoId = produto.Id;
            venda.Data = DateTime.UtcNow;
            venda.Quantidade = quantidade;
            await _Contexto.SaidaEstoque.AddAsync(venda);
            await _Contexto.SaveChangesAsync();
        }

        public async Task IncrementarQuantidade(int id,
                                                int quantidade,
                                                int usuarioId)
        {
            Produto produto = await BuscarPorId(id, usuarioId);
            produto.Quantidade += quantidade;
            await SalvarMudancas();
        }

        public async Task<Produto> BuscarPorId(int id, int usuarioId)
        {
            return _Contexto.Produto.Where(x => x.Id == id && x.UsuarioId == usuarioId).SingleOrDefault();
        }

        public async Task AtualizarProduto(int id, int usuarioId, Produto produto)
        {
            var produtoBuscado = await BuscarPorId(id, usuarioId);
            produtoBuscado.Nome = produto.Nome;
            produtoBuscado.Preco = produto.Preco;
            produtoBuscado.Tipo = produto.Tipo;
            produtoBuscado.Quantidade = produto.Quantidade;
            await SalvarMudancas();
        }
    }
}