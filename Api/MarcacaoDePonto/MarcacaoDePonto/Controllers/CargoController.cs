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

    public class CargoController : ControllerBase
    {
        private readonly CargoServices _cargoServices;
        public CargoController()
        {
            _cargoServices = new CargoServices();
        }

        [Authorize(Roles = "1,2,3")]
        [HttpGet("cargosListar")]
        public IActionResult Listar([FromQuery] string? nome)
        {
            return StatusCode(200, _cargoServices.Listar(nome));
        }

        [Authorize(Roles = "2")]
        [HttpPost("cargoInsert")]
        public IActionResult Inserir([FromQuery] Cargo cargo)
        {
            try
            {
                _cargoServices.Inserir(cargo);
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
        [HttpDelete("cargoDelete")]
        public IActionResult Apagar([FromQuery] Cargo cargo)
        {
            try
            {
                _cargoServices.Apagar(cargo);
                return StatusCode(200, "Cargo apagado do banco de dados");
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
        [HttpPut("cargoAtualizar")]
        public IActionResult Atualizar([FromQuery] Cargo cargo)
        {
            try
            {
                _cargoServices.Atualizar(cargo);
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
