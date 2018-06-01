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

namespace SignUpProgram
{
    /// <summary>
    /// Interaction logic for StartPage.xaml
    /// </summary>
    public partial class StartPage : UserControl
    {
        Exceptions exceptions;
        MemberVO Member = new MemberVO();

        //초기 로그인 화면, 시작 페이지
        public StartPage()
        {
            InitializeComponent();
            exceptions = Exceptions.getInstance();
        }

    }

}
