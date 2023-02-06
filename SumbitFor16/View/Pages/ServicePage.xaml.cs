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
        }

        private void BtnAddServiceClick(object sender, RoutedEventArgs e)
        {

        }

        private void BtnEditClick(object sender, RoutedEventArgs e)
        {

        }

        private void BtnDeleteClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
