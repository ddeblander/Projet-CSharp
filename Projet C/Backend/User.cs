using Projet_C.Backend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_C.Backend
{
    
    public abstract class User
    {

        public string Username { get; set; }
        public string Password { get; set; }

        public int Id_User { get; set; }
    }

}
