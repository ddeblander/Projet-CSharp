using Projet_C.Backend;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace Projet_C.Management
{
    internal class DaoCopy: Dao
    {
        private DaoPlayer DP;
        private DaoVideoGame DVG;
        public DaoCopy()
        {
            DP= new DaoPlayer();
            DVG= new DaoVideoGame();
        }
        public List<Copy> ReadAll()
        {
            connection.Open();
            List<Copy> list = new List<Copy>();

            Copy ad;
            try
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from dbo.Copies";


                reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    ad = new Copy(DVG.ReadByID(reader.GetInt32(1)), DP.ReadByID(reader.GetInt32(2)));
                    
                    ad.Id= reader.GetInt32(0);
                    if(!reader.IsDBNull(3))
                    {
                        ad.Pl_Borrower =DP.ReadByID(reader.GetInt32(3));
                    }
                    
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
        public Copy ReadByID(int ID)
        {
            connection.Open();
            Copy ad = null;
            try
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from dbo.Copies where ID=@ID";
                cmd.Parameters.AddWithValue("ID", ID);
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ad = new Copy(DVG.ReadByID(reader.GetInt32(1)), DP.ReadByID(reader.GetInt32(2)));
                    ad.Id = reader.GetInt32(0);
                    if (!reader.IsDBNull(3))
                    {
                        ad.Pl_Borrower = DP.ReadByID(reader.GetInt32(3));
                    }
                }
            }
            catch (Exception ex) { Trace.Write(ex.Message); }
            connection.Close();
            cmd.Parameters.Clear();
            return ad;
        }
        public Copy ReadByUnique(VideoGame vG, Player pl_Owner)
        {
            connection.Open();

            Copy ad = null;
            try
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from dbo.Copies where ID_VideoGame=@VG and ID_Player_owner=@PO";
                cmd.Parameters.AddWithValue("VG", vG.Id);
                cmd.Parameters.AddWithValue("PO", pl_Owner.Id_User);
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ad = new Copy(DVG.ReadByID(reader.GetInt32(1)), DP.ReadByID(reader.GetInt32(2)));
                    ad.Id = reader.GetInt32(0);
                    if (!reader.IsDBNull(3))
                    {
                        ad.Pl_Borrower = DP.ReadByID(reader.GetInt32(3));
                    }
                }
            }
            catch (Exception ex) { Trace.WriteLine(ex.Message); }
            cmd.Parameters.Clear();
            connection.Close();
            return ad;
        }
       

        public Boolean Insert(Copy ad)
        {

            connection.Open();
            try
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "insert into dbo.Copies(ID_VideoGame,ID_Player_owner) values(@IDGAME,@IDPLAYER)";
                cmd.Parameters.AddWithValue("IDGAME", ad.Vd.Id);
                cmd.Parameters.AddWithValue("IDPLAYER", ad.Pl_owner.Id_User);
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex) { Trace.WriteLine(ex.Message); }

            cmd.Parameters.Clear();
            connection.Close();
            return true;
        }

        public Boolean Delete(Copy ad)
        {

            connection.Open();
            try
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "delete dbo.Copies where ID=@ID";
                cmd.Parameters.AddWithValue("ID", ad.Id);
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex) { Trace.WriteLine(ex.Message); }

            cmd.Parameters.Clear();
            connection.Close();
            return true;
        }
        public Boolean Update(Copy ad)
        {

            connection.Open();
            try
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "update dbo.Copies set ID_VideoGame=@IDGAME,ID_Player_owner=@IDPLAYER where ID=@ID";
                cmd.Parameters.AddWithValue("IDGAME", ad.Vd.Id);
                cmd.Parameters.AddWithValue("IDPLAYER", ad.Pl_owner.Id_User);
                cmd.Parameters.AddWithValue("ID", ad.Id);
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex) { Trace.WriteLine(ex.Message); }

            cmd.Parameters.Clear();
            connection.Close();
            return true;
        }


    }
}
