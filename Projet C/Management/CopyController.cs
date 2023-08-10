using Projet_C.Backend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;

namespace Projet_C.Management
{
    internal class CopyController
    {
        // pour gerer la liste des copy ? ou peut être a mettre directement dans VideoGame
        private Copy cp;
        private VideoGame vG;
        private List<Copy> copyList;
        public CopyController() 
        {
            copyList= new List<Copy>();
        }
        public VideoGame VG { get; set; } 

        public Copy Cp { get; set; } 

        public void CreateCopy(VideoGame vg)
        {

        }
    }
}
