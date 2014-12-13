using HaoDouCookBook.Common;
using HaoDouCookBook.Controls;
using HaoDouCookBook.HaoDou.API;
using HaoDouCookBook.HaoDou.DataModels.Choiceness;
using Shared.Utility;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace HaoDouCookBook.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AllAlbumListPage : BackablePage
    {

        #region Field && Property

        public ObservableCollection<HaoDouCookBook.ViewModels.AlbumTile> Albums { get; set; }

        #endregion

        #region Life Cycle

        public AllAlbumListPage()
        {
            this.InitializeComponent();
            Albums = new ObservableCollection<ViewModels.AlbumTile>();
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

            Albums.Clear();
            rootScrollViewer.ScrollToVerticalOffset(0);
            LoadFirstPageDataAsync();
        }

        #endregion

        #region Data Prepare

        private void DataBinding()
        {
            this.root.DataContext = this;
        }


        private async Task LoadFirstPageDataAsync()
        {
            loading.SetState(LoadingState.LOADING);
            await RecipeAPI.GetAlbumList(0, 20, UserGlobal.Instance.uuid, success =>
                {
                    UpdateData(success);
                    loading.SetState(LoadingState.SUCCESS);
                }, 
                error => {
                    Utilities.CommonLoadingRetry(loading, error, async () => await LoadFirstPageDataAsync());
                });
        }

        private void UpdateData(AllAlbumsData data)
        {
            if (data.Albums != null)
            {
                foreach (var item in data.Albums)
                {
                    Albums.Add(new ViewModels.AlbumTile() { 
                        AlbumTitle = item.Title,
                        AlbumCover = item.Cover,
                        AlbumId = item.Id,
                        AlbumIntro = item.Content
                    });
                }
            }
        }

        #endregion

        #region Event

        private void Album_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var dataContext = sender.GetDataContext<ViewModels.AlbumTile>();
            AlbumPage.AlbumPageParams paras = new AlbumPage.AlbumPageParams();
            paras.AlbumId = dataContext.AlbumId;

            App.Current.RootFrame.Navigate(typeof(AlbumPage), paras);
        }

        #endregion
    }
}
