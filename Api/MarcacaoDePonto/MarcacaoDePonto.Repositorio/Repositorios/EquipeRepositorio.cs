using MarcacaoDePonto.Models.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarcacaoDePonto.Repositorio.Repositorios
{
    public class EquipeRepositorio : Contexto
    {
        public List<Equipe> ListarEquipe(int Id)
        {
            string comandoSql = @"SELECT 
                                    EquipeId, LiderancaId, FuncionarioId    
                                  FROM
                                    Equipes";

            using (var cmd = new SqlCommand(comandoSql, _connection))
            {

                using (var rdr = cmd.ExecuteReader())
                {
                    var equipes = new List<Equipe>();
                    while (rdr.Read())
                    {
                        var equipe = new Equipe();
                        equipe.EquipeId = Convert.ToInt32(rdr["EquipeId"]);
                        equipe.LiderancaId = Convert.ToInt32(rdr["LiderancaId"]);
                        equipe.FuncionarioId = Convert.ToInt32(rdr["FuncionarioId"]);
                        equipes.Add(equipe);
                    }
                    return equipes;
                }
            }
        }
        public void Inserir(Equipe model)
        {
            string comandoSql = @"INSERT INTO Equipes 
                                        (LiderancaId, FuncionarioId) 
                                            VALUES
                                        (@LiderancaId, @FuncionarioId);"
            ;

            using (var cmd = new SqlCommand(comandoSql, _connection))
            {
                cmd.Parameters.AddWithValue("@LiderancaId", model.LiderancaId);
                cmd.Parameters.AddWithValue("@FuncionarioId", model.FuncionarioId);
                cmd.ExecuteNonQuery();
            }
        }
        public void Deletar(int id)
        {
            string comandoSql = @"DELETE FROM Equipes 
                                    WHERE EquipeId = @EquipeId";

            using (var cmd = new SqlCommand(comandoSql, _connection))
            {
                cmd.Parameters.AddWithValue("@EquipeId", id);

                if (cmd.ExecuteNonQuery() == 0)
                    throw new InvalidOperationException($"Nenhum registro afetado para o identificaro de número {id}");
            }
        }
        public void Atualizar(Equipe equipe)
        {
            string comandoSql = @"UPDATE Equipes 
                                    SET 
                                        LiderancaId = @LiderancaId, FuncionarioId = @FuncionarioId
                                    WHERE 
                                        EquipeId = @EquipeId;";

            using (var cmd = new SqlCommand(comandoSql, _connection))
            {
                cmd.Parameters.AddWithValue("@EquipeId", equipe.EquipeId);
                cmd.Parameters.AddWithValue("@LiderancaId", equipe.LiderancaId);
                cmd.Parameters.AddWithValue("@FuncionarioId", equipe.FuncionarioId);
                if (cmd.ExecuteNonQuery() == 0)
                    throw new InvalidOperationException($"Nenhum registro afetado para o o registro {equipe.EquipeId}");
            }
        }
        public bool SeExisteLider(Equipe equipe)
        {
            string comandoSql = @"SELECT COUNT(LiderancaId) as Total FROM Liderancas WHERE LiderancaId = @LiderancaId";

            using (var cmd = new SqlCommand(comandoSql, _connection))
            {
                cmd.Parameters.AddWithValue("@LiderancaId", equipe.LiderancaId);
                return Convert.ToBoolean(cmd.ExecuteScalar());
            }
        }
        public bool SeExisteFuncionario(Equipe equipe)
        {
            string comandoSql = @"SELECT COUNT(FuncionarioId) as Total FROM Funcionario WHERE FuncionarioId = @FuncionarioId";

            using (var cmd = new SqlCommand(comandoSql, _connection))
            {
                cmd.Parameters.AddWithValue("@FuncionarioId", equipe.FuncionarioId);
                return Convert.ToBoolean(cmd.ExecuteScalar());
            }
        }
        public bool SeExisteEquipe(int equipeExiste)
        {
            string comandoSql = @"SELECT COUNT(EquipeId) as Total FROM Equipes WHERE EquipeId = @EquipeId";

            using (var cmd = new SqlCommand(comandoSql, _connection))
            {
                cmd.Parameters.AddWithValue("@EquipeId", equipeExiste);
                return Convert.ToBoolean(cmd.ExecuteScalar());
            }
        }
    }

}
