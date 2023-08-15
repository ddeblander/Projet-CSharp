using Projet_C.Management;

namespace Projet_C.Backend.Singleton
{
    public class DaoPlayerSingleton
    {
        private static DaoPlayer _instance;
        public static DaoPlayer Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DaoPlayer();
                }
                return _instance;
            }
        }
    }
}
