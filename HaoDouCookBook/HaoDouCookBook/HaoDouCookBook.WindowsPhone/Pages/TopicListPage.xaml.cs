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
    public sealed partial class TopicListPage : BackablePage
    {
        #region Page Parameters Definition

        public enum SourcePage
        {
            SEARCH_RESULT,
            NORMAL
        }

        public class TopicListPageParams
        {
            public int CategoryId { get; set; }

            public string CategoryName { get; set; }

            public SourcePage SourcePage { get; set; }

            public TopicListPageParams()
            {
                SourcePage = TopicListPage.SourcePage.NORMAL;
            }
        }

        #endregion

        #region Field && Property

        private TopicListPageViewModel viewModel = new TopicListPageViewModel();
        private TopicListPageParams pageParams;

        #endregion

        #region Life Cycle

        public TopicListPage()
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

            pageParams = e.Parameter as TopicListPageParams;

            if (pageParams != null)
            {
                viewModel = new TopicListPageViewModel();
                rootScrollViewer.ScrollToVerticalOffset(0);
                DataBinding();
                this.title.Text = pageParams.CategoryName;

                switch (pageParams.SourcePage)
                {
                    case SourcePage.SEARCH_RESULT:
                        LoadFirstPageDataByKeywordAsync(pageParams.CategoryName);
                        break;
                    case SourcePage.NORMAL:
                    default:
                        LoadFirstPageDataByIdAsync(pageParams.CategoryId);
                        break;
                }
            }

        }

        #endregion

        #region Data Prepare

        private void DataBinding()
        {
            this.root.DataContext = viewModel;
        }

        private async Task LoadFirstPageDataByKeywordAsync(string keyword)
        {
            await SearchAPI.GetTopicList(0, 20, UserGlobal.Instance.uuid, null, keyword, 
                success =>
                {
                    viewModel.Count = success.Count;

                    if (success.Topics != null)
                    {
                        foreach (var item in success.Topics)
                        {
                            viewModel.Topics.Add(new TopicModel() { 
                                    Id = item.TopicId,
                                    Url= item.Url,
                                    TopicPreviewImageSource = item.Cover,
                                    Title = item.Title,
                                    PreviewContent = item.Intro,
                                    Author = item.UserName,
                                    CreateTimeDescription = item.CreateTime
                            });
                        }
                    }
                    page = 1;
                    loading.SetState(LoadingState.SUCCESS);
                }, 
                error => {
                    Utilities.CommonLoadingRetry(loading, async () => await LoadFirstPageDataByKeywordAsync(keyword));
                });
        }

        private async Task LoadFirstPageDataByIdAsync(int cateId)
        {
            loading.SetState(LoadingState.LOADING);
            await TopicAPI.GetList(0, 20, cateId, UserGlobal.Instance.GetInt32UserId(), 
                success =>
                {
                    viewModel.Count = 0;

                    if (success.Items != null)
                    {
                        foreach (var item in success.Items)
                        {
                            int topicId = 0;
                            int.TryParse(item.TopicId.ToString(), out topicId);
                            viewModel.Topics.Add(new TopicModel()
                            {
                                Id = topicId,
                                Url = item.Url,
                                TopicPreviewImageSource = item.ImageUrl,
                                Title = item.Title,
                                PreviewContent = item.PreviewContent,
                                Author = item.UserName,
                                CreateTimeDescription = item.LastPostTime,
                            });
                        }
                    }

                    page = 1;
                    loading.SetState(LoadingState.SUCCESS);
                }, 
                error => {
                    Utilities.CommonLoadingRetry(loading, async () => await LoadFirstPageDataByIdAsync(cateId));
                });
        }


        #endregion

        #region Event

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

        private void loadMore_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Utilities.RegistComonadLoadMoreBehavior<TopicModel>(sender,
                async loadmore =>
                {
                    loadmore.SetState(LoadingState.LOADING);
                    switch(pageParams.SourcePage)
                    {
                        case SourcePage.SEARCH_RESULT:
                            await SearchAPI.GetTopicList(
                                page * limit,
                                limit,
                                UserGlobal.Instance.uuid,
                                null,
                                pageParams.CategoryName,
                                success =>
                                {
                                    if (success.Topics != null)
                                    {
                                        foreach (var item in success.Topics)
                                        {
                                            viewModel.Topics.Add(new TopicModel()
                                            {
                                                Id = item.TopicId,
                                                Url = item.Url,
                                                TopicPreviewImageSource = item.Cover,
                                                Title = item.Title,
                                                PreviewContent = item.Intro,
                                                Author = item.UserName,
                                                CreateTimeDescription = item.CreateTime
                                            });
                                        }

                                        page++;
                                    }

                                    loadmore.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                                    loadmore.SetState(LoadingState.SUCCESS);
                                },
                                error =>
                                {
                                    Utilities.CommondLoadMoreErrorBehavoir(loadmore, error);
                                });
                            break;
                        case SourcePage.NORMAL:
                            await TopicAPI.GetList(
                                page * limit,
                                limit,
                                pageParams.CategoryId,
                                UserGlobal.Instance.GetInt32UserId(),
                                success =>
                                {
                                    if (success.Items != null)
                                    {
                                        foreach (var item in success.Items)
                                        {
                                            int topicId = 0;
                                            int.TryParse(item.TopicId.ToString(), out topicId);
                                            viewModel.Topics.Add(new TopicModel()
                                            {
                                                Id = topicId,
                                                Url = item.Url,
                                                TopicPreviewImageSource = item.ImageUrl,
                                                Title = item.Title,
                                                PreviewContent = item.PreviewContent,
                                                Author = item.UserName,
                                                CreateTimeDescription = item.LastPostTime,
                                            });
                                        }
                                        page++;
                                    }
                                    
                                    loadmore.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                                    loadmore.SetState(LoadingState.SUCCESS);
                                },
                                error =>
                                {
                                    Utilities.CommondLoadMoreErrorBehavoir(loadmore, error);
                                });
                            break;
                    }
                   
                });
        }

        #endregion
    }

}
