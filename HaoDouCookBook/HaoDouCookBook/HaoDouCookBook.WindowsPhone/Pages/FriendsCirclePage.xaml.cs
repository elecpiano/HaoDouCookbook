using HaoDouCookBook.Common;
using HaoDouCookBook.Controls;
using HaoDouCookBook.HaoDou.API;
using HaoDouCookBook.ViewModels;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;
using Shared.Utility;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace HaoDouCookBook.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class FriendsCirclePage : BackablePage
    {
        #region Field && Property

        public ObservableCollection<UserActivityItem> Activities { get; set; }

        #endregion

        #region Life Cycle

        public FriendsCirclePage()
        {
            this.InitializeComponent();
            DataBinding();
            Activities = new ObservableCollection<UserActivityItem>();
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

            rootScrollViewer.ScrollToVerticalOffset(0);
            DataBinding();
            LoadFirstPageDataAsync();
        }

        #endregion

        #region Data Prepare

        private void DataBinding()
        {
            this.root.DataContext = this;
            this.noItemsPanel.DataContext = Activities;
        }

        private async Task LoadFirstPageDataAsync()
        {
            loading.SetState(LoadingState.LOADING);
            await UserFeedAPI.GetFollowUserFeed(0, 10, null, UserGlobal.Instance.GetInt32UserId(), UserGlobal.Instance.UserInfo.Sign, 
                success =>
                {
                    if (success.Activities != null)
                    {
                        Activities.Clear();
                        foreach (var item in success.Activities)
                        {
                            Activities.Add(new UserActivityItem() {
                                Content = item.Content,
                                CreateTime = item.CreateTime,
                                DiggCount = item.DiggCnt,
                                Image = item.Pic,
                                Name = item.Name,
                                ProductId = item.ItemId,
                                Type = item.Type,
                                UserId = item.UserId,
                                Avatar = item.Avatar,
                                ActivityId = item.FeedId,
                                IsDigg = item.IsDigg == 1 ? true : false
                            });
                        }

                    }

                    loading.SetState(LoadingState.SUCCESS);

                }, error =>
                {
                    if (Utilities.IsMatchNetworkFail(error.ErrorCode))
                    {
                        loading.RetryAction = async () => await LoadFirstPageDataAsync();
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

        private void ActivitySupportAndComment_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            ActivitySupportAndComment asac = sender as ActivitySupportAndComment;
            if (asac != null)
            {
                asac.Toast = this.toast;
            }
        }

        private void RecipeOrProduct_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var dataContext = sender.GetDataContext<UserActivityItem>();
            switch (dataContext.Type)
            {
                case 10:
                    RecipeInfoPage.RecipeInfoPageParams para = new RecipeInfoPage.RecipeInfoPageParams();
                    para.RecipeId = dataContext.ProductId;

                    App.Current.RootFrame.Navigate(typeof(RecipeInfoPage), para);
                    break;
                case 30:
                    SingleProductViewPage.SingleProductViewPageParams p = new SingleProductViewPage.SingleProductViewPageParams();
                    p.ProductId = dataContext.ProductId;

                    App.Current.RootFrame.Navigate(typeof(SingleProductViewPage), p);
                    break;
                default:
                    break;
            }
        }

        #endregion
    }
}
