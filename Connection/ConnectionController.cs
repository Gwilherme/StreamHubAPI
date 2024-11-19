using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace API.Connection
{
    public class ConnectionController : Controller
    {
        public string connetionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;"; 
        public SqlConnection sqlConnection { get; set; }
        public SqlCommand SqlCommand { get; private set; }

        public ConnectionController() 
        {
            sqlConnection = new SqlConnection(connetionString);
            //Console.ReadLine();
        }


        [Obsolete]
        public void addConteudo()
        {
            string query = " INSERT INTO [dbo].[Conteudo] (Id, Titulo, Tipo, IdCriador) " +
                           " VALUES (@id, @titulo, @tipo, @criador ); ";

            using (sqlConnection)
            {
                SqlCommand = new SqlCommand(query, sqlConnection);
                SqlCommand.Parameters.AddWithValue("@id", 1);
                SqlCommand.Parameters.AddWithValue("@titulo", "Video teste");
                SqlCommand.Parameters.AddWithValue("@tipo", "teste");
                SqlCommand.Parameters.AddWithValue("@criador", 3);

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
