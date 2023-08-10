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
    /// Logique d'interaction pour userAdd.xaml
    /// </summary>
    public partial class UserAdd : Window
    {
        private DaoPlayer daoP = new DaoPlayer();
        public UserAdd()
        {
            InitializeComponent();
        }

        private void addUser(object sender, RoutedEventArgs e)
        {
            String username = plUsername.Text;
            String password = plPassword.Password;
            String pseudo = plPseudo.SelectedText;
            DateTime date = DateTime.Now;
            DateTime dateOfBirth = (DateTime)plDateOfBirth.SelectedDate;
            int credit = 10;

            Player pl = new Player(pseudo, date, dateOfBirth) { Username = username, Password = password };
            pl.Credit = credit;
            daoP.Insert(pl);

            MainWindow mainWindow = new MainWindow();
            this.Visibility = Visibility.Hidden;
            mainWindow.Show();
        }
    }
}
