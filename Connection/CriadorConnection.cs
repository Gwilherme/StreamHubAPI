using API.Model;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace API.Connection
{
    public class CriadorConnection
    {
        public ConnectionController connectionController = new ConnectionController(); 
        public SqlConnection sqlConnection { get; set; }
        public SqlCommand SqlCommand { get; private set; }

        public CriadorConnection() 
        {
            sqlConnection = new SqlConnection(connectionController.connetionString);
            //Console.ReadLine();
        }


        public List<Criador> GetAllCriadors()
        {
            string query = connectionController.QueryGetAll("Criador");
            var Criadors = new List<Criador>();

            using (sqlConnection)
            {
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                sqlConnection.Open();

                using (var reader = sqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var Criador = new Criador
                        {
                            ID = reader.GetInt32(reader.GetOrdinal("Id")),
                            Nome = reader.GetString(reader.GetOrdinal("Nome")),
                            //ListConteudo = reader.GetString(reader.GetOrdinal("Email"))

                        };
                        Criadors.Add(Criador);
                    }
                }
            }

            return Criadors;
        }

        public Criador GetCriadorByID(int id)
        {
            string query = connectionController.QueryGetByID("Criador", "ID");
            var Criador = new Criador();

            using (sqlConnection)
            {
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                sqlCommand.Parameters.AddWithValue("@id", id);

                sqlConnection.Open();

                using (var reader = sqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Criador = new Criador
                        {
                            ID = reader.GetInt32(reader.GetOrdinal("Id")),
                            Nome = reader.GetString(reader.GetOrdinal("Nome")),
                            //Email = reader.GetString(reader.GetOrdinal("Email"))

                        };
                    }
                }
            }

            return Criador;
        }

        public void addCriador(string nome)
        {
            string[] camp = { "Nome" };
            string[] param = { "@nome"};
            string query = connectionController.QueryAdd("Criador", camp, param);

            using (sqlConnection)
            {
                SqlCommand = new SqlCommand(query, sqlConnection);
                SqlCommand.Parameters.AddWithValue("@nome", nome);

                sqlConnection.Open();
                try
                {
                    SqlCommand.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine("FALHA");
                    Console.WriteLine(e.Message);
                }


                sqlConnection.Close();
            }
        }

        public void UpdateCriador(int id, string nome)
        {
            string[] param = new string[2];
            string[] val = new string[2];
            int countParam = 0;

            if (!string.IsNullOrEmpty(nome))
            {
                param[0] = "Nome";
                val[0] = nome;
                countParam++;
            }

            for (int i = 0; i < countParam; i++)
            {
                string query = connectionController.QueryUpdate("Criador", "ID", param[i]);

                // Crie uma nova conexão para cada iteração
                using (SqlConnection sqlConnection = new SqlConnection(connectionController.connetionString))
                {
                    using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                    {
                        // Adicione os parâmetros
                        sqlCommand.Parameters.AddWithValue("@id", id);
                        sqlCommand.Parameters.AddWithValue("@valorUpdate", val[i]);

                        try
                        {
                            sqlConnection.Open();
                            sqlCommand.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            // Manipule exceções para capturar erros e evitar falhas não tratadas
                            Console.WriteLine($"Erro ao executar atualização para o parâmetro {i}: {ex.Message}");
                        }
                    }
                }
            }


        }

        public void DeleteCriador(int id)
        {
            string query = connectionController.QueryDeleteByID("Criador", "ID");

            using (sqlConnection)
            {
                SqlCommand = new SqlCommand(query, sqlConnection);
                SqlCommand.Parameters.AddWithValue("@id", id);

                sqlConnection.Open();
                int i = SqlCommand.ExecuteNonQuery();
                if (i > 0)
                {
                    Console.WriteLine("COMPLETO");
                }
                else
                {
                    Console.WriteLine("FALHA");
                }

                sqlConnection.Close();
            }
        }
    
    }
}
