using KioskApp.Service;
using KioskAppServer.Service;
using KisokApp.Util;
using System.Diagnostics;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;

namespace KioskApp
{
    public class MainViewModel : ViewModelBase
    {
        IViewModelFactory viewModelFactory;
        private readonly INavigationService _navigationService;
        private readonly OrderService _orderService;
        private readonly GoogleDriveDataService _googleDriveDataService;
        //포장 커맨드
        public ICommand TakeawayCommand { get; }
        //식사 커맨드
        public ICommand DineInCommand { get; }
        public MainViewModel(INavigationService navigationService,OrderService orderService,GoogleDriveDataService googleDriveDataService)
        {
            
            TakeawayCommand = new RelayCommand(OnTakeaway);
            DineInCommand = new RelayCommand(OnDineIn);
            _navigationService = navigationService;
            _orderService = orderService;
            _googleDriveDataService = googleDriveDataService;

            viewModelFactory = new ViewModelFactory();
        }
        private void OnTakeaway()
        {
            if (_googleDriveDataService.KioskData == null)
            { 
                MessageBox.Show("구글 드라이브 연동 중입니다. 잠시만 대기해주세요.");
                return;
            }
            Debug.WriteLine("포장");
            _orderService.CurrentOrderType = OrderType.Takeaway;

            var model = viewModelFactory.CreateViewModel<MenuSelectionViewModel>(_navigationService, _orderService,_googleDriveDataService);
            var page = new MenuSelectionView()
            {
                DataContext = model,
            };

            _navigationService.NavigateToNew(page);
        }
        private void OnDineIn()
        {
            if (_googleDriveDataService.KioskData == null)
            {
                MessageBox.Show("구글 드라이브 연동 중입니다. 잠시만 대기해주세요.");
                return;
            }
            Debug.WriteLine("매장 식사");
            _orderService.CurrentOrderType = OrderType.DineIn;
            var model = viewModelFactory.CreateViewModel<MenuSelectionViewModel>(_navigationService,_orderService,_googleDriveDataService);
            var page = new MenuSelectionView()
            {
                DataContext = model,
            };

            _navigationService.NavigateToNew(page);
        }

        //메뉴 화면으로 이동
        private void NavigateToMenuSelection()
        {
            
        }
    }
}