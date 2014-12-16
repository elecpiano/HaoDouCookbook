using HaoDouCookBook.Common;
using HaoDouCookBook.Controls;
using HaoDouCookBook.HaoDou.API;
using HaoDouCookBook.Utility;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace HaoDouCookBook.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SignupPage : BackablePage
    {
        public SignupPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.NavigationMode == NavigationMode.Back)
            {
                return;
            }

            this.phoneRegStepOne.Visibility = Windows.UI.Xaml.Visibility.Visible;
            this.phoneRegStepTwo.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            this.emailAddress.Text = string.Empty;
            this.phone.Text = string.Empty;
            this.passowrd.Password = string.Empty;
            this.nick.Text = string.Empty;
        }

        #region Event

        private void GotoLogin_Tapped(object sender, TappedRoutedEventArgs e)
        {
            App.Current.RootFrame.Navigate(typeof(LoginPage));
        }


        #endregion

        #region By Email
        private async void registByEmail_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckEmail() || !CheckNickName(this.nick.Text.Trim()) || !CheckPassword(this.passowrd.Password.Trim()))
            {
                return;
            }

            await AccountAPI.RegByEmail(this.emailAddress.Text.Trim(), this.nick.Text.Trim(), this.passowrd.Password.Trim(),
                succes =>
                {
                    if (succes.UserId != null && !string.IsNullOrEmpty(succes.Sign))
                    {
                        UserGlobal.Instance.UserInfo = succes;
                        UserGlobal.Instance.CommitDataAsync();

                        MainPage.MainPageParams paras = new MainPage.MainPageParams();
                        paras.PivotIndex = 3;
                        App.Current.RootFrame.Navigate(typeof(MainPage), paras);
                    }
                },
                error =>
                {
                    toast.Show(error.Message);
                });
        }

        private bool CheckPassword(string pwd)
        {
            if (pwd.Length < 6 || pwd.Length > 32)
            {
                toast.Show(Constants.ERRORMESSAGE_INVALID_PASSWORD_LENGTH);
                return false;
            }

            return true;
        }

        private bool CheckNickName(string nickName)
        {
            if (nickName.Length < 4 || nickName.Length > 16)
            {
                toast.Show(Constants.ERRORMESSAGE_INVALID_NICKNAME_LENGTH);
                return false;
            }

            var invalidChars = "!#$%^&*()-+={}[]<>/,.'\"\\`".ToCharArray();

            if (nickName.Any(nickChar => invalidChars.Any(invaidChar => invalidChars.Equals(nickChar))))
            {
                toast.Show(Constants.ERRORMESSAGE_INVALID_NICKNAME_CHARS);
                return false;
            }

            return true;
        }

        private bool CheckEmail()
        {
            string email = this.emailAddress.Text.Trim();
            string emailRegexPattern = @"\s*([A-Za-z0-9_-]+(\.\w+)*@([\w-]+\.)+\w{2,10})\s*$";
            if (string.IsNullOrEmpty(email)
               || !Regex.IsMatch(email, emailRegexPattern))
            {

                toast.Show(Constants.ERRORMESSAGE_INVALID_EMAIL);
                return false;
            }

            return true;
        }

        #endregion

        #region By Phone

        private bool CheckPhoneNumber()
        {
            string phonePattern = "^(13[0-9]|15[012356789]|18[0236789]|14[57])[0-9]{8}$";
            string phone = this.phone.Text.Trim();
            return Regex.IsMatch(phone, phonePattern);
        }

        private void GotoPhoneRegStepTwo_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (!CheckPhoneNumber())
            {
                toast.Show(Constants.ERRORMESSAGE_INVALIED_PHONE);
                return;
            }

            SendVerifyCode();
        }

        private async void SendVerifyCode()
        {
            await CommonAPI.SendCode(this.phone.Text.Trim(), DeviceHelper.GetUniqueDeviceID(),
                success => { 
                    if(success.Message.Contains("成功"))
                    {
                        toast.Show(Constants.PHONE_CODE_HAVE_SENT);
                        App.Current.RunAsync(() => {
                            this.phoneRegStepOne.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                            this.phoneRegStepTwo.Visibility = Windows.UI.Xaml.Visibility.Visible;
                        });
                        StartResendVerifyCodeAnimation();
                    }
                },
                error => {
                    toast.Show(error.Message);
                });
        }

        private int count = 59;
        private void StartResendVerifyCodeAnimation()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += (s, e) => 
            {
                if(count >= 0)
                {
                    retryText.Text = string.Format("重新获取({0})", count);
                    count--;
                }
                if(count == 0)
                {
                    var t = s as DispatcherTimer;
                    if(t != null)
                    {
                        t.Stop();
                        count = 59;
                        this.resendVerifyCodeButton.IsHitTestVisible = true;
                    }
                }
            };

            this.resendVerifyCodeButton.IsHitTestVisible = false;
            timer.Start();
        }

        private void ReSendVerifyCode_Tapped(object sender, TappedRoutedEventArgs e)
        {
            SendVerifyCode();
        }
        private async void RegByPhone_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (!CheckNickName(this.phoneNick.Text.Trim()) || !CheckPassword(this.phonePassword.Password.Trim()))
            {
                return;
            }

            await AccountAPI.RegByPhone(
                this.phone.Text.Trim(),
                this.verifyCode.Text.Trim(),
                this.phoneNick.Text.Trim(),
                this.phonePassword.Password.Trim(),
                succes =>
                {
                    UserGlobal.Instance.UserInfo = succes;
                    UserGlobal.Instance.CommitDataAsync();

                    MainPage.MainPageParams paras = new MainPage.MainPageParams();
                    paras.PivotIndex = 3;
                    App.Current.RootFrame.Navigate(typeof(MainPage), paras);
                },
                error =>
                {
                    toast.Show(error.Message);
                });
        }

        #endregion

      

        
    }
}
