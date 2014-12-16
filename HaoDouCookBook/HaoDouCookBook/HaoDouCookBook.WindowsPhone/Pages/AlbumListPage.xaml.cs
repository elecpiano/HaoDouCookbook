using HaoDouCookBook.Common;
using HaoDouCookBook.Controls;
using HaoDouCookBook.HaoDou.API;
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
        private AlbumListPageParams pageParams;

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

            pageParams = e.Parameter as AlbumListPageParams;
            if (pageParams != null)
            {
                viewModel = new AlbumListPageViewModel();
                this.title.Text = pageParams.Keyword;
                rootScrollViewer.ChangeViewExtersion(0, 0, 1.0f);
                DataBinding();
                LoadFirstPageDataAsync(pageParams.Keyword);
            }
        }

        #endregion

        #region Data Prepare

        private void DataBinding()
        {
            this.root.DataContext = viewModel;
        }

        private async Task LoadFirstPageDataAsync(string keyword)
        {
            loading.SetState(LoadingState.LOADING);
            await SearchAPI.GetAlbumList(0, 20, UserGlobal.Instance.uuid, null, keyword, 
                success =>
                {
                    viewModel.Count = success.Count;

                    if (success.AlbumItems != null)
                    {
                        RemoveLoadMoreControl();
                        foreach (var item in success.AlbumItems)
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
                        
                        if(success.AlbumItems.Length == 20)
                        {
                            EnusureLoadMoreControl();
                        }
                    }

                    page = 1;
                    loading.SetState(LoadingState.SUCCESS);

                }, 
                error => {
                    Utilities.CommonLoadingRetry(loading, async () => await LoadFirstPageDataAsync(keyword));
                });
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

        #region Load More

        int page = 1;
        int limit = 20;

        private ViewModels.AlbumTile loadMoreControlDataContext = new ViewModels.AlbumTile() { IsLoadMore = true };

        public void EnusureLoadMoreControl()
        {
            if (viewModel.AlbumItems != null && !viewModel.AlbumItems.Contains(loadMoreControlDataContext))
           {
               viewModel.AlbumItems.Add(loadMoreControlDataContext);
           }
        }

        public void RemoveLoadMoreControl()
        {
            if (viewModel.AlbumItems != null && viewModel.AlbumItems.Contains(loadMoreControlDataContext))
            {
                viewModel.AlbumItems.Remove(loadMoreControlDataContext);
            } 
        }

        private void loadMore_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Utilities.RegistComonadLoadMoreBehavior(sender,
                 async loadmore =>
                 {
                     loadmore.SetState(LoadingState.LOADING);
                     await SearchAPI.GetAlbumList(
                         page * limit,
                         limit,
                         UserGlobal.Instance.uuid,
                         null,
                         pageParams.Keyword,
                         success =>
                         {
                             if (success.AlbumItems != null)
                             {
                                 RemoveLoadMoreControl();
                                 foreach (var item in success.AlbumItems)
                                 {
                                     viewModel.AlbumItems.Add(new ViewModels.AlbumTile()
                                     {
                                         AlbumCover = item.Cover,
                                         AlbumId = string.IsNullOrEmpty(item.AlbumId) ? -1 : int.Parse(item.AlbumId),
                                         AlbumIntro = item.Intro,
                                         AlbumLikeCount = item.LikeCount,
                                         AlbumRecipeCount = item.RecipeCount,
                                         AlbumTitle = item.Title,
                                         AlbumViewCount = item.ViewCount
                                     });
                                 }

                                 page++;
                                 if(success.AlbumItems.Length == limit)
                                 {
                                     EnusureLoadMoreControl();
                                 }
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
