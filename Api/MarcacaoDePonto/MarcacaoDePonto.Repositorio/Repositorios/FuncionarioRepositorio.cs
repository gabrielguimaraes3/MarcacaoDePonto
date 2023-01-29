using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarcacaoDePonto.Models.Models;

namespace MarcacaoDePonto.Repositorio.Repositorios
{
    public class FuncionarioRepositorio : Contexto
    {
        public List<Funcionario> ListarFuncionarios(string? nome)
        {
            string comandoSql = @"SELECT 
                                    FuncionarioId, NomeDoFuncionario , Cpf , NascimentoFuncionario, DataDeAdmissao, CelularFuncionario, EmailFuncionario, CargoId 
                                FROM 
                                    Funcionario";

            if (!string.IsNullOrWhiteSpace(nome))
                comandoSql += " WHERE NomeDoFuncionario LIKE @NomeDoFuncionario";

            using (var cmd = new SqlCommand(comandoSql, _connection))
            {
                if (!string.IsNullOrWhiteSpace(nome))
                    cmd.Parameters.AddWithValue("@NomeDoFuncionario", "%" + nome + "%");

                using (var rdr = cmd.ExecuteReader())
                {
                    var funcionarios = new List<Funcionario>();
                    while (rdr.Read())
                    {
                        var funcionario = new Funcionario();
                        funcionario.FuncionarioId = Convert.ToInt32(rdr["FuncionarioId"]);
                        funcionario.NomeDoFuncionario = Convert.ToString(rdr["NomeDoFuncionario"]);
                        funcionario.Cpf = Convert.ToString(rdr["Cpf"]);
                        funcionario.NascimentoFuncionario = Convert.ToDateTime(rdr["NascimentoFuncionario"]);
                        funcionario.DataDeAdmissao = Convert.ToDateTime(rdr["DataDeAdmissao"]);
                        funcionario.CelularFuncionario = Convert.ToString(rdr["CelularFuncionario"]);
                        funcionario.EmailFuncionario = Convert.ToString(rdr["EmailFuncionario"]);
                        funcionario.CargoId = Convert.ToInt32(rdr["CargoId"]);
                        funcionarios.Add(funcionario);
                    }
                    return funcionarios;
                }

            }
        }
        public void Inserir(Funcionario model)
        {
            string comandoSql = @"INSERT INTO Funcionario
                                    (NomeDoFuncionario , Cpf , NascimentoFuncionario, DataDeAdmissao, CelularFuncionario, EmailFuncionario, CargoId)
                                        VALUES 
                                    (@NomeDoFuncionario, @Cpf, @NascimentoFuncionario, @DataDeAdmissao, @CelularFuncionario, @EmailFuncionario, @CargoId);"
            ;

            using (var cmd = new SqlCommand(comandoSql, _connection))
            {
                cmd.Parameters.AddWithValue("@NomeDoFuncionario", model.NomeDoFuncionario);
                cmd.Parameters.AddWithValue("@Cpf", model.Cpf);
                cmd.Parameters.AddWithValue("@NascimentoFuncionario", model.NascimentoFuncionario);
                cmd.Parameters.AddWithValue("@DataDeAdmissao", model.DataDeAdmissao);
                cmd.Parameters.AddWithValue("@CelularFuncionario", model.CelularFuncionario == null ? DBNull.Value : model.CelularFuncionario);
                cmd.Parameters.AddWithValue("@EmailFuncionario", model.EmailFuncionario);
                cmd.Parameters.AddWithValue("@CargoId", model.CargoId);
                cmd.ExecuteNonQuery();
            }




        }
        public void Apagar(string cpf)
        {
            string comandoSql = @"DELETE FROM Funcionario 
                                  WHERE Cpf = @Cpf;";

            using (var cmd = new SqlCommand(comandoSql, _connection))
            {
                cmd.Parameters.AddWithValue("@Cpf", cpf);


                if (cmd.ExecuteNonQuery() == 0)
                    throw new InvalidOperationException($"Nenhum resgistro afetado para o Cpf{cpf}");
            }
        }
        public void Atualizar(Funcionario model)
        {
            string comandoSql = @"UPDATE Funcionario
                                SET 
                                    NomeDoFuncionario = @NomeDoFuncionario, 
                                    Cpf = @Cpf, 
                                    NascimentoFuncionario = @NascimentoFuncionario,
                                    DataDeAdmissao = @DataDeAdmissao,
                                    CelularFuncionario = @CelularFuncionario,
                                    EmailFuncionario = @EmailFuncionario,
                                    CargoId = @CargoId

                                WHERE FuncionarioId = @FuncionarioId;";

            using (var cmd = new SqlCommand(comandoSql, _connection))
            {
                cmd.Parameters.AddWithValue("@FuncionarioId", model.FuncionarioId);
                cmd.Parameters.AddWithValue("@NomeDoFuncionario", model.NomeDoFuncionario);
                cmd.Parameters.AddWithValue("@Cpf", model.Cpf);
                cmd.Parameters.AddWithValue("@NascimentoFuncionario", model.NascimentoFuncionario);
                cmd.Parameters.AddWithValue("@DataDeAdmissao", model.DataDeAdmissao);
                cmd.Parameters.AddWithValue("@CelularFuncionario", model.CelularFuncionario);
                cmd.Parameters.AddWithValue("@EmailFuncionario", model.EmailFuncionario);
                cmd.Parameters.AddWithValue("@CargoId", model.CargoId);
                if (cmd.ExecuteNonQuery() == 0)
                    throw new InvalidOperationException($"Nenhum resgistro afetado para o FuncionarioId{model.FuncionarioId}");
            }

        }
        public bool SeExiste(int id)
        {
            string comandoSql = @"SELECT COUNT(CargoId) as Total FROM Cargo WHERE CargoId = @CargoId";

            using (var cmd = new SqlCommand(comandoSql, _connection))
            {

                cmd.Parameters.AddWithValue("@CargoId", id);
                return Convert.ToBoolean(cmd.ExecuteScalar());

            }

        }
        public bool SeExisteCpf(Funcionario funcionario)
        {
            string comandoSql = @"SELECT COUNT(Cpf) as Total FROM Funcionario WHERE Cpf = @Cpf";

            using (var cmd = new SqlCommand(comandoSql, _connection))
            {

                cmd.Parameters.AddWithValue("@Cpf", funcionario.Cpf);
                return Convert.ToBoolean(cmd.ExecuteScalar());

            }

        }
        public bool ValidarCargo(Funcionario funcionario)
        {
            string comandoSql = @"SELECT COUNT(CargoId) as Total FROM Cargo WHERE CargoId = @CargoId";

            using (var cmd = new SqlCommand(comandoSql, _connection))
            {

                cmd.Parameters.AddWithValue("@CargoId", funcionario.CargoId);
                return Convert.ToBoolean(cmd.ExecuteScalar());

            }

        }
        public bool SeExisteCadastra(Funcionario funcionario)
        {
            string comandoSql = @"SELECT COUNT(Cpf) as Total FROM Funcionario WHERE Cpf = @Cpf";

            using (var cmd = new SqlCommand(comandoSql, _connection))
            {
                cmd.Parameters.AddWithValue("@Cpf", funcionario.Cpf);
                return Convert.ToBoolean(cmd.ExecuteScalar());
            }
        }
    }
}
