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
    public class EquipeService
    {
        private readonly EquipeRepositorio _equipeRepositorio;
        public EquipeService()
        {
            _equipeRepositorio = new EquipeRepositorio();
        }

        public List<Equipe> Listar(int Id)
        {
            try
            {
                _equipeRepositorio.AbrirConexao();
                return _equipeRepositorio.ListarEquipe(Id);
            }
            finally
            {
                _equipeRepositorio.FecharConexao();
            }
        }
        public void Inserir(Equipe equipe)
        {
            try
            {
                ValidarModelEquipe(equipe);
                _equipeRepositorio.AbrirConexao();

                if (!_equipeRepositorio.SeExisteLider(equipe))
                    throw new ValidacoesExcepition($"Lider não existe {equipe.LiderancaId}");

                if (!_equipeRepositorio.SeExisteFuncionario(equipe))
                    throw new ValidacoesExcepition($"Funcionario não existe {equipe.FuncionarioId}");

                _equipeRepositorio.Inserir(equipe);
            }
            finally
            {
                _equipeRepositorio.FecharConexao();
            }
        }
        public void Deletar(int equipeExiste)
        {
            try
            {
                _equipeRepositorio.AbrirConexao();

                if (!_equipeRepositorio.SeExisteEquipe(equipeExiste))
                    throw new ValidacoesExcepition($"Nenhum registro afetado para o identificaro de número {equipeExiste}");

                _equipeRepositorio.Deletar(equipeExiste);
            }
            finally
            {
                _equipeRepositorio.FecharConexao();
            }
        }
        public void Atualizar(Equipe model)
        {
            try
            {

                ValidarModelEquipe(model);
                _equipeRepositorio.AbrirConexao();

                if (!_equipeRepositorio.SeExisteLider(model))
                    throw new ValidacoesExcepition($"Lider não existe {model.LiderancaId}");

                if (!_equipeRepositorio.SeExisteFuncionario(model))
                    throw new ValidacoesExcepition($"Funcionario não existe {model.FuncionarioId}");

                _equipeRepositorio.Atualizar(model);
            }
            finally
            {
                _equipeRepositorio.FecharConexao();
            }
        }

        private static void ValidarModelEquipe(Equipe equipe)
        {
            if (equipe.LiderancaId <= 0)
                throw new ValidacoesExcepition("Consulte a tabela e veja as equipes e funcionarios disponiveis");

            if (equipe.FuncionarioId <= 0)
                throw new ValidacoesExcepition("Consulte a tabela e veja as equipes e funcionarios disponiveis");
        }
    }

}
