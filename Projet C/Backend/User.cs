using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_C.Backend
{
    
    public abstract class User
    {
        protected int id_User;
        protected string username;
        protected string password;

        public bool Login()
        {
            return true;
        }
        public string Username { get; set; }
        public string Password { get; set; }

        public int Id_User { get; set; }
    }

}
