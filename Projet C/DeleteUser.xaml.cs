using Projet_C.Management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Projet_C
{
    /// <summary>
    /// Logique d'interaction pour DeleteUser.xaml
    /// </summary>
    public partial class DeleteUser : Window
    {
        private DaoPlayer daoP = new DaoPlayer();
        public DeleteUser()
        {
            InitializeComponent();
        }

        private void OnClick(object sender, RoutedEventArgs e)
        {
            String user = ad_user.Text;

            Player pl = daoP.ReadUser(user);

            if (daoP != null)
            {
                
                daoP.Delete(pl);
            }

            MainWindow mainWindow = new MainWindow();
            this.Visibility = Visibility.Hidden;
            mainWindow.Show();
           
        }
    }
}
