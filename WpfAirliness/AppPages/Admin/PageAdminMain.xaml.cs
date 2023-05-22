using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
using WPFAirelines.Action;
using WPFAirelines.AppPages.Admin;
using WPFAirelines.Models;

namespace WpfAirliness.AppPages.Admin
{
    /// <summary>
    /// Логика взаимодействия для PageAdminMain.xaml
    /// </summary>

    public partial class PageAdminMain : Page
    {
        private string shortFio = "";
        List<Captain> captainsList;

        public PageAdminMain(string _shortFio)
        {
            InitializeComponent();
            shortFio = _shortFio;
            FrmNav.Navigation = FrmContentPage;
            FrmNav.Navigation.Navigate(new PageRoute(shortFio));
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            FrmNav.Navigation.Navigate(new Authentication());
        }

        private void TxbSearch_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Text = string.Empty;
            tb.GotFocus -= TxbSearch_GotFocus;
        }

        private void BtnRoute_Click(object sender, RoutedEventArgs e)
        {
            FrmNav.Navigation.Navigate(new PageRoute(shortFio));
        }

        private void BtnSoldTickets_Click(object sender, RoutedEventArgs e)
        {
            FrmNav.Navigation.Navigate(new PageSoldTickets(shortFio));
        }

        private void BtnAirplane_Click(object sender, RoutedEventArgs e)
        {
            FrmNav.Navigation.Navigate(new PagePlane(shortFio));
        }

        private void BtnCaptain_Click(object sender, RoutedEventArgs e)
        {
            FrmNav.Navigation.Navigate(new PageCaptain(shortFio));
        }

    }

}
