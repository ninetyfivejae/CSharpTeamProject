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
    /// Interaction logic for SignedInPage.xaml
    /// </summary>
    public partial class SignedInPage : UserControl
    {
        //로그인한 후의 페이지
        public SignedInPage()
        {
            InitializeComponent();

            BitmapImage updateButton = new BitmapImage(new Uri(Environment.CurrentDirectory + @"/Image/img1.jpg"));
            Image updateImage = new Image();
            updateImage.Source = updateButton;
            updateProfile.Content = updateImage;

            BitmapImage signOutButton = new BitmapImage(new Uri(Environment.CurrentDirectory + @"/Image/img2.jpg"));
            Image signOutImage = new Image();
            signOutImage.Source = signOutButton;
            signOut.Content = signOutImage;

            BitmapImage deleteAccountButton = new BitmapImage(new Uri(Environment.CurrentDirectory + @"/Image/img3.jpg"));
            Image deleteAccountImage = new Image();
            deleteAccountImage.Source = deleteAccountButton;
            deleteAccount.Content = deleteAccountImage;
        }


    }
}
