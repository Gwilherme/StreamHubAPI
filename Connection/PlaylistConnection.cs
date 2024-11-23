using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace API.Connection
{
    public class PlaylistConnection
    {
        private ConnectionController connectionController = new ConnectionController();
        public SqlConnection sqlConnection { get; set; }
        public SqlCommand SqlCommand { get; private set; }

        public PlaylistConnection() 
        {
            sqlConnection = new SqlConnection(connectionController.connetionString);
            //Console.ReadLine();
        }


        public List<Playlist> GetAllPlaylists()
        {
            string query = connectionController.QueryGetAll("Playlist");
            var Playlists = new List<Playlist>();

            using (sqlConnection )
            {
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                sqlConnection.Open();

                using (var reader = sqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var Playlist = new Playlist
                        {
                            ID = reader.GetInt32(reader.GetOrdinal("Id")),
                            Nome = reader.GetString(reader.GetOrdinal("Nome")),
                            //Usuario = reader.GetString(reader.GetOrdinal("Email"))
                             
                        };
                        Playlists.Add(Playlist);
                    }
                }
            }

            return Playlists;
        }
        
        public Playlist GetPlaylistByID(int id)
        {
            string query = connectionController.QueryGetByID("Playlist", "ID");
            var Playlist = new Playlist();

            using (sqlConnection )
            {
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                sqlCommand.Parameters.AddWithValue("@id", id);

                sqlConnection.Open();

                using (var reader = sqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Playlist = new Playlist
                        {
                            ID = reader.GetInt32(reader.GetOrdinal("Id")),
                            Nome = reader.GetString(reader.GetOrdinal("Nome")),
                            //Usuario = reader.GetString(reader.GetOrdinal("Email"))
                             
                        };
                    }
                }
            }

            return Playlist;
        }

        public void addPlaylist(string nome, int usuario)
        {
            string[] camp = { "Nome", "UsuarioID" };
            string[] param = { "@nome", "@usuario" };
            string query = connectionController.QueryAdd("Playlist", camp, param);    
            
            using (sqlConnection)
            {
                SqlCommand = new SqlCommand(query, sqlConnection);
                //SqlCommand.Parameters.AddWithValue("@id", 1);
                SqlCommand.Parameters.AddWithValue("@nome", nome);
                SqlCommand.Parameters.AddWithValue("@usuario", usuario);

                sqlConnection.Open();
                try
                {
                    SqlCommand.ExecuteNonQuery();
                }
                catch(Exception e)
                {
                    Console.WriteLine("FALHA");
                    Console.WriteLine(e.Message);
                }
                
                    
                sqlConnection.Close();
            }
        }   
    
        public void UpdatePlaylist(int id, string nome)
        {
            // TODO como funcionar update
            string query = connectionController.QueryUpdate("Playlist", "ID", "Nome");

            using (sqlConnection)
            {
                SqlCommand = new SqlCommand(query, sqlConnection);
                SqlCommand.Parameters.AddWithValue("@id", id);    
                SqlCommand.Parameters.AddWithValue("@valorUpdate", nome);

                sqlConnection.Open();
                int i = SqlCommand.ExecuteNonQuery();
                if(i > 0)
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

        public void DeletePlaylist(int id)
        {
            // TODO como funcionar update
            string query = connectionController.QueryDeleteByID("Playlist", "ID");

            using (sqlConnection)
            {
                SqlCommand = new SqlCommand(query, sqlConnection);
                SqlCommand.Parameters.AddWithValue("@id", id);

                sqlConnection.Open();
                int i = SqlCommand.ExecuteNonQuery();
                if(i > 0)
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
