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
                SubType = string.Empty;
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
                            int id = 0;
                            if(!string.IsNullOrEmpty(item.Content.Rid))
                            {
                                id = int.Parse(item.Content.Rid);
                            }
                            else
                            {
                                if(!string.IsNullOrEmpty(item.Content.Pid))
                                {
                                    id = int.Parse(item.Content.Pid);
                                }
                            }

                            viewModel.Notices.Add(new NoticeItem() { 
                                Avatar = item.Avatar,
                                Content = content,
                                Time = item.Time,
                                Type = item.Type,
                                UserId = item.Uid,
                                UserName = item.UserName,
                                ContentId = id
                            });
                        }
                    }

                    loading.SetState(LoadingState.SUCCESS);

                }, error => {
                    Utilities.CommonLoadingRetry(loading, error, async () => await LoadFirstPageDataAsync(subType));
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
                    CommentListPage.CommentListPageParams paras3 = new CommentListPage.CommentListPageParams();
                    paras3.RecipeId = dataContext.ContentId;
                    paras3.Type = 0;
                    paras3.Cid = 0;
                    paras3.SourcePage = CommentListPage.SourcePage.NOTICE_PAGE;

                    App.Current.RootFrame.Navigate(typeof(CommentListPage), paras3);

                    break;
                case 101:
                    CommentListPage.CommentListPageParams paras101 = new CommentListPage.CommentListPageParams();
                    paras101.RecipeId = dataContext.ContentId;
                    paras101.Type = 2;
                    paras101.Cid = 0;
                    paras101.SourcePage = CommentListPage.SourcePage.NOTICE_PAGE;
                    App.Current.RootFrame.Navigate(typeof(CommentListPage), paras101);
                    break;
                case 103:
                    SingleProductViewPage.SingleProductViewPageParams paras103 = new SingleProductViewPage.SingleProductViewPageParams();
                    paras103.ProductId = dataContext.ContentId;
                    App.Current.RootFrame.Navigate(typeof(SingleProductViewPage), paras103);
                    break;
                default:
                    break;
            }
        }

        #endregion
    }
}
