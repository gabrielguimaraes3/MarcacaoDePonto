using MarcacaoDePonto.Models.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarcacaoDePonto.Repositorio.Repositorios
{
    public class CargoRepositorio : Contexto
    {
        public List<Cargo> ListarCargos(string? descricao)
        {
            string comandoSql = @"SELECT 
                                    CargoId, Descricao
                                FROM 
                                    Cargo";

            if (!string.IsNullOrWhiteSpace(descricao))
                comandoSql += " WHERE Descricao LIKE @Descricao";

            using (var cmd = new SqlCommand(comandoSql, _connection))
            {
                if (!string.IsNullOrWhiteSpace(descricao))
                    cmd.Parameters.AddWithValue("@Descricao", "%" + descricao + "%");

                using (var rdr = cmd.ExecuteReader())
                {
                    var cargos = new List<Cargo>();
                    while (rdr.Read())
                    {
                        var cargo = new Cargo();
                        cargo.CargoId = Convert.ToInt32(rdr["CargoId"]);
                        cargo.Descricao = Convert.ToString(rdr["Descricao"]);

                        cargos.Add(cargo);
                    }
                    return cargos;
                }

            }
        }
        public void Inserir(Cargo cargo)
        {
            string comandoSql = @"INSERT INTO Cargo
                                    (Descricao)
                                        VALUES 
                                    (@Descricao);";

            using (var cmd = new SqlCommand(comandoSql, _connection))
            {
                cmd.Parameters.AddWithValue("@Descricao", cargo.Descricao);
                
                cmd.ExecuteNonQuery();
            }




        }
        public void Apagar(int id)
        {
            string comandoSql = @"DELETE FROM Cargo 
                                      WHERE CargoId = @CargoId;";

            using (var cmd = new SqlCommand(comandoSql, _connection))
            {
                cmd.Parameters.AddWithValue("@CargoId", id);


                if (cmd.ExecuteNonQuery() == 0)
                    throw new InvalidOperationException($"Nenhum resgistro afetado para o CargoId{id}");
            }
        }
        public void Atualizar(Cargo cargo)
        {
            string comandoSql = @"UPDATE Cargo
                                SET 
                                    Descricao = @Descricao

                                WHERE CargoId = @CargoId;";

            using (var cmd = new SqlCommand(comandoSql, _connection))
            {
                cmd.Parameters.AddWithValue("@Descricao", cargo.Descricao);
                if (cmd.ExecuteNonQuery() == 0)
                    throw new InvalidOperationException($"Nenhum resgistro afetado para o FuncionarioId{cargo.CargoId}");
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
