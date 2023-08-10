using Projet_C.Backend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Controls.Primitives;
using ADO.Net.Client.Core;
using System.Data;
using System.Diagnostics;

namespace Projet_C.Management
{
    public abstract class Dao
    {
        protected SqlConnection connection;
        protected SqlCommand cmd = new SqlCommand();

        protected SqlDataReader reader;
        public Dao()
        {
            try
            {
                connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString);
                cmd.Connection = connection;
            }catch(Exception ex) { Trace.WriteLine(ex.ToString); }
            
            
            
        }
       
    }
}
