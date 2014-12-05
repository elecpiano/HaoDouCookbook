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
        private int pageOffset = 0;
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
                        LoadDataByKeywordAsync(pageParams.CategoryName);
                        break;
                    case SourcePage.NORMAL:
                    default:
                        LoadDataByIdAsync(pageParams.CategoryId);
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

        private async Task LoadDataByKeywordAsync(string keyword)
        {
            await SearchAPI.GetTopicList(pageOffset, 20, DeviceHelper.GetUniqueDeviceID(), null, keyword, data =>
                {
                    viewModel.Count = data.Count;

                    if (data.Topics != null)
                    {
                        foreach (var item in data.Topics)
                        {
                            viewModel.Topics.Add(new TopicModel() { 
                                    Id = item.TopicId.ToString(),
                                    Url= item.Url,
                                    TopicPreviewImageSource = item.Cover,
                                    Title = item.Title,
                                    PreviewContent = item.Intro,
                                    Author = item.UserName,
                                    CreateTimeDescription = item.CreateTime
                            });
                        }
                    }
                }, error => { });
        }

        private async Task LoadDataByIdAsync(int cateId)
        {
            await TopicAPI.GetList(pageOffset, 20, cateId, null, data =>
                {
                    viewModel.Count = 0;

                    if (data.Items != null)
                    {
                        foreach (var item in data.Items)
                        {
                            viewModel.Topics.Add(new TopicModel()
                            {
                                Id = item.TopicId,
                                Url = item.Url,
                                TopicPreviewImageSource = item.ImageUrl,
                                Title = item.Title,
                                PreviewContent = item.PreviewContent,
                                Author = item.UserName,
                                CreateTimeDescription = item.LastPostTime,
                            });
                        }
                    }

                }, error =>
                {
                });
        }


        #endregion

        #region Event

        private void TopicTile_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ArticleViewer.ArticleViewerPageParams paras = new ArticleViewer.ArticleViewerPageParams();
            paras.Url = sender.GetDataContext<TopicModel>().Url;

            App.Current.RootFrame.Navigate(typeof(ArticleViewer), paras);
        }

        #endregion
    }
}
