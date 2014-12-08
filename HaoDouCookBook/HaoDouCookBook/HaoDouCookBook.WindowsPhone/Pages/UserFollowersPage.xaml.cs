using HaoDouCookBook.Common;
using HaoDouCookBook.Controls;
using HaoDouCookBook.HaoDou.API;
using HaoDouCookBook.ViewModels;
using System;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Shared.Utility;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace HaoDouCookBook.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class UserFollowersPage : BackablePage
    {
        #region Page Parameter Definition

        public enum PageType
        { 
            FOLLOW,
            FANS
        }

        public class UserFollowsPageParams
        {
            public int UserId { get; set; }

            public PageType PageType { get; set; }

            public UserFollowsPageParams()
            {

            }
        }

        #endregion

        #region Field && Property

        private UserFollowersPageViewModel viewModel = new UserFollowersPageViewModel();
        private UserFollowsPageParams pageParams;

        #endregion

        #region Lief Cycle

        public UserFollowersPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.NavigationMode == NavigationMode.Back)
            {
                return;
            }

            pageParams = e.Parameter as UserFollowsPageParams;
            if (pageParams != null)
            {
                viewModel = new UserFollowersPageViewModel();
                rootScrollViewer.ScrollToVerticalOffset(0);
                SetPageTypeRelatedItems(pageParams.UserId, pageParams.PageType);
                DataBinding();
                await LoadDataAsync(20, 0, pageParams.UserId);
            }
            
        }
        
        #endregion

        #region Data Prepare

        private void DataBinding()
        {
            this.root.DataContext = viewModel;
        }

        private async Task LoadDataAsync(int offset, int limit, int userid)
        {
            loading.SetState(LoadingState.LOADING);
            int uid = Utilities.GetInt32UserId(UserGlobal.Instance.UserInfo.UserId);

            if (pageParams == null)
            {
                return;
            }

            switch (pageParams.PageType)
            {
                case PageType.FOLLOW:
                    await RecipeUserAPI.GetFollows(offset, limit, userid, uid, UserGlobal.Instance.UserInfo.Sign, data =>
                        {
                            if (data.Followers != null)
                            {
                                foreach (var item in data.Followers)
                                {
                                    viewModel.Followers.Add(new UserFollower() { 
                                        Avatar = item.Avatar,
                                        UserName = item.UserName,
                                        UserId = item.UserId,
                                        CanFollow = item.CanFollow == 1 ? true : false,
                                        IsVip = item.Vip == 1 ? true : false
                                    });
                                }
                            }

                            loading.SetState(LoadingState.SUCCESS);

                        }, error =>
                        {
                            if (Utilities.IsMatchNetworkFail(error.ErrorCode))
                            {
                                loading.RetryAction = async () => await LoadDataAsync(offset, limit, userid);
                            }
                            else
                            {
                                loading.SetState(LoadingState.DONE);
                            }
                        });
                    break;
                case PageType.FANS:
                    await RecipeUserAPI.GetFans(offset, limit, userid, uid, UserGlobal.Instance.UserInfo.Sign, data =>
                    {
                        if (data.Followers != null)
                        {
                            foreach (var item in data.Followers)
                            {
                                viewModel.Followers.Add(new UserFollower()
                                {
                                    Avatar = item.Avatar,
                                    UserName = item.UserName,
                                    UserId = item.UserId,
                                    CanFollow = item.CanFollow == 1 ? true : false,
                                    IsVip = item.Vip == 1 ? true : false
                                });
                            }
                        }

                        loading.SetState(LoadingState.SUCCESS);

                    }, error =>
                    {
                        if (Utilities.IsMatchNetworkFail(error.ErrorCode))
                        {
                            loading.RetryAction = async () => await LoadDataAsync(offset, limit, userid);
                        }
                        else
                        {
                            loading.SetState(LoadingState.DONE);
                        }
                    });
                    break;
                default:
                    break;
            }

        }

        #endregion

        #region Private Methods

        private void SetPageTypeRelatedItems(int userId, PageType pageType)
        {
            switch (pageParams.PageType)
            {
                case PageType.FOLLOW:
                    if (Utilities.IsSignedInUser(pageParams.UserId))
                    {
                        viewModel.Title = "我关注的人";
                        this.noItemText.Text = Constants.I_FOLLOW_NOONE;
                    }
                    else
                    {
                        viewModel.Title = "ta关注的人";
                        this.noItemText.Text = Constants.OTHERUSER_FOLLOW_NOONE;
                    }
                    break;
                case PageType.FANS:
                    if (Utilities.IsSignedInUser(pageParams.UserId))
                    {
                        viewModel.Title = "我的粉丝";
                        this.noItemText.Text = Constants.ONONE_FOLLOW_ME;
                    }
                    else
                    {
                        viewModel.Title = "ta的粉丝";
                        this.noItemText.Text = Constants.NOONE_FOLLOW_OTHERUSER;
                    }
                    break;
                default:
                    break;
            } 
        }

        #endregion

        #region Follow/UnFollow

        private async void Follow_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            var dataContext = sender.GetDataContext<UserFollower>();
            await RecipeUserAPI.Follow(dataContext.UserId, UserGlobal.Instance.GetInt32UserId(), UserGlobal.Instance.UserInfo.Sign, data =>
                {
                    dataContext.CanFollow = false;
                    toast.Show(data.Message);
                }, error =>
                {
                    toast.Show(error.Message);
                });
        }

        private async void CancelFollow_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            var dataContext = sender.GetDataContext<UserFollower>();
            await RecipeUserAPI.UnFollow(dataContext.UserId, UserGlobal.Instance.GetInt32UserId(), UserGlobal.Instance.UserInfo.Sign, data =>
                {
                    dataContext.CanFollow = true;
                    toast.Show(data.Message);
                }, error =>
                {
                    toast.Show(error.Message);
                });
        }

        #endregion
    }
}
