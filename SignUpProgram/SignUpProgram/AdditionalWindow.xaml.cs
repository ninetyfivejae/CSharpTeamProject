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
using System.Xml;
using System.Data;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;

namespace SignUpProgram
{
    /// <summary>
    /// Interaction logic for AdditionalWindow.xaml
    /// </summary>
    public partial class AdditionalWindow : UserControl
    {
        public AdditionalWindow()
        {
            InitializeComponent();
        }

        private void searchButton_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void AddressButton_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }
        //주소검색을 위해 추가적인 윈도우 유저컨트롤, 도 시 군구 동 도로명 건물번호 입력받음
        private void searchAddress_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (Regex.IsMatch(searchAddress.Text, @"^([가-힣]*[-]?\d?\s*)*([0-9]+[-]?[0-9]*)$"))
                searchButton.IsEnabled = true;
            else
                searchButton.IsEnabled = false;
        }
    }
}
