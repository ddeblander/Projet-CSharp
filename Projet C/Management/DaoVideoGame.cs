using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace Projet_C.Management
{
    internal class DaoVideoGame : Dao
    {
        public List<VideoGame> ReadAll()
        {
            connection.Open();
            List<VideoGame> list = new List<VideoGame>();

            VideoGame vd;
            try
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from dbo.VideoGames";
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    vd = new VideoGame(reader.GetString(1), reader.GetString(3));
                    vd.Id = reader.GetInt32(0);
                    vd.CreditCost = reader.GetInt32(2);
                    list.Add(vd);
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
        public VideoGame ReadByID(int ID)
        {
            if (connection.State == ConnectionState.Closed)
                connection.Open();
            VideoGame vd = null;
            try
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from dbo.VideoGames where ID=@ID";
                cmd.Parameters.AddWithValue("ID", ID);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    vd = new VideoGame(reader.GetString(1), reader.GetString(3));
                    vd.Id = reader.GetInt32(0);
                    vd.CreditCost = reader.GetInt32(2);
                }
            }
            catch (Exception ex) { Trace.Write(ex.Message); }
            connection.Close();
            cmd.Parameters.Clear();
            return vd;
        }
        public VideoGame ReadByUnique(String name, String console)
        {
            connection.Open();
            VideoGame vd = null;
            try
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from dbo.VideoGames where name=@NAME and console=@CONS";
                cmd.Parameters.AddWithValue("NAME", name);
                cmd.Parameters.AddWithValue("CONS", console);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    vd = new VideoGame(reader.GetString(1), reader.GetString(3));
                    vd.Id = reader.GetInt32(0);
                    vd.CreditCost = reader.GetInt32(2);
                }
            }
            catch (Exception ex) { Trace.WriteLine(ex.Message); }
            cmd.Parameters.Clear();
            connection.Close();
            return vd;
        }
        public List<VideoGame> ReadName(String name)
        {
            if (connection.State == ConnectionState.Closed)
                connection.Open();
            VideoGame vd;
            List<VideoGame> list= new List<VideoGame>(); 
            try
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from dbo.VideoGames where name=@NAME";
                cmd.Parameters.AddWithValue("NAME", name);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    vd = new VideoGame(reader.GetString(1), reader.GetString(3));
                    vd.Id = reader.GetInt32(0);
                    vd.CreditCost = reader.GetInt32(2);
                    list.Add(vd);
                }
            }
            catch (Exception ex) { Trace.WriteLine(ex.Message); }
            cmd.Parameters.Clear();
            connection.Close();
            return list;
        }
        public Boolean Insert(VideoGame vd)
        {
            if (ReadByUnique(vd.Name,vd.Console) != null)
            {
                return false;

            }
            connection.Open();
            try
            {
               cmd.CommandType = CommandType.Text;
                cmd.CommandText = "insert into dbo.VideoGames(name,creditCost,console) values(@NAME,@CC,@CONS)";
                cmd.Parameters.AddWithValue("NAME", vd.Name);
                cmd.Parameters.AddWithValue("CC", vd.CreditCost);
                cmd.Parameters.AddWithValue("CONS", vd.Console);      
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex) { Trace.WriteLine(ex.Message); }

            cmd.Parameters.Clear();
            connection.Close();
            return true;
        }

        public Boolean Delete(VideoGame vd)
        {

            connection.Open();
            try
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "delete dbo.VideoGames where ID=@ID";
                cmd.Parameters.AddWithValue("ID", vd.Id);
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex) { Trace.WriteLine(ex.Message); }

            cmd.Parameters.Clear();
            connection.Close();
            return true;

        }
        public Boolean Update(VideoGame vd)
        {
            if (ReadByUnique(vd.Name, vd.Console) != null)
            {
                return false;

            }
            connection.Open();
            try
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "update dbo.VideoGames set name=@NAME,creditCost=@CC,console=@CONS where ID=@ID";
                cmd.Parameters.AddWithValue("NAME", vd.Name);
                cmd.Parameters.AddWithValue("CC", vd.CreditCost);
                cmd.Parameters.AddWithValue("CONS", vd.Console);
                cmd.Parameters.AddWithValue("ID", vd.Id);
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex) { Trace.WriteLine(ex.Message); }

            cmd.Parameters.Clear();
            connection.Close();
            return true;
        }
    }
}

