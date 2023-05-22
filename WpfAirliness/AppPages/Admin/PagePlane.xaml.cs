using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
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
using WpfAirliness.AppPages.Admin;
using WpfAirliness.AppWindow;

namespace WPFAirelines.AppPages.Admin
{
    /// <summary>
    /// Логика взаимодействия для PagePlane.xaml
    /// </summary>
    public partial class PagePlane : Page
    {
        List<Plane> planesList;
        private string _shortFio = "";

        public PagePlane(string _shortFio)
        {
            InitializeComponent();
            LblShortFio.Content = _shortFio;
            GetAllPlanes();
        }

        private async void GetAllPlanes()
        {
            try
            {
                string url = "https://private-public.site/Rodnoy/airlines-api/planes";
                HttpClient client = new HttpClient();

                var response = await client.GetAsync(url);
                var responseContent = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var planes = JsonConvert.DeserializeObject<List<Plane>>(responseContent);
                    planesList = planes;
                    GridPlanesList.ItemsSource = planesList;

                    LblPalneCount.Content =  $"Количество самолетов - {planesList.Count()} шт.";
                    LblPlaneNotDeparture.Content = $"Количество самолетов не готовых к вылету - {planesList.Where(p => p.ReadyFly == "0").Count()} шт.";
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

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string filterText = TxbSearch.Text;

                if (filterText == "")
                {
                    GridPlanesList.ItemsSource = planesList;
                }
                else
                {
                    var filterList = planesList.Where(x =>
                   x.BortNumber == filterText ||
                   x.Model == filterText ||
                   x.CreationDate == filterText ||
                   x.YearsOfUse == filterText ||
                   x.ReadyFly == filterText ||
                   x.MaxPassenger == filterText).ToList();

                    if (filterList != null)
                    {
                        GridPlanesList.ItemsSource = filterList;
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

        private void BtnAddPlane_Click(object sender, RoutedEventArgs e)
        {
            new AddPlane().Show();
        }

        //private void BtnDeletePlane_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        MessageBoxResult messageBoxResult = MessageBox.Show("Вы действительно хотите удалить данные?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question);
        //        if (messageBoxResult == MessageBoxResult.Yes)
        //        {
        //           //var url = 
        //        }
        //        else
        //        {
        //            return;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message.ToString(), "Ошибка в обработке данных");
        //    }
        //}
    }
}
