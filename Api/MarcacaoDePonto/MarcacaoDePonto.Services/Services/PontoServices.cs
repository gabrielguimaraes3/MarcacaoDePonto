using MarcacaoDePonto.Models.Exeption;
using MarcacaoDePonto.Models.Models;
using MarcacaoDePonto.Repositorio.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarcacaoDePonto.Services.Services
{
    public class PontoServices
    {
        private readonly PontoRepositorio _pontoRepositorio;
        public PontoServices()
        {
            _pontoRepositorio = new PontoRepositorio();
        }
        public List<Ponto> Listar(string? id)
        {
            try
            {
                _pontoRepositorio.AbrirConexao();
                return _pontoRepositorio.ListarPontos(id);
            }
            finally
            {
                _pontoRepositorio.FecharConexao();
            }
        }

        public void Inserir(Ponto ponto)
        {
            try
            {
                ValidarPonto(ponto);
                _pontoRepositorio.AbrirConexao();
                if (!_pontoRepositorio.SeExisteIdFuncionario(ponto.FuncionarioId))
                    throw new ValidacoesExcepition($"Nenhum registro encontrado para o Identificador do funcionario {ponto.FuncionarioId}");
                _pontoRepositorio.Inserir(ponto);
            }
            finally
            {
                _pontoRepositorio.FecharConexao();
            }
        }
        public void Apagar(double id)
        {
            try
            {
                _pontoRepositorio.AbrirConexao();

                if (!_pontoRepositorio.SeExiste(id))
                    throw new ValidacoesExcepition($"Nenhum registro encontrado para o Identificador {id}");

                _pontoRepositorio.Apagar(id);
            }
            finally
            {
                {
                    _pontoRepositorio.FecharConexao();
                }
            }
        }
        public void Atualizar(Ponto ponto)
        {
            try
            {

                ValidarPonto(ponto);
                _pontoRepositorio.AbrirConexao();


                if (!_pontoRepositorio.SeExiste(ponto.PontoId))
                    throw new ValidacoesExcepition($"Nenhum reistro encontrado par o Identificador {ponto.PontoId}");

                _pontoRepositorio.Atualizar(ponto);
            }
            finally
            {
                _pontoRepositorio.FecharConexao();
            }
        }

        public static void ValidarPonto(Ponto ponto)
        {


            if (string.IsNullOrWhiteSpace(ponto.DataHorarioPonto.ToString()))
                throw new ValidacoesExcepition("A data e horario são obrigatorios.");
           
            if (ponto.Justificativa is not null)
            {
                if (ponto.Justificativa.Length > 255)
                    throw new ValidacoesExcepition("A descrição do cargo precisa ter entre 2 e 255 caracteres");
            }


            if (ponto.FuncionarioId == 0)
                throw new ValidacoesExcepition("O identificador de funcionario é obrigatorio.");
        }
    }
}
