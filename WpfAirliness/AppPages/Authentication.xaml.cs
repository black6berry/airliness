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
using WpfAirliness.AppPages.Admin;
using WpfAirliness.AppPages.User;
using WpfAirliness.Helper;

namespace WpfAirliness.AppPages
{
    /// <summary>
    /// Логика взаимодействия для Authentication.xaml
    /// </summary>
    public partial class Authentication : Page
    {
        public Authentication()
        {
            InitializeComponent();
        }

        private async void BtnSignIn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string url = $"https://private-public.site/Rodnoy/airlines-api/sign-in?Login={TxbLogin.Text}&Password={PsbPassword.Password}";
                HttpClient client = new HttpClient();

                var response = await client.GetAsync(url);
                var responseContent = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var _signIn = JsonConvert.DeserializeObject<SignIn>(responseContent);

                    DataUserClass.Firstname = _signIn.Firstname;
                    DataUserClass.Lastname = _signIn.Lastname;
                    DataUserClass.Patronymic = _signIn.Patronymic;
                    DataUserClass.PasportNumber = _signIn.PasportNumber;

                    string shortFio = $"{_signIn.Lastname}.{_signIn.Firstname[0]}.{_signIn.Patronymic[0]}";

                    switch (_signIn.RoleUser)
                    {
                        case "Админ":
                            MessageBox.Show($"Добро пожаловать {_signIn.Firstname} {_signIn.Patronymic}");
                            FrmNav.Navigation.Navigate(new PageAdminMain(shortFio));
                            break;
                        case "Пользователь":
                            MessageBox.Show("Авторизация выполнена успешно");
                            FrmNav.Navigation.Navigate(new PageUserMain());
                            break;
                        default:
                            MessageBox.Show("Данные введены некорректно");
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }
    }
}
