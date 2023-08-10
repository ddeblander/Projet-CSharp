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
    /// Logique d'interaction pour ModifyAccount.xaml
    /// </summary>
    public partial class ModifyAccount : Window
    {
        private DaoPlayer daoP = new DaoPlayer();
        private String username, password, pseudo;
        
        private Player pl;
       
        public ModifyAccount()
        {
            InitializeComponent();
        }

        private void OnClick(object sender, RoutedEventArgs e)
        {
      

            pl = daoP.ReadByUnique(username,password);



            if (pl != null)
            {
                if ((plUsername.Text != "") && (daoP.ReadUser(plUsername.Text) == null))
                {
                    username = plUsername.Text;
                    pl.Username = username;
                }
                else
                {
                    username = pl.Username;

                }
                if (plPassword.Password != "")
                {
                    password = plPassword.Password;
                    pl.Password = password;
                }
                else
                {
                    password = pl.Password;

                }

                if (plPseudo.Text != "")
                {
                    pseudo = plPseudo.Text;
                    pl.Pseudo = pseudo;
                }
                else
                {
                    pseudo = pl.Pseudo;
                }

                

               
                daoP.update(pl);
            }

            MainWindow mainWindow = new MainWindow();
            this.Visibility = Visibility.Hidden;
            mainWindow.Show();
        }
    }
}
