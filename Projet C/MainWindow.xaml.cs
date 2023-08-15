using Projet_C.Backend;
using Projet_C.Backend.Singleton;
using Projet_C.Management;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

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

        private List<VideoGame> lVG;
        private List<VideoGame> lVGSearched;

        private ObservableCollection<VideoGame> videoGames;

        private VideoGame selectedVideoGame;
        public MainWindow()
        {
            InitializeComponent();
            menu_selection_admin.SelectedIndex = 0;
            menu_selection_player.SelectedIndex = 0;
            lVG = new List<VideoGame>();
            lVGSearched = new List<VideoGame>();

            lVG= DaoVideoGameSingleton.Instance.ReadAll();
            lVGSearched = lVG;
            videoGames = new ObservableCollection<VideoGame>(lVGSearched);
            VideoGameLV.ItemsSource = videoGames;
        }

        private void Selector_OnSelected(object sender, RoutedEventArgs e)
        {
            HideAdminGrid();
            var listView = (ListView)sender;
            switch (listView.SelectedIndex)
            {
                case 0:
                   
                    break;

                case 1:
                    
                    break;

                case 2:
                   
                    break;
                case 3:
                    
                    break;

                case 4:                    
                                      
                    break;

                case 5:                  
                    
                    break;

                case 6:
                    
                    break;
                case 7:
                    
                    break;
                case 8:                    
                    
                    break;

                default:
                    //grid_ad_user.Visibility = Visibility.Visible;
                    break;
            }
        }
        /// <summary>
        /// show selected videogame specs 
        /// </summary>
        private void VideoGameLV_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            VideoGame vg = (VideoGame)((ListView)sender).SelectedItem;
            if(vg != null)
            {
                SelectedVideoGameNameTB.Text = vg.Name;
                SelectedVideoGameConsoleTB.Text = vg.Console;
                SelectedVideoGameCreditCostTB.Text = vg.CreditCost.ToString();
            }
            
        }
        /// <summary>
        /// search the videogame(s) by name 
        /// </summary>
        private void VideoGameSB_OnTextChanged(object sender, TextChangedEventArgs  e)
        {
            videoGames.Clear();
            /*foreach (var item in lVG)
            {
                if(item.Name.ToLower().Contains(VideoGameSB.Text.ToLower()))
                {
                    
                    videoGames.Add(item);
                }
            }*/
            var searchTB =(TextBox)sender;
            lVG.Where(x=> x.Name.ToLower().Contains(searchTB.Text.ToLower())).ToList().ForEach(x=> videoGames.Add(x));
            if (videoGames.Count == 0 ) 
            {
                SelectedVideoGameNameTB.Text = "";
                SelectedVideoGameConsoleTB.Text = "";
                SelectedVideoGameCreditCostTB.Text = "";
            }
            else
            {
                SelectedVideoGameNameTB.Text = videoGames.First().Name;
                SelectedVideoGameConsoleTB.Text = videoGames.First().Console;
                SelectedVideoGameCreditCostTB.Text = videoGames.First().CreditCost.ToString();
            }
            

        }

    /// <summary>
    /// hide all admin grid
    /// </summary>
    public void HideAdminGrid()
        {
            
        }

        private void Selector_OnSelected2(object sender, RoutedEventArgs e)
        {
            HidePlayerGrid();
            var listView = (ListView)sender;
            switch (listView.SelectedIndex)
            {
                case 0:
                    
                   
                    ShowUserList();

                    break;
                case 1:                   
                    
                   
                    break;

                case 2:                   
                    
                    break;
                case 3:
                    
                    break;
                case 4:
                   
                    break;
                default:
                    
                    break;
            }
        }

        /// <summary>
        /// hide all player grid
        /// </summary>
        public void HidePlayerGrid()
        {
           
        }

        private void ShowUserList()
        {
            
        }

        private void ButtonLogin_OnClick(object sender, RoutedEventArgs e)
        {
            Admin  ad = DaoAdminSingleton.Instance.ReadByUnique(login_user.Text,login_pwd.Password);
            Player pl = DaoPlayerSingleton.Instance.ReadByUnique(login_user.Text, login_pwd.Password);
   
            if (ad==null && pl==null)
            {
                login_error_text.Visibility = Visibility.Visible;
                login_pwd.Password = string.Empty;
                MessageBox.Show("Pseudo/Mot de passe incorrect");
                return;
            }

            else if(ad==null)
            {
                menu_selection_admin.Visibility = Visibility.Collapsed;
                HideAdminGrid();
                DaoPlayerSingleton.Instance.CurrentPlayer = pl;
            }
            else if(pl==null)
            {
                menu_selection_player.Visibility = Visibility.Collapsed;
                HidePlayerGrid();
                DaoAdminSingleton.Instance.CurrentAdmin = ad;
            }

            Login_ui.Visibility = Visibility.Collapsed;
            login_error_text.Visibility = Visibility.Visible;
            login_pwd.Password = string.Empty;
        }

        private void ButtonReg_OnClick(object sender, RoutedEventArgs e)
        {
            Register register = new Register();
            this.Visibility = Visibility.Hidden;
            register.Show();
        }

        private void Selector_OnSelected2(object sender, SelectionChangedEventArgs e)
        {
            ListView lv = (ListView)sender;
            switch (lv.SelectedIndex)
            {
                case 2://déconnexion
                    if(MessageBox.Show("Voulez-vous vous déconnecter ?", "", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        DaoPlayerSingleton.Instance.CurrentPlayer = null;
                        DaoAdminSingleton.Instance.CurrentAdmin = null;
                        Login_ui.Visibility = Visibility.Visible;
                    }
                    break;
            }
            lv.SelectedItem = null;
        }

        private void Selector_OnSelected(object sender, SelectionChangedEventArgs e)
        {
            ListView lv = (ListView)sender;
            switch (lv.SelectedIndex)
            {
                case 1://déconnexion
                    if (MessageBox.Show("Voulez-vous vous déconnecter ?", "", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        DaoPlayerSingleton.Instance.CurrentPlayer = null;
                        DaoAdminSingleton.Instance.CurrentAdmin = null;
                        Login_ui.Visibility = Visibility.Visible;
                    }
                    break;
            }
           
            lv.SelectedItem = null;
        }
    }
}
