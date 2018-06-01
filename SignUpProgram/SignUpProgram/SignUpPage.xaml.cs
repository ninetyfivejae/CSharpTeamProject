using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Text.RegularExpressions;
using System.Xml;
using System.Net;
using System.IO;

namespace SignUpProgram
{
    /// <summary>
    /// Interaction logic for SignUpPage.xaml
    /// </summary>
    public partial class SignUpPage : UserControl
    {
        Window window = new Window();
        AdditionalWindow additionalWindow = new AdditionalWindow();
        MemberVO Member = new MemberVO();
        Database database;
        Exceptions exceptions;

        bool isNameConfirmed = false;                   //회원이름이 형식에 맞게 입력됐는지 체크
        bool isFirstResidentNumberConfirmed = false;    //회원주민등록번호 앞자리가 형식에 맞게 입력됐는지 체크
        bool isSecondResidentNumberConfirmed = false;   //회원주민등록번호 뒷자리가 형식에 맞게 입력됐는지 체크
        bool isUsernameConfirmed = false;               //회원아이디가 형식에 맞게 입력됐는지 체크
        bool isFirstEmailConfirmed = false;             //회원이메일 앞자리가 형식에 맞게 입력됐는지 체크
        bool isSecondEmailConfirmed = false;            //회원이메일 뒸자리가 형식에 맞게 입력됐는지 체크
        bool isPasswordConfirmed = false;               //회원비밀번호가 형식에 맞게 입력됐는지 체크
        bool isRePasswordConfirmed = false;             //회원이 입력한 비밀번호와 일치하게 입력했는지 체크
        bool isSecondPhoneNumberConfirmed = false;      //전화번호 두번째 칸 입력 형식에 맞게 입력됐는지 체크
        bool isThirdPhoneNumberConfirmed = false;       //전화번호 세번째 칸 입력 형식에 맞게 입력됐는지 체크
        bool isAddressConfirmed = false;                //회원 주소가 형식에 맞게 입력됐는지 체크

        public SignUpPage()
        {
            InitializeComponent();
            database = Database.getInstance();
            exceptions = Exceptions.getInstance();
        }
        //이름 입력받는 란에서 포커스 아웃되는 순간 형식에 맞게 입력됐는지 체크
        private void nameTextbox_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {   //형식에 안 맞으면 빨간색 글씨 표시
            if (!Regex.IsMatch(nameTextbox.Text, @"^[가-힣]{2,6}$"))
            {
                name.Foreground = Brushes.Red;
                isNameConfirmed = false;
            }
            else
            {   //형식에 맞게 입력받으면 검은색 글씨 표시
                name.Foreground = Brushes.Black;
                isNameConfirmed = true;
            }
            SatisfiedRequirements();
        }
        //주민등록번호 앞자리 입력란 포커스 아웃되는 순간 형식에 맞게 입력됐는지 체크
        private void firstResidentNumber_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (!Regex.IsMatch(firstResidentNumber.Text, @"^[0-9]{6}$"))
            {
                residentNumber.Foreground = Brushes.Red;
                isFirstResidentNumberConfirmed = false;
            }
            else
            {
                isFirstResidentNumberConfirmed = true;
                if (isSecondResidentNumberConfirmed)
                {
                    residentNumber.Foreground = Brushes.Black;
                }
            }
            SatisfiedRequirements();
        }

