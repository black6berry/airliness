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
using System.Windows.Shapes;
using WPFAirelines.Models;
using WpfAirliness.Helper;
using WpfAirliness.Models;

namespace WpfAirliness.AppWindow
{
    /// <summary>
    /// Логика взаимодействия для AddPlane.xaml
    /// </summary>
    public partial class AddPlane : Window
    {
        public List<string> _readFlyItems = new List<string>() {"Да", "Нет"};
        public bool readFly { get; set; }
        public AddPlane()
        {
            InitializeComponent();

            CmbReadFly.ItemsSource = _readFlyItems;

            if (CmbReadFly.Text == "Да")
            {
                readFly = true;
            }
            else
            {
                readFly = false;   
            }
        }

        private async void BtnAddPlane_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var result = CheckingData.Check(CmbReadFly, ClndrCreationDate, TxbBortNumber, TxbModel, TxbMaxPassenger, TxbYearsOfUse);
                
                if (result != false ) 
                {
                    int year = ClndrCreationDate.SelectedDate.Value.Year;
                    int month = ClndrCreationDate.SelectedDate.Value.Month;
                    int day = ClndrCreationDate.SelectedDate.Value.Day;

                    var formatingData = $"{year}-{month}-{day}";

                    string url = "https://private-public.site/Rodnoy/airlines-api/create-plane";
                    HttpClient client = new HttpClient();

                    var request = new Plane()
                    {
                        BortNumber = TxbBortNumber.Text,
                        Model = TxbBortNumber.Text,
                        CreationDate = formatingData,
                        YearsOfUse = TxbYearsOfUse.Text,
                        ReadyFly = readFly.ToString(),
                        MaxPassenger = TxbMaxPassenger.Text,
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
            catch(Exception ex) 
            {
                MessageBox.Show("Ошибка в обработке данных");
                Trace.WriteLine(ex);
            }
        }

        private void ClearFields()
        {
            TxbBortNumber.Clear();
            TxbMaxPassenger.Clear();
            TxbModel.Clear();
            TxbYearsOfUse.Clear();
            CmbReadFly.SelectedItem = -1;
            ClndrCreationDate.SelectedDate = null;
        }
    }
}
