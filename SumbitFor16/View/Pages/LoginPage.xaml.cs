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

namespace SumbitFor16.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        Core db = new Core();
        public LoginPage()
        {
            InitializeComponent();
        }
        private void BtnLoginClick(object sender, RoutedEventArgs e)
        {
            Client currentClient = db.context.Client.Where(x => x.Login == TBoxLogin.Text).FirstOrDefault();
            if (currentClient != null)
            {
                if (currentClient.Login == TBoxLogin.Text && currentClient.Password == PBoxPassword.Password)
                {
                    App.CurrentClient = currentClient;
                    this.NavigationService.Navigate(new ServicePage());
                }
                else
                {
                    MessageBox.Show("НЕВЕРНЫЙ ПАРОООООЛЬ");
                }
            }
            else 
            {
                MessageBox.Show("НЕт аакаунта с таким логинам");
            }
        }

        private void BtnRegClick(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new RegPage());
        }
        private void ForgotPasswordMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("ВСПОМИНАЙ");
        }
    }
}
