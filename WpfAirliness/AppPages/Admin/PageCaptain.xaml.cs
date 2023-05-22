using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
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
using WPFAirelines.AppWindow;
using WPFAirelines.Models;
using WpfAirliness.AppWindow;
using WpfAirliness.Helper;
using WpfAirliness.Models;

namespace WpfAirliness.AppPages.Admin
{
    /// <summary>
    /// Логика взаимодействия для PageCaptain.xaml
    /// </summary>
    public partial class PageCaptain : Page
    {
        List<Captain> captainsList;
        private string shortFio = "";

        public PageCaptain(string _shortFio)
        {
            InitializeComponent();
            GetAllCaptains();
            shortFio = _shortFio;
            LblShortFio.Content = shortFio;
        }


        private void TxbSearch_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            TxbSearch.Clear();
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string filterText = TxbSearch.Text;

                if (filterText == "")
                {
                    GridCaptainList.ItemsSource = captainsList;
                }
                else
                {
                   var filterList = captainsList.Where(x =>
                   x.Firstname == filterText ||
                   x.Lastname == filterText ||
                   x.Patronymic == filterText ||
                   x.Patronymic == filterText ||
                   x.PasportNumber == filterText ||
                   x.Address == filterText ||
                   x.PersonalNumber == filterText ||
                   x.PilotExperience == filterText).ToList();

                    if (filterList != null)
                    {
                        GridCaptainList.ItemsSource = filterList;
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
            FrmNav.Navigation.Navigate(new PageAdminMain(shortFio));
        }

        public async void GetAllCaptains()
        {
            try
            {
                string url = "https://private-public.site/Rodnoy/airlines-api/captains";
                HttpClient client = new HttpClient();

                var response = await client.GetAsync(url);
                var responseContent = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var captains = JsonConvert.DeserializeObject<List<Captain>>(responseContent);
                    captainsList = captains;
                    GridCaptainList.ItemsSource = captainsList;

                    TxbCaptinCount.Text = captainsList.Count().ToString();
                }
                else
                {
                    MessageBox.Show(responseContent);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка в обработке данных");
                throw;
            }
        }

        private void BtnAddCaptain_Click(object sender, RoutedEventArgs e)
        {
            new AddCaptain().Show();
        }

        private async void BtnDeleteCaptain_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("Вы действительно хотите удалить?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    var row = (Captain)GridCaptainList.SelectedItem;
                    var userId  = row?.Id;
                    Trace.WriteLine(userId);

                    string url = $"https://private-public.site/Rodnoy/airlines-api/delete-user/{userId}";
                    HttpClient client = new HttpClient();

                    var requestContent = new StringContent(string.Empty, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync(url, requestContent);
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var msg = JsonConvert.DeserializeObject<RequestMessage>(responseContent);

                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        MessageBox.Show(msg.Description);
                        GetAllCaptains();
                    }
                    else
                    {
                        MessageBox.Show(msg.Description);
                    }
                }
                else
                {
                    MessageBox.Show("Удаление отклоненено", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Stop);
                }
            }
            catch
            {
                MessageBox.Show("Ошибка в обработке данных");
            }
        }

        private void BtnUpdateCaptain_Click(object sender, RoutedEventArgs e)
        {
            var captain = (Captain)GridCaptainList.SelectedItem;
            CaptainClass.Id = captain.Id;
            CaptainClass.Role = captain.Role;
            CaptainClass.Firstname = captain.Firstname;
            CaptainClass.Lastname = captain.Lastname;
            CaptainClass.Patronymic = captain.Patronymic;
            CaptainClass.PhoneNumber = captain.PhoneNumber;
            CaptainClass.PasportNumber = captain.PasportNumber;
            CaptainClass.Address = captain.Address;
            CaptainClass.PersonalNumber = captain.PersonalNumber;
            CaptainClass.PilotExperience = captain.PilotExperience;

            UpdateCaptain updateCaptain = new UpdateCaptain();
            updateCaptain.Show();

        }
    }
}
