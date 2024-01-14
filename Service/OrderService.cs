
using System.Collections.Generic;
using System;

namespace KioskApp.Service
{
    public enum OrderType
    {
        DineIn,
        Takeaway
    }

    public class OrderService
    {
        public OrderType CurrentOrderType { get; set; }
        public List<Menu> OrderedItems { get; set; }
        public Dictionary<Menu, int> OrderedMenuQuantities { get; set; } = new Dictionary<Menu, int>();
        public DateTime OrderTime { get; set; }
        public string PaymentMethod { get; set; }
        public bool IsTakeaway { get; set; }
    }
}