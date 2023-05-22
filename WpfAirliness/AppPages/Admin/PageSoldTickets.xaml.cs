using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    /// Логика взаимодействия для PageSoldTickets.xaml
    /// </summary>
    public partial class PageSoldTickets : Page
    {
        List<Tickets> soldTicketsList;
        
        public PageSoldTickets(string _shortFio)
        {
            InitializeComponent();
            LblShortFio.Content = _shortFio;
            GetSoldTickets();

        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string filterText = TxbSearch.Text;

                if (filterText == "" )
                {
                    GridSoldTicketsList.ItemsSource = soldTicketsList;
                }
                else
                {
                    var filterList = soldTicketsList.Where(x =>
                   x.Firstname == filterText ||
                   x.Lastname == filterText ||
                   x.Patronymic == filterText ||
                   x.PasportNumber == filterText ||
                   x.ArrivalAirport == filterText ||
                   x.DepartureAirport == filterText ||
                   x.BortNumber == filterText ||
                   x.PlaneModel == filterText ||
                   x.SeatNumber == filterText ||
                   x.DateTimeDeparture == filterText).ToList();

                    if (filterList != null)
                    {
                        GridSoldTicketsList.ItemsSource = filterList;
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

        private async void GetSoldTickets()
        {
            try
            {
                string url = "https://private-public.site/Rodnoy/airlines-api/ticketsSold";
                HttpClient client = new HttpClient();

                var response = await client.GetAsync(url);
                var responseContent = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var soldTickets = JsonConvert.DeserializeObject<List<Tickets>>(responseContent);
                    soldTicketsList = soldTickets;
                    
                    GridSoldTicketsList.ItemsSource = soldTicketsList;


                    LblSoldTicketsCount.Content = $"Количество проданных билетов: {soldTicketsList.Where(x => x.SeatNumber != "000").Count()}";
                    LblRevenue.Content = $"Суммарный доход {soldTicketsList.Where(x => x.SeatNumber != "000").Sum(p => p.PriceTicket)} руб.";
                }
                else
                {
                    MessageBox.Show("Ошибка в обработке данных");
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
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
