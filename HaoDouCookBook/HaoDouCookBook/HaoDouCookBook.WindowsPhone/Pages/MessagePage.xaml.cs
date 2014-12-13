using HaoDouCookBook.Common;
using HaoDouCookBook.Controls;
using HaoDouCookBook.HaoDou.API;
using HaoDouCookBook.ViewModels;
using System.Threading.Tasks;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace HaoDouCookBook.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MessagePage : BackablePage
    {
        #region Field && Proprety

        private MessagePageViewModel viewModel = new MessagePageViewModel();

        #endregion

        #region Life Cycle

        public MessagePage()
        {
            this.InitializeComponent();
            DataBinding();
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

            this.rootScrollViewer.ScrollToVerticalOffset(0);
            LoadFirstDataAysnc();
        }

        #endregion

        #region Data Prepare

        private void DataBinding()
        {
            this.root.DataContext = viewModel;
        }

        private async Task LoadFirstDataAysnc()
        {
            loading.SetState(LoadingState.LOADING);
            await MessageAPI.GetListByUid(0, 20, UserGlobal.Instance.GetInt32UserId(), UserGlobal.Instance.uuid, UserGlobal.Instance.UserInfo.Sign, data =>
                {
                    if (data.Notices != null && data.Notices.Count > 0)
                    {
                        viewModel.NoticeContent = data.Notices[0].Content;
                        viewModel.SubType = data.Notices[0].SubType;
                    }

                    if (data.Messages != null)
                    {
                        viewModel.Messages.Clear();
                        foreach (var item in data.Messages)
                        {
                            viewModel.Messages.Add(new Message() {
                                Avatar = item.Avatar,
                                Content = item.LastMsg,
                                UnreadCount = item.UnreadCount,
                                UpdateTime = item.UpdateTime,
                                UserName = item.UserName,
                                UserId = item.UserId
                            });
                        }
                    }

                    loading.SetState(LoadingState.SUCCESS);
                    
                }, error => {
                    Utilities.CommonLoadingRetry(loading, error, async () => await LoadFirstDataAysnc());
                });
        }

        #endregion

        private void Notice_Tapped(object sender, TappedRoutedEventArgs e)
        {
            NoticeDetailPage.NoticeDetailPageParams paras = new NoticeDetailPage.NoticeDetailPageParams();
            App.Current.RootFrame.Navigate(typeof(NoticeDetailPage), paras);
        }
    }
}
