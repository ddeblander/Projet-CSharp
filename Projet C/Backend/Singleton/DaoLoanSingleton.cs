using Projet_C.Management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_C.Backend.Singleton
{
    internal class DaoLoanSingleton
    {
        private static DaoLoan _instance;
        public static DaoLoan Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DaoLoan();
                }
                return _instance;
            }
        }
    }
}
