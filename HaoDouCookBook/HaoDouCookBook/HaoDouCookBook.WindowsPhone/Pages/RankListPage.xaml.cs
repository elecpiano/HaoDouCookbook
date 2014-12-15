using HaoDouCookBook.Common;
using HaoDouCookBook.Controls;
using HaoDouCookBook.HaoDou.API;
using HaoDouCookBook.ViewModels;
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
    public sealed partial class RankListPage : BackablePage
    {

        #region Field && Property

        private ObservableCollection<RankItemData> rankListData = new ObservableCollection<RankItemData>();

        #endregion

        #region Life Cycle

        public RankListPage()
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

            rankListData.Clear();
            rootScrollViewer.ScrollToVerticalOffset(0);
            LoadFirstPageDataAsync();
        }

        #endregion

        #region Data Prepare

        private void DataBinding()
        {
            this.ranklist.ItemsSource = rankListData;
        }

        private async Task LoadFirstPageDataAsync()
        {
            loading.SetState(LoadingState.LOADING);
            await RankAPI.GetRankList(
                0, 
                20, 
                UserGlobal.Instance.UserInfo.Sign,
                UserGlobal.Instance.GetInt32UserId(),
                UserGlobal.Instance.uuid, 
                success =>
                {
                    if (success.Items != null)
                    {
                        RemoveLoadMoreControl();
                        foreach (var item in success.Items)
                        {
                            rankListData.Add(new RankItemData() { 
                                Title = item.Title, 
                                CoverImage = item.Cover, 
                                Description = item.Intro, 
                                Type = item.RankType,
                                Id = int.Parse(item.Id)
                            });
                        }
                        EnusureLoadMoreControl();
                    }
                    page = 1;

                    loading.SetState(LoadingState.SUCCESS);

                }, 
                error => {
                    Utilities.CommonLoadingRetry(loading, async () => await LoadFirstPageDataAsync());
                });
        }

        #endregion

        #region Event

        private void RecipeItem_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var rankItemData = sender.GetDataContext<RankItemData>();
            RankViewPage.RankViewPageParams paras = new RankViewPage.RankViewPageParams();
            paras.Id = rankItemData.Id;
            paras.Type = rankItemData.Type;
            App.Current.RootFrame.Navigate(typeof(RankViewPage), paras);
        }

        #endregion

        #region Load More

        int page = 1;
        int limit = 20;

        private RankItemData loadMoreControlDataContext = new RankItemData() { IsLoadMore = true };

        public void EnusureLoadMoreControl()
        {
            if (rankListData != null && !rankListData.Contains(loadMoreControlDataContext))
           {
               rankListData.Add(loadMoreControlDataContext);
           }
        }

        public void RemoveLoadMoreControl()
        {
            if (rankListData != null && rankListData.Contains(loadMoreControlDataContext))
            {
                rankListData.Remove(loadMoreControlDataContext);
            } 
        }

        private void loadMore_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Utilities.RegistComonadLoadMoreBehavior(sender,
                 async loadmore =>
                 {
                     loadmore.SetState(LoadingState.LOADING);
                     await RankAPI.GetRankList(
                         page * limit,
                         limit,
                         UserGlobal.Instance.UserInfo.Sign,
                         UserGlobal.Instance.GetInt32UserId(),
                         UserGlobal.Instance.uuid,
                         success =>
                         {
                             if (success.Items != null)
                             {
                                 RemoveLoadMoreControl();
                                 foreach (var item in success.Items)
                                 {
                                     rankListData.Add(new RankItemData()
                                     {
                                         Title = item.Title,
                                         CoverImage = item.Cover,
                                         Description = item.Intro,
                                         Type = item.RankType,
                                         Id = int.Parse(item.Id)
                                     });
                                 }

                                 if(success.Items.Length > 0)
                                 {
                                     EnusureLoadMoreControl();
                                 }

                                 page++;
                             }

                            loadmore.SetState(LoadingState.SUCCESS);
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
