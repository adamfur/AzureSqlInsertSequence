using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using Dapper;

namespace TransactionReader
{
    class Program
    {
        static void Main(string[] args)
        {
            new Program().Run();
        }

        private void Run()
        {
            int id = 0;

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Database"].ConnectionString))
            {
                while (true)
                {
                    var items = connection.Query<ListItem>("select top 20 id, payload from list where id > @Id", new { id = id }).ToList();

                    foreach (var item in items)
                    {
                        Console.WriteLine("Item: {0}", item.Id);

                        if (item.Id != id + 1)
                        {
                            throw new Exception("Invalid sequence");
                        }

                        id = item.Id;
                    }
                }
            }
        }
    }
}
