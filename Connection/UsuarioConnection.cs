﻿using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace API.Connection
{
    public class UsuarioConnection
    {
        public ConnectionController connectionController = new ConnectionController(); 
        public SqlConnection sqlConnection { get; set; }
        public SqlCommand SqlCommand { get; private set; }

        public UsuarioConnection() 
        {
            sqlConnection = new SqlConnection(connectionController.connetionString);
            //Console.ReadLine();
        }


        public List<Usuario> GetAllUsuarios()
        {
            string query = connectionController.QueryGetAll("Usuario");
            var Usuarios = new List<Usuario>();

            using (sqlConnection)
            {
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                sqlConnection.Open();

                using (var reader = sqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var Usuario = new Usuario
                        {
                            ID = reader.GetInt32(reader.GetOrdinal("Id")),
                            Nome = reader.GetString(reader.GetOrdinal("Nome")),
                            Email = reader.GetString(reader.GetOrdinal("Email"))

                        };
                        Usuarios.Add(Usuario);
                    }
                }
            }

            return Usuarios;
        }

        public Usuario GetUsuarioByID(int id)
        {
            string query = connectionController.QueryGetByID("Usuario", "ID");
            var Usuario = new Usuario();

            using (sqlConnection)
            {
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                sqlCommand.Parameters.AddWithValue("@id", id);

                sqlConnection.Open();

                using (var reader = sqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Usuario = new Usuario
                        {
                            ID = reader.GetInt32(reader.GetOrdinal("Id")),
                            Nome = reader.GetString(reader.GetOrdinal("Nome")),
                            Email = reader.GetString(reader.GetOrdinal("Email"))

                        };
                    }
                }
            }

            return Usuario;
        }

        public void addUsuario(string nome, string email)
        {
            string[] camp = { "Nome", "Email" };
            string[] param = { "@nome", "@email "};
            string query = connectionController.QueryAdd("Usuario", camp, param);

            using (sqlConnection)
            {
                SqlCommand = new SqlCommand(query, sqlConnection);
                //SqlCommand.Parameters.AddWithValue("@id", 1);
                SqlCommand.Parameters.AddWithValue("@nome", nome);
                SqlCommand.Parameters.AddWithValue("@email", email);

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

        public void UpdateUsuario(int id, string nome, string email)
        {
            // TODO como funcionar update
            // TODO colocar [] para os campos

            string[] param = new string[2];
            string[] val = new string[2];
            int countParam = 0;

            if (!string.IsNullOrEmpty(nome))
            {
                param[0] = "Nome";
                val[0] = nome;
                countParam++;
            }
            if (!string.IsNullOrEmpty(email))
            {
                param[1] = "Email";
                val[1] = email;
                countParam++;
            }


            //for (int i = 0; i < countParam; i++)
            //{
            //    string query = connectionController.QueryUpdate("Usuario", "ID", param[i]);

            //    using (sqlConnection)
            //    {
            //        using (SqlCommand = new SqlCommand(query, sqlConnection))
            //        {
            //            SqlCommand.Parameters.AddWithValue("@id", id);
            //            SqlCommand.Parameters.AddWithValue("@valorUpdate", val[i]);

            //            try
            //            {
            //                if(i==0) sqlConnection.Open();

            //                SqlCommand.ExecuteNonQuery();
            //            }
            //            catch (Exception ex)
            //            {
            //                Console.WriteLine($"Erro ao executar atualização para o parâmetro {i}: {ex.Message}");
            //            }
            //        }
            //    }

            //    sqlConnection.Close();
            //}

            for (int i = 0; i < countParam; i++)
            {
                string query = connectionController.QueryUpdate("Usuario", "ID", param[i]);

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

        public void DeleteUsuario(int id)
        {
            string query = connectionController.QueryDeleteByID("Usuario", "ID");

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
