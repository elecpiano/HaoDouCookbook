using HaoDouCookBook.Common;
using HaoDouCookBook.Controls;
using HaoDouCookBook.HaoDou.API;
using HaoDouCookBook.HaoDou.DataModels.My;
using HaoDouCookBook.ViewModels;
using System.Threading.Tasks;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;
using ViewModes = HaoDouCookBook.ViewModels;
using Shared.Utility;
// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace HaoDouCookBook.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class FriendsPage : BackablePage
    {
        #region Field && Property

        private FriendsPageViewModel viewModel = new FriendsPageViewModel();

        #endregion

        #region Life Cycle

        public FriendsPage()
        {
            this.InitializeComponent();
            DataBinding();
            this.NoItemsText.Text = "";
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            rootScrollViewer.ScrollToVerticalOffset(0);
            viewModel = new FriendsPageViewModel();
            DataBinding();
            LoadFirstPageDataAsync();
        }

        #endregion

        #region Data Prepare

        private void DataBinding()
        {
            this.root.DataContext = viewModel;
        }

        private async Task LoadFirstPageDataAsync()
        {
            loading.SetState(LoadingState.LOADING);
            await RecipeUserAPI.GetFriendList(0, limit, UserGlobal.Instance.GetInt32UserId(), UserGlobal.Instance.UserInfo.Sign, data =>
                {
                    UpateData(data);
                    page = 1;
                    loading.SetState(LoadingState.SUCCESS);

                }, error => {
                    Utilities.CommonLoadingRetry(loading, error, async () => await LoadFirstPageDataAsync());
                });
        }

        public void UpateData(FriendsData data)
        {
            viewModel.AllFansCount = data.AllFansCount;
            viewModel.FansCount = data.FansCount;
            if (data.Info != null)
            {
                viewModel.Info.Avatar = data.Info.Avatar;
                viewModel.Info.IsVip = data.Info.Vip == 1 ? true : false;
                viewModel.Info.UserId = data.Info.UserId;
                viewModel.Info.UserName = data.Info.UserName;
            }

            if (data.FriendsNameCategories != null)
            {
                RemoveLoadMoreControl();
                foreach (var item in data.FriendsNameCategories)
                {
                    ViewModels.FriendsNameCategory category = new ViewModels.FriendsNameCategory();
                    category.FirstWord = item.FirstWord;
                    if (item.Friends != null)
                    {
                        foreach (var f in item.Friends)
                        {
                            category.Friends.Add(new ViewModes.Friend() { 
                                Avatar = f.Avatar,
                                Description = f.VipDesc,
                                IsVip = f.Vip == 1 ? true : false,
                                UserId = f.UserId,
                                UserName = f.UserName
                            });
                        }
                    }

                    viewModel.FriendsNameCategories.Add(category);
                }

                if (data.FriendsNameCategories.Length == limit)
                {
                    EnusureLoadMoreControl();
                }
            }
        }

        #endregion

        #region Event

        private void InfoPanel_Tapped(object sender, TappedRoutedEventArgs e)
        {
            UserFollowersPage.UserFollowsPageParams paras = new UserFollowersPage.UserFollowsPageParams();
            paras.UserId = UserGlobal.Instance.GetInt32UserId();
            paras.PageType = UserFollowersPage.PageType.FANS;

            App.Current.RootFrame.Navigate(typeof(UserFollowersPage), paras);
            e.Handled = true;
        }
        private void Friend_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var dataContext = sender.GetDataContext<ViewModels.Friend>();
            UserProfilePage.UserProfilePageParams paras = new UserProfilePage.UserProfilePageParams();
            paras.UserId = dataContext.UserId;

            App.Current.RootFrame.Navigate(typeof(UserProfilePage), paras);
        }

        #endregion

        #region Load More

        int page = 1;
        int limit = 20;

        private ViewModels.FriendsNameCategory loadMoreControlDataContext = new ViewModels.FriendsNameCategory() { IsLoadMore = true };

        public void EnusureLoadMoreControl()
        {
            if (viewModel.FriendsNameCategories != null && !viewModel.FriendsNameCategories.Contains(loadMoreControlDataContext))
            {
                viewModel.FriendsNameCategories.Add(loadMoreControlDataContext);
            }
        }

        public void RemoveLoadMoreControl()
        {
            if (viewModel.FriendsNameCategories != null && viewModel.FriendsNameCategories.Contains(loadMoreControlDataContext))
            {
                viewModel.FriendsNameCategories.Remove(loadMoreControlDataContext);
            }
        }

        private void loadMore_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Utilities.RegistComonadLoadMoreBehavior(sender,
                 async loadmore =>
                 {
                     loadmore.SetState(LoadingState.LOADING);
                     await RecipeUserAPI.GetFriendList(
                         page * limit,
                         limit,
                         UserGlobal.Instance.GetInt32UserId(),
                         UserGlobal.Instance.UserInfo.Sign,
                         success =>
                         {
                             if (success.FriendsNameCategories != null)
                             {
                                 RemoveLoadMoreControl();
                                 foreach (var item in success.FriendsNameCategories)
                                 {
                                     ViewModels.FriendsNameCategory category = new ViewModels.FriendsNameCategory();
                                     category.FirstWord = item.FirstWord;
                                     if (item.Friends != null)
                                     {
                                         foreach (var f in item.Friends)
                                         {
                                             category.Friends.Add(new ViewModes.Friend()
                                             {
                                                 Avatar = f.Avatar,
                                                 Description = f.VipDesc,
                                                 IsVip = f.Vip == 1 ? true : false,
                                                 UserId = f.UserId,
                                                 UserName = f.UserName
                                             });
                                         }
                                     }

                                     viewModel.FriendsNameCategories.Add(category);
                                 }

                                 if (success.FriendsNameCategories.Length == limit)
                                 {
                                     EnusureLoadMoreControl();
                                 }
                                 page++;
                                 loadmore.SetState(LoadingState.SUCCESS);
                             }
                             else
                             {
                                 loadmore.SetState(LoadingState.DONE);
                             }
                        },
                        error =>
                        {
                            Utilities.CommondLoadMoreErrorBehavoir(loadmore, error);
                        });
                });
        }

        #endregion
    }
}
