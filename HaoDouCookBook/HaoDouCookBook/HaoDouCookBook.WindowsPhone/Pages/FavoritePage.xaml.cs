using HaoDouCookBook.Common;
using HaoDouCookBook.Controls;
using HaoDouCookBook.HaoDou.API;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace HaoDouCookBook.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class FavoritePage : BackablePage
    {
        #region Life Cycle

        public FavoritePage()
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
            if(e.NavigationMode == NavigationMode.Back)
            {
                favoriteRecipesAlubm.LoadFirstPageDataAsync();
                return;
            }

            InitAndLoadData();
        }

        private void InitAndLoadData()
        {
            // init
            //
            this.pivot.SelectedIndex = 0;
            favoriteRecipesAlbumsScrollViewer.ChangeViewExtersion(0, 0, 1.0f);
            favoriteAlbumsScrollViewer.ChangeViewExtersion(0, 0, 1.0f);
            favoriteTopicsScrollViewer.ChangeViewExtersion(0, 0, 1.0f);

            // load data
            //
            favoriteRecipesAlubm.LoadFirstPageDataAsync();
            favoriteRecipesAlubm.OnAlbumTapped = album =>
            {
                FavoriteRecipeAlbumPage.FavoriteRecipeAlbumPageParams paras = new FavoriteRecipeAlbumPage.FavoriteRecipeAlbumPageParams();
                paras.AlbumId = album.AlbumId;
                paras.Title = album.AlbumName;

                App.Current.RootFrame.Navigate(typeof(FavoriteRecipeAlbumPage), paras);
            };

            favoriteRecipesAlubm.CreateNewAlubmTapped = ()=>
            {
                BigTextBox.BigTextBoxParams paras = new BigTextBox.BigTextBoxParams();
                paras.MaxLength = 20;
                paras.PlaceholderText = "输入分类名称，最多20个字符";
                paras.ConfirmAction = CreateNewAlbum;

                App.Current.RootFrame.Navigate(typeof(BigTextBox), paras);
            };


            favoriteAlbums.LoadFirstPageDataAsync();
            favoriteAlbums.DeleteAllSuccessAction = (message) => toast.Show(message);
            favoriteAlbums.DeleteSingleSuccessAction = (message) => toast.Show(message);
            favoriteAlbums.DeleteAllFailAction = (error) => toast.Show(error.Message);
            favoriteAlbums.DeleteSingleFailAction = (error) => toast.Show(error.Message);


            favoriteTopics.LoadFirstPageDataAsync();
            favoriteTopics.DeleteAllSuccessAction = (message) => toast.Show(message);
            favoriteTopics.DeleteSingleSuccessAction = (message) => toast.Show(message);
            favoriteTopics.DeleteAllFailAction = (error) => toast.Show(error.Message);
            favoriteTopics.DeleteSingleFailAction = (error) => toast.Show(error.Message);
        }

        #endregion

        #region Private Methods

        private async void CreateNewAlbum(string newAlbumName)
        {
            if (string.IsNullOrEmpty(newAlbumName))
            {
                toast.Show("分类名不能为空");
                return;
            }

            await FavoriteAPI.AddMyAlbum(UserGlobal.Instance.GetInt32UserId(), UserGlobal.Instance.UserInfo.Sign, newAlbumName, async success =>
            {
                toast.Show(success.Message);
                await favoriteRecipesAlubm.LoadFirstPageDataAsync();

            }, error =>
            {
                toast.Show(error.Message);
            });
        }

        #endregion
    }
}
