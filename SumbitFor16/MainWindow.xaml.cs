using SumbitFor16.View.Pages;
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

namespace SumbitFor16
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            FrameMain.Navigate(new LoginPage());
        }


        private void WindowClosed(object sender, EventArgs e)
        {

        }

        private void BtnBackClick(object sender, RoutedEventArgs e)
        {
            if (FrameMain.CanGoBack)
                FrameMain.GoBack();

        }
        private void FrameMainNavigated(object sender, NavigationEventArgs e)
        {
            if (!FrameMain.CanGoBack)
            {
                BtnBack.Visibility = Visibility.Collapsed;
            }
            else 
            {
                BtnBack.Visibility = Visibility.Visible;
            }
        }
    }
}
