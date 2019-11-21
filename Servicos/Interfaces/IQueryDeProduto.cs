using System.Collections.Generic;
using System.Threading.Tasks;
using ControleDeEstoque.Models;

namespace ControleDeEstoque.Servicos.Interfaces
{
    public interface IQueryDeProduto
    {
         Produto BuscarPorNome(string produto, int usuarioId);
         Task<Produto> BuscarPorId(int id, int usuarioId);
         Task AdicionarProduto(Produto produto);
         Task<dynamic> SalvarMudancas();
         Task<ICollection<Produto>> ListarProdutos(int usuarioId);
         Task<bool> RetirarProduto(string nome, int quantidade, int usuarioId);
         Task<bool> InserirProduto(Produto produto, int usuarioId);
         Task IncrementarQuantidade(string nomeDoProduto, int quantidade, int usuarioId);
         Task AtualizarProduto(Produto produto);
    }
}