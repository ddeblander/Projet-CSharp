using Projet_C.Backend;
using Projet_C.Management;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Logique d'interaction pour ModifyUser.xaml
    /// </summary>
    public partial class ModifyUser : Window
    {
        private DaoPlayer daoP = new DaoPlayer();
        private String username, password, pseudo;
        private DateTime date, dateOfBirth;
        private Player pl;
        int id, credit;
        public ModifyUser()
        {
            InitializeComponent();
        }

        private void OnClick(object sender, RoutedEventArgs e)
        {
            if(pl_id.Text!="")
                id = Convert.ToInt32(pl_id.Text.Trim());

            pl = daoP.ReadByID(id);
            


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
                
                if(plDateOfBirth.SelectedDate.HasValue)
                {
                    dateOfBirth = (DateTime)plDateOfBirth.SelectedDate;
                    pl.DateOfBirth = dateOfBirth;
                }
                else
                {
                    dateOfBirth = pl.DateOfBirth;
                }
                if(plCredit.Text!="")
                {
                    credit = Convert.ToInt32(plCredit.Text.Trim());
                    pl.Credit = credit;

                }
                else
                {
                    credit = pl.Credit;

                }

                Trace.WriteLine(pl.Id_User.ToString());
                daoP.update(pl);
            }

            MainWindow mainWindow = new MainWindow();
            this.Visibility = Visibility.Hidden;
            mainWindow.Show();
        }
        
    }
}
