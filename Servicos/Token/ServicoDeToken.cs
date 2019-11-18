using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ControleDeEstoque.Models;
using Microsoft.IdentityModel.Tokens;

namespace ControleDeEstoque.Servicos.Token
{
    public static class ServicoDeToken
    {
        public static string GerarToken(Usuario usuario)
        {
            var manipuladorDeToken = new JwtSecurityTokenHandler();
            var chave = Encoding.ASCII.GetBytes(Configuracoes.Segredo);
            var descritorDeToken = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, usuario.Username.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(6),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(chave), SecurityAlgorithms.HmacSha256Signature
                )
            };
            var token = manipuladorDeToken.CreateToken(descritorDeToken);
            return manipuladorDeToken.WriteToken(token);
        }
    }
}