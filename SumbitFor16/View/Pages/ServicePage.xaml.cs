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
            this.NavigationService.Navigate(new AddEditServicePage());
        }

        private void BtnEditClick(object sender, RoutedEventArgs e)
        {

        }

        private void BtnDeleteClick(object sender, RoutedEventArgs e)
        {

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
