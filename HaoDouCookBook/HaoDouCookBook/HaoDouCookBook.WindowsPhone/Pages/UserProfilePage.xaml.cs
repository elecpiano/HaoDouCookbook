using HaoDouCookBook.Common;
using HaoDouCookBook.Controls;
using HaoDouCookBook.ViewModels;
using System;
using System.Threading.Tasks;
using Windows.UI.Xaml.Navigation;
using HaoDouCookBook.HaoDou.API;
using HaoDouCookBook.Utility;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace HaoDouCookBook.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class UserProfilePage : BackablePage
    {
        #region Page Parameter Definition

        public class UserProfilePageParams
        {
            public int UserId { get; set; }

            public UserProfilePageParams()
            {
                UserId = 0;
            }
        }

        #endregion

        #region Field && Proprety

        private UserProfilePageViewModel viewModel = new UserProfilePageViewModel();
        private UserProfilePageParams pageParams;

        #endregion

        #region Life Cycle

        public UserProfilePage()
        {
            this.InitializeComponent();
            InitUserInfoSummary();
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
                UpdateSummaryInfo();
                return;
            }

            pageParams = e.Parameter as UserProfilePageParams;
            if (pageParams != null)
            {
                Init(pageParams);
            }
        }

        #endregion

        #region Data Prepare

        private void DataBinding()
        {
            this.root.DataContext = viewModel;
        }

        private async Task UpdateSummaryInfo()
        {
            await RecipeUserAPI.GetUserInfo(viewModel.UserId, UserGlobal.Instance.GetInt32UserId(), UserGlobal.Instance.UserInfo.Sign, data =>
                {
                    viewModel.FollowCount = data.SummaryInfo.FollowCnt;
                    viewModel.FansCount = data.SummaryInfo.FansCount;
                    viewModel.Coin = data.SummaryInfo.Wealth;
                    viewModel.UserAvatar = data.SummaryInfo.Avatar;
                    viewModel.UserIntro = string.IsNullOrEmpty(data.SummaryInfo.Intro) ? Constants.DEFAULT_USER_INTRO : data.SummaryInfo.Intro;
                    viewModel.UserName = data.SummaryInfo.UserName;
                    viewModel.CanFollow = data.SummaryInfo.CanFollow == 1 ? true : false;
                    viewModel.CheckIn = data.SummaryInfo.CheckIn;

                    if (Utilities.IsSignedInUser(data.SummaryInfo.UserId))
                    {
                        UserGlobal.Instance.UpdateUserInfoBySummary(data.SummaryInfo);
                    };

                }, error => { });
        }

        private async Task UpdateProductsData(int offset, int limit, int userId)
        {
            await RecipePhotoAPI.GetList(offset, limit, userId, UserGlobal.Instance.GetInt32UserId(), UserGlobal.Instance.UserInfo.Sign,
                data =>
                {
                    if (data.Products != null)
                    {
                        foreach (var item in data.Products)
                        {
                            viewModel.Products.Add(new UserProduct()
                            {
                                Id = item.RecipePhotoId,
                                Cover = item.PhotoUrl
                            });
                        }
                    }

                }, error => { });
        }

        private async Task UpdateRecipes(int offset, int limit, int userId)
        {
            await RecipeUserAPI.GetUserRecipeList(offset, limit, userId, UserGlobal.Instance.GetInt32UserId(), data =>
                {
                    if (data.Recipes != null)
                    {
                        foreach (var item in data.Recipes)
                        {
                            viewModel.Recipes.Add(new UserRecipe()
                            {
                                Id = item.Rid,
                                Cover = item.Cover
                            });
                        }
                    }

                }, error => { });
        }

        private async Task LoadDataAsync()
        {
            loading.SetState(LoadingState.LOADING);

            int uid = Utilities.GetInt32UserId(UserGlobal.Instance.UserInfo.UserId);

            await RecipeUserAPI.GetUserInfo(pageParams.UserId, uid, UserGlobal.Instance.UserInfo.Sign, data =>
            {
                if (pageParams != null)
                {
                    viewModel.UserId = pageParams.UserId;
                }

                if (data.SummaryInfo != null)
                {
                    viewModel.FollowCount = data.SummaryInfo.FollowCnt;
                    viewModel.FansCount = data.SummaryInfo.FansCount;
                    viewModel.Coin = data.SummaryInfo.Wealth;
                    viewModel.UserAvatar = data.SummaryInfo.Avatar;
                    viewModel.UserIntro = string.IsNullOrEmpty(data.SummaryInfo.Intro) ? Constants.DEFAULT_USER_INTRO : data.SummaryInfo.Intro;
                    viewModel.UserName = data.SummaryInfo.UserName;
                    viewModel.CanFollow = data.SummaryInfo.CanFollow == 1 ? true : false;
                    viewModel.CheckIn = data.SummaryInfo.CheckIn;

                    if (Utilities.IsSignedInUser(data.SummaryInfo.UserId))
                    {
                        UserGlobal.Instance.UpdateUserInfoBySummary(data.SummaryInfo);
                    }
                }

                loading.SetState(LoadingState.SUCCESS);

            }, error =>
            {
                if (Utilities.IsMatchNetworkFail(error.ErrorCode))
                {
                    DelayHelper.Delay(TimeSpan.FromSeconds(0.7), () =>
                    {
                        loading.RetryAction = async () => await LoadDataAsync();
                        loading.SetState(LoadingState.NETWORK_UNAVAILABLE);
                    });
                }
                else
                {
                    loading.SetState(LoadingState.DONE);
                }
            });
        }

        #endregion

        #region Private method

        private void InitNoItemsImageAndText(int userId)
        {
            string noProductsImage = "ms-appx:///../assets/images/user_noproducts.png";
            this.userProducts1.NoItemsImage = noProductsImage;
            this.userProducts2.NoItemsImage = noProductsImage;

            string noRecipesImage = "ms-appx:///../assets/images/user_norecipes.png";
            this.userRecipes1.NoItemsImage = noRecipesImage;
            this.userRecipes2.NoItemsImage = noRecipesImage;

            if (Utilities.IsSignedInUser(userId))
            {
                this.userProducts1.NoItemsText = Constants.I_DONT_HAVE_PRODUCTS;
                this.userProducts2.NoItemsText = Constants.I_DONT_HAVE_PRODUCTS;

                this.userRecipes1.NoItemsText = Constants.I_DONT_HAVE_RECIPES;
                this.userRecipes2.NoItemsText = Constants.I_DONT_HAVE_RECIPES;

                this.userActivities1.NoItemsText = Constants.I_DONT_HAVE_ACTIVITIES;
                this.userActivities2.NoItemsText = Constants.I_DONT_HAVE_ACTIVITIES;
            }
            else
            {
                this.userProducts1.NoItemsText = Constants.OTHERUSER_DONT_HAVE_PRODUCTS;
                this.userProducts2.NoItemsText = Constants.OTHERUSER_DONT_HAVE_PRODUCTS;

                this.userRecipes1.NoItemsText = Constants.OTHERUSER_DONT_HAVE_RECIPES;
                this.userRecipes2.NoItemsText = Constants.OTHERUSER_DONT_HAVE_RECIPES;

                this.userActivities1.NoItemsText = Constants.OTHERUSER_DONT_HAVE_ACTIVITIES;
                this.userActivities2.NoItemsText = Constants.OTHERUSER_DONT_HAVE_ACTIVITIES;
            }
        }

        private void InitUserInfoSummary()
        {
            this.otherUserProfile.FollowAction = Follow;
            this.otherUserProfile.UnFollowAction = UnFollow;
            this.myProfileSummary.CheckInAction = CheckIn;
        }

        

        private void Init(UserProfilePageParams paras)
        {
            viewModel = new UserProfilePageViewModel();
            DataBinding();
            InitNoItemsImageAndText(pageParams.UserId);
            if (Utilities.IsSignedInUser(pageParams.UserId))
            {
                this.pivoitMe.SelectedIndex = 0;
                userActivities2.LoadFirstDataAsync(pageParams.UserId);
            }
            else
            {
                this.pivotOtherUser.SelectedIndex = 0;
                this.userActivities1.LoadFirstDataAsync(pageParams.UserId);
            }

            this.userActivities1.ResetScrollViewerToBegin();
            this.userActivities2.ResetScrollViewerToBegin();
            this.userProducts1.ResetScrollViewerToBegin();
            this.userProducts2.ResetScrollViewerToBegin();
            this.userRecipes1.ResetScrollViewerToBegin();
            this.userRecipes2.ResetScrollViewerToBegin();
            this.rootScrollViewer.ScrollToVerticalOffset(0);
            UpdateProductsData(0, 21, pageParams.UserId);
            UpdateRecipes(0, 21, pageParams.UserId);
            LoadDataAsync();
        }

        #endregion

        #region Follow/UnFollow/Checkin

        private async void UnFollow(UserProfileSummary userSummary)
        {
            await RecipeUserAPI.UnFollow(userSummary.UserId, UserGlobal.Instance.GetInt32UserId(), UserGlobal.Instance.UserInfo.Sign, data =>
            {
                viewModel.CanFollow = true;
                toast.Show(data.Message);

            }, error =>
            {
                toast.Show(error.Message);
            });
        }

        private async void Follow(UserProfileSummary userSummary)
        {
            await RecipeUserAPI.Follow(userSummary.UserId, UserGlobal.Instance.GetInt32UserId(), UserGlobal.Instance.UserInfo.Sign, data =>
            {
                viewModel.CanFollow = false;
                toast.Show(data.Message);

            }, error =>
            {
                toast.Show(error.Message);
            });
        }

        private async void CheckIn(UserProfileSummary obj)
        {
            await AccountAPI.Checkin(UserGlobal.Instance.GetInt32UserId(), UserGlobal.Instance.UserInfo.Sign, data =>
            {
                if (data.Message == "签到成功")
                {
                    viewModel.CheckIn = true;
                    viewModel.Coin = data.Wealth;
                }

                toast.Show(data.Message);
            }, error => {
                toast.Show(error.Message);
            });
        }

        #endregion

        #region Event

        private async void Pivot_SelectionChanged(object sender, Windows.UI.Xaml.Controls.SelectionChangedEventArgs e)
        {
            if (pageParams == null)
            {
                return;
            }

            Pivot pivot = sender as Pivot;
            if (pivot != null)
            {
                switch (pivot.SelectedIndex)
                {
                    case 0:  //动态
                        if (pageParams != null)
                        {
                            if (Utilities.IsSignedInUser(pageParams.UserId))
                            {
                                await this.userActivities2.LoadFirstDataAsync(pageParams.UserId);
                            }
                            else
                            {
                                await this.userActivities1.LoadFirstDataAsync(pageParams.UserId);
                            }
                        }
                        break;
                    case 1:  //作品
                        break;
                    case 2:  //菜谱
                        break;
                    case 3: //草稿
                        break;
                    default:
                        break;
                }
            }
        }

        #endregion
    }
}
