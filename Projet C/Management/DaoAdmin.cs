using Projet_C.Backend;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace Projet_C.Management
{
    public class DaoAdmin : Dao
    {
        public Admin CurrentAdmin { get; set; }
        public List<Admin> ReadAll()
        {
            
            List<Admin> list = new List<Admin>();

            Admin ad;
            try
            {
                connection.Open();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from dbo.admins";


                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ad = new Admin { Id_User = reader.GetInt32(0), Username = reader.GetString(1), Password = reader.GetString(2) };
                    list.Add(ad);
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
        public Admin ReadByID(int ID)
        {
            connection.Open();
            Admin ad = null;
            try
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from dbo.admins where ID=@ID";
                cmd.Parameters.AddWithValue("ID", ID);
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ad = new Admin { Id_User = reader.GetInt32(0), Username = reader.GetString(1), Password = reader.GetString(2) };
                }
            }
            catch (Exception ex) { Trace.Write(ex.Message); }
            connection.Close();
            cmd.Parameters.Clear();
            return ad;
        }
        public Admin ReadByUnique(String user, String pwd)
        {
            connection.Open();

            Admin ad = null;
            try
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from dbo.admins where username=@USER and password=@PWD";
                cmd.Parameters.AddWithValue("USER", user);
                cmd.Parameters.AddWithValue("PWD", pwd);
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ad = new Admin { Id_User = reader.GetInt32(0), Username = reader.GetString(1), Password = reader.GetString(2) };
                }
            }
            catch (Exception ex) { Trace.WriteLine(ex.Message); }
            cmd.Parameters.Clear();
            connection.Close();
            return ad;
        }
        public Admin ReadUser(String user)
        {
            if (connection.State == ConnectionState.Closed)
                connection.Open();

            Admin ad = null;
            try
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from dbo.admins where username=@USER";
                cmd.Parameters.AddWithValue("USER", user);
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ad = new Admin { Id_User = reader.GetInt32(0), Username = reader.GetString(1), Password = reader.GetString(2) };
                }
            }
            catch (Exception ex) { Trace.WriteLine(ex.Message); }
            cmd.Parameters.Clear();
            connection.Close();
            return ad;
        }

        public Boolean Insert(Admin ad)
        {

            if (ReadUser(ad.Username) != null)
            {

                return false;

            }
            connection.Open();
            try
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "insert into dbo.admins(username,password) values(@USER,@PWD)";
                cmd.Parameters.AddWithValue("USER", ad.Username);
                cmd.Parameters.AddWithValue("PWD", ad.Password);
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex) { Trace.WriteLine(ex.Message); }

            cmd.Parameters.Clear();
            connection.Close();
            return true;
        }

        public Boolean Delete(Admin ad)
        {

            connection.Open();
            try
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "delete dbo.admins where ID=@ID";
                cmd.Parameters.AddWithValue("ID", ad.Id_User);
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex) { Trace.WriteLine(ex.Message); }

            cmd.Parameters.Clear();
            connection.Close();
            return true;
        }
        public void update(Admin ad)
        {

            connection.Open();
            try
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "update dbo.admins set username=@USER,password=@PWD where id=@ID";
                cmd.Parameters.AddWithValue("USER", ad.Username);
                cmd.Parameters.AddWithValue("PWD", ad.Password);
                cmd.Parameters.AddWithValue("ID", ad.Id_User);
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex) { Trace.WriteLine(ex.Message); }

            cmd.Parameters.Clear();
            connection.Close();
        }


    }
}
