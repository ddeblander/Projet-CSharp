using Projet_C.Backend;
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
using System.Windows.Shapes;

namespace Projet_C
{
    /// <summary>
    /// Logique d'interaction pour AddVideoGame.xaml
    /// </summary>
    public partial class AddVideoGame : Window
    {
        private DaoVideoGame daoVG = new DaoVideoGame();
        public AddVideoGame()
        {
            InitializeComponent();
        }

        private void OnClick(object sender, RoutedEventArgs e)
        {
            String name = ad_name.Text;
            int ad_credit = 10;
            String console = ad_console.Text;


            VideoGame vd = new VideoGame(name, console) { CreditCost=ad_credit};
            daoVG.Insert(vd);

            MainWindow mainWindow = new MainWindow();
            this.Visibility = Visibility.Hidden;
            mainWindow.Show();
        }
    }
}
