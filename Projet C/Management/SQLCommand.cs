using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_C.Management
{
    internal class SQLCommand
    {
        public static void CreateCommand(string queryString)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
