using Projet_C.Backend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
namespace Projet_C.Backend
{
    public class Player : User
    {


        public Player(string pseudo, DateTime rd, DateTime dob, DateTime ba)
        {

            Pseudo = pseudo;
            RegistrationDate = rd;
            DateOfBirth = dob;
            Credit = 10;
            BonusAcquired = ba;
        }

        public int Credit { get; set; }

        public string Pseudo { get; set; }

        public DateTime RegistrationDate { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime BonusAcquired { get; set; }

        public bool bonusAnniversary()
        {
            if (BonusAcquired.Year != DateTime.Now.Year)
            {
                BonusAcquired = DateTime.Now;
            }
            else
            { return false; }

            return true;
        }

    }
}
