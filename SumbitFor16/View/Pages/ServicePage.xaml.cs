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
    /// Логика взаимодействия для ServicePage.xaml
    /// </summary>
    public partial class ServicePage : Page
    {
        Core db = new Core();
        List<Service> arrayService;

        public ServicePage()
        {
            InitializeComponent();
            arrayService = db.context.Service.ToList();
            LViewServices.ItemsSource = arrayService;

            ComboDiscount.SelectedIndex = 0;
            ComboSortBy.SelectedIndex = 0;
            //1-admin 2-user
            if (App.CurrentUser.RoleId == 1)
            {
                BtnAddService.Visibility = Visibility.Visible;
            }
            else 
            {
                BtnAddService.Visibility = Visibility.Hidden;
            }
            UpdateService();
        }

        private void UpdateService()
        {
            var services = db.context.Service.ToList();

            if (ComboSortBy.SelectedIndex == 0)
            {
                services = services.OrderBy(p => p.CostWithDiscount).ToList();
            }
            else 
            {
                services = services.OrderByDescending(p => p.CostWithDiscount).ToList();
            }

            if (ComboSortBy.SelectedIndex == 1)
                services = services.Where(p => p.Discount >= 0 && p.Discount < 0.05).ToList();
            if (ComboSortBy.SelectedIndex == 2)
                services = services.Where(p => p.Discount >= 0.05 && p.Discount < 0.15).ToList();
            if (ComboSortBy.SelectedIndex == 3)
                services = services.Where(p => p.Discount >= 0.15 && p.Discount < 0.30).ToList();
            if (ComboSortBy.SelectedIndex == 4)
                services = services.Where(p => p.Discount >= 0.30 && p.Discount < 0.70).ToList();
            if (ComboSortBy.SelectedIndex == 5)
                services = services.Where(p => p.Discount >= 0.70 && p.Discount < 1).ToList();

            services = services.Where(p => p.Title.ToLower().Contains(TBoxSearch.Text.ToLower())).ToList();

            LViewServices.ItemsSource = services;
        }
        
        private void BtnAddServiceClick(object sender, RoutedEventArgs e)
        {
            Service activeService = null;
            this.NavigationService.Navigate(new AddEditServicePage(activeService));
        }

        private void BtnEditClick(object sender, RoutedEventArgs e)
        {
            Button activeButton = sender as Button;
            Service activeService = activeButton.DataContext as Service;
            this.NavigationService.Navigate(new AddEditServicePage(activeService));
        }

        private void BtnDeleteClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Button selectedButton = (Button)sender;
                Service item = selectedButton.DataContext as Service;

                MessageBoxResult result = MessageBox.Show("Вы действительно хотите удалить?", "Удаление", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    db.context.Service.Remove(item);
                    db.context.SaveChanges();
                    MessageBox.Show("Данные удалены");
                }

                //обновление DataGrid
                LViewServices.ItemsSource = db.context.Service.ToList();
            }
            catch (Exception)
            {
                MessageBox.Show("Данные не удалены. ");
            }
        }

        private void ComboSortBySelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateService();
        }

        private void ComboDiscountSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateService();
        }

        private void TBoxSearchTextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateService();
        }
    }
}
