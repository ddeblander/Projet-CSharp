using Projet_C.Backend;
using Projet_C.Backend.Singleton;
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
            if (date.Subtract(dateOfBirth).TotalMinutes < 0)
                MessageBox.Show("date de naissance incohérente !");
            else if (DaoPlayerSingleton.Instance.ReadUser(username)!=null ||(DaoAdminSingleton.Instance.ReadUser(username)!=null))
                MessageBox.Show("utilisateur existe déjà, selectionnez un autre Username");
            else
            {
                int credit = 10;

                Player pl = new Player(pseudo, date, dateOfBirth, dateOfBirth) { Username = username, Password = password };
                pl.Credit = credit;
                DaoPlayerSingleton.Instance.Insert(pl);

                MainWindow mainWindow = new MainWindow();
                this.Visibility = Visibility.Hidden;

                mainWindow.Show();
            }
            
        }
        protected override void OnClosed(EventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }
    }
}
