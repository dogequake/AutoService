using Microsoft.Win32;
using SumbitFor16.Models;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace SumbitFor16.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddEditServicePage.xaml
    /// </summary>
    public partial class AddEditServicePage : Page
    {
        int imageId;
        private Service _currentService = null;
        Core db = new Core();
        private byte[] _mainImageData;
        public AddEditServicePage(Service currentService)
        {
            InitializeComponent();
            _currentService = currentService;
            if (currentService!=null)
            {
                TBoxTitle.Text = _currentService.Title;
                TBoxCost.Text = Convert.ToString(_currentService.Cost);
                TBoxDiscount.Text = Convert.ToString(_currentService.Discount);
                TBoxDescription.Text = _currentService.Description;
                TBoxDuration.Text = _currentService.DurationInSeconds;
                string path = db.context.ServicePhoto.FirstOrDefault(x => x.ID == _currentService.MainImagePath).PhotoPath;
                Uri AddEditPhoto = new Uri(path);

                ServiceImage.Source = new BitmapImage(AddEditPhoto);
            }
          
        }
        private void BtnSelectImageClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image |*.png; *.jpg; *.jpeg";
            if (ofd.ShowDialog() == true)
            {
                _mainImageData = File.ReadAllBytes(ofd.FileName);
                ImageService.Source = (ImageSource)new ImageSourceConverter().ConvertFrom(_mainImageData);
            }
            if (_currentService == null)
            {
                string newfilename = "\\Assets\\Images\\SERVICES\\";
                string appFolderPath = Directory.GetCurrentDirectory();
                appFolderPath = appFolderPath.Replace("\\bin\\Debug", "");//обрезанный путь
                string imageName = System.IO.Path.GetFileName(ofd.FileName);//имя картинки с расширением
                newfilename = appFolderPath + newfilename + imageName;

                if (File.Exists($"{AppDomain.CurrentDomain.BaseDirectory}\\..\\..\\Assets\\Images\\SERVICES\\{System.IO.Path.GetFileName(ofd.FileName)}"))
                {
                    string name = System.IO.Path.GetFileName(ofd.FileName);
                    ServicePhoto selectedImage = db.context.ServicePhoto.FirstOrDefault(x => x.PhotoPath.Contains(name));
                    if (selectedImage!=null)
                    {
                        imageId = selectedImage.ID;
                    }
                   
                }
                else
                {
                    File.Copy(ofd.FileName, newfilename);
                    ServicePhoto photo = new ServicePhoto
                    {
                        PhotoPath = $"SERVICES/{System.IO.Path.GetFileName(ofd.FileName)}"
                    };
                    db.context.ServicePhoto.Add(photo);
                   // MessageBox.Show("Жесть");
                    db.context.SaveChanges();
                    imageId = photo.ID;
                }
                var service = new Service
                {
                    Title = TBoxTitle.Text,
                    //Cost = decimal.Parse(TBoxCost.Text),
                    Cost = 0,
                    DurationInSeconds = $"{int.Parse(TBoxDuration.Text) * 60} мин.",
                    Description = TBoxDescription.Text,
                    //Discount = string.IsNullOrWhiteSpace(TBoxDiscount.Text)? 0 : double.Parse(TBoxDiscount.Text) / 100,
                    MainImagePath = imageId
                };
                db.context.Service.Add(service);
                db.context.SaveChanges();
                MessageBox.Show("УСПЕШНОЕ ДОБАВЛЕНИЕ");
            }
            else
            {
                _currentService.Title = TBoxTitle.Text;
                _currentService.Cost = decimal.Parse(TBoxCost.Text);
                _currentService.DurationInSeconds = Convert.ToString(int.Parse(TBoxDuration.Text) * 60);
                _currentService.Description = TBoxDescription.Text;
                _currentService.Discount = String.IsNullOrWhiteSpace(TBoxDiscount.Text) ? 0 : double.Parse(TBoxDiscount.Text) / 100;
                if (_mainImageData != null)
                    _currentService.MainImagePath = 1;
                db.context.SaveChanges();
                MessageBox.Show("УСПЕШНОЕ РЕДАКТИРОВАНИЕ");
            }
            NavigationService.GoBack();
        }
    }
}
