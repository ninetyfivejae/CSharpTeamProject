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
    /// Interaction logic for FindPassword.xaml
    /// </summary>
    public partial class FindPassword : UserControl
    {
        public FindPassword()
        {
            InitializeComponent();
        }

        public void FindClicked(object sender, RoutedEventArgs e)
        {
            bool IsSucceed;

            MailService mailService = new MailService();
            IsSucceed = mailService.Send(inputPhone.Text, inputEmail.Text);
            if (IsSucceed == false)
            {
                MessageBox.Show("존재하는 회원이 아닙니다.");
                inputPhone.Clear();
                return;
            }

            MessageBox.Show("입력하신 이메일 주소로 이메일과 비밀번호를 보냈습니다.");
            inputPhone.Clear();
            inputEmail.Clear();
        }

        public void BackClicked(object sender, RoutedEventArgs e)
        {

        }
    }
}
