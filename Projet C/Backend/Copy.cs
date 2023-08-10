using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_C.Backend
{
     class Copy
    {
        private int ID;
        private VideoGame vd;
        private Player pl_Owner,pl_borrower;

        public Copy(VideoGame vd, Player pl)
        {
            Vd = vd;
            Pl_owner = pl;
        }
        public int Id { get; set; }
        public VideoGame Vd { get; set; }
        public Player Pl_owner { get; set; }
        public Player Pl_Borrower { get; set;}

        public void ReleaseCopy()
        {
            Pl_Borrower = null;
            //Appel de la méthode qui retire l'ID_Borrower dans la BDD
        }
        public void Borrow(Player pl)
        {
            if(isAvailable())
                Pl_Borrower= pl;
            //Appel de la méthode qui ajoute  l'ID_Borrower dans la BDD
        }
        public Boolean isAvailable()
        {
            return Pl_Borrower==null;  
        }





    }
}
