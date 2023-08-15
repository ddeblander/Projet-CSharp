using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_C.Backend
{
    public class Copy
    {

        public Copy(VideoGame vg, Player pl)
        {
            Vg = vg;
            Pl_owner = pl;
            Pl_Borrower = null;
        }
        public int Id { get; set; }
        public VideoGame Vg { get; set; }
        public Player Pl_owner { get; set; }
        public Player? Pl_Borrower { get; set;}


        public void ReleaseCopy()
        {
            Pl_Borrower = null;
            //Appel de la méthode qui retire l'ID_Borrower dans la BDD
        }
        public bool Borrow(Player pl)
        {
            if (isAvailable())
                Pl_Borrower = pl;
            else
                return false;

            return true;
            //Appel de la méthode qui ajoute  l'ID_Borrower dans la BDD
        }
        public Boolean isAvailable()
        {
            return Pl_Borrower==null;  
        }





    }
}
