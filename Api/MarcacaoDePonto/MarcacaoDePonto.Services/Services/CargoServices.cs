
using MarcacaoDePonto.Models.Exeption;
using MarcacaoDePonto.Models.Models;
using MarcacaoDePonto.Repositorio.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MarcacaoDePonto.Services.Services
{
    public class CargoServices
    {
        private readonly CargoRepositorio _cargoRepositorio;
        public CargoServices()
        {
            _cargoRepositorio = new CargoRepositorio();
        }
        public List<Cargo> Listar(string? descricao)
        {
            try
            {
                _cargoRepositorio.AbrirConexao();
                return _cargoRepositorio.ListarCargos(descricao);
            }
            finally
            {
                _cargoRepositorio.FecharConexao();
            }
        }

        public void Inserir(Cargo cargo)
        {
            try
            {
                ValidarDescricao(cargo);
                _cargoRepositorio.AbrirConexao();
                _cargoRepositorio.Inserir(cargo);
            }
            finally
            {
                _cargoRepositorio.FecharConexao();
            }
        }
        public void Apagar(Cargo cargo)
        {
            try
            {
                _cargoRepositorio.AbrirConexao();

                if (!_cargoRepositorio.SeExiste(cargo.CargoId))
                    throw new ValidacoesExcepition($"Nenhum registro encontrado para o Identificador {cargo.CargoId}");

                _cargoRepositorio.Apagar(cargo.CargoId);
            }
            finally
            {
                {
                    _cargoRepositorio.FecharConexao();
                }
            }
        }
        public void Atualizar(Cargo cargo)
        {
            try
            {
               
                ValidarDescricao(cargo);
                _cargoRepositorio.AbrirConexao();

                
                if (!_cargoRepositorio.SeExiste(cargo.CargoId))
                    throw new ValidacoesExcepition($"O nenhum reistro encontrado par o Identificador {cargo.CargoId}");

                _cargoRepositorio.Atualizar(cargo);
            }
            finally
            {
                _cargoRepositorio.FecharConexao();
            }
        }

        private static void ValidarDescricao(Cargo cargo)
        {
            if (string.IsNullOrWhiteSpace(cargo.CargoId.ToString()))
                throw new ValidacoesExcepition("O identificador do cargo é obrigatorio."); 
            
            if (string.IsNullOrWhiteSpace(cargo.Descricao))
                throw new ValidacoesExcepition("A descriçaõ do cargo é obrigatoria.");
            if (cargo.Descricao.Trim().Length < 2
                || cargo.Descricao.Trim().Length > 255)
                throw new ValidacoesExcepition("A descrição do cargo precisa ter entre 2 e 255 caracteres");
        }
    }
}
