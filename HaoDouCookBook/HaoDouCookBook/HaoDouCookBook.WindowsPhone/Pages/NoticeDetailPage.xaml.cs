using HaoDouCookBook.Common;
using HaoDouCookBook.Controls;
using HaoDouCookBook.HaoDou.API;
using HaoDouCookBook.ViewModels;
using System.Threading.Tasks;
using Windows.UI.Xaml.Navigation;
using Shared.Utility;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace HaoDouCookBook.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NoticeDetailPage : BackablePage
    {
        #region Page Parmeters Definition

        public class NoticeDetailPageParams
        {
            public string SubType { get; set; }

            public NoticeDetailPageParams()
            {

            }
        }

        #endregion

        #region Field && Property

        private NoticePageViewModel viewModel = new NoticePageViewModel();
        private NoticeDetailPageParams pageParams = new NoticeDetailPageParams();

        #endregion

        #region Life Cycle

        public NoticeDetailPage()
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

            pageParams = e.Parameter as NoticeDetailPageParams;
            if (pageParams != null)
            {
                viewModel = new NoticePageViewModel();
                rootScrollViewer.ScrollToVerticalOffset(0);
                DataBinding();
                LoadFirstPageDataAsync(pageParams.SubType);
            }
        }

        #endregion

        #region Data Prepare

        private void DataBinding()
        {
            this.root.DataContext = viewModel;
        }

        private async Task LoadFirstPageDataAsync(string subType)
        {
            loading.SetState(LoadingState.LOADING);
            await NoticeAPI.GetUserNotice(0, 20, UserGlobal.Instance.GetInt32UserId(), UserGlobal.Instance.uuid, 0, subType, UserGlobal.Instance.UserInfo.Sign,
                data => {

                    if (data.Notices != null)
                    {
                        viewModel.Notices.Clear();
                        foreach (var item in data.Notices)
                        {
                            string content = "回复了您，快去看看吧！";
                            int recpieId = 0;
                            if(!string.IsNullOrEmpty(item.Content.Rid))
                            {
                                recpieId = int.Parse(item.Content.Rid);
                            }

                            viewModel.Notices.Add(new NoticeItem() { 
                                Avatar = item.Avatar,
                                Content = content,
                                Time = item.Time,
                                Type = item.Type,
                                UserId = item.Uid,
                                UserName = item.UserName,
                                ContentId = recpieId
                            });
                        }
                    }

                    loading.SetState(LoadingState.SUCCESS);

                }, error => {
                    if (Utilities.IsMatchNetworkFail(error.ErrorCode))
                    {
                        loading.RetryAction = async () => await LoadFirstPageDataAsync(subType);
                        loading.SetState(LoadingState.NETWORK_UNAVAILABLE);
                    }
                    else
                    {
                        loading.SetState(LoadingState.DONE);
                    }
                });
        }


        #endregion

        #region Event

        private void Notice_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            var dataContext = sender.GetDataContext<NoticeItem>();
            switch (dataContext.Type)
            {
                case 3:
                    CommentListPage.CommentListPageParams paras = new CommentListPage.CommentListPageParams();
                    paras.RecipeId = dataContext.ContentId;
                    paras.Type = 0;
                    paras.Cid = 0;
                    paras.SourcePage = CommentListPage.SourcePage.NOTICE_PAGE;

                    App.Current.RootFrame.Navigate(typeof(CommentListPage), paras);

                    break;
                default:
                    break;
            }
        }

        #endregion
    }
}
