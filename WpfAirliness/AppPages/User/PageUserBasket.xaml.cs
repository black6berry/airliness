using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
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
using WpfAirliness.Models;

namespace WpfAirliness.AppPages.User
{
    /// <summary>
    /// Логика взаимодействия для PageUserBasket.xaml
    /// </summary>
    public partial class PageUserBasket : Page
    {
        public PageUserBasket()
        {
            InitializeComponent();
            UserTicketsList.ItemsSource = BasketClass.basketListTikets;

            TxbAmmount.Text = $"{BasketClass.basketListTikets.Sum(x => Convert.ToDecimal(x.Price))} руб.";
            //string a = "3,5";
            //string b = "1,5";
            //decimal d = Convert.ToDecimal(a);
            //decimal e = Convert.ToDecimal(b);
            //TxbAmmount.Text = $"{ d + e  }";
        }

        private void BtnBackUserMain_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                FrmNav.Navigation.GoBack();
            }
            catch
            {
                MessageBox.Show("Ошибка в навигации страниц. Закройте приложение и повторите попытку");
            }
        }

        private void BtnDeleteTicket_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var itemToRemove = (Route)UserTicketsList.SelectedItem;
                BasketClass.basketListTikets.Remove(itemToRemove);
                UserTicketsList.ItemsSource = BasketClass.basketListTikets;
                TxbAmmount.Text = $"{BasketClass.basketListTikets.Sum(x => Convert.ToDecimal(x.Price))} руб.";
            }
            catch
            {
                MessageBox.Show("Ошибка в обработке данных");
            }
        }

        private async void BtnBuyTicket_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string url = "https://private-public.site/Rodnoy/airlines-api/add-ticket";
                HttpClient client = new HttpClient();

                foreach(Route route in BasketClass.basketListTikets)
                {
                    var seatNumber = BasketClass.RandomSeatNumber<CharSeat>();
                    var request = new AddTicket()
                    {
                        Firstname = DataUserClass.Firstname,
                        Lastname = DataUserClass.Lastname,
                        Patronymic = DataUserClass.Patronymic,
                        PasportNumber = DataUserClass.PasportNumber,
                        DepartureAirport = route.DepartureAirport,
                        ArrivalAirport = route.ArrivalAirport,
                        TimeTripMinute = route.TimeTripMinute,
                        DateTimeDeparture = route.DateTimeDeparture,
                        Price = route.Price.ToString(),
                        BortNumber = route.BortNumber,
                        SeatNumber = seatNumber
                    };

                    var jsonContent = JsonConvert.SerializeObject(request);
                    var requestContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                    var responce = await client.PostAsync(url, requestContent);
                    var responceContent = await responce.Content.ReadAsStringAsync();
                    var msg = JsonConvert.DeserializeObject<RequestMessage>(responceContent);

                    if (responce.StatusCode == System.Net.HttpStatusCode.Created)
                    {
                        MessageBox.Show($"{msg.Description}\nНомер билета - {seatNumber}");
                    }
                    else
                    {
                        MessageBox.Show(msg.Description);
                    }
                }
               
            }
            catch{
                MessageBox.Show("Ошибка в обработке данных");
            }
        }

       
    }
}
