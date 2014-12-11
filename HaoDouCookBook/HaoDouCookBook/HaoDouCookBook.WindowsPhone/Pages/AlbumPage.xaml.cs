using HaoDouCookBook.Common;
using HaoDouCookBook.Controls;
using HaoDouCookBook.HaoDou.API;
using HaoDouCookBook.HaoDou.DataModels.ChoicenessPage;
using HaoDouCookBook.Utility;
using HaoDouCookBook.ViewModels;
using Shared.Utility;
using System;
using System.Threading.Tasks;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace HaoDouCookBook.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AlbumPage : BackablePage
    {
        #region Page Parameters Definition 

        public class AlbumPageParams
        {
            public int AlbumId { get; set; }

            public AlbumPageParams()
            {
                AlbumId = -1;
            }
 
        }

        #endregion

        #region Field && Property

        private AlbumPageViewModel viewModel = new AlbumPageViewModel();
        private AlbumPageParams pageParams;

        #endregion

        #region Life Cycle

        public AlbumPage()
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

            pageParams = e.Parameter as AlbumPageParams;
            if (pageParams != null)
            {
                viewModel = new AlbumPageViewModel();
                rootScrollViewer.ScrollToVerticalOffset(0);
                DataBinding();
                LoadFirstPageDataAsync(pageParams.AlbumId);
            }
        }
       

        #endregion

        #region Data Prepare
        private void DataBinding()
        {
            this.root.DataContext = viewModel;
            this.cmd.DataContext = viewModel;
        }

        private async Task LoadFirstPageDataAsync(int albumId)
        {
            // show loading
            //
            loading.SetState(LoadingState.LOADING);
            await InfoAPI.GetAlbumInfo(0, 20, albumId, UserGlobal.Instance.UserInfo.Sign, UserGlobal.Instance.GetInt32UserId(), UserGlobal.Instance.uuid, success=>
                {
                    UpdateData(success);

                    // loaded
                    //
                    loading.SetState(LoadingState.SUCCESS);
 
                }, error => {
                    if (Utilities.IsMatchNetworkFail(error.ErrorCode))
                    {
                        DelayHelper.Delay(TimeSpan.FromSeconds(0.7), () =>
                            {
                                // retry action
                                //
                                loading.RetryAction = async () => { await LoadFirstPageDataAsync(albumId); };

                                // show netork unavailable
                                //
                                loading.SetState(LoadingState.NETWORK_UNAVAILABLE);
                            });
                    }
                });
        }

        private void UpdateData(AlbumPageData data)
        {
            viewModel.AlbumAvatar = data.Info.AlbumAvatarUrl;
            viewModel.AlbumContent = data.Info.AlbumContent;
            viewModel.AlbumCover = data.Info.AlbumCover;
            viewModel.AlbumTitle = data.Info.AlbumTitle;
            viewModel.AlbumUserId = data.Info.AlbumUserId;
            viewModel.AlbumUserName = data.Info.AlbumUserName;

            if (data.Info.AlbumIsLike == 0)
            {
                this.favorite.Visibility = Windows.UI.Xaml.Visibility.Visible;
                this.removeFavorite.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            }
            else
            {
                this.favorite.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                this.removeFavorite.Visibility = Windows.UI.Xaml.Visibility.Visible;
            }

            if (!string.IsNullOrEmpty(data.Info.CommentCount))
            {
                viewModel.CommentCount = int.Parse(data.Info.CommentCount);
            }

            if (data.Recipes != null)
            {
                foreach (var item in data.Recipes)
                {
                    viewModel.Recipes.Add(new RecipeTileData()
                    {
                        RecipeId = item.RecipeId,
                        SupportNumber = item.LikeCount.ToString(),
                        Recommendation = item.Intro,
                        Author = item.UserName,
                        RecipeImage = item.Cover,
                        RecipeName = item.Title
                    });
                }
            }
        }

        #endregion

        #region Event

        private void RecipeTile_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var dataContext = sender.GetDataContext<RecipeTileData>(); 
            RecipeInfoPage.RecipeInfoPageParams paras = new RecipeInfoPage.RecipeInfoPageParams();
            paras.RecipeId = dataContext.RecipeId;

            App.Current.RootFrame.Navigate(typeof(RecipeInfoPage), paras);
        }

        private void Comments_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if(pageParams != null)
            {
                CommentListPage.CommentListPageParams paras = new CommentListPage.CommentListPageParams();
                paras.RecipeId = pageParams.AlbumId;

                App.Current.RootFrame.Navigate(typeof(CommentListPage), paras);
            }
        }

        private async void Favorite_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if(pageParams != null)
            {
                await FavoriteAPI.Add(UserGlobal.Instance.GetInt32UserId(), 2, pageParams.AlbumId, UserGlobal.Instance.uuid, UserGlobal.Instance.UserInfo.Sign,
                    success => {
                        toast.Show(success.Message);
                        viewModel.AlbumIsLike = true;
                        this.favorite.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                        this.removeFavorite.Visibility = Windows.UI.Xaml.Visibility.Visible;
                    }, 
                    error => {
                        toast.Show(error.Message);
                    });
            }
        }

        private void ShowAllAlbums_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            App.Current.RootFrame.Navigate(typeof(AllAlbumListPage));
        }


        private async void removeFavorite_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (pageParams != null)
            {
                await FavoriteAPI.Del(UserGlobal.Instance.GetInt32UserId(), 2, pageParams.AlbumId.ToString(), UserGlobal.Instance.uuid, UserGlobal.Instance.UserInfo.Sign, false,
                    success =>
                    {
                        toast.Show(success.Message);
                        viewModel.AlbumIsLike = true;
                        this.favorite.Visibility = Windows.UI.Xaml.Visibility.Visible;
                        this.removeFavorite.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                    },
                    error =>
                    {
                        toast.Show(error.Message);
                    });
            }
        }

        #endregion

    }
}