        private void secondResidentNumber_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (!Regex.IsMatch(secondResidentNumber.Password, @"^[0-9]{7}$"))
            {
                residentNumber.Foreground = Brushes.Red;
                isSecondResidentNumberConfirmed = false;
                createAccount.IsEnabled = false;
            }
            else if (exceptions.isDuplicated("member", "secondresidentnumber", secondResidentNumber.Password))
            {
                residentNumber.Foreground = Brushes.Red;
                isSecondResidentNumberConfirmed = false;
                duplicationMessage.Content = "Resident Number already exists";
                duplicationMessage.Visibility = Visibility.Visible;
                createAccount.IsEnabled = false;
            }
            else
            {
                duplicationMessage.Visibility = Visibility.Hidden;
                isSecondResidentNumberConfirmed = true;
                if (isFirstResidentNumberConfirmed)
                {
                    residentNumber.Foreground = Brushes.Black;
                }
            }
            SatisfiedRequirements();
        }

        private void usernameTextbox_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (!Regex.IsMatch(usernameTextbox.Text, @"^[A-Za-z0-9]{1}([-_.]?[0-9a-zA-Z]){3,14}$"))
            {
                username.Foreground = Brushes.Red;
                isUsernameConfirmed = false;
                createAccount.IsEnabled = false;
            }
            else if (exceptions.isDuplicated("member", "username", usernameTextbox.Text))
            {
                username.Foreground = Brushes.Red;
                isUsernameConfirmed = false;
                duplicationMessage.Content = "Username already exists";
                duplicationMessage.Visibility = Visibility.Visible;
                createAccount.IsEnabled = false;
            }
            else
            {
                duplicationMessage.Visibility = Visibility.Hidden;
                username.Foreground = Brushes.Black;
                isUsernameConfirmed = true;
            }
            SatisfiedRequirements();
        }

        private void emailFirst_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (!Regex.IsMatch(emailFirst.Text, @"^[0-9a-zA-Z]{1}([-_.]?[0-9a-zA-Z])*$"))
            {
                email.Foreground = Brushes.Red;
                isFirstEmailConfirmed = false;
                createAccount.IsEnabled = false;
            }
            else
            {
                isFirstEmailConfirmed = true;
                if(isSecondEmailConfirmed)
                {
                    if(exceptions.isDuplicated("member", "email", emailFirst.Text + "@" + emailSecond.Text))
                    {
                        email.Foreground = Brushes.Red;
                        duplicationMessage.Content = "Email already exists";
                        duplicationMessage.Visibility = Visibility.Visible;
                        createAccount.IsEnabled = false;
                    }
                    else
                    {
                        duplicationMessage.Visibility = Visibility.Hidden;
                        email.Foreground = Brushes.Black;
                    }
                }
            }
            SatisfiedRequirements();
        }
        private void emailSecond_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (!Regex.IsMatch(emailSecond.Text, @"^[0-9a-zA-Z]([-_.]?[0-9a-zA-Z])*.[a-zA-Z]{2,3}$"))
            {
                email.Foreground = Brushes.Red;
                isSecondEmailConfirmed = false;
            }
            else
            {
                isSecondEmailConfirmed = true;
                if(isFirstEmailConfirmed)
                {
                    if (exceptions.isDuplicated("member", "email", emailFirst.Text + "@" + emailSecond.Text))
                    {
                        email.Foreground = Brushes.Red;
                        duplicationMessage.Content = "Email already exists";
                        duplicationMessage.Visibility = Visibility.Visible;
                        createAccount.IsEnabled = false;
                    }
                    else
                    {
                        duplicationMessage.Visibility = Visibility.Hidden;
                        email.Foreground = Brushes.Black;
                    }
                }
            }
            SatisfiedRequirements();
        }
        private void emailSelect_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(emailSelectOne.IsSelected == true)
            {
                emailSecond.Text = "";
                emailSecond.IsReadOnly = false;
            }
            else if(emailSelectTwo.IsSelected == true)
            {
                emailSecond.Text = "gmail.com";
                emailSecond.IsReadOnly = true;
                if (isFirstEmailConfirmed && !exceptions.isDuplicated("member", "email", emailFirst.Text + "@" + emailSecond.Text))
                {
                    duplicationMessage.Visibility = Visibility.Hidden;
                    email.Foreground = Brushes.Black;
                }
            }
            else if (emailSelectThree.IsSelected == true)
            {
                emailSecond.Text = "naver.com";
                emailSecond.IsReadOnly = true;
                if (isFirstEmailConfirmed && !exceptions.isDuplicated("member", "email", emailFirst.Text + "@" + emailSecond.Text))
                {
                    duplicationMessage.Visibility = Visibility.Hidden;
                    email.Foreground = Brushes.Black;
                }
            }
            else if (emailSelectFour.IsSelected == true)
            {
                emailSecond.Text = "hanmail.net";
                emailSecond.IsReadOnly = true;
                if (isFirstEmailConfirmed && !exceptions.isDuplicated("member", "email", emailFirst.Text + "@" + emailSecond.Text))
                {
                    duplicationMessage.Visibility = Visibility.Hidden;
                    email.Foreground = Brushes.Black;
                }
            }
            else if (emailSelectFive.IsSelected == true)
            {
                emailSecond.Text = "nate.com";
                emailSecond.IsReadOnly = true;
                if (isFirstEmailConfirmed && !exceptions.isDuplicated("member", "email", emailFirst.Text + "@" + emailSecond.Text))
                {
                    duplicationMessage.Visibility = Visibility.Hidden;
                    email.Foreground = Brushes.Black;
                }
            }
            SatisfiedRequirements();
        }

        private void passwordBox_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (!Regex.IsMatch(passwordBox.Password, @"^(?=.*[a-zA-Z])(?=.*[^a-zA-Z0-9])(?=.*[0-9]).{6,16}$"))
            {
                password.Foreground = Brushes.Red;
                isPasswordConfirmed = false;
                createAccount.IsEnabled = false;
            }
            else
            {
                password.Foreground = Brushes.Black;
                isPasswordConfirmed = true;
            }
            SatisfiedRequirements();
        }
        private void reEnterPasswordBox_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (!Regex.IsMatch(reEnterPasswordBox.Password, @"^(?=.*[a-zA-Z])(?=.*[^a-zA-Z0-9])(?=.*[0-9]).{6,16}$") || passwordBox.Password != reEnterPasswordBox.Password)
            {
                rePassword.Foreground = Brushes.Red;
                isRePasswordConfirmed = false;
                createAccount.IsEnabled = false;
            }
            else
            {
                rePassword.Foreground = Brushes.Black;
                isRePasswordConfirmed = true;
            }
            SatisfiedRequirements();
        }
        //전화번호 첫번째 칸 010,011,016...콤보박스 클릭시 
        private void phoneNumberFirst_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (phoneNumberSelectOne.IsSelected == true)
            {
                if (isSecondPhoneNumberConfirmed && isThirdPhoneNumberConfirmed && !exceptions.isDuplicated("member", "phonenumber", phoneNumberFirst.Text + phoneNumberSecond.Text + phoneNumberThird.Text))
                {
                    duplicationMessage.Visibility = Visibility.Hidden;
                    phoneNumber.Foreground = Brushes.Black;
                }
            }
            else if (phoneNumberSelectTwo.IsSelected == true)
            {
                if (isSecondPhoneNumberConfirmed && isThirdPhoneNumberConfirmed && !exceptions.isDuplicated("member", "phonenumber", phoneNumberFirst.Text + phoneNumberSecond.Text + phoneNumberThird.Text))
                {
                    duplicationMessage.Visibility = Visibility.Hidden;
                    phoneNumber.Foreground = Brushes.Black;
                }
            }
            else if (phoneNumberSelectThree.IsSelected == true)
            {
                if (isSecondPhoneNumberConfirmed && isThirdPhoneNumberConfirmed && !exceptions.isDuplicated("member", "phonenumber", phoneNumberFirst.Text + phoneNumberSecond.Text + phoneNumberThird.Text))
                {
                    duplicationMessage.Visibility = Visibility.Hidden;
                    phoneNumber.Foreground = Brushes.Black;
                }
            }
            else if (phoneNumberSelectFour.IsSelected == true)
            {
                if (isSecondPhoneNumberConfirmed && isThirdPhoneNumberConfirmed && !exceptions.isDuplicated("member", "phonenumber", phoneNumberFirst.Text + phoneNumberSecond.Text + phoneNumberThird.Text))
                {
                    duplicationMessage.Visibility = Visibility.Hidden;
                    phoneNumber.Foreground = Brushes.Black;
                }
            }
            else if (phoneNumberSelectFive.IsSelected == true)
            {
                if (isSecondPhoneNumberConfirmed && isThirdPhoneNumberConfirmed && !exceptions.isDuplicated("member", "phonenumber", phoneNumberFirst.Text + phoneNumberSecond.Text + phoneNumberThird.Text))
                {
                    duplicationMessage.Visibility = Visibility.Hidden;
                    phoneNumber.Foreground = Brushes.Black;
                }
            }
            else if (phoneNumberSelectSix.IsSelected == true)
            {
                if (isSecondPhoneNumberConfirmed && isThirdPhoneNumberConfirmed && !exceptions.isDuplicated("member", "phonenumber", phoneNumberFirst.Text + phoneNumberSecond.Text + phoneNumberThird.Text))
                {
                    duplicationMessage.Visibility = Visibility.Hidden;
                    phoneNumber.Foreground = Brushes.Black;
                }
            }
            SatisfiedRequirements();
        }
        //두번째 전화번호 칸 입력 확인 및 중복 값 있는지 체크
        private void phonNumberSecond_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (!Regex.IsMatch(phoneNumberSecond.Text, @"^[1-9]{1}[0-9]{3}$"))
            {
                phoneNumber.Foreground = Brushes.Red;
                isSecondPhoneNumberConfirmed = false;
                createAccount.IsEnabled = false;
            }
            else
            {
                isSecondPhoneNumberConfirmed = true;
                if (isThirdPhoneNumberConfirmed)
                {
                    if (exceptions.isDuplicated("member", "phonenumber", phoneNumberFirst.Text + "-" + phoneNumberSecond.Text + "-" + phoneNumberThird.Text))
                    {
                        phoneNumber.Foreground = Brushes.Red;
                        duplicationMessage.Content = "Phone Number already exists";
                        duplicationMessage.Visibility = Visibility.Visible;
                        createAccount.IsEnabled = false;
                    }
                    else
                    {
                        duplicationMessage.Visibility = Visibility.Hidden;
                        phoneNumber.Foreground = Brushes.Black;
                    }
                }
            }
            SatisfiedRequirements();
        }
        //세번째 전화번호칸 입력 확인 및 중복 값 있는지 체크
        private void phoneNumberThird_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (!Regex.IsMatch(phoneNumberThird.Text, @"^[0-9]{4}$"))
            {
                phoneNumber.Foreground = Brushes.Red;
                isThirdPhoneNumberConfirmed = false;
                createAccount.IsEnabled = false;
            }
            else
            {
                isThirdPhoneNumberConfirmed = true;
                if(isSecondPhoneNumberConfirmed)
                { 
                    if (exceptions.isDuplicated("member", "phonenumber", phoneNumberFirst.Text + "-" + phoneNumberSecond.Text + "-" + phoneNumberThird.Text))
                    {
                        phoneNumber.Foreground = Brushes.Red;
                        duplicationMessage.Content = "Phone Number already exists";
                        duplicationMessage.Visibility = Visibility.Visible;
                        createAccount.IsEnabled = false;
                    }
                    else
                    {
                        duplicationMessage.Visibility = Visibility.Hidden;
                        phoneNumber.Foreground = Brushes.Black;
                    }
                }
            }
            SatisfiedRequirements();
        }
        //주소검색 버튼을 클릭시 주소검색할 수 있는 새창을 띄움
        private void searchAddress_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            window = new Window();
            window.Content = additionalWindow;

            additionalWindow.searchButton.Click += SearchButton_Click;
            
            window.Topmost = true;
            window.Show();
        }
        //주소 API받아와서 검색해서 찾은 값들을 버튼 안에 넣어줌, 버튼을 클릭하면 버튼 Content를 넘겨줌
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string currentPage = "1";                                   //현재 페이지
            string countPerPage = "100";                                //1페이지당 출력 갯수
            string confmKey = "U01TX0FVVEgyMDE3MDUyOTEzNDEwMzIxNTMw";   //테스트 Key
            string keyword = additionalWindow.searchAddress.Text;

            string ServiceUrl = "http://www.juso.go.kr/addrlink/addrLinkApi.do?currentPage=" + currentPage + "&countPerPage=" + countPerPage + "&keyword=" + keyword + "&confmKey=" + confmKey;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(ServiceUrl);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream stream = response.GetResponseStream();
            StreamReader reader = new StreamReader(stream, Encoding.UTF8);
            string text = reader.ReadToEnd();

            XmlDocument xml = new XmlDocument();
            xml.LoadXml(text);

            XmlNodeList nodeList = xml.GetElementsByTagName("juso");

            additionalWindow.addressStackPanel.Children.Clear();

            foreach (XmlNode el in nodeList)
            {
                XmlNode roadAddressNode = el.SelectSingleNode("roadAddr");
                XmlNode jibunAddressNode = el.SelectSingleNode("jibunAddr");
                XmlNode zipNumber = el.SelectSingleNode("zipNo");

                Button addressButton = new Button();
                addressButton.Content = roadAddressNode.InnerText.ToString() + "\n" + jibunAddressNode.InnerText.ToString() + "\n" + zipNumber.InnerText.ToString();
                addressButton.FontSize = 10;
                addressButton.Background = Brushes.White;
                addressButton.Width = 500;
                addressButton.Height = 50;
                addressButton.VerticalAlignment = VerticalAlignment.Top;
                addressButton.HorizontalAlignment = HorizontalAlignment.Left;
                addressButton.VerticalContentAlignment = VerticalAlignment.Top;
                addressButton.HorizontalContentAlignment = HorizontalAlignment.Left;
                additionalWindow.addressStackPanel.Children.Add(addressButton);
                addressButton.Click += AddressButton_Click;
            }
        }
        //주소검색버튼 클릭
        private void AddressButton_Click(object sender, RoutedEventArgs e)
        {
            addressTextbox.Text = (sender as Button).Content.ToString();
            window.Close();
        }
        //상세주소 입력란 형식에 맞게 입력했는지 체크
        private void specificAddressTextbox_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (!Regex.IsMatch(specificAddressTextbox.Text, @"^(?:[^\s]{1}|[^\s]{1}[\d\w\W\s]*[^\s]{1})$") || addressTextbox.Text == String.Empty)
            {
                specificAddress.Foreground = Brushes.Red;
                isAddressConfirmed = false;
                createAccount.IsEnabled = false;
            }
            else
            {
                isAddressConfirmed = true;
                if (addressTextbox.Text != String.Empty)
                    specificAddress.Foreground = Brushes.Black;
            }
            SatisfiedRequirements();
        }
        //입력받는 모든 란을 정확하게 받으면 회원등록 버튼 클릭가능
        public void SatisfiedRequirements()
        {
            if (isNameConfirmed && isFirstResidentNumberConfirmed && isSecondResidentNumberConfirmed && isUsernameConfirmed 
                //&& isFirstEmailConfirmed && isSecondEmailConfirmed
                && email.Foreground == Brushes.Black
                && isPasswordConfirmed && isRePasswordConfirmed && isSecondPhoneNumberConfirmed && isThirdPhoneNumberConfirmed && isAddressConfirmed)
                createAccount.IsEnabled = true;
        }
        //회원등록 버튼 클릭했을 시
        private void createAccount_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Member.MemberNAME = nameTextbox.Text;
            Member.MemberFIRSTRESIDENTNUMBER = firstResidentNumber.Text;
            Member.MemberSECONDRESIDENTNUMBER = secondResidentNumber.Password;
            Member.MemberUSERNAME = usernameTextbox.Text;
            Member.MemberEMAIL = emailFirst.Text + "@" + emailSecond.Text;
            Member.MemberPASSWORD = passwordBox.Password;
            Member.MemberPHONENUMBER = phoneNumberFirst.Text + "-" + phoneNumberSecond.Text + "-" + phoneNumberThird.Text;
            Member.MemberADDRESS = addressTextbox.Text;
            Member.MemberSPECIFICADDRESS = specificAddressTextbox.Text;
            //데이터베이스에 삽입
            database.InsertMember(Member);
        }
        //회원수정 버튼 클릭했을 시
        private void updateAccount_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //MemberVO에 텍스트박스안의 값을 넣어줌
            Member.MemberNAME = nameTextbox.Text;
            Member.MemberFIRSTRESIDENTNUMBER = firstResidentNumber.Text;
            Member.MemberSECONDRESIDENTNUMBER = secondResidentNumber.Password;
            Member.MemberUSERNAME = usernameTextbox.Text;
            Member.MemberEMAIL = emailFirst.Text + "@" + emailSecond.Text;
            Member.MemberPASSWORD = passwordBox.Password;
            Member.MemberPHONENUMBER = phoneNumberFirst.Text + "-" + phoneNumberSecond.Text + "-" + phoneNumberThird.Text;
            Member.MemberADDRESS = addressTextbox.Text;
            Member.MemberSPECIFICADDRESS = specificAddressTextbox.Text;
            //데이터베이스 업데이트
            database.UpdateMember(Member);
        }
        //회원등록페이지에서 취소버튼으로 뒤로 돌아가는 경우 
        private void cancel_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ClearSignUpPage();
        }
        //회원등록하는 페이지 초기화
        public void ClearSignUpPage()
        {
            nameTextbox.Text = String.Empty;
            firstResidentNumber.Text = String.Empty;
            secondResidentNumber.Password = String.Empty;
            usernameTextbox.Text = String.Empty;
            emailFirst.Text = String.Empty;
            emailSecond.Text = String.Empty;
            emailSelectOne.IsSelected = true;
            passwordBox.Password = String.Empty;
            reEnterPasswordBox.Password = String.Empty;
            phoneNumberSelectOne.IsSelected = true;
            phoneNumberSecond.Text = String.Empty;
            phoneNumberThird.Text = String.Empty;
            addressTextbox.Text = String.Empty;
            specificAddressTextbox.Text = String.Empty;

            isNameConfirmed = false;
            isFirstResidentNumberConfirmed = false;
            isSecondResidentNumberConfirmed = false;
            isUsernameConfirmed = false;
            isFirstEmailConfirmed = false;
            isSecondEmailConfirmed = false;
            isPasswordConfirmed = false;
            isRePasswordConfirmed = false;
            isSecondPhoneNumberConfirmed = false;
            isThirdPhoneNumberConfirmed = false;
            isAddressConfirmed = false;

            name.Foreground = Brushes.Black;
            residentNumber.Foreground = Brushes.Black;
            username.Foreground = Brushes.Black;
            email.Foreground = Brushes.Black; ;
            password.Foreground = Brushes.Black;
            rePassword.Foreground = Brushes.Black;
            phoneNumber.Foreground = Brushes.Black;
            address.Foreground = Brushes.Black;
            specificAddress.Foreground = Brushes.Black;
            duplicationMessage.Visibility = Visibility.Hidden;

            createAccount.IsEnabled = false;
        }
        
    }
}
