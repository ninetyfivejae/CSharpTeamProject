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
using System.Text.RegularExpressions;
using System.Windows.Media.Animation;

namespace SignUpProgram
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        StartPage startPage = new StartPage();
        SignUpPage signUpPage = new SignUpPage();
        SignedInPage signedInPage = new SignedInPage();
        SignUpPage updatePage = new SignUpPage();
        Window window = new Window();
        FindUsernamePassword findUsernamePassword = new FindUsernamePassword();

        Exceptions exceptions;
        Database database;
        MemberVO Member = new MemberVO();
        MemberVO CurrentUser = new MemberVO();

        public MainWindow()
        {
            InitializeComponent();
            exceptions = Exceptions.getInstance();
            database = Database.getInstance();
        }
        //윈도우 캔버스에 각 페이지들을 담아서 화면전환을 이룸
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            startPage.Width = canvas.ActualWidth;
            startPage.Height = canvas.ActualHeight;

            signUpPage.Width = canvas.ActualWidth;
            signUpPage.Height = canvas.ActualHeight;
            signUpPage.Visibility = Visibility.Hidden;

            signedInPage.Width = canvas.ActualWidth;
            signedInPage.Height = canvas.ActualHeight;
            signedInPage.Visibility = Visibility.Hidden;

            updatePage.Width = canvas.ActualWidth;
            updatePage.Height = canvas.ActualHeight;
            updatePage.Visibility = Visibility.Hidden;

            canvas.Children.Add(startPage);
            canvas.Children.Add(signUpPage);
            canvas.Children.Add(signedInPage);
            canvas.Children.Add(updatePage);

            startPage.signInButton.Click += SignInButton_Click;
            startPage.signUpHyperlink.Click += SignUpButton_Click;
            startPage.forgotUsername.Click += ForgotUsername_Click;
            startPage.forgotPassword.Click += ForgotPassword_Click;

            signUpPage.cancel.Click += SignUpPageCancel_Click;
            signUpPage.createAccount.Click += SignUpPageCancel_Click;

            signedInPage.signOut.Click += SignedInPageCancel_Click;
            signedInPage.deleteAccount.Click += DeleteAccount_Click;
            signedInPage.updateProfile.Click += UpdateProfile_Click;

            updatePage.updateAccount.Click += UpdateMemberData_Click;
            updatePage.cancel.Click += Cancel_Click;
        }
        //업데이트 페이지에서 취소버튼을 누를 시, 로그인한 후의 페이지로 화면전환
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            int previousPage = 3;
            int currentPage = 2;

            DoubleAnimation doubleAnimation = new DoubleAnimation();
            doubleAnimation.From = 0.0;
            doubleAnimation.To = -this.Width;
            doubleAnimation.Duration = duration;
            doubleAnimation.FillBehavior = FillBehavior.HoldEnd;
            doubleAnimation.Completed += delegate (object sender1, EventArgs e1)
            {
                canvas.Children[previousPage].Visibility = Visibility.Hidden;
            };
            DoubleAnimation doubleAnimationTwo = new DoubleAnimation();
            doubleAnimationTwo.From = this.Width;
            doubleAnimationTwo.To = 0.0;
            doubleAnimationTwo.Duration = duration;
            doubleAnimationTwo.FillBehavior = FillBehavior.HoldEnd;
            canvas.Children[currentPage].Visibility = Visibility.Visible;
            canvas.Children[previousPage].BeginAnimation(Canvas.LeftProperty, doubleAnimation);
            canvas.Children[currentPage].BeginAnimation(Canvas.LeftProperty, doubleAnimationTwo);
        }
        //회원정보수정페이지에서 수정 완료 클릭 시
        private void UpdateMemberData_Click(object sender, RoutedEventArgs e)
        {
            int previousPage = 3;
            int currentPage = 2;

            DoubleAnimation doubleAnimation = new DoubleAnimation();
            doubleAnimation.From = 0.0;
            doubleAnimation.To = -this.Width;
            doubleAnimation.Duration = duration;
            doubleAnimation.FillBehavior = FillBehavior.HoldEnd;
            doubleAnimation.Completed += delegate (object sender1, EventArgs e1)
            {
                canvas.Children[previousPage].Visibility = Visibility.Hidden;
            };
            DoubleAnimation doubleAnimationTwo = new DoubleAnimation();
            doubleAnimationTwo.From = this.Width;
            doubleAnimationTwo.To = 0.0;
            doubleAnimationTwo.Duration = duration;
            doubleAnimationTwo.FillBehavior = FillBehavior.HoldEnd;
            canvas.Children[currentPage].Visibility = Visibility.Visible;
            canvas.Children[previousPage].BeginAnimation(Canvas.LeftProperty, doubleAnimation);
            canvas.Children[currentPage].BeginAnimation(Canvas.LeftProperty, doubleAnimationTwo);
        }
        //회원정보수정 클릭시 현재 로그인한 회원의 정보를 가져와서 회원정보수정페이지에 설정해둔다
        private void UpdateProfile_Click(object sender, RoutedEventArgs e)
        {
            int previousPage = 2;
            int currentPage = 3;

            CurrentUser = database.GetMemberInfo("member", "username", updatePage.usernameTextbox.Text);

            string[] email = Regex.Split(CurrentUser.MemberEMAIL, "@");
            string[] phonenumber = Regex.Split(CurrentUser.MemberPHONENUMBER, "-");
            updatePage.nameTextbox.Text = CurrentUser.MemberNAME;
            updatePage.firstResidentNumber.Text = CurrentUser.MemberFIRSTRESIDENTNUMBER;
            updatePage.secondResidentNumber.Password = CurrentUser.MemberSECONDRESIDENTNUMBER;
            updatePage.usernameTextbox.Text = CurrentUser.MemberUSERNAME;
            updatePage.emailFirst.Text = email[0];
            updatePage.emailSecond.Text = email[1];
            updatePage.passwordBox.Password = CurrentUser.MemberPASSWORD;
            updatePage.reEnterPasswordBox.Password = CurrentUser.MemberPASSWORD;
            updatePage.phoneNumberFirst.Text = phonenumber[0];
            updatePage.phoneNumberSecond.Text = phonenumber[1];
            updatePage.phoneNumberThird.Text = phonenumber[2];
            updatePage.addressTextbox.Text = CurrentUser.MemberADDRESS;
            updatePage.specificAddressTextbox.Text = CurrentUser.MemberSPECIFICADDRESS;

            updatePage.firstResidentNumber.IsReadOnly = true;
            updatePage.secondResidentNumber.IsEnabled = false;
            updatePage.usernameTextbox.IsReadOnly = true;
            updatePage.createAccount.Visibility = Visibility.Hidden;
            updatePage.updateAccount.Visibility = Visibility.Visible;
            
            DoubleAnimation doubleAnimation = new DoubleAnimation();
            doubleAnimation.From = 0.0;
            doubleAnimation.To = this.Width;
            doubleAnimation.Duration = duration;
            doubleAnimation.FillBehavior = FillBehavior.HoldEnd;
            doubleAnimation.Completed += delegate (object sender1, EventArgs e1)
            {
                canvas.Children[previousPage].Visibility = Visibility.Hidden;
            };
            DoubleAnimation doubleAnimationTwo = new DoubleAnimation();
            doubleAnimationTwo.From = -this.Width;
            doubleAnimationTwo.To = 0.0;
            doubleAnimationTwo.Duration = duration;
            doubleAnimationTwo.FillBehavior = FillBehavior.HoldEnd;
            canvas.Children[currentPage].Visibility = Visibility.Visible;
            canvas.Children[previousPage].BeginAnimation(Canvas.LeftProperty, doubleAnimation);
            canvas.Children[currentPage].BeginAnimation(Canvas.LeftProperty, doubleAnimationTwo);
        }
        //회원정보삭제, 회원탈퇴버튼 클릭 시
        private void DeleteAccount_Click(object sender, RoutedEventArgs e)
        {
            int previousPage = 2;
            int currentPage = 0;
            //데이터베이스에서 정보 삭제
            database.DeleteMember(CurrentUser);

            DoubleAnimation doubleAnimation = new DoubleAnimation();
            doubleAnimation.From = 0.0;
            doubleAnimation.To = this.Width;
            doubleAnimation.Duration = duration;
            doubleAnimation.FillBehavior = FillBehavior.HoldEnd;
            doubleAnimation.Completed += delegate (object sender1, EventArgs e1)
            {
                canvas.Children[previousPage].Visibility = Visibility.Hidden;
            };
            DoubleAnimation doubleAnimationTwo = new DoubleAnimation();
            doubleAnimationTwo.From = -this.Width;
            doubleAnimationTwo.To = 0.0;
            doubleAnimationTwo.Duration = duration;
            doubleAnimationTwo.FillBehavior = FillBehavior.HoldEnd;
            canvas.Children[currentPage].Visibility = Visibility.Visible;
            canvas.Children[previousPage].BeginAnimation(Canvas.LeftProperty, doubleAnimation);
            canvas.Children[currentPage].BeginAnimation(Canvas.LeftProperty, doubleAnimationTwo);
        }
        //비밀번호를 잊어버렸을 때, 아이디 입력
        private void ForgotPassword_Click(object sender, RoutedEventArgs e)
        {
            window = new Window();
            findUsernamePassword = new FindUsernamePassword();

            window.Content = findUsernamePassword;

            findUsernamePassword.inputLabel.Content = "Username";
            findUsernamePassword.forgotButton.Click += ForgotButton_Click1;

            window.Topmost = true;
            window.Show();
        }
        //비밀번호를 찾기위해 아이디 입력하면 중복되는 값이 있는지 확인
        private void ForgotButton_Click1(object sender, RoutedEventArgs e)
        {
            if (exceptions.isDuplicated("member", "username", findUsernamePassword.inputText.Text))
            {
                findUsernamePassword.outputLabel.Content = database.GetMemberInfo("member", "username", findUsernamePassword.inputText.Text).MemberPASSWORD;
            }
            else
            {
                findUsernamePassword.outputLabel.Content = "Data don't exists";
            }
        }
        //아이디를 찾기위해 전화번호 입력
        private void ForgotUsername_Click(object sender, RoutedEventArgs e)
        {
            window = new Window();
            findUsernamePassword = new FindUsernamePassword();

            window.Content = findUsernamePassword;

            findUsernamePassword.inputLabel.Content = "Phone Number";
            findUsernamePassword.forgotButton.Click += ForgotButton_Click;

            window.Topmost = true;
            window.Show();
        }
        //아이디를 찾기위해 전화번호 입력해서 중복되는 값이 있는지 확인
        private void ForgotButton_Click(object sender, RoutedEventArgs e)
        {
            if(exceptions.isDuplicated("member", "phonenumber", findUsernamePassword.inputText.Text))
            {
                findUsernamePassword.outputLabel.Content = database.GetMemberInfo("member", "phonenumber", findUsernamePassword.inputText.Text).MemberUSERNAME;
            }
            else
            {
                findUsernamePassword.outputLabel.Content = "Data don't exists";
            }
        }
        //화면전환 시간, 기간
        Duration duration = new Duration(new TimeSpan(0, 0, 0, 0, 500));
        //로그인 버튼 클릭 시
        private void SignInButton_Click(object sender, RoutedEventArgs e)
        {
            int previousPage = 0;
            int currentPage = 2;
            //일치하는 아이디 비번이 없으면 버튼 빨간색 표시
            if (!exceptions.isDuplicated("member", "username", startPage.usernameTextbox.Text) || !exceptions.isDuplicated("member", "password", startPage.passwordTextbox.Password))
            {
                startPage.signInButton.Background = Brushes.Red;
                startPage.signInButton.Content = "Try Again";
            }
            else//일치하는 데이터가 있으면 버튼 초록색 표시
            {
                startPage.signInButton.Background = Brushes.Green;
                startPage.signInButton.Content = "Sign In";
                //현재 접속한 유저의 정보 저장
                CurrentUser = database.GetMemberInfo("member", "username", startPage.usernameTextbox.Text);

                DoubleAnimation doubleAnimation = new DoubleAnimation();
                doubleAnimation.From = 0.0;
                doubleAnimation.To = -this.Width;
                doubleAnimation.Duration = duration;
                doubleAnimation.FillBehavior = FillBehavior.HoldEnd;
                doubleAnimation.Completed += delegate (object sender1, EventArgs e1)
                {
                    canvas.Children[previousPage].Visibility = Visibility.Hidden;
                };
                DoubleAnimation doubleAnimationTwo = new DoubleAnimation();
                doubleAnimationTwo.From = this.Width;
                doubleAnimationTwo.To = 0.0;
                doubleAnimationTwo.Duration = duration;
                doubleAnimationTwo.FillBehavior = FillBehavior.HoldEnd;
                canvas.Children[currentPage].Visibility = Visibility.Visible;
                canvas.Children[previousPage].BeginAnimation(Canvas.LeftProperty, doubleAnimation);
                canvas.Children[currentPage].BeginAnimation(Canvas.LeftProperty, doubleAnimationTwo);
            }
            startPage.usernameTextbox.Text = String.Empty;
            startPage.passwordTextbox.Password = String.Empty;
        }
        //회원가입 버튼 클릭시 화면 전환
        private void SignUpButton_Click(object sender, RoutedEventArgs e)
        {
            int previousPage = 0;
            int currentPage = 1;

            DoubleAnimation doubleAnimation = new DoubleAnimation();
            doubleAnimation.From = 0.0;
            doubleAnimation.To = -this.Width;
            doubleAnimation.Duration = duration;
            doubleAnimation.FillBehavior = FillBehavior.HoldEnd;
            doubleAnimation.Completed += delegate (object sender1, EventArgs e1)
            {
                canvas.Children[previousPage].Visibility = Visibility.Hidden;
            };
            DoubleAnimation doubleAnimationTwo = new DoubleAnimation();
            doubleAnimationTwo.From = this.Width;
            doubleAnimationTwo.To = 0.0;
            doubleAnimationTwo.Duration = duration;
            doubleAnimationTwo.FillBehavior = FillBehavior.HoldEnd;
            canvas.Children[currentPage].Visibility = Visibility.Visible;
            canvas.Children[previousPage].BeginAnimation(Canvas.LeftProperty, doubleAnimation);
            canvas.Children[currentPage].BeginAnimation(Canvas.LeftProperty, doubleAnimationTwo);
        }
        //회원가입 페이지에서 취소버튼으로 뒤로감
        private void SignUpPageCancel_Click(object sender, RoutedEventArgs e)
        {
            int previousPage = 1;
            int currentPage = 0;

            signUpPage.ClearSignUpPage();
            startPage.signInButton.Background = Brushes.Green;
            startPage.signInButton.Content = "Sign In";

            DoubleAnimation doubleAnimation = new DoubleAnimation();
            doubleAnimation.From = 0.0;
            doubleAnimation.To = this.Width;
            doubleAnimation.Duration = duration;
            doubleAnimation.FillBehavior = FillBehavior.HoldEnd;
            doubleAnimation.Completed += delegate (object sender1, EventArgs e1)
            {
                canvas.Children[previousPage].Visibility = Visibility.Hidden;
            };
            DoubleAnimation doubleAnimationTwo = new DoubleAnimation();
            doubleAnimationTwo.From = -this.Width;
            doubleAnimationTwo.To = 0.0;
            doubleAnimationTwo.Duration = duration;
            doubleAnimationTwo.FillBehavior = FillBehavior.HoldEnd;
            canvas.Children[currentPage].Visibility = Visibility.Visible;
            canvas.Children[previousPage].BeginAnimation(Canvas.LeftProperty, doubleAnimation);
            canvas.Children[currentPage].BeginAnimation(Canvas.LeftProperty, doubleAnimationTwo);
        }
        //로그아웃 버튼 클릭시
        private void SignedInPageCancel_Click(object sender, RoutedEventArgs e)
        {
            int previousPage = 2;
            int currentPage = 0;

            DoubleAnimation doubleAnimation = new DoubleAnimation();
            doubleAnimation.From = 0.0;
            doubleAnimation.To = this.Width;
            doubleAnimation.Duration = duration;
            doubleAnimation.FillBehavior = FillBehavior.HoldEnd;
            doubleAnimation.Completed += delegate (object sender1, EventArgs e1)
            {
                canvas.Children[previousPage].Visibility = Visibility.Hidden;
            };
            DoubleAnimation doubleAnimationTwo = new DoubleAnimation();
            doubleAnimationTwo.From = -this.Width;
            doubleAnimationTwo.To = 0.0;
            doubleAnimationTwo.Duration = duration;
            doubleAnimationTwo.FillBehavior = FillBehavior.HoldEnd;
            canvas.Children[currentPage].Visibility = Visibility.Visible;
            canvas.Children[previousPage].BeginAnimation(Canvas.LeftProperty, doubleAnimation);
            canvas.Children[currentPage].BeginAnimation(Canvas.LeftProperty, doubleAnimationTwo);
        }


    }
}
