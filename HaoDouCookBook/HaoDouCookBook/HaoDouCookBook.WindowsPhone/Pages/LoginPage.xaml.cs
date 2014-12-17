using HaoDouCookBook.Common;
using HaoDouCookBook.Controls;
using HaoDouCookBook.Utility;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace HaoDouCookBook.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginPage : BackablePage
    {
        public class LoginPageParams
        {
            public Action SignedInAction { get; set; }

            public LoginPageParams()
            {
                SignedInAction = null;
            }
        }

        public Action SignedInAction { get; set; }

        #region Life Cycle

        public LoginPage()
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

            LoginPageParams paras = e.Parameter as LoginPageParams;
            if(paras != null)
            {
                SignedInAction = paras.SignedInAction;
            }
        }

        #endregion

        #region Event

        private void register_Tapped(object sender, TappedRoutedEventArgs e)
        {
            App.CurrentInstance.RootFrame.Navigate(typeof(SignupPage));
        }

        private async void Login_Click(object sender, RoutedEventArgs e)
        {
            string userName = this.userName.Text.Trim();
            string password = this.userPwd.Password.Trim();

            if (string.IsNullOrEmpty(userName)) 
            {
                toast.Show("请输入注册的邮箱或者手机号码");
                return;
            }

            if (string.IsNullOrEmpty(password))
            {
                toast.Show("请输入您的密码");
                return;
            }

            await UserGlobal.Instance.Login(userName, password, () =>
                {
                    if (SignedInAction != null)
                    {
                        SignedInAction.Invoke();
                    }

                    App.CurrentInstance.RootFrame.GoBack();

                }, error =>
                {
                    toast.Show(error.Message);
                });

        }

        private async void forgetPassword_Tapped(object sender, TappedRoutedEventArgs e)
        {
            await DeviceHelper.OpenHttpURL(Constants.HAODOU_FIND_LOSTPASSWORD);
        }

        #endregion

    }
}
