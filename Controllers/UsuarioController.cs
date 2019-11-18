using Microsoft.AspNetCore.Mvc;
using ControleDeEstoque.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using ControleDeEstoque.Servicos.Token;
using System.Linq.Expressions;
using System;
using ControleDeEstoque.Servicos.Interfaces;

namespace ControleDeEstoque.Controllers
{
    [ApiController]
    [Route("api/usuario")]
    public class UsuarioController : ControllerBase
    {
        private readonly IQueryDeUsuario _QueryDeUsuario;

        public UsuarioController(IQueryDeUsuario queryDeUsuario)
        {
            _QueryDeUsuario = queryDeUsuario;
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<dynamic>> Autenticar([FromBody] Usuario usuario)
        {
            Usuario usuarioEncontrado = await _QueryDeUsuario.AutenticarUsuario(usuario);

            if (usuarioEncontrado == null)
                return BadRequest(new { message = "Username ou senha inválidos" });

            var token = ServicoDeToken.GerarToken(usuario);
            usuario.Password = "";

            return new
            {
                user = usuario,
                token = token
            };
        }

        [HttpPost]
        [Route("cadastro")]
        public async Task<ActionResult<Usuario>> CadastrarUsuario([FromBody] Usuario usuario)
        {
            if (!ModelState.IsValid)
                return BadRequest("Dados Inválidos");

            await _QueryDeUsuario.SalvarUsuario(usuario);
            return Ok(new { message = "Dados salvos com sucesso!" });
        }
    }
}