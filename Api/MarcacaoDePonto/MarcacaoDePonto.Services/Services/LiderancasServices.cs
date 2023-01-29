using MarcacaoDePonto.Models.Exeption;
using MarcacaoDePonto.Models.Models;
using MarcacaoDePonto.Repositorio;
using MarcacaoDePonto.Repositorio.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MarcacaoDePonto.Services.Services
{
    public class LiderancasServices
    {
        private readonly LiderancasRepositorio _liderancasRepositorio;

        public LiderancasServices()
        {
            _liderancasRepositorio = new LiderancasRepositorio();
        }
        public List<Lideranca> Listar(string? nome)
        {
            try
            {
                _liderancasRepositorio.AbrirConexao();
                return _liderancasRepositorio.ListarLideres(nome);
            }
            finally
            {
                _liderancasRepositorio.FecharConexao();
            }
        }

        public void Inserir(Lideranca lideranca)
        {
            try
            {
                ValidarDescricao(lideranca);
                _liderancasRepositorio.AbrirConexao();
                _liderancasRepositorio.Inserir(lideranca);
            }
            finally
            {
                _liderancasRepositorio.FecharConexao();
            }
        }
        public void Apagar(int id)
        {
            try
            {
                _liderancasRepositorio.AbrirConexao();
                if (!_liderancasRepositorio.SeExiste((id)))
                    throw new ValidacoesExcepition($"Nenhum registro encontrado para o Cpf {id}");
                _liderancasRepositorio.Apagar(id);
            }
            finally
            {

                _liderancasRepositorio.FecharConexao();

            }
        }

        public void Atualizar(Lideranca lideranca)
        {
            try
            {
                ValidarDescricao(lideranca);
                _liderancasRepositorio.AbrirConexao();
                if (!_liderancasRepositorio.SeExiste(lideranca.LiderancaId))
                    throw new ValidacoesExcepition($"Nenhum registro encontrado para o identificador de liderança {lideranca.LiderancaId}");
                _liderancasRepositorio.Atualizar(lideranca);
            }
            finally
            {
                _liderancasRepositorio.FecharConexao();
            }
        }
     

            private static void ValidarDescricao(Lideranca lideranca)
        {
            if (string.IsNullOrWhiteSpace(lideranca.FuncionarioId.ToString()))
                throw new ValidacoesExcepition("O identificador do usuario é obrigatorio.");

            if (string.IsNullOrWhiteSpace(lideranca.DescricaoEquipe))
                throw new ValidacoesExcepition("A descriçaõ da equipe é obrigatoria.");
            if (lideranca.DescricaoEquipe.Trim().Length < 2
                || lideranca.DescricaoEquipe.Trim().Length > 155)
                throw new ValidacoesExcepition("A descrição do cargo precisa ter entre 2 e 155 caracteres");
        }
        
      
    }
}
