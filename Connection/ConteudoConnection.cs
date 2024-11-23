using API.Model;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace API.Connection
{
    public class ConteudoConnection
    {
        public ConnectionController connectionController = new ConnectionController(); 
        public SqlConnection sqlConnection { get; set; }
        public SqlCommand SqlCommand { get; private set; }

        public ConteudoConnection() 
        {
            sqlConnection = new SqlConnection(connectionController.connetionString);
            //Console.ReadLine();
        }


        public List<Conteudo> GetAllConteudos()
        {
            string query = connectionController.QueryGetAll("Conteudo");
            var Conteudos = new List<Conteudo>();

            using (sqlConnection)
            {
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                sqlConnection.Open();

                using (var reader = sqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var Conteudo = new Conteudo
                        {
                            ID = reader.GetInt32(reader.GetOrdinal("Id")),
                            Titulo = reader.GetString(reader.GetOrdinal("Titulo")),
                            Tipo = reader.GetString(reader.GetOrdinal("Tipo"))

                        };
                        Conteudos.Add(Conteudo);
                    }
                }
            }

            return Conteudos;
        }

        public Conteudo GetConteudoByID(int id)
        {
            string query = connectionController.QueryGetByID("Conteudo", "ID");
            var Conteudo = new Conteudo();

            using (sqlConnection)
            {
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                sqlCommand.Parameters.AddWithValue("@id", id);

                sqlConnection.Open();

                using (var reader = sqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Conteudo = new Conteudo
                        {
                            ID = reader.GetInt32(reader.GetOrdinal("Id")),
                            Titulo = reader.GetString(reader.GetOrdinal("Titulo")),
                            Tipo = reader.GetString(reader.GetOrdinal("Tipo"))

                        };
                    }
                }
            }

            return Conteudo;
        }

        public void addConteudo(string tipo, string titulo, int criador)
        {
            string[] camp = { "Tipo", "Titulo", "CriadorID" };
            string[] param = { "@tipo", "@titulo", "@criador "};
            string query = connectionController.QueryAdd("Conteudo", camp, param);

            using (sqlConnection)
            {
                SqlCommand = new SqlCommand(query, sqlConnection);
                SqlCommand.Parameters.AddWithValue("@tipo", tipo);
                SqlCommand.Parameters.AddWithValue("@titulo", titulo);
                SqlCommand.Parameters.AddWithValue("@criador", criador);

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

        public void UpdateConteudo(int id, string tipo, string titulo)
        {

            string[] param = new string[2];
            string[] val = new string[2];
            int countParam = 0;

            if (!string.IsNullOrEmpty(tipo))
            {
                param[0] = "Tipo";
                val[0] = tipo;
                countParam++;
            }
            if (!string.IsNullOrEmpty(titulo))
            {
                param[1] = "Titulo";
                val[1] = titulo;
                countParam++;
            }

            for (int i = 0; i < countParam; i++)
            {
                string query = connectionController.QueryUpdate("Conteudo", "ID", param[i]);

                using (SqlConnection sqlConnection = new SqlConnection(connectionController.connetionString))
                {
                    using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                    {
                        sqlCommand.Parameters.AddWithValue("@id", id);
                        sqlCommand.Parameters.AddWithValue("@valorUpdate", val[i]);

                        try
                        {
                            sqlConnection.Open();
                            sqlCommand.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Erro ao executar atualização para o parâmetro {i}: {ex.Message}");
                        }
                    }
                }
            }


        }

        public void DeleteConteudo(int id)
        {
            string query = connectionController.QueryDeleteByID("Conteudo", "ID");

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
