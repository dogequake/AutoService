﻿using System;
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
            User currentUser = db.context.User.Where(x => x.Login == TBoxLogin.Text).FirstOrDefault();
            if (currentUser != null)
            {
                if (currentUser.Login == TBoxLogin.Text && currentUser.Password == PBoxPassword.Password)
                {
                    App.CurrentUser = currentUser;
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
    }
}
