using HaoDouCookBook.Common;
using HaoDouCookBook.Controls;
using HaoDouCookBook.Utility;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace HaoDouCookBook.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AboutPage : BackablePage
    {
        #region Life Cycle

        public AboutPage()
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

            rootScrollViewer.ChangeViewExtersion(0, 0, 1.0f);
            this.intro.Text = Constants.HAODOU_INTRODUCTION;
            this.HaoDouWebSite.Text = Constants.HAODOU_WEBSITE;
            this.HaouDouServiceCall.Text = Constants.HAODOU_SERVICE_PHONE_NUMBER_STRING;
            this.QQ.Text = Constants.HAODOU_QQ;
            this.SinaWeibo.Text = Constants.HAODOU_SINA_WEIBO;
            this.TencentWeiBo.Text = Constants.HAODOU_QQ_WEIBO;
        }

        #endregion

        #region Event

        private async void OpenWebSite_Tapped(object sender, TappedRoutedEventArgs e)
        {
            await DeviceHelper.OpenHttpURL(Constants.HAODOU_WEBSITE);
        }

        private void PhoneCall_Tapped(object sender, TappedRoutedEventArgs e)
        {
            string number = Constants.HAODOU_SERVICE_PHONE_NUMBER_STRING.Replace("-", "");
            DeviceHelper.PhoneCall(number);
        }

        private async void QQWeiBo_Tapped(object sender, TappedRoutedEventArgs e)
        {
            await DeviceHelper.OpenHttpURL(Constants.HAODOU_QQ_WEIBO);
        }

        private async void SinaWeiBo_Tapped(object sender, TappedRoutedEventArgs e)
        {
            await DeviceHelper.OpenHttpURL(Constants.HAODOU_SINA_WEIBO);
        }

        #endregion
    }
}
