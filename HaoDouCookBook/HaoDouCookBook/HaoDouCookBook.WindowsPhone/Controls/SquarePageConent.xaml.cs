using HaoDouCookBook.Common;
using HaoDouCookBook.HaoDou.API;
using HaoDouCookBook.HaoDou.DataModels.Square;
using HaoDouCookBook.Pages;
using HaoDouCookBook.ViewModels;
using Shared.Utility;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace HaoDouCookBook.Controls
{
    public sealed partial class SquarePageConent : UserControl
    {
        #region Field && Property

        public ObservableCollection<TopicModel> LatestTopics = new ObservableCollection<TopicModel>();
       
        public ObservableCollection<CategoryTileData> Categories = new ObservableCollection<CategoryTileData>();

        #endregion

        #region Life Cycle

        public SquarePageConent()
        {
            this.InitializeComponent();
            DataBinding();
            Init();
        }

        #endregion

        #region Data Prepare

        private void DataBinding()
        {
            this.topicList.ItemsSource = LatestTopics;
            this.CategoryList.ItemsSource = Categories;
        }

        private async Task LoadFirstPageDataAsync()
        {
            loading.SetState(LoadingState.LOADING);
            await TopicAPI.GetGroupIndexData(0, 20, 
                success => {
                    UpdatePageData(success);
                    loading.SetState(LoadingState.SUCCESS);
                    page = 1;
                }, 
                error => {
                    Utilities.CommonLoadingRetry(loading, async () => await LoadFirstPageDataAsync());
                });
        }

        private void UpdatePageData(SquarePageData data)
        {
            // Category
            //
            if (data.CateInfos != null)
            {
                Categories.Clear();
                foreach (var item in data.CateInfos)
                {
                    Categories.Add(new CategoryTileData() { ImageSource = item.ImageUrl, Title = item.Name, Id = item.CateId });
                }
            }

            // Topic
            //
            if (data.Topics != null)
            {
                RemoveLoadMoreControl();
                foreach (var item in data.Topics)
                {
                    int topicId = 0;
                    int.TryParse(item.TopicId.ToString(), out topicId);
                    LatestTopics.Add(new TopicModel()
                    {
                        Id = topicId,
                        Url = item.Url,
                        Author = item.UserName,
                        CreateTimeDescription = item.LastPostTime,
                        PreviewContent = item.PreviewContent,
                        Title = item.Title,
                        TopicPreviewImageSource = item.ImageUrl
                    });
                }
                EnusureLoadMoreControl();
            }
        }

        #endregion

        #region Private Method
        private void Init()
        {
            LoadFirstPageDataAsync();
        }

        #endregion

        #region Envent

        private void CategoryImageTile_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var dataContext = sender.GetDataContext<CategoryTileData>(); 
            TopicListPage.TopicListPageParams paras = new TopicListPage.TopicListPageParams();
            paras.CategoryId = int.Parse(dataContext.Id);
            paras.CategoryName = dataContext.Title;

            App.Current.RootFrame.Navigate(typeof(TopicListPage), paras);
        }

        private void TopicTile_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var dataContext = sender.GetDataContext<TopicModel>(); 
            ArticleViewer.ArticleViewerPageParams paras = new ArticleViewer.ArticleViewerPageParams();
            paras.Url = dataContext.Url;
            paras.TopicId = dataContext.Id;

            App.Current.RootFrame.Navigate(typeof(ArticleViewer), paras);
        }

        #endregion

        #region Load More

        int page = 1;
        int limit = 20;

        private TopicModel loadMoreControlDataContext = new TopicModel() { IsLoadMore = true };

        public void EnusureLoadMoreControl()
        {
            if (LatestTopics != null && !LatestTopics.Contains(loadMoreControlDataContext))
            {
                LatestTopics.Add(loadMoreControlDataContext);
            } 
        }

        public void RemoveLoadMoreControl()
        {
            if (LatestTopics != null && LatestTopics.Contains(loadMoreControlDataContext))
            {
                LatestTopics.Remove(loadMoreControlDataContext);
            }
        }

        private void loadMore_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Utilities.RegistComonadLoadMoreBehavior(sender,
                async loadmore =>
                {
                    loadmore.SetState(LoadingState.LOADING);
                    await TopicAPI.GetGroupIndexData(
                        page * limit, 
                        limit,
                        success =>
                        {
                            if (success.Topics != null)
                            {
                                RemoveLoadMoreControl();
                                foreach (var item in success.Topics)
                                {
                                    int topicId = 0;
                                    int.TryParse(item.TopicId.ToString(), out topicId);
                                    LatestTopics.Add(new TopicModel()
                                    {
                                        Id = topicId,
                                        Url = item.Url,
                                        Author = item.UserName,
                                        CreateTimeDescription = item.LastPostTime,
                                        PreviewContent = item.PreviewContent,
                                        Title = item.Title,
                                        TopicPreviewImageSource = item.ImageUrl
                                    });
                                }

                                if(success.Topics.Length > 0)
                                {
                                    EnusureLoadMoreControl();
                                }
                                page++;
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
