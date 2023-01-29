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
    public class FuncionarioServices
    {
        private readonly FuncionarioRepositorio _repositorio;
        public FuncionarioServices()
        {
            _repositorio = new FuncionarioRepositorio();
        }
        public List<Funcionario> Listar(string? nome)
        {
            try
            {
                _repositorio.AbrirConexao();
                return _repositorio.ListarFuncionarios(nome);
            }
            finally
            {
                _repositorio.FecharConexao();
            }
        }

        public void Inserir(Funcionario funcionario)
        {
            try
            {
                ValidarModelFuncionario(funcionario);
                _repositorio.AbrirConexao();

                if(!_repositorio.ValidarCargo(funcionario))
                    throw new ValidacoesExcepition($"Nenhum cargo encontrado para o CargoId {funcionario.CargoId}");

                if (_repositorio.SeExisteCadastra(funcionario))
                    throw new ValidacoesExcepition($"Essa pessoa já é cadastrada");

                _repositorio.Inserir(funcionario);
            }
            finally
            {
                _repositorio.FecharConexao();
            }
        }
        public void Apagar(string cpf)
        {
            try
            {
                _repositorio.AbrirConexao();
                if (!_repositorio.SeExiste(Convert.ToInt32(cpf)))
                    throw new ValidacoesExcepition($"Nenhum registro encontrado para o Cpf {cpf}");
                _repositorio.Apagar(Convert.ToString(cpf));
            }
            finally
            {
                {
                    _repositorio.FecharConexao();
                }
            }
        }
        public void Atualizar(Funcionario funcionario)
        {
            try
            {
                ValidarModelFuncionario(funcionario);
                _repositorio.AbrirConexao();

                if (!_repositorio.SeExiste(funcionario.CargoId))
                    throw new ValidacoesExcepition($"Nenhum registro encontrado para o identificador de cargo {funcionario.CargoId}");

                funcionario.FuncionarioId = default; 
                _repositorio.Atualizar(funcionario);
            }
            finally
            {
                _repositorio.FecharConexao();
            }
        }

        public static void ValidarModelFuncionario(Funcionario funcionario)
        {
            if (funcionario is null)
                throw new ValidacoesExcepition("O json esta mal formatado, ou foi enviado vazio.");

            if (string.IsNullOrWhiteSpace(funcionario.NomeDoFuncionario))
                throw new ValidacoesExcepition("O nome é obrigatorio");
            if (funcionario.NomeDoFuncionario.Trim().Length < 3 || funcionario.NomeDoFuncionario.Trim().Length > 255)
                throw new ValidacoesExcepition("O nome precisa ter entre 3 a 255 caracteres");



            if (string.IsNullOrWhiteSpace(funcionario.Cpf))
                throw new ValidacoesExcepition("O cpf é obrigatorio");
            if (funcionario.Cpf is not null
                && !ValidarCpf(funcionario.Cpf))
                throw new ValidacoesExcepition("O cpf não é valido");
            funcionario.Cpf = CpfSemFormatacao(funcionario.Cpf);


            if (ObterIdade(funcionario.NascimentoFuncionario) < 16)
                throw new ValidacoesExcepition("É necessario ter mais que 16 anos.");

            if (string.IsNullOrWhiteSpace(funcionario.CelularFuncionario))
                throw new ValidacoesExcepition("O Telefone é obrigatorio");
            if (funcionario.CelularFuncionario.Trim().Length < 11
                || funcionario.CelularFuncionario.Trim().Length > 15
                || funcionario.CelularFuncionario.Trim().Length != RemoverMascaraTelefone(funcionario.CelularFuncionario).Length)
                throw new ValidacoesExcepition("O telefone precisa conter entre 11 e 15 digitos, e não pode conter mascaras");

            if (string.IsNullOrWhiteSpace(funcionario.EmailFuncionario))
                throw new ValidacoesExcepition("O Email é obrigatorio");
            if (funcionario.EmailFuncionario.Trim().Length > 255)
                throw new ValidacoesExcepition("O email não pode ter mais que 55 caracteres");
            if (!ValidarEmail(funcionario.EmailFuncionario))
                throw new ValidacoesExcepition("O Email é invalido");

        }
        public static bool ValidarCpf(string cpf)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(cpf))
                    return false;

                int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
                int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
                string tempCpf;
                string digito;
                int soma;
                int resto;
                cpf = cpf.Trim();
                cpf = cpf.Replace(".", "").Replace("-", "");
                if (cpf.Length != 11)
                    return false;
                tempCpf = cpf.Substring(0, 9);
                soma = 0;

                for (int i = 0; i < 9; i++)
                    soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
                resto = soma % 11;
                if (resto < 2)
                    resto = 0;
                else
                    resto = 11 - resto;
                digito = resto.ToString();
                tempCpf = tempCpf + digito;
                soma = 0;
                for (int i = 0; i < 10; i++)
                    soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
                resto = soma % 11;
                if (resto < 2)
                    resto = 0;
                else
                    resto = 11 - resto;
                digito = digito + resto.ToString();
                return cpf.EndsWith(digito);
            }
            catch
            {
                return false;

            }

        }
        private static string RemoverMascaraTelefone(string telefone)
        {
            return Regex.Replace(telefone, @"[^\d]", "");
        }
        private static int ObterIdade(DateTime idade)
        {
            var today = DateTime.Today;
            var age = today.Year - idade.Year;
            if (idade > today.AddYears(-age)) age--;
            return age;
        }
        private static bool ValidarEmail(string email)
        {

            Regex rg = new Regex(@"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$");

            if (rg.IsMatch(email))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static string CpfSemFormatacao(string cpf)
        {
            return cpf.Replace(".", string.Empty).Replace("-", string.Empty).Replace("/", string.Empty);
        }
    }
}
