using Projet_C.Management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_C.Backend
{
    
    public class Loan 
    {
        public Loan(Copy c,DateTime SD,DateTime ED) 
        {
            Copie = c;
            StartDate = SD;
            EndDate = ED;
        }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Copy Copie  { get; set; }
        public int Id { get; set; }

    }
}
