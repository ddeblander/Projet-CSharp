using Projet_C.Management;

namespace Projet_C.Backend.Singleton
{
    public class DaoCopySingleton
    {
        private static DaoCopy _instance;
        public static DaoCopy Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DaoCopy();
                }
                return _instance;
            }
        }
    }
}
