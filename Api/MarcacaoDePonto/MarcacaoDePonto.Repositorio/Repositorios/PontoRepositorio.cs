using MarcacaoDePonto.Models.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarcacaoDePonto.Repositorio.Repositorios
{
    public class PontoRepositorio : Contexto
    {
        public List<Ponto> ListarPontos(string? id)
        {
            string comandoSql = @"SELECT 
                                    PontoId, DataHorarioPonto ,Justificativa ,FuncionarioId
                                FROM 
                                    Ponto";

            if (!string.IsNullOrWhiteSpace(id))
                comandoSql += " WHERE PontoId LIKE @PontoId";

            using (var cmd = new SqlCommand(comandoSql, _connection))
            {
                if (!string.IsNullOrWhiteSpace(id))
                    cmd.Parameters.AddWithValue("@PontoId", "%" + id + "%");

                using (var rdr = cmd.ExecuteReader())
                {
                    var pontos = new List<Ponto>();
                    while (rdr.Read())
                    {
                        var ponto = new Ponto();
                        ponto.PontoId = Convert.ToInt64(rdr["PontoId"]);
                        ponto.DataHorarioPonto = Convert.ToDateTime(rdr["DataHorarioPonto"]);
                        ponto.Justificativa = Convert.ToString(rdr["Justificativa"]);
                        ponto.FuncionarioId = Convert.ToInt32(rdr["FuncionarioId"]);

                        pontos.Add(ponto);
                    }
                    return pontos;
                }

            }
        }
        public void Inserir(Ponto ponto)
        {
            string comandoSql = @"INSERT INTO Ponto
                                    (DataHorarioPonto , Justificativa ,FuncionarioId )
                                        VALUES 
                                    (@DataHorarioPonto, @Justificativa, @FuncionarioId);";

            using (var cmd = new SqlCommand(comandoSql, _connection))
            {
                cmd.Parameters.AddWithValue("@DataHorarioPonto", ponto.DataHorarioPonto == default ? ponto.DataHorarioPonto = DateTime.Now : ponto.DataHorarioPonto);
                cmd.Parameters.AddWithValue("@Justificativa", ponto.Justificativa == null ? DBNull.Value : ponto.Justificativa);
                cmd.Parameters.AddWithValue("@FuncionarioId", ponto.FuncionarioId);

                cmd.ExecuteNonQuery();
            }




        }
        public void Apagar(double id)
        {
            string comandoSql = @"DELETE FROM Ponto 
                                          WHERE PontoId  = @PontoId ;";

            using (var cmd = new SqlCommand(comandoSql, _connection))
            {
                cmd.Parameters.AddWithValue("@PontoId ", id);


                if (cmd.ExecuteNonQuery() == 0)
                    throw new InvalidOperationException($"Nenhum resgistro afetado para o PontoId {id}");
            }
        }
        public void Atualizar(Ponto ponto)
        {
            string comandoSql = @"UPDATE Ponto
                                SET 
                                    DataHorarioPonto  = @DataHorarioPonto, 
                                    Justificativa = @Justificativa,
                                    FuncionarioId = @FuncionarioId 

                                WHERE PontoId  = @PontoId;";

            using (var cmd = new SqlCommand(comandoSql, _connection))
            {
                cmd.Parameters.AddWithValue("@DataHorarioPonto", ponto.DataHorarioPonto);
                cmd.Parameters.AddWithValue("@Justificativa", ponto.Justificativa);
                cmd.Parameters.AddWithValue("@FuncionarioId", ponto.FuncionarioId);

                if (cmd.ExecuteNonQuery() == 0)
                    throw new InvalidOperationException($"Nenhum resgistro afetado para o indicador{ponto.PontoId}");
            }

        }
        public bool SeExiste(double id)
        {
            string comandoSql = @"SELECT COUNT(PontoId) as Total FROM Ponto WHERE PontoId  = @PontoId ";

            using (var cmd = new SqlCommand(comandoSql, _connection))
            {

                cmd.Parameters.AddWithValue("@PontoId ", id);
                return Convert.ToBoolean(cmd.ExecuteScalar());

            }

        }
        public bool SeExisteIdFuncionario(int id)
        {
            string comandoSql = @"SELECT COUNT(FuncionarioId) as Total FROM Ponto WHERE FuncionarioId  = @FuncionarioId ";

            using (var cmd = new SqlCommand(comandoSql, _connection))
            {

                cmd.Parameters.AddWithValue("@FuncionarioId ", id);
                return Convert.ToBoolean(cmd.ExecuteScalar());

            }

        }
    }
    }

