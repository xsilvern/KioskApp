
using KioskApp.Service;
using KioskAppServer.Service;
using KisokApp.Util;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
namespace KioskApp
{
    public class MenuSelectionViewModel : ViewModelBase
    {


        private decimal _total;
        private OrderService _orderService;

        public OrderService OrderService 
        { 
            get => _orderService; 
            set => SetProperty(ref _orderService, value); 
        }

        private ObservableCollection<Menu> _cart;
        private Category _currentCategory;

        public Category CurrentCategory
        {
            get => _currentCategory;
            set=>SetProperty(ref _currentCategory, value);
        }

        public decimal Total
        {
            get => _total;
            set => SetProperty(ref _total, value);
        }
        public ObservableCollection<Menu> Cart
        {
            get => _cart;
            set => SetProperty(ref _cart, value);
        }

        private ObservableCollection<Menu> _currentMenus;
        public ObservableCollection<Menu> CurrentMenus
        {
            get => _currentMenus;
            set => SetProperty(ref _currentMenus, value);
        }
       
        
        public ICommand ChangeCategoryCommand { get; }
        public ICommand ChooseMenuButtonCommand { get; }
        public ICommand PaymentCommand { get; }

        public ICommand DeleteMenuCommand { get; }


        private readonly INavigationService _navigationService;
        private IViewModelFactory _viewModelFactory;

        private GoogleDriveDataService _googleDriveDataService;

        private KioskDataSet _kioskData;
        public KioskDataSet KioskData
        {
            get => _kioskData;
            set
            {
                _kioskData = value;
                OnPropertyChanged(nameof(KioskData));
                OnPropertyChanged(nameof(KioskData.CategoryList));
                OnPropertyChanged(nameof(KioskData.MenuList));
            }
        }

        public MenuSelectionViewModel(INavigationService navigationService, OrderService orderService, GoogleDriveDataService googleDriveDataService)
        {
            _viewModelFactory = new ViewModelFactory();
            _navigationService = navigationService;
            _orderService = orderService;
            _googleDriveDataService = googleDriveDataService;

            LoadMenus();
            _cart = new ObservableCollection<Menu>();
            ChangeCategoryCommand = new RelayCommand<string>(ChangeCategoryByName);
            ChooseMenuButtonCommand = new RelayCommand<Menu>(ChooseMenu);
            PaymentCommand = new RelayCommand(Payment);
            DeleteMenuCommand = new RelayCommand<object>(DeleteMenu);
        }
        public void Payment()
        {
            var model= _viewModelFactory.CreateViewModel<PaymentSelectionViewModel>(_navigationService,_orderService);
            var page = new PaymentSelectionView()
            {
                DataContext= model,
            };
            
            _navigationService.NavigateToNew(page);
        }
        private void ChooseMenu(Menu menu)
        {
            
            Debug.WriteLine("메뉴 추가:"+menu.Name);
            
            if(_orderService.OrderedMenuQuantities.ContainsKey(menu))
            {
                _orderService.OrderedMenuQuantities[menu]++;
            }
            else
            {
                _orderService.OrderedMenuQuantities.Add(menu, 1);
                Cart.Add(menu);
            }
            menu.Quantity = _orderService.OrderedMenuQuantities[menu];
            Total += menu.Price;
            OnPropertyChanged(nameof(Cart));
            OnPropertyChanged(nameof(Total));
        }
        private void DeleteMenu(object param)
        {
            if(param is Menu menu)
            {
                Total -= menu.Price;
                _orderService.OrderedMenuQuantities[menu]--;
                menu.Quantity = _orderService.OrderedMenuQuantities[menu];
                if (_orderService.OrderedMenuQuantities[menu]<=0)
                {
                    Cart.Remove(menu);
                    _orderService.OrderedMenuQuantities.Remove(menu);
                }
                OnPropertyChanged(nameof(Cart));
                OnPropertyChanged(nameof(Total));
            }
        }
        private void LoadMenus()
        {
            KioskData = _googleDriveDataService.KioskData;
            ChangeCategory(KioskData.CategoryList.First());
        }
        private void ChangeCategory(Category category)
        {
            CurrentCategory = category;
            CurrentMenus = category.Menus;
            Debug.WriteLine("카테고리 변경됨:"+category.Name);
            OnPropertyChanged(nameof(CurrentCategory));
            OnPropertyChanged(nameof(CurrentMenus));
        }
        private void ChangeCategoryByName(string categoryName)
        {
            var category = KioskData.CategoryList.FirstOrDefault(c => c.Name == categoryName);
            if (category != null)
            {
                ChangeCategory(category);
            }
        }
    }
}