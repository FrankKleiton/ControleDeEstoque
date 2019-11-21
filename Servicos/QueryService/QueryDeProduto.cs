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
            return _Contexto.Produtos.Where(x =>
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
            await _Contexto.Produtos.AddAsync(produto);
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
            return await _Contexto.Produtos.Where(
                u => u.UsuarioId == usuarioId
            ).ToListAsync();
        }

        public async Task<bool> RetirarProduto(string nome, int quantidade, int usuarioId)
        {
            var produto = BuscarPorNome(nome, usuarioId);
            if (produto.Quantidade < quantidade)
                return false;

            await ComputarVenda(quantidade, produto);
            return true;
        }

        private async Task ComputarVenda(int quantidade, Produto produto)
        {
            TotalVenda venda = new TotalVenda();
            produto.Quantidade -= quantidade;
            venda.ValorVendido = quantidade * produto.Preco;
            venda.ProdutoId = produto.Id;
            venda.Data = DateTime.UtcNow;
            await _Contexto.TotalVendas.AddAsync(venda);
            await _Contexto.SaveChangesAsync();
        }

        public async Task IncrementarQuantidade(string nomeDoProduto,
                                                int quantidade,
                                                int usuarioId)
        {
            Produto produto = BuscarPorNome(nomeDoProduto, usuarioId);
            produto.Quantidade += quantidade;
            await SalvarMudancas();
        }

        public async Task<Produto> BuscarPorId(int id, int usuarioId)
        {
            return _Contexto.Produtos.Where(x => x.UsuarioId == usuarioId && x.UsuarioId == usuarioId).SingleOrDefault();
        }

        public async Task AtualizarProduto(Produto produto)
        {
            _Contexto.Entry(produto).State = EntityState.Modified;
            await SalvarMudancas();
        }
    }
}