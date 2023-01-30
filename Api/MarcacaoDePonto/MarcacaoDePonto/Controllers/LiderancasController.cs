using Autofac.Core;
using MarcacaoDePonto.Models.Exeption;
using MarcacaoDePonto.Models.Models;
using MarcacaoDePonto.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace MarcacaoDePonto.Controllers
{

    public class LiderancasController : ControllerBase
    {
        private readonly LiderancasServices _liderancasService;

        public LiderancasController()
        {

            _liderancasService = new LiderancasServices();
        }

        [Authorize(Roles = "1,2,3")]
        [HttpGet("liderancas")]
        public IActionResult Listar([FromQuery] string? descricao)
        {
            return StatusCode(200, _liderancasService.Listar(descricao));
        }

        [Authorize(Roles = "2")]
        [HttpPost("liderancas")]
        public IActionResult Inserir([FromQuery] Lideranca lideranca)
        {
            try
            {
                _liderancasService.Inserir(lideranca);
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
        [HttpDelete("liderancas")]

        [Authorize(Roles = "2")]
        public IActionResult Apagar([FromQuery] int liderancaId)
        {
            try
            {
                _liderancasService.Apagar(liderancaId);
                return StatusCode(200, "Liderança apagado do banco de dados");
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
        [HttpPut("liderancas")]
        public IActionResult Atualizar([FromQuery] Lideranca lideranca
            )
        {
            try
            {
                _liderancasService.Atualizar(lideranca);
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
    }
}

