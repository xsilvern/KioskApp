using KioskApp.Service;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Shapes;

namespace KioskApp
{
    /// <summary>
    /// MenuSelectionView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MenuSelectionView : Page
    {
        public MenuSelectionView()
        {
            InitializeComponent();
            Debug.WriteLine("메뉴 셀렉션 생성");
        }
    }
}
