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
    /// Logique d'interaction pour ModifyVideoGame.xaml
    /// </summary>
    public partial class ModifyVideoGame : Window
    {
        private DaoVideoGame daoVG = new DaoVideoGame();
        private String name, console;
        private VideoGame vg;
        int id, credit;
        public ModifyVideoGame()
        {
            InitializeComponent();
        }

        private void OnClick(object sender, RoutedEventArgs e)
        {
            if (ad_id.Text != "")
                id = Convert.ToInt32(ad_id.Text.Trim());

            vg = daoVG.ReadByID(id);



            if (vg != null)
            {
                if ((ad_name.Text != "") && (daoVG.ReadName(ad_name.Text) == null))
                {
                    name = ad_name.Text;
                    vg.Name = name;
                }
                else
                {
                    name = vg.Name;

                }
                if (ad_console.Text != "")
                {
                    console = ad_console.Text;
                    vg.Console = console;
                }
                else
                {
                    console = vg.Console;

                }

                if (ad_credit.Text != "")
                {
                    credit = Convert.ToInt32(ad_credit.Text.Trim());
                    vg.CreditCost = credit;

                }
                else
                {
                    credit = vg.CreditCost;

                }

                Trace.WriteLine(vg.Id.ToString());
                daoVG.Update(vg);
            }

            MainWindow mainWindow = new MainWindow();
            this.Visibility = Visibility.Hidden;
            mainWindow.Show();
        }
    }
}
