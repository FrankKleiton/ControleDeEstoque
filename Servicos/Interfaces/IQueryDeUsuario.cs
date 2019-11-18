using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ControleDeEstoque.Models;

namespace ControleDeEstoque.Servicos.Interfaces
{
    public interface IQueryDeUsuario
    {
        Task<Usuario> AutenticarUsuario(Usuario usuario);
        Task SalvarUsuario(Usuario usuario);
    }
}