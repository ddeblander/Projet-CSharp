using Projet_C.Backend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

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
            if ((BonusAcquired.Year < DateTime.Now.Year)&&(BonusAcquired.Month> DateTime.Now.Month))
            {
                BonusAcquired = DateTime.Now;
                Credit += 2;
            }
            else if(((BonusAcquired.Year - DateTime.Now.Year)<1)&& (BonusAcquired.Month < DateTime.Now.Month))
            {
                BonusAcquired = DateTime.Now.AddYears(-1);
                Credit += 2;
            }
            else
            { return false; }

            return true;
        }

        public override bool Equals(object? obj)
        {
            return obj is Player player &&
                   Username == player.Username &&
                   Password == player.Password &&
                   Id_User == player.Id_User &&
                   Credit == player.Credit &&
                   Pseudo == player.Pseudo &&
                   RegistrationDate == player.RegistrationDate &&
                   DateOfBirth == player.DateOfBirth &&
                   BonusAcquired == player.BonusAcquired;
        }
    }
}
