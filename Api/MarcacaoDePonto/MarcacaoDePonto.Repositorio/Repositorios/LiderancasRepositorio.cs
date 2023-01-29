using MarcacaoDePonto.Models.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarcacaoDePonto.Repositorio.Repositorios
{
    public class LiderancasRepositorio : Contexto
    {
        public List<Lideranca> ListarLideres(string? descricao)
        {
            string comandoSql = @"SELECT 
                                    LiderancaId ,FuncionarioId ,DescricaoEquipe 
                                FROM 
                                    Liderancas";

            if (!string.IsNullOrWhiteSpace(descricao))
                comandoSql += " WHERE Descricao LIKE @DescricaoEquipe";

            using (var cmd = new SqlCommand(comandoSql, _connection))
            {
                if (!string.IsNullOrWhiteSpace(descricao))
                    cmd.Parameters.AddWithValue("@DescricaoEquipe", "%" + descricao + "%");

                using (var rdr = cmd.ExecuteReader())
                {
                    var liderancas = new List<Lideranca>();
                    while (rdr.Read())
                    {
                        var lideranca = new Lideranca();
                        lideranca.LiderancaId = Convert.ToInt32(rdr["LiderancaId"]);
                        lideranca.FuncionarioId = Convert.ToInt32(rdr["FuncionarioId"]);
                        lideranca.DescricaoEquipe = Convert.ToString(rdr["DescricaoEquipe"]);

                        liderancas.Add(lideranca);
                    }
                    return liderancas;
                }

            }
        }
        public void Inserir(Lideranca lideranca)
        {
            string comandoSql = @"INSERT INTO Liderancas
                                    (FuncionarioId, DescricaoEquipe)
                                        VALUES 
                                    (@FuncionarioId, @DescricaoEquipe);";

            using (var cmd = new SqlCommand(comandoSql, _connection))
            {
                cmd.Parameters.AddWithValue("@FuncionarioId", lideranca.FuncionarioId);
                cmd.Parameters.AddWithValue("@DescricaoEquipe", lideranca.DescricaoEquipe);

                cmd.ExecuteNonQuery();
            }




        }
        public void Apagar(int id)
        {
            string comandoSql = @"DELETE FROM Liderancas 
                                      WHERE LiderancaId  = @LiderancaId ;";

            using (var cmd = new SqlCommand(comandoSql, _connection))
            {
                cmd.Parameters.AddWithValue("@LiderancaId ", id);


                if (cmd.ExecuteNonQuery() == 0)
                    throw new InvalidOperationException($"Nenhum resgistro afetado para o LiderancaId {id}");
            }
        }
        public void Atualizar(Lideranca lideranca)
        {
            string comandoSql = @"UPDATE Liderancas
                                SET 
                                    FuncionarioId = @FuncionarioId,
                                    DescricaoEquipe = @DescricaoEquipe

                                WHERE LiderancaId  = @LiderancaId ;";

            using (var cmd = new SqlCommand(comandoSql, _connection))
            {
                cmd.Parameters.AddWithValue("@FuncionarioId", lideranca.FuncionarioId);
                cmd.Parameters.AddWithValue("@Descricao", lideranca.DescricaoEquipe);
                if (cmd.ExecuteNonQuery() == 0)
                    throw new InvalidOperationException($"Nenhum resgistro afetado para o FuncionarioId{lideranca.LiderancaId}");
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
    }
}
