using HaoDouCookBook.Common;
using HaoDouCookBook.Controls;
using HaoDouCookBook.HaoDou.API;
using HaoDouCookBook.Utility;
using HaoDouCookBook.ViewModels;
using Shared.Utility;
using System.Threading.Tasks;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace HaoDouCookBook.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AlbumListPage : BackablePage
    {
        #region Page Parameters Definition

        public class AlbumListPageParams
        {
            public string Keyword { get; set; }

            public AlbumListPageParams()
            {
                Keyword = string.Empty;
            }
        }

        #endregion

        #region Filed && Property

        private AlbumListPageViewModel viewModel = new AlbumListPageViewModel();

        #endregion

        #region Life Cycle


        public AlbumListPage()
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

            AlbumListPageParams paras = e.Parameter as AlbumListPageParams;
            if (paras != null)
            {
                viewModel = new AlbumListPageViewModel();
                this.title.Text = paras.Keyword;
                rootScrollViewer.ScrollToVerticalOffset(0);
                DataBinding();
                LoadDataAsync(0, 20, paras.Keyword);
            }
        }

        #endregion

        #region Data Prepare

        private void DataBinding()
        {
            this.root.DataContext = viewModel;
        }

        private async Task LoadDataAsync(int offset, int limit, string keyword)
        {
            await SearchAPI.GetAlbumList(offset, limit, UserGlobal.Instance.uuid, null, keyword, data =>
                {
                    viewModel.Count = data.Count;

                    if (data.AlbumItems != null)
                    {
                        foreach (var item in data.AlbumItems)
                        {
                            viewModel.AlbumItems.Add(new ViewModels.AlbumTile() { 
                                    AlbumCover = item.Cover,
                                    AlbumId = string.IsNullOrEmpty(item.AlbumId) ? -1 : int.Parse(item.AlbumId),
                                    AlbumIntro = item.Intro,
                                    AlbumLikeCount = item.LikeCount,
                                    AlbumRecipeCount = item.RecipeCount,
                                    AlbumTitle = item.Title,
                                    AlbumViewCount = item.ViewCount
                            });
                        }
                    }

                }, error => { });
        }

        #endregion

        #region Event

        private void AlbumItem_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var dataContext = sender.GetDataContext<ViewModels.AlbumTile>();

            AlbumPage.AlbumPageParams paras = new AlbumPage.AlbumPageParams();
            paras.AlbumId = dataContext.AlbumId;

            App.Current.RootFrame.Navigate(typeof(AlbumPage), paras);

        }

        #endregion
    }
}
