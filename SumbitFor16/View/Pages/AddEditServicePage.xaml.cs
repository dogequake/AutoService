using SumbitFor16.Models;
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
using Microsoft.Win32;
using System.IO;

namespace SumbitFor16.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddEditServicePage.xaml
    /// </summary>
    public partial class AddEditServicePage : Page
    {
        private Service _currentService = null;
        private byte[] _mainImageData;
        public AddEditServicePage()
        {
            InitializeComponent();
        }
        private void BtnSelectImageClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image |*.png; *.jpg; *.jpeg";
            if (ofd.ShowDialog() == true)
            {
                _mainImageData = File.ReadAllBytes(ofd.FileName);
                ImageService.Source = (ImageSource)new ImageSourceConverter()
                    .ConvertFrom(_mainImageData);
            }
            if (_currentService == null)
            {
                var service = new Service
                {
                    Title = TBoxTitle.Text,
                    Cost = decimal.Parse(TBoxCost.Text),
                    DurationInSeconds = int.Parse(TBoxDuration.Text) * 60,
                    Description = TBoxDescription.Text,
                    Discount = string.IsNullOrWhiteSpace(TBoxDiscount.Text)
                    ? 0 : double.Parse(TBoxDiscount.Text) / 100,
                    MainImage = _mainImageData
                };
                App.Context.Services.Add(service);
                App.Context.SaveChanges();
            }
            else
            {
                _currentService.Title = TBoxTitle.Text;
                _currentService.Cost = decimal.Parse(TBoxCost.Text);
                _currentService.DurationInSeconds = int.Parse(TBoxDuration.Text) * 60;
                _currentService.Description = TBoxDescription.Text;
                _currentService.Discount = string.IsNullOrWhiteSpace(TBoxDicsount.Text)
                    ? 0 : double.Parse(TBoxDiscount.Text) / 100;
                if (_mainImageData != null)
                    _currentService.MainImage = _mainImageData;
                App.Context.SaveChanges();
            }
            NavigationService.GoBack();
        }

    }
}
