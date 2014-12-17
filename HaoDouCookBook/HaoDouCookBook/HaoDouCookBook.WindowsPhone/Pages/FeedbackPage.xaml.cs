using HaoDouCookBook.Common;
using HaoDouCookBook.Controls;
using HaoDouCookBook.HaoDou.API;
using HaoDouCookBook.Utility;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace HaoDouCookBook.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class FeedbackPage : BackablePage
    {
        public FeedbackPage()
        {
            this.InitializeComponent();
            this.HaouDouServiceCall.Text = Constants.HAODOU_SERVICE_PHONE_NUMBER_STRING;
            this.kfQQ.Text = Constants.HAODOU_QQ;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if(e.NavigationMode == NavigationMode.Back)
            {
                return;
            }


        }

        private async void sendFeedback_Tapped(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(this.feedbackContent.Text.Trim()))
            {
                toast.Show("内容不能为空");
                return;
            }

            await FeedbackAPI.Add(
                this.feedbackContent.Text.Trim(),
                UserGlobal.Instance.GetInt32UserId(),
                UserGlobal.Instance.uuid,
                success => {
                    toast.Show(success.Message);
                },
                error => {
                    toast.Show(error.Message);
                });
        }

        private void PhoneCall_Tapped(object sender, TappedRoutedEventArgs e)
        {
            DeviceHelper.PhoneCall(Constants.HAODOU_SERVICE_PHONE_NUMBER_STRING);
        }

        private void ShowHistory_Tapped(object sender, TappedRoutedEventArgs e)
        {
            App.CurrentInstance.RootFrame.Navigate(typeof(FeebackHistory));
        }
    }
}
