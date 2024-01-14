using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace KioskApp.Service
{
    public interface INavigationService
    {
        void NavigateTo(Type pageType);
        void NavigateToNew(Page page);
    }
    internal class NavigationService:INavigationService
    {
        private readonly Frame _mainFrame;
        private readonly Window _window;
        public NavigationService(Window window,Frame frame)
        {
            _mainFrame = frame;
            _window = window;
        }
        public void NavigateTo(Type pageType)
        {
            var page= Activator.CreateInstance(pageType) as Page;
            Debug.WriteLine(page.ToString()+"네비게이트 로드");
            if(page != null)
            {
                _mainFrame.Visibility = Visibility.Visible;
                _mainFrame.Content = page;
                //Debug.WriteLine("Navigate:"+_mainFrame.Navigate(page));
                _window.Show();
            }
        }
        public void NavigateToNew(Page page)
        {
            if (page != null)
            {
                _mainFrame.Visibility = Visibility.Visible;
                _mainFrame.Content = page;
                //Debug.WriteLine("Navigate:"+_mainFrame.Navigate(page));
                _window.Show();
            }
        }
    }
}
