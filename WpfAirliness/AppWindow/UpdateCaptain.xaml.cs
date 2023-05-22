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
    /// Логика взаимодействия для UpdateCaptain.xaml
    /// </summary>
    /// 
        
    public partial class UpdateCaptain : Window
    {
        public UpdateCaptain()
        {
            InitializeComponent();

            TxbFirstameEdit.Text = CaptainClass.Firstname;
            TxbLastnameEdit.Text = CaptainClass.Lastname;
            TxbPatronymicEdit.Text = CaptainClass.Patronymic;
            TxbPhoneNumberEdit.Text = CaptainClass.PhoneNumber;
            TxbPasportNumberEdit.Text = CaptainClass.PasportNumber;
            TxbAddressEdit.Text = CaptainClass.Address;
            TxbPilotExperienceEdit.Text = CaptainClass.PilotExperience;
        }

        private async void BtnUpdateCaptain_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("Вы действительно хотите изменить данную запись?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    var result = CheckingData.Check(TxbFirstameEdit, TxbLastnameEdit, TxbPatronymicEdit, TxbPasportNumberEdit, TxbPhoneNumberEdit, TxbPilotExperienceEdit);
                    if (result != false)
                    {
                        var url = "https://private-public.site/Rodnoy/airlines-api/update-user";
                        HttpClient client = new HttpClient();

                        var captainData = new Captain()
                        {
                            Id = CaptainClass.Id,
                            Role = CaptainClass.Role,
                            Firstname = TxbFirstameEdit.Text,
                            Lastname = TxbLastnameEdit.Text,
                            Patronymic = TxbPatronymicEdit.Text,
                            PhoneNumber = TxbPhoneNumberEdit.Text,
                            PasportNumber = TxbPasportNumberEdit.Text,
                            Address = TxbAddressEdit.Text,
                            PersonalNumber = CaptainClass.PersonalNumber,
                            PilotExperience = TxbPilotExperienceEdit.Text
                        };
                        var jsonContent = JsonConvert.SerializeObject(captainData);
                        var requestContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                        var response = await client.PostAsync(url, requestContent);
                        var responseContent = await response.Content.ReadAsStringAsync();
                        var msg = JsonConvert.DeserializeObject<RequestMessage>(responseContent);

                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            MessageBox.Show(msg.Description);
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show(msg.Description);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Заполнены не все поля", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.Cancel);
                    }
                }
                else
                {
                    MessageBox.Show("Удаление отклоненено", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Stop);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка в обработке данных");
                Trace.WriteLine(ex.ToString());
            }

        }

    }
}
