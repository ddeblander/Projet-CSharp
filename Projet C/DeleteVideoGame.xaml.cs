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
    /// Logique d'interaction pour DeleteVideoGame.xaml
    /// </summary>
    public partial class DeleteVideoGame : Window
    {
        private DaoVideoGame daoVG = new DaoVideoGame();
        public DeleteVideoGame()
        {
            InitializeComponent();
        }

        private void OnClick(object sender, RoutedEventArgs e)
        {
            String name = gameName.Text;
            String console = consoleName.Text;

            VideoGame vg = daoVG.ReadByUnique(name, console);

            if (vg != null)
            {

                daoVG.Delete(vg);
            }

            MainWindow mainWindow = new MainWindow();
            this.Visibility = Visibility.Hidden;
            mainWindow.Show();

        }
    }
}
