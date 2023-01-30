using MarcacaoDePonto.Models.Exeption;
using MarcacaoDePonto.Models.Models;
using MarcacaoDePonto.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MarcacaoDePonto.Controllers
{

    public class FuncionarioController : ControllerBase
    {
        private readonly FuncionarioServices _services;
        public FuncionarioController()
        {

            _services = new FuncionarioServices();
        }

        [Authorize(Roles = "1,2,3")]
        [HttpGet("funcionario")]
        public IActionResult Listar([FromQuery] string? descricao)
        {
            return StatusCode(200, _services.Listar(descricao));
        }

        [Authorize(Roles = "2")]
        [HttpPost("funcionario")]
        public IActionResult Inserir([FromQuery] Funcionario funcionario)
        {
            try
            {
                _services.Inserir(funcionario);
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
        [HttpDelete("funcionario")]
        public IActionResult Apagar([FromQuery] int cpf)
        {
            try
            {
                _services.Apagar(cpf.ToString());
                return StatusCode(200, "Funcionario apagado do banco de dados");
            }
            catch (ValidacoesExcepition ex)
            {
                return StatusCode(4, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.ToString());
            }


        }

        [Authorize(Roles = "2")]
        [HttpPut("funcionario")]
        public IActionResult Atualizar([FromQuery] Funcionario funcionario)
        {
            try
            {
                _services.Atualizar(funcionario);
                return StatusCode(201);
            }
            catch (ValidacoesExcepition ex)
            {
                return StatusCode(4, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.ToString());
            }
        }
    }

}
