using MarcacaoDePonto.Models.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MarcacaoDePonto.Models.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace MarcacaoDePonto.Controllers
{
    [AllowAnonymous]
    [ApiController]
    public class AutorizacaoController : ControllerBase
    {
        private readonly IConfiguration _config;
        public AutorizacaoController(IConfiguration configuration)
        {
            _config = configuration;
        }
        [HttpPost("Autorizacao")]
        public IActionResult Login(Usuario usuario)
        {
            if (usuario.Login == "string" && usuario.Senha == "string")
            {

                var senhaJwt = Encoding.ASCII.GetBytes
                (_config["SenhaJwt"]);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                        new Claim("Login", usuario.Login),
                        new Claim(ClaimTypes.Role, EnumPermissaoUsuario.Rh.GetHashCode().ToString())
                    }),
                    Expires = DateTime.UtcNow.AddMinutes(5),
                    SigningCredentials = new SigningCredentials
                    (new SymmetricSecurityKey(senhaJwt),
                    SecurityAlgorithms.HmacSha512Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var jwtToken = tokenHandler.WriteToken(token);
                var stringToken = tokenHandler.WriteToken(token);
                return StatusCode(200, new
                {
                    Token = stringToken,
                    Validade = tokenDescriptor.Expires
                });
            }
            else
            {
                return StatusCode(401);
            }

        }
    }
}
