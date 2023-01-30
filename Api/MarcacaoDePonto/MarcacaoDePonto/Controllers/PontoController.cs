using MarcacaoDePonto.Models.Exeption;
using MarcacaoDePonto.Models.Models;
using MarcacaoDePonto.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace MarcacaoDePonto.Controllers
{

    public class PontoController : ControllerBase
    {
        private readonly PontoServices _pontoServices;

        public PontoController()
        {
            _pontoServices = new PontoServices();
        }

        [Authorize(Roles = "2")]
        [HttpGet("pontoListar")]
        public IActionResult Listar([FromQuery] string? id)
        {
            return StatusCode(200, _pontoServices.Listar(id));
        }

        [Authorize(Roles = "1,2,3")]
        [HttpPost("pontoInsert")]
        public IActionResult Inserir([FromQuery] Ponto ponto)
        {
            try
            {
                _pontoServices.Inserir(ponto);
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
        [HttpDelete("pontoDelete")]
        public IActionResult Apagar([FromQuery] double id)
        {
            try
            {
                _pontoServices.Apagar(id);
                return StatusCode(200, "Ponto apagado do banco de dados");
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
        [HttpPut("pontoAtualizar")]
        public IActionResult Atualizar([FromQuery] Ponto ponto)
        {
            try
            {
                _pontoServices.Atualizar(ponto);
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

