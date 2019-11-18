using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ControleDeEstoque.Models;
using ControleDeEstoque.Servicos.Interfaces;

namespace ControleDeEstoque.Servicos.QuerieService
{
    public class QueryDeUsuario : IQueryDeUsuario
    {
        private readonly Contexto _contexto;
        public QueryDeUsuario(Contexto contexto)
        {
            _contexto = contexto;
        }
        public async Task<Usuario> AutenticarUsuario(Usuario usuario)
        {
            return _contexto.Usuarios.Where(ValidarDados(usuario)).FirstOrDefault();
        }

        private Expression<Func<Usuario, bool>> ValidarDados(Usuario usuario)
        {
            return x => x.Username == usuario.Username && x.Password == usuario.Password;
        }

        public async Task SalvarUsuario(Usuario usuario)
        {
            await _contexto.Usuarios.AddAsync(usuario);
            await _contexto.SaveChangesAsync();
        }
    }
}