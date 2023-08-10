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
    /// Logique d'interaction pour Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        private DaoPlayer daoP = new DaoPlayer();
        public Register()
        {
            InitializeComponent();
        }

        private void OnClick(object sender, RoutedEventArgs e)
        {
            String username = plUsername.Text;
            String password = plPassword.Password;
            String pseudo = plPseudo.Text;
            DateTime date = DateTime.Now;
            DateTime dateOfBirth = (DateTime)plDateOfBirth.SelectedDate;
            int credit = 10;

            Player pl = new Player(pseudo, date, dateOfBirth) {Username=username,Password=password };
            pl.Credit= credit;
            daoP.Insert(pl);

            MainWindow mainWindow = new MainWindow();
            this.Visibility = Visibility.Hidden;
            mainWindow.Show();
        }
    }
}
