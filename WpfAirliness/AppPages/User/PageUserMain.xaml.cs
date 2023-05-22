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
using WPFAirelines.Models;
using WpfAirliness.Helper;

namespace WpfAirliness.AppPages.User
{
    /// <summary>
    /// Логика взаимодействия для PageUserMain.xaml
    /// </summary>
    /// 
    public partial class PageUserMain : Page
    {
        List<Route> _basket = new();
        public PageUserMain()
        {
            
            InitializeComponent();
            LoadDataRoutes();
            
            TxbBasketCount.Text = "0";
            
        }

        private async void LoadDataRoutes()
        {
            try
            {
                string url = "https://private-public.site/Rodnoy/airlines-api/routes";
                HttpClient client = new HttpClient();

                var response = await client.GetAsync(url);
                var responseContent = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var routes = JsonConvert.DeserializeObject<List<Route>>(responseContent);
                    GridRoutes.ItemsSource = routes;
                }
                else
                {
                    MessageBox.Show(responseContent);
                }
            }
            catch
            {
                MessageBox.Show("Ошибка в обработке данных");
                throw;
            }
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            FrmNav.Navigation.Navigate(new Authentication());
        }

        public void BtnBascket_Click(object sender, RoutedEventArgs e)
        {
            FrmNav.Navigation.Navigate(new PageUserBasket());
        }

        private void BtnBuyTicket_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedTicket = (Route)GridRoutes.SelectedItem;

                _basket.Add(selectedTicket);

                BasketClass.basketListTikets = _basket.ToList();
                TxbBasketCount.Text = _basket.Count().ToString();
            }
            catch 
            {
                MessageBox.Show("Ошибка в обработке данных");
            }
        }

        private void BtnUserAccount_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Данный функционал не реализован");
        }
    }
}
