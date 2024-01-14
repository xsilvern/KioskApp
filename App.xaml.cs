using KioskApp;
using KioskApp.Service;
using KioskAppServer.Service;
using KisokApp.Util;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
namespace KioskApp
{
    /// <summary>
    /// App.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            IViewModelFactory viewModelFactory = new ViewModelFactory();
            base.OnStartup(e);
            var mainWindow = new MainWindow();
            var mainFrame = mainWindow.FindName("frame") as Frame;
            Debug.WriteLine("frame" + mainFrame);
            var navigationService = new NavigationService(mainWindow, mainFrame);
            var orderService = new OrderService()
            {
                OrderedItems=new List<Menu>(),
            };
            var googleDriveService = new GoogleDriveDataService();

            var mainViewModel = new MainViewModel(navigationService, orderService,googleDriveService);
            var mainView = new MainView();
            mainView.DataContext = mainViewModel;
            mainFrame.Content = mainView;
            mainWindow.Show();

        }
    }
}
