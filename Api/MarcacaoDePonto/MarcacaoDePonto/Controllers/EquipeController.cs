using MarcacaoDePonto.Models.Exeption;
using MarcacaoDePonto.Models.Models;
using MarcacaoDePonto.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace MarcacaoDePonto.Controllers
{
    [ApiController]
    public class EquipeController : ControllerBase
    {
        private readonly EquipeService _equipeService;
        public EquipeController()
        {
            _equipeService = new EquipeService();
        }


        [Authorize(Roles = "1,2,3")]
        [HttpGet("equipe")]
        public IActionResult Listar([FromQuery] int Id)
        {
            return StatusCode(200, _equipeService.Listar(Id));
        }

        [Authorize(Roles = "2")]
        [HttpPost("equipe")]
        public IActionResult Inserir([FromQuery] Equipe equipe)
        {
            try
            {
                _equipeService.Inserir(equipe);
                return StatusCode(201);
            }
            catch (ValidacoesExcepition ex)
            {
                return StatusCode(400, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.ToString());
            }
        }

        [Authorize(Roles = "2")]
        [HttpDelete("equipe")]
        public IActionResult Deletar([FromQuery] int equipeExiste)
        {
            try
            {
                _equipeService.Deletar(equipeExiste);
                return StatusCode(200, "cargo apagado com sucesso");
            }
            catch (ValidacoesExcepition ex)
            {
                return StatusCode(400, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.ToString());
            }
        }

        [Authorize(Roles = "2")]
        [HttpPut("equipe")]
        public IActionResult Atualizar([FromQuery] Equipe model)
        {
            try
            {
                _equipeService.Atualizar(model);
                return StatusCode(201, "Atualizado com sucesso");
            }
            catch (ValidacoesExcepition ex)
            {
                return StatusCode(400, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.ToString());
            }
        }

    }
}
