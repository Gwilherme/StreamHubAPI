using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace API.Connection
{
    public class UsuarioConnection
    {
        public string connetionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;"; 

        public SqlConnection sqlConnection { get; set; }
        public SqlCommand SqlCommand { get; private set; }

        public UsuarioConnection() 
        {
            sqlConnection = new SqlConnection(connetionString);
            //Console.ReadLine();
        }


        [Obsolete]
        public void addUsuario(string nome, string email)
        {
            string query = " INSERT INTO [dbo].[Usuario] (ID, Nome, Email) " +
                           " VALUES (@id, @nome, @email); ";

            using (sqlConnection)
            {
                SqlCommand = new SqlCommand(query, sqlConnection);
                SqlCommand.Parameters.AddWithValue("@id", 1);
                SqlCommand.Parameters.AddWithValue("@nome", nome);
                SqlCommand.Parameters.AddWithValue("@email", email);

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
