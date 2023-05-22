using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace WPFAirelines.AppPages.Admin
{
    /// <summary>
    /// Логика взаимодействия для PageRoute.xaml
    /// </summary>
    public partial class PageRoute : Page
    {
        List<Route> routesList;
        private string _shortFio = "";
        public PageRoute(string _shortFio)
        {
            InitializeComponent();
            LblShortFio.Content = _shortFio;
            GetAllRoutes();
        }
        private async void GetAllRoutes()
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
                    routesList = routes;
                    GridRouteList.ItemsSource = routesList;
                    decimal max = Convert.ToDecimal(routesList.Max(p => Convert.ToDecimal(p.Price)));
                    LblHightPriceRoute.Content = $"{max} руб.";
                }
                else
                {
                    MessageBox.Show("Ошибка в обработке данных");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string filterText = TxbSearch.Text;

                if (filterText == "")
                {
                    GridRouteList.ItemsSource = routesList;
                }
                else
                {
                   var filterList = routesList.Where(x =>
                   x.BortNumber == filterText ||
                   x.DepartureAirport == filterText ||
                   x.ArrivalAirport == filterText ||
                   x.DateTimeDeparture == filterText ||
                   x.BortNumber == filterText ||
                   x.TimeTripMinute == filterText).ToList();

                    if (filterList != null)
                    {
                        GridRouteList.ItemsSource = filterList;
                    }
                    else
                    {
                        MessageBox.Show("Поиск не дал результатов");
                    }
                    MessageBox.Show("Поиск выполнен успешно");
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка в обработке данных");
            }
        }

        private void TxbSearch_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Text = string.Empty;
            tb.GotFocus -= TxbSearch_GotFocus;
        }

        private void TxbSearch_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            TxbSearch.Clear();
        }

    }
}
