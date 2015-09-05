using System;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using Dapper;

namespace TransactionWriter
{
    class Program
    {
        static void Main(string[] args)
        {
            new Program().Run();
        }

        private void Run()
        {
            int count = 0;

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Database"].ConnectionString))
            {
                connection.Execute("create table List (Id int identity primary key, payload nvarchar(max))");
                while (true)
                {
                    connection.Execute("insert into list (payload) values (@payload)", new {payload = "@@@"});
                    Console.WriteLine("Count: {0}", ++count);
                }
            }
        }
    }
}
