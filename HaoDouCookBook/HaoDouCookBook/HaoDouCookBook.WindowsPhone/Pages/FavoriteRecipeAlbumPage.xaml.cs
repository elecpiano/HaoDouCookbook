using HaoDouCookBook.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;
using HaoDouCookBook.ViewModels;
using System.Threading.Tasks;
using HaoDouCookBook.HaoDou.API;
using HaoDouCookBook.Common;
using Shared.Utility;
using Windows.UI.Xaml.Media.Imaging;
using System;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace HaoDouCookBook.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class FavoriteRecipeAlbumPage : BackablePage
    {
        #region Page Parameters Definition

        public class FavoriteRecipeAlbumPageParams
        {
            public int AlbumId { get; set; }

            public string Title { get; set; }

            public FavoriteRecipeAlbumPageParams()
            {
                AlbumId = -1;
                Title = string.Empty;
            }
        }

        #endregion

        #region Field && Proprety

        private FavoriteRecipesAlbumPageViewModel viewModel = new FavoriteRecipesAlbumPageViewModel();
        private FavoriteRecipeAlbumPageParams pageParams;

        #endregion

        #region Life Cycle

        public FavoriteRecipeAlbumPage()
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

            pageParams = e.Parameter as FavoriteRecipeAlbumPageParams;
            if (pageParams != null)
            {
                this.rootScrollViewer.ScrollToVerticalOffset(0);
                viewModel = new FavoriteRecipesAlbumPageViewModel();
                this.title.Text = pageParams.Title;

                if (pageParams.Title.Equals("我喜欢的菜谱") && pageParams.AlbumId == 0)
                {
                    this.noItemsImage.Source = new BitmapImage(new Uri("ms-appx:///../assets/images/likeheart.png", UriKind.RelativeOrAbsolute));
                    this.noItemsText.Text = Constants.NO_LIKE_RECIPES;
                }
                else
                {
                    this.noItemsImage.Source = new BitmapImage(new Uri("ms-appx:///../assets/images/star.png", UriKind.RelativeOrAbsolute));
                    this.noItemsText.Text = Constants.NO_FAVORITE_RECIPES;
                }

                DataBinding();
                LoadFirstPageDataAsync(pageParams.AlbumId);
            }
        }

        #endregion

        #region Data Prepare
        private void DataBinding()
        {
            this.root.DataContext = viewModel;
        }

        private async Task LoadFirstPageDataAsync(int albumId)
        {
            await InfoAPI.GetAlbumInfo(0, 20, albumId,
                UserGlobal.Instance.UserInfo.Sign,
                UserGlobal.Instance.GetInt32UserId(),
                UserGlobal.Instance.uuid,
                success => {
                    if (success.Recipes != null)
                    {
                        foreach (var item in success.Recipes)
                        {
                            viewModel.Recipes.Add(new FavoriteRecipe()
                            {
                                Cover = item.Cover,
                                LikeNumber = item.LikeCount,
                                ViewNumber = item.ViewCount,
                                Title = item.Title,
                                RecipeId = item.RecipeId,
                                Intro = item.Stuff
                            });
                        }
                    }

                },
                error => {
                    Utilities.CommonLoadingRetry(loading, error, async () => await LoadFirstPageDataAsync(albumId));
                });
        }

        #endregion

        #region Event

        private void Recipe_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var dataContext = sender.GetDataContext<FavoriteRecipe>();
            RecipeInfoPage.RecipeInfoPageParams paras = new RecipeInfoPage.RecipeInfoPageParams();
            paras.RecipeId = dataContext.RecipeId;

            App.Current.RootFrame.Navigate(typeof(RecipeInfoPage), paras);
        }

        #endregion
    }
}
