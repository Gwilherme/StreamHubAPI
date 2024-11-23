using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace API.Connection
{
    public class ConnectionController : Controller
    {
        public string connetionString { get; set; } 
       
        public ConnectionController() 
        {
            connetionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;";
        }


        //[Obsolete]
        //public void addConteudo()
        //{
        //    string query = " INSERT INTO [dbo].[Conteudo] (Id, Titulo, Tipo, IdCriador) " +
        //                   " VALUES (@id, @titulo, @tipo, @criador ); ";

        //    using (sqlConnection)
        //    {
        //        SqlCommand = new SqlCommand(query, sqlConnection);
        //        SqlCommand.Parameters.AddWithValue("@id", 1);
        //        SqlCommand.Parameters.AddWithValue("@titulo", "Video teste");
        //        SqlCommand.Parameters.AddWithValue("@tipo", "teste");
        //        SqlCommand.Parameters.AddWithValue("@criador", 3);

        //        sqlConnection.Open();
        //        int i = SqlCommand.ExecuteNonQuery();
        //        if(i > 0)
        //        {
        //            Console.WriteLine("COMPLETO");
        //        }
        //        else
        //        {
        //            Console.WriteLine("FALHA");    
        //        }
                    
        //        sqlConnection.Close();
        //    }
        //}

        public String QueryGetAll(string classe)
        {
            string query = "SELECT * FROM [dbo].["+classe+"];";

            return query;
        }
        
        public String QueryGetByID(string classe, string param)
        {
            string query = "SELECT * FROM [dbo].["+classe+"] WHERE "+param+" = @id ;";

            return query;
        }
        
        public String QueryAdd(string classe, string[] camp, string[] param)
        {
            //string query = "SELECT * FROM [dbo].["+classe+"] WHERE "+param+" = @id ;";
            string query = " INSERT INTO [dbo].[" + classe + "] (";
            for(int i = 0; i < camp.Length; i++)
            {
                query += camp[i];
                if (i < camp.Length-1)
                {
                    query += ", ";
                }
            }
            query += " ) VALUES (";

            for(int i = 0; i < param.Length; i++)
            {
                query += param[i];
                if (i < param.Length-1)
                {
                    query += ", ";
                }
            }
            query += " ); ";

            return query;
        }
        
        public String QueryUpdate(string classe, string param, string paramUpd)
        {
            string query = " UPDATE [dbo].[" + classe + "] SET "+paramUpd+" = @valorUpdate " +
                "WHERE "+param+" = @id ";

            return query;
        }

        public String QueryDeleteByID(string classe, string param)
        {
            string query = "DELETE FROM [dbo].[" + classe + "] WHERE " + param + " = @id ;";

            return query;
        }

    }
}
