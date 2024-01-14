using KioskApp;
using KioskApp.Service;
using KioskAppServer.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KisokApp.Util
{
    public interface IViewModelFactory
    {
        T CreateViewModel<T>() where T : ViewModelBase,new();
        T CreateViewModel<T>(INavigationService navigationService, OrderService orderService) where T : ViewModelBase;
        T CreateViewModel<T>(INavigationService navigationService, OrderService orderService,GoogleDriveDataService googleDriveDataService) where T : ViewModelBase;
    }
    public class ViewModelFactory : IViewModelFactory
    {
        public T CreateViewModel<T>() where T : ViewModelBase, new()
        {
            // ViewModel의 생성 로직 작성 나중에 필요하면 넣기
            return new T();
        }
        public T CreateViewModel<T>(INavigationService navigationService, OrderService orderService) where T : ViewModelBase
        {

            return (T)Activator.CreateInstance(typeof(T), navigationService, orderService);
        }
        public T CreateViewModel<T>(INavigationService navigationService, OrderService orderService,GoogleDriveDataService googleDriveDataService) where T : ViewModelBase
        {
            
            return (T)Activator.CreateInstance(typeof(T),navigationService,orderService,googleDriveDataService);
        }
        
    }
}
