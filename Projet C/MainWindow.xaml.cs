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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Projet_C
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DaoAdmin dao= new DaoAdmin();
        private DaoPlayer daoP= new DaoPlayer();
        private DaoVideoGame daoVG= new DaoVideoGame();
        private DaoCopy daoCP= new DaoCopy();
        public MainWindow()
        {
            InitializeComponent();
            menu_selection_admin.SelectedIndex = 0;
            menu_selection_player.SelectedIndex = 0;
        }

        private void Selector_OnSelected(object sender, RoutedEventArgs e)
        {
            HideAdminGrid();
            var listView = (ListView)sender;
            switch (listView.SelectedIndex)
            {
                case 0:
                    grid_ad_user.Visibility = Visibility.Visible;
                    break;

                case 1:
                    grid_ad_user_add.Visibility = Visibility.Visible;
                    break;

                case 2:
                    grid_ad_user_delete.Visibility = Visibility.Visible;
                    break;
                case 3:
                    grid_ad_user_modify.Visibility = Visibility.Visible;
                    break;

                case 4:                    
                    grid_ad_video_game.Visibility = Visibility.Visible;                    
                    break;

                case 5:                  
                    grid_ad_video_game_add.Visibility = Visibility.Visible;
                    break;

                case 6:
                    grid_ad_video_game_delete.Visibility = Visibility.Visible;
                    break;
                case 7:
                    grid_ad_video_game_modify.Visibility = Visibility.Visible;
                    break;
                case 8:                    
                    grid_ad_user_loan.Visibility = Visibility.Visible;
                    break;

                default:
                    grid_ad_user.Visibility = Visibility.Visible;
                    break;
            }
        }

        /// <summary>
        /// hide all admin grid
        /// </summary>
        public void HideAdminGrid()
        {
            grid_ad_user.Visibility = Visibility.Collapsed;
            grid_ad_user_add.Visibility = Visibility.Collapsed;
            grid_ad_user_delete.Visibility = Visibility.Collapsed;
            grid_ad_user_modify.Visibility = Visibility.Collapsed;
            grid_ad_video_game.Visibility = Visibility.Collapsed;
            grid_ad_video_game_add.Visibility = Visibility.Collapsed;
            grid_ad_video_game_delete.Visibility = Visibility.Collapsed;
            grid_ad_video_game_modify.Visibility = Visibility.Collapsed;
            grid_ad_user_loan.Visibility = Visibility.Collapsed;
        }

        private void Selector_OnSelected2(object sender, RoutedEventArgs e)
        {
            HidePlayerGrid();
            var listView = (ListView)sender;
            switch (listView.SelectedIndex)
            {
                case 0:
                    
                    grid_pl_video_game.Visibility = Visibility.Visible;
                    ShowUserList();

                    break;
                case 1:                   
                    grid_pl_video_game_add.Visibility = Visibility.Visible;
                   
                    break;

                case 2:                   
                    grid_pl_user_loan.Visibility = Visibility.Visible;
                    break;
                case 3:
                    grid_pl_user_account.Visibility = Visibility.Visible;
                    break;
                case 4:
                    grid_pl_modify_account.Visibility = Visibility.Visible;
                    break;
                default:
                    grid_pl_video_game.Visibility = Visibility.Visible;
                    break;
            }
        }

        /// <summary>
        /// hide all player grid
        /// </summary>
        public void HidePlayerGrid()
        {
            grid_pl_video_game.Visibility = Visibility.Collapsed;
            grid_pl_video_game_add.Visibility = Visibility.Collapsed;
            grid_pl_user_loan.Visibility = Visibility.Collapsed;
            grid_pl_user_account.Visibility = Visibility.Collapsed;
            grid_pl_modify_account.Visibility = Visibility.Collapsed;
        }

        private void ShowUserList()
        {
            dg_ad_user.ItemsSource = dao.ReadAll();
            dg_ad_video_game.ItemsSource = daoVG.ReadAll();
            dg_ad_user_loan.ItemsSource = daoCP.ReadAll();

            dg_pl_video_game.ItemsSource = daoVG.ReadAll();
            dg_pl_user_loan.ItemsSource = daoCP.ReadAll();
        }

        private void ButtonLogin_OnClick(object sender, RoutedEventArgs e)
        {
            Admin  ad = dao.ReadByUnique(login_user.Text,login_pwd.Password);
            Player pl = daoP.ReadByUnique(login_user.Text, login_pwd.Password);
   
            if (ad==null && pl==null)
            {
                login_error_text.Visibility = Visibility.Visible;
                login_pwd.Password = string.Empty;
                return;
            }

            else if(ad==null)
            {
                menu_selection_admin.Visibility = Visibility.Collapsed;
                HideAdminGrid();
            }
            else if(pl==null)
            {
                menu_selection_player.Visibility = Visibility.Collapsed;
                HidePlayerGrid();
            }

            Login_ui.Visibility = Visibility.Collapsed;
        }

        private void ButtonReg_OnClick(object sender, RoutedEventArgs e)
        {
            Register register = new Register();
            this.Visibility = Visibility.Hidden;
            register.Show();
        }

       private void Ad_add_user_OnClick(object sender, RoutedEventArgs e)
        {
            UserAdd userAdd = new UserAdd();
            this.Visibility = Visibility.Hidden;
            userAdd.Show();
        }

        private void Ad_delete_user_OnClick(object sender, RoutedEventArgs e)
        {
            DeleteUser deleteUser = new DeleteUser();
            this.Visibility = Visibility.Hidden;
            deleteUser.Show();
        }

        private void Ad_modify_user_OnClick(object sender, RoutedEventArgs e)
        {
            ModifyUser modifyUser = new ModifyUser();
            this.Visibility = Visibility.Hidden;
            modifyUser.Show();
        }

        private void Ad_add_video_game_OnClick(object sender, RoutedEventArgs e)
        {
            AddVideoGame addVideoGame = new AddVideoGame();
            this.Visibility = Visibility.Hidden;
            addVideoGame.Show();
        }

        private void Ad_delete_video_game_OnClick(object sender, RoutedEventArgs e)
        {
            DeleteVideoGame deleteVideoGame = new DeleteVideoGame();
            this.Visibility = Visibility.Hidden;
            deleteVideoGame.Show();
        }

        private void Ad_modify_video_game_OnClick(object sender, RoutedEventArgs e)
        {
            ModifyVideoGame modifyVideoGame = new ModifyVideoGame();
            this.Visibility = Visibility.Hidden;
            modifyVideoGame.Show();
        }

        private void Pl_user_account_OnClick(object sender, RoutedEventArgs e)
        {
            UserAccount userAccount = new UserAccount();
            this.Visibility = Visibility.Hidden;
            userAccount.Show();
        }

        private void Pl_modify_account_OnClick(object sender, RoutedEventArgs e)
        {
            ModifyAccount modifyAccount = new ModifyAccount();
            this.Visibility = Visibility.Hidden;
            modifyAccount.Show();
        }

    }
}
