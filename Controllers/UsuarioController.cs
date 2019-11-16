using Microsoft.AspNetCore.Mvc;
using ControleDeEstoque.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using ControleDeEstoque.Servicos.Token;
using System.Linq.Expressions;
using System;

namespace ControleDeEstoque.Controllers
{
    [ApiController]
    [Route("api/v1")]
    public class UsuarioController : ControllerBase
    {
        private readonly Contexto _contexto;

        public UsuarioController(Contexto contexto)
        {
            _contexto = contexto;
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<dynamic>> Autenticar([FromBody] Usuario usuario)
        {
            Usuario usuarioEncontrado = BuscarUsuario(usuario);

            if (usuarioEncontrado == null)
                return NotFound(new { message = "Usuário ou senha inseridos inválidos" });

            var token = ServicoDeToken.GerarToken(usuario);
            usuario.Password = "";

            return new
            {
                user = usuario,
                token = token
            };
        }

        private Usuario BuscarUsuario(Usuario usuario)
        {
            return _contexto.Usuarios.Where(ValidarDados(usuario)).FirstOrDefault();
        }

        private static Expression<Func<Usuario, bool>> ValidarDados(Usuario usuario)
        {
            return x => x.Username == usuario.Username && x.Password == usuario.Password;
        }

        [HttpPost]
        [Route("cadastro")]
        public async Task<ActionResult<Usuario>> CadastrarUsuario([FromBody] Usuario usuario)
        {
            if (!ModelState.IsValid)
                return BadRequest("Dados Inválidos");

            await SalvarUsuario(usuario);
            return Ok(new { message = "Dados salvos com sucesso!" });
        }

        private async Task SalvarUsuario(Usuario usuario)
        {
            await _contexto.Usuarios.AddAsync(usuario);
            await _contexto.SaveChangesAsync();
        }
    }
}