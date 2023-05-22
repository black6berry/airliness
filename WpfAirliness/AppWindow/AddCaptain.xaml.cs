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
using System.Windows.Shapes;
using WpfAirliness.Helper;
using WpfAirliness.Models;

namespace WPFAirelines.AppWindow
{
    /// <summary>
    /// Логика взаимодействия для AddCaptain.xaml
    /// </summary>
    public partial class AddCaptain : Window
    {
        
        public AddCaptain()
        {
            InitializeComponent();
            CmbRole.ItemsSource = new List<string>() { "Админ", "Пользователь", "Пилот" };
            InputPilot.Visibility = Visibility.Hidden;

        }

        private void CmbRole_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            InputPilot.Visibility = CmbRole.SelectedValue != null && CmbRole.SelectedItem.ToString() == "Пилот" ? Visibility.Visible : Visibility.Hidden;
        }

        private async void BtnAddCaptain_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var result = CheckingData.Check(CmbRole, TxbFirstame, TxbLastname, TxbLastname, TxbPasportNumber, TxbAddress, TxbPhoneNumber);
                if (result != false)
                {
                    string url = "https://private-public.site/Rodnoy/airlines-api/create-user";
                    HttpClient client = new HttpClient();

                    var request = new AddUser()
                    {
                        Firstname = TxbFirstame.Text,
                        Lastname = TxbLastname.Text,
                        Patronymic = TxbPatronymic.Text,
                        PasportNumber = TxbPasportNumber.Text,
                        Address = TxbAddress.Text,
                        PhoneNumber = TxbPhoneNumber.Text,
                        Role = CmbRole.SelectedItem.ToString(),
                        PilotExpirience = TxbPilotExpirience.Text
                    };

                    var jsonContent = JsonConvert.SerializeObject(request);
                    var requestContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                    var responce = await client.PostAsync(url, requestContent);
                    var responceContent = await responce.Content.ReadAsStringAsync();
                    var msg = JsonConvert.DeserializeObject<RequestMessage>(responceContent);

                    if (responce.StatusCode == System.Net.HttpStatusCode.Created)
                    {
                        MessageBox.Show($"Id - {msg.Id}\n{msg.Description}");
                    }
                    else
                    {
                        MessageBox.Show(msg.Description);
                    }

                    ClearFields();

                }
                else
                {
                    MessageBox.Show("Заполнены не все поля");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка в обработке данных");
                Console.WriteLine(ex);
            }
        }

        public void ClearFields()
        {
            TxbFirstame.Clear();
            TxbLastname.Clear();
            TxbPatronymic.Clear();
            TxbPasportNumber.Clear();
            TxbAddress.Clear();
            TxbPhoneNumber.Clear();
            CmbRole.SelectedItem = -1;
            TxbPilotExpirience.Clear();
        }

        private void TxbPhoneNumber_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Text != "+7(XXX)-XXX-XX-XX")
            {
                tb.GotFocus -= TxbPhoneNumber_GotFocus;
            }
            else
            {
                tb.Text = string.Empty;
            }

        }

        private void TxbPhoneNumber_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Text != string.Empty && tb.Text != "")
            {
                tb.LostFocus -= TxbPhoneNumber_LostFocus;
            }
            else
            {
                tb.Text = "+7(XXX)-XXX-XX-XX";
            }

        }
    }
}
