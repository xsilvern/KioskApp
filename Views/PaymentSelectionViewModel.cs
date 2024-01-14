using System.Windows.Controls;
using System;
using System.Windows.Input;
using System.Collections.Generic;
using KioskApp.Service;
namespace KioskApp
{
    public class PaymentSelectionViewModel : ViewModelBase
    {
        private OrderService _orderService;
        public ICommand KakaoPayCommand { get; }
        public ICommand TossCommand { get; }
        public ICommand CardPaymentCommand { get; }

        private readonly INavigationService _navigationService;
        
        public PaymentSelectionViewModel(INavigationService navigationService,OrderService orderService)
        {
            _navigationService = navigationService;
            _orderService = orderService;
            KakaoPayCommand = new RelayCommand(ExecuteKakaoPay);
            TossCommand = new RelayCommand(ExecuteToss);
            CardPaymentCommand = new RelayCommand(ExecuteCardPayment);
        }

        private void ExecuteKakaoPay()
        {

        }

        private void ExecuteToss()
        {

        }

        private void ExecuteCardPayment()
        {

        }
        private void ExecutePayment(string paymentMethod)
        {
            // 결제 로직 작성 해야 함

        }
    }
}