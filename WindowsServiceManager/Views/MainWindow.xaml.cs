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
using WindowsServiceManager.Models;

namespace WindowsServiceManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
            ServicesDataGrid.ItemsSource = ServiceHandler.GetInstance().services.GetRange(0,5);
            this.DataContext = ServiceHandler.GetInstance();
        }

        private async void OnClickStart(object sender, RoutedEventArgs e)
        {
            var service = ServicesDataGrid.SelectedItem as Service;
            var result = await ServiceHandler.StartService(service);
        }
        private async void OnClickStop(object sender, RoutedEventArgs e)
        {
            var service = ServicesDataGrid.SelectedItem as Service;
            var result = await ServiceHandler.StopService(service);
        }
    }
}
