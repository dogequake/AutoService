using System;
using System.Collections.Generic;
using System.Linq;
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
using SumbitFor16.Models;
using System.Text.RegularExpressions;

namespace SumbitFor16.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для RegPage.xaml
    /// </summary>
    public partial class RegPage : Page
    {

        Core db = new Core();
        public RegPage()
        {
            InitializeComponent();
        }
        private void RegButtonClick(object sender, RoutedEventArgs e)
        {
            Regex Email = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Regex PhoneNumber = new Regex(@"^((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}$");
            int i = 0;

            //Проверка Email
            if (!Regex.IsMatch(TBoxEmail.Text, Email.ToString(), RegexOptions.IgnoreCase))
            {
                MessageBox.Show("Указан неверный email");
                i++;
            }

            //Проверка пароля
            if (TBoxPassword.Text != TBoxPasswordAgain.Text)
            {
                MessageBox.Show("Пароли не совпадают!");
                i++;
            }

            //проверка телефона
            if (!Regex.IsMatch(TBoxPhone.Text, PhoneNumber.ToString(), RegexOptions.IgnoreCase))
            {
                MessageBox.Show("Указан неверный телефон");
                i++;
            }


            if (i == 0)
            {
                if (CBoxGenderCode.Text == "Мужской")
                {
                    CBoxGenderCode.Text = "1";
                }
                else
                {
                    CBoxGenderCode.Text = "2";
                }
                var client = new Client
                {
                    LastName = TBoxLastName.Text,
                    FirstName = TBoxLastName.Text,
                    Patronymic = TBoxPatronymic.Text,
                    GenderCode = CBoxGenderCode.Text,
                    Phone = TBoxPhone.Text,
                    Birthday = DPBirthday.SelectedDate,
                    Email = TBoxEmail.Text,
                    RegistrationDate = DateTime.Now,
                    Login = TBoxLogin.Text,
                    Password = TBoxPassword.Text,
                    RoleId = 2
                };
                db.context.Client.Add(client);
                db.context.SaveChanges();
                MessageBox.Show("Успешная регистрация");
                NavigationService.GoBack();
            }
        }
    }
}
