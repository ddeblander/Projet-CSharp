using ADO.Net.Client.Annotations;
using Projet_C.Backend;
using Projet_C.Backend.Singleton;
using Projet_C.Management;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
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

        private List<VideoGame> lVG;
        private List<Loan> loanList;
        private List<Copy> listCopy;


        private ObservableCollection<VideoGame> videoGames;
        private ObservableCollection<Loan> loans;
        private ObservableCollection<Copy> copies;

        private VideoGame selectedAdminVideoGame;
        private VideoGame selectedVideoGame;
        private VideoGame selectedCopyVideoGame;

        private Copy selectedCopy;

        private Loan selectedLoan;

        private int amende=5;

        

        

        public MainWindow()
        {
            InitializeComponent();
            menu_selection_admin.SelectedIndex = 0;
            menu_selection_player.SelectedIndex = 0;

            lVG = new List<VideoGame>();


            PopulateVideoGameList();

            loanList = new List<Loan>();

            listCopy= new List<Copy>();

            

            Login_ui.Visibility = Visibility.Visible;
            OnLoanBT.Visibility = Visibility.Collapsed;
        }

        private void PopulateVideoGameList()
        {
            lVG = DaoVideoGameSingleton.Instance.ReadAll();
            videoGames = new ObservableCollection<VideoGame>();

            VideoGameAdminListLV.ItemsSource = videoGames;
            VideoGameLV.ItemsSource = videoGames;
            lVG = lVG.OrderBy(x => x.Name).ToList();
            lVG.ForEach(x=> videoGames.Add(x));

            selectedVideoGame = videoGames.FirstOrDefault();
            selectedAdminVideoGame = videoGames.FirstOrDefault();
            FilterVideoGameAdmin(searchTB.Text);
        }
        private void PopulateLoanList()
        {
            PopulateCopyList();
            loanList.Clear();
            List<Copy> lCopy= new List<Copy>();

            loanList =DaoLoanSingleton.Instance.ReadAll();
            List<Loan> lLoan2= new List<Loan>(); 
            foreach (var item in loanList)
            {
                Copy cpTest = DaoCopySingleton.Instance.ReadByID(item.Copie.Id);
                if((cpTest != null)&&(DaoPlayerSingleton.Instance.CurrentPlayer.Equals(cpTest.Pl_Borrower)))
                {
                    lCopy.Add(cpTest);
                    Loan loan1=DaoLoanSingleton.Instance.ReadByCopy(cpTest);
                    if (loan1 != null)
                        lLoan2.Add(loan1);
                }
            }



            loans = new ObservableCollection<Loan>();

            LoanLV.ItemsSource = loans;
            lLoan2.OrderBy(x => x.Copie.Vg.Name);
            lLoan2.ForEach(x=> loans.Add(x));
             
            
        }

        private void PopulateCopyList()
        {
            listCopy = DaoCopySingleton.Instance.ReadByPlayer(DaoPlayerSingleton.Instance.CurrentPlayer);
            copies = new ObservableCollection<Copy>();

            CopiesLV.ItemsSource = copies;

            listCopy.OrderBy(x => x.Vg.Name);
            listCopy.ForEach(x=> copies.Add(x));

            





        }


        /// <summary>
        /// show selected videogame specs 
        /// </summary>
        private void VideoGameLV_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            OnLoanBT.Visibility = Visibility.Collapsed;
            VideoGame vg = (VideoGame)((ListView)sender).SelectedItem;
            if (vg != null)
            {
                selectedCopyVideoGame = vg;
                SelectedVideoGameNameTB.Text = vg.Name;
                SelectedVideoGameConsoleTB.Text = vg.Console;
                SelectedVideoGameCreditCostTB.Text = vg.CreditCost.ToString();
                int count = 0;

                if (DaoCopySingleton.Instance.ReadByVideoGame(vg).Count() > 0)
                {
                    List<Copy> cpTest = DaoCopySingleton.Instance.ReadByVideoGame(vg).Where(x => x.isAvailable()).ToList();
                    count = cpTest.Count();
                    if (count > 0)
                    {
                        OnLoanBT.Visibility = Visibility.Visible;
                        selectedCopy = cpTest.First();
                    }


                }
                SelectedVideoGameavailableTB.Text = count.ToString();

            }
        }
        /// <summary>
        /// search the videogame(s) by name 
        /// </summary>
        private void VideoGameSB_OnTextChanged(object sender, TextChangedEventArgs  e)
        {
            videoGames.Clear();
           
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


        private void LoanLV_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Loan loan = (Loan)((ListView)sender).SelectedItem;
            
            if (loan != null)
            {
                selectedLoan = loan;

                SelectedLoanGameNameTB.Text = loan.Copie.Vg.Name;
                SelectedLoanDateBeginTB.Text = loan.StartDate.ToString();
                SelectedLoanDateEndTB.Text = loan.EndDate.ToString();
            }
        }

        private void CopiesLV_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            

            Copy copy = (Copy)((ListView)sender).SelectedItem;
            if (copy != null)
            {
                loans.Clear();
                PopulateLoanList();
                if (loanList.Where(x => x.Copie.Id == copy.Id).Count()>0)
                {
                    Loan loan = loanList.Where(x => x.Copie.Id == copy.Id).Last();
                
                SelectedCopyDateBeginTB.Text = loan.StartDate.ToString();
                SelectedCopyDateEndTB.Text = loan.EndDate.ToString();
                SelectedCopyPlayerBorrowedTB.Text = loan.Copie.Pl_Borrower.Pseudo ;
                }
                else
                {
                    SelectedCopyDateBeginTB.Text = "Aucune date";
                    SelectedCopyDateEndTB.Text = "Aucune date";
                    SelectedCopyPlayerBorrowedTB.Text = "Aucun locataire";
                }
            }

        }





        private void ButtonLogin_OnClick(object sender, RoutedEventArgs e)
        {
            LoanList.Visibility = Visibility.Collapsed;
            listGames.Visibility = Visibility.Collapsed;
            GameManager.Visibility = Visibility.Collapsed;
            Admin  ad = DaoAdminSingleton.Instance.ReadByUnique(login_user.Text,login_pwd.Password);

            if(ad != null)
            {
                menu_selection_admin.Visibility = Visibility.Visible;
                GameManager.Visibility = Visibility.Visible;


                menu_selection_player.Visibility = Visibility.Collapsed;
                listGames.Visibility = Visibility.Collapsed;
                LoanList.Visibility = Visibility.Collapsed;
                DaoAdminSingleton.Instance.CurrentAdmin = ad;

                Login_ui.Visibility = Visibility.Collapsed;
                login_pwd.Password = string.Empty;
                return;
            }

            Player pl = DaoPlayerSingleton.Instance.ReadByUnique(login_user.Text, login_pwd.Password);
            if(pl != null)
            {
                menu_selection_admin.Visibility = Visibility.Collapsed;
                GameManager.Visibility = Visibility.Collapsed;

                menu_selection_player.Visibility = Visibility.Visible;
                menu_selection_player.SelectedIndex = 0;

                DaoPlayerSingleton.Instance.CurrentPlayer = pl;
                if(DaoPlayerSingleton.Instance.CurrentPlayer.bonusAnniversary())
                {
                    DaoPlayerSingleton.Instance.update(DaoPlayerSingleton.Instance.CurrentPlayer);
                    MessageBox.Show("c'était votre anniversaire, vous avez reçu 2 Crédits supplémentaire");
                }
                    

                Login_ui.Visibility = Visibility.Collapsed;
                login_pwd.Password = string.Empty;

                return;
            }

            login_error_text.Visibility = Visibility.Visible;
            login_pwd.Password = string.Empty;
            MessageBox.Show("Pseudo/Mot de passe incorrect");
            Login_ui.Visibility = Visibility.Visible;
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
                case 0://menu player
                    HideAll();
                    break;
                case 1://games list
                    HideAll();
                    listGames.Visibility = Visibility.Visible;
                    break;
                case 2://location list
                    HideAll();
                    PopulateLoanList();
                    LoanList.Visibility = Visibility.Visible;   
                    break;
                
                case 3://player's copies list
                    HideAll();
                    PopulateCopyList();
                    PopulateLoanList();
                    CopiesList.Visibility = Visibility.Visible;
                    break;


                case 4://logout
                    if(MessageBox.Show("Voulez-vous vous déconnecter ?", "", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        HideAll();
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
                case 0:
                    GameManager.Visibility = Visibility.Visible;
                    LoanList.Visibility = Visibility.Collapsed;
                    break;
                case 1://logout
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


        private void VideoGameAdminLV_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            VideoGame vg = (VideoGame)((ListView)sender).SelectedItem;
            selectedAdminVideoGame = vg;
            if (vg != null)
            {
                SelectedVideoGameAdminNameTB.Text = vg.Name;
                SelectedVideoGameAdminConsoleTB.Text = vg.Console;
                SelectedVideoGameAdminCreditCostTB.Text = vg.CreditCost.ToString();
                DeleteBtn.Visibility = Visibility.Visible;
            }
        }
        /// <summary>
        /// delete the videogame selected
        /// </summary>
        private void ButtonBaseDelete_OnClick(object sender, RoutedEventArgs e)
        {
            DaoVideoGameSingleton.Instance.Delete(selectedAdminVideoGame);
            PopulateVideoGameList();
        }

        /// <summary>
        /// save a videogame selected or create a new one
        /// </summary>
        private void ButtonBaseSave_OnClick(object sender, RoutedEventArgs e)
        {
            string name = SelectedVideoGameAdminNameTB.Text;
            string console = SelectedVideoGameAdminConsoleTB.Text;
            int cost = 0;
            if (int.TryParse(SelectedVideoGameAdminCreditCostTB.Text, NumberStyles.Any, CultureInfo.InvariantCulture,
                    out var res))
            {
                cost = res;
            }
            else
            {
                MessageBox.Show("Erreur lors de la lecture du cout, remplacement par 0");
            }

            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Le nom ne peux pas être vide");
                return;
            }

            if (string.IsNullOrEmpty(console))
            {
                MessageBox.Show("Le nom ne peux pas être vide");
                return;
            }

            VideoGame vg = new VideoGame(name, console);
            vg.CreditCost = cost;
            vg.Id = selectedAdminVideoGame.Id;

            if (DeleteBtn.Visibility == Visibility.Visible)
            {
                DaoVideoGameSingleton.Instance.Update(vg);
            }
            else
            {
                DaoVideoGameSingleton.Instance.Insert(vg);
            }
            PopulateVideoGameList();
        }

        /// <summary>
        /// propose a Videogame's copy 
        /// </summary>
        private void ButtonBaseOnLoan_OnClick(object sender, RoutedEventArgs e)
        {
            if(selectedCopyVideoGame!=null)
            {
                Copy cp = new Copy(selectedCopyVideoGame, DaoPlayerSingleton.Instance.CurrentPlayer);
                if(DaoCopySingleton.Instance.Insert(cp))
                    MessageBox.Show("copie en location.");
                else
                    MessageBox.Show("problème lors de l'insertion, veuillez réessayer .");
            }
            else 
                MessageBox.Show("Choisissez d'abord un jeu avant de le proposer en location");

            PopulateCopyList();


        }
        /// <summary>
        /// loan a videogame selected or reserve one 
        /// </summary>
        private void ButtonBaseLoan_OnClick(object sender, RoutedEventArgs e)
        {
            if (selectedCopyVideoGame != null)
            {
                Copy cp = selectedCopy;
                int nbWeeks = -1;
                while(nbWeeks<0)
                {
                    string input = Microsoft.VisualBasic.Interaction.InputBox("veuillez indiquez le nombre de semaine désiré", "Selection de location", "1", -1, -1);
                    if(!int.TryParse(input, out nbWeeks))
                    {
                        MessageBox.Show("Veuillez introduire que des nombres entre 1 et 10 !");
                    }
                    else if((nbWeeks < 0)||(nbWeeks>10))
                    {
                        MessageBox.Show("Veuillez introduire le nombre de semaine entre 1 et 10 !");
                        nbWeeks = -1;
                    }
                    
                    
                }
                if (nbWeeks == 0)
                    return;
                else if (!(selectedCopyVideoGame.CreditCost * nbWeeks <= DaoPlayerSingleton.Instance.CurrentPlayer.Credit))
                {
                    MessageBox.Show("pas assez de crédit ! il vous reste : " + DaoPlayerSingleton.Instance.CurrentPlayer.Credit.ToString() + " et il en faut : " + (selectedCopyVideoGame.CreditCost * nbWeeks).ToString());
                    return;
                }
                cp.Pl_Borrower = DaoPlayerSingleton.Instance.CurrentPlayer;


                if (DaoLoanSingleton.Instance.Insert(new Loan(cp, DateTime.Now.AddDays(1), DateTime.Now.AddDays((7 * nbWeeks) + 1))) && (DaoCopySingleton.Instance.Update(cp)))
                {
                    
                    DaoPlayerSingleton.Instance.CurrentPlayer.Credit = (DaoPlayerSingleton.Instance.CurrentPlayer.Credit - (selectedCopyVideoGame.CreditCost * nbWeeks));
                    Player owner = DaoPlayerSingleton.Instance.ReadByID(cp.Pl_owner.Id_User);
                    owner.Credit += selectedCopyVideoGame.CreditCost * nbWeeks;
                    if ((DaoPlayerSingleton.Instance.update(DaoPlayerSingleton.Instance.CurrentPlayer))&&(DaoPlayerSingleton.Instance.update(owner)))
                        MessageBox.Show("copie reservé.");
                    else
                        MessageBox.Show("problème lors de l'insertion, veuillez réessayer .");
                }
                else
                    MessageBox.Show("problème lors de l'insertion, veuillez réessayer .");
            }
            else
                MessageBox.Show("Choisissez d'abord un jeu avant de vouloir le reserver ");
            PopulateCopyList();
            PopulateVideoGameList();
        }

        /// <summary>
        /// button that release the loan and paid in credit the owner
        /// </summary>
        private void ButtonBaseBringBack_OnClick(object sender, RoutedEventArgs e)
        {
            DateTime dtnow = DateTime.Now;
           
            if(selectedLoan==null)
            {
                MessageBox.Show("Veuilez d'abord selectionner une location");
                return;
            }
            else
            {
                Copy cp = DaoCopySingleton.Instance.ReadByID(selectedLoan.Copie.Id);
                Player playerOwner = DaoPlayerSingleton.Instance.ReadByID(cp.Pl_owner.Id_User);

                if (dtnow.Subtract(selectedLoan.EndDate).Days <= 0)
                {
                    cp.ReleaseCopy();
                    if(DaoCopySingleton.Instance.Update(cp)&&(DaoLoanSingleton.Instance.Delete(selectedLoan))) 
                    {

                        MessageBox.Show("Jeu vidéo correctement rendu");
                    }
                    else
                        MessageBox.Show("Erreur, veuillez réessayer");
                }
                else
                {
                    int Days = dtnow.Subtract(selectedLoan.EndDate).Days;
                    int amendeTot = Days * amende;

                    int weeksAmende = (Days / 7)+1;
                    amendeTot +=(DaoVideoGameSingleton.Instance.ReadByID(cp.Vg.Id).CreditCost)* weeksAmende;
                    DaoPlayerSingleton.Instance.CurrentPlayer.Credit -= amendeTot;
                    DaoPlayerSingleton.Instance.update(DaoPlayerSingleton.Instance.CurrentPlayer);
                    Player owner = DaoPlayerSingleton.Instance.ReadByID(cp.Pl_owner.Id_User);
                    owner.Credit += amendeTot;
                    DaoPlayerSingleton.Instance.update(owner);
                    cp.ReleaseCopy();
                    if (DaoCopySingleton.Instance.Update(cp) && (DaoLoanSingleton.Instance.Delete(selectedLoan)))
                        MessageBox.Show("Jeu vidéo correctement rendu avec retard ! avec ces crédit en moins : " +amendeTot.ToString());
                    else
                        MessageBox.Show("Erreur, veuillez réessayer");


                }
                PopulateCopyList();
                PopulateLoanList();
                PopulateVideoGameList();
            }
            

        }

        private void TextBoxBase_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            FilterVideoGameAdmin(tb.Text);
        }

        private void FilterVideoGameAdmin(string filter)
        {
            //if (string.IsNullOrEmpty(filter)) return;

            videoGames.Clear();
            lVG.Where(x => x.Name.ToLower().Contains(filter.ToLower())).ToList().ForEach(x =>
            {
                videoGames.Add(x);
            });
        }

        private void ButtonBase_OnAdd_OnClick(object sender, RoutedEventArgs e)
        {
            DeleteBtn.Visibility = Visibility.Hidden;
            SelectedVideoGameAdminConsoleTB.Text = string.Empty;
            SelectedVideoGameAdminNameTB.Text = string.Empty;
            SelectedVideoGameAdminCreditCostTB.Text = string.Empty;
        }


        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            App.Current.Shutdown(0);
        }

        private void HideAll()
        {
            LoanList.Visibility = Visibility.Collapsed;
            listGames.Visibility = Visibility.Collapsed;
            GameManager.Visibility = Visibility.Collapsed;
            Login_ui.Visibility = Visibility.Collapsed;
            login_error_text.Visibility = Visibility.Collapsed;
            CopiesList.Visibility = Visibility.Collapsed;
            listGames.Visibility= Visibility.Collapsed;
        }

        
    }
}
