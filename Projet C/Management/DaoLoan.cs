using Projet_C.Backend;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_C.Management
{
    internal class DaoLoan : Dao
    {
        private List<Loan> list;
        private Loan loan;
        private DaoCopy dc;
        public DaoLoan()
        {
            dc= new DaoCopy();
            list = new List<Loan>();
        }
        public List<Loan> ReadAll()
        {
           
            try
            {
                connection.Open();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from dbo.loan";


                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    loan = new Loan(dc.ReadByID(reader.GetInt32(3)),reader.GetDateTime(1),reader.GetDateTime(2));
                    loan.Id = reader.GetInt32(0);
                    list.Add(loan);
                }
            }
            catch (Exception ex)
            {
                Trace.Write(ex.Message);

            }
            connection.Close();
            cmd.Parameters.Clear();
            return list;

        }
        public Loan ReadByID(int ID)
        {
            connection.Open();
            Loan loan = null;
            try
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from dbo.loan where ID=@ID";
                cmd.Parameters.AddWithValue("ID", ID);
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    loan = new Loan(dc.ReadByID(reader.GetInt32(3)), reader.GetDateTime(1), reader.GetDateTime(2));
                    loan.Id = reader.GetInt32(0);

                }
            }
            catch (Exception ex) { Trace.Write(ex.Message); }
            connection.Close();
            cmd.Parameters.Clear();
            return loan;
        }
        public Loan ReadByCopy(Copy cp)
        {
            connection.Open();
            try
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from dbo.loan where ID_Copies=@ID";
                cmd.Parameters.AddWithValue("ID", cp.Id);
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    loan = new Loan(dc.ReadByID(reader.GetInt32(3)), reader.GetDateTime(1), reader.GetDateTime(2));
                    loan.Id = reader.GetInt32(0);
                    connection.Close();
                    return loan;

                }
            }
            catch (Exception ex) { Trace.Write(ex.Message); }
            connection.Close();
            cmd.Parameters.Clear();
            return null;
        }

        public Boolean Insert(Loan lo)
        {
            connection.Open();
            try
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "insert into dbo.loan(startdate,enddate,ID_Copies) values(@SD,@ED,@IDC)";
                cmd.Parameters.AddWithValue("SD", lo.StartDate);
                cmd.Parameters.AddWithValue("ED", lo.EndDate);
                cmd.Parameters.AddWithValue("IDC", lo.Copie.Id);
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex) { 
                Trace.WriteLine(ex.Message);
                return false;
            }

            cmd.Parameters.Clear();
            connection.Close();
            return true;
        }

        public Boolean Delete(Loan lo)
        {

            connection.Open();
            try
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "delete dbo.loan where ID=@ID";
                cmd.Parameters.AddWithValue("ID", lo.Id);
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex) { Trace.WriteLine(ex.Message); }

            cmd.Parameters.Clear();
            connection.Close();
            return true;
        }
        public void update(Loan lo)
        {

            connection.Open();
            try
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "update dbo.admins set startdate=@SD,enddate=@ED,ID_Copies=@IDC where id=@ID";
                cmd.Parameters.AddWithValue("SD", lo.StartDate);
                cmd.Parameters.AddWithValue("ED", lo.EndDate);
                cmd.Parameters.AddWithValue("IDC", lo.Copie.Id);
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex) { Trace.WriteLine(ex.Message); }

            cmd.Parameters.Clear();
            connection.Close();
        }
    }
}
