using System;
using System.Linq;
using System.Threading.Tasks;
using ControleDeEstoque.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeEstoque.Controllers
{
    [ApiController]
    [Route("api/produto")]
    public class ProdutoController : ControllerBase
    {
        private readonly Contexto _contexto;
        public ProdutoController(Contexto contexto)
        {
            _contexto = contexto;
        }

        [Authorize]
        [HttpPost]
        [Route("inserir")]
        public async Task<ActionResult> InserirProdutor(Produto produto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { message = "Erro durante inserção" });

            Usuario usuario = _contexto.Usuarios.Where(x => x.Username == User.Identity.Name).FirstOrDefault();

            produto.UsuarioId = usuario.Id;

            await _contexto.Produtos.AddAsync(produto);
            await _contexto.SaveChangesAsync();

            return Ok();
        }
    }
}