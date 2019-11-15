using Microsoft.AspNetCore.Mvc;
using ControleDeEstoque.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

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

    // [HttpPost]
    // public async Task<ActionResult<Usuario>> CriarUsuario(Usuario usuario)
    // {
    //   if (usuario == null)
    //   {
    //     return NotFound();
    //   }

    //   _contexto.Usuarios.Add(usuario);
    //   return Ok();
    // }

    // [HttpPost]
    // [Route("login")]
    // public async Task<ActionResult<Usuario>> ValidarDadosDeUsuario(Usuario usuario)
    // {
      
    // }
  }
}