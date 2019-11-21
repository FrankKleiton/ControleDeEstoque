using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ControleDeEstoque.Models;
using ControleDeEstoque.Servicos.Interfaces;
using CryptSharp;

namespace ControleDeEstoque.Servicos.QuerieService
{
    public class QueryDeUsuario : IQueryDeUsuario
    {
        private readonly Contexto _Contexto;
        public QueryDeUsuario(Contexto contexto)
        {
            _Contexto = contexto;
        }
        public Usuario AutenticarUsuario(Usuario usuario)
        {
            var u = _Contexto.Usuarios.Where(
                x => x.Username == usuario.Username
            ).FirstOrDefault();

            if (Crypter.CheckPassword(usuario.Password, u.Password))
                return u;
            return null;
        }

        public async Task SalvarUsuario(Usuario usuario)
        {
            usuario.Password = Crypter.Sha256.Crypt(usuario.Password);
            await _Contexto.Usuarios.AddAsync(usuario);
            await _Contexto.SaveChangesAsync();
        }
    }
}