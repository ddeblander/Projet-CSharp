using Projet_C.Management;

namespace Projet_C.Backend.Singleton
{
    public class DaoAdminSingleton
    {
        private static DaoAdmin _instance;
        public static DaoAdmin Instance
        {
            get
            {
               if(_instance == null)
                {
                    _instance = new DaoAdmin();
                }
                return _instance;
            }
        }
    }
}
