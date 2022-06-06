using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

            ServicesDataGrid.ItemsSource = ServiceHandler.GetInstance().services;
            this.DataContext = ServiceHandler.GetInstance();
            //var dataUpdateThread = new Thread(BackgroundDataUpdater);
            //dataUpdateThread.Start();
        }

        private async void OnClickStart(object sender, RoutedEventArgs e)
        {
            var service = ServicesDataGrid.SelectedItem as Service;
            var result = await ServiceHandler.StartService(service);
            if (!result.Contains("OK")) MessageBox.Show(result);

            ServicesDataGrid.ItemsSource = ServiceHandler.GetInstance().services;
            ServicesDataGrid.Items.Refresh();
        }
        private async void OnClickStop(object sender, RoutedEventArgs e)
        {
            var service = ServicesDataGrid.SelectedItem as Service;
            var result = await ServiceHandler.StopService(service);
            if (!result.Contains("OK")) MessageBox.Show(result);

            ServicesDataGrid.ItemsSource = ServiceHandler.GetInstance().services;
            ServicesDataGrid.Items.Refresh();
        }
        private void BackgroundDataUpdater()
        {
            while (1 < 2)
            {
                lock (this)
                {
                    ServiceHandler.GetInstance().UpdateServiceList();
                    ServicesDataGrid.ItemsSource = ServiceHandler.GetInstance().services;
                    ServicesDataGrid.Items.Refresh();
                }
                
                Thread.Sleep(5000);
            }
        }
    }
}
