using HaoDouCookBook.Controls;
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
            favoriteRecipesAlbumsScrollViewer.ScrollToVerticalOffset(0);
            favoriteAlbumsScrollViewer.ScrollToVerticalOffset(0);
            favoriteTopicsScrollViewer.ScrollToVerticalOffset(0);

            // load data
            //
            favoriteRecipesAlubm.LoadFirstPageDataAsync();
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
    }
}
