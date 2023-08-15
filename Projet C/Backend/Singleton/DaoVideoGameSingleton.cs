using Projet_C.Management;

namespace Projet_C.Backend.Singleton
{
    public class DaoVideoGameSingleton
    {
        private static DaoVideoGame _instance;
        public static DaoVideoGame Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DaoVideoGame();
                }
                return _instance;
            }
        }
    }
}
