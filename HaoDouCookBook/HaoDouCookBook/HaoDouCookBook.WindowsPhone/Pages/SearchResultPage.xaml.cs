using HaoDouCookBook.Common;
using HaoDouCookBook.Controls;
using HaoDouCookBook.HaoDou.API;
using HaoDouCookBook.Utility;
using HaoDouCookBook.ViewModels;
using Shared.Utility;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace HaoDouCookBook.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SearchResultPage : BackablePage
    {
        #region Page Parameters Definition

        public class SearchResultPageParams
        {
            public string Keyword { get; set; }

            public string TagId { get; set; }

            public SearchResultPageParams()
            {
                Keyword = string.Empty;
            }
        }

        #endregion

        #region Field && Property

        private SearchResultPageViewModel viewModel = new SearchResultPageViewModel();
        private SearchResultPageParams paras;

        #endregion

        #region Life Cycle

        public SearchResultPage()
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

            paras = e.Parameter as SearchResultPageParams;
            if (paras != null)
            {
                HaoDouSearchHelper.AddSearchKeywordAsync(paras.Keyword);

                viewModel = new SearchResultPageViewModel();
                rootScrollViewer.ScrollToVerticalOffset(0);
                DataBinding();
                LoadDataAsync(paras.Keyword, paras.TagId);
            }
        }

        #endregion

        #region Data Prepare

        private void DataBinding()
        {
            this.root.DataContext = viewModel;
        }

        private async Task LoadDataAsync(string keyword, string tagid)
        {
            await SearchAPI.GetSearchIndex(keyword, tagid, UserGlobal.Instance.uuid, data =>
                {
                    if (data.Food != null)
                    {
                        viewModel.Food.FoodCover = data.Food.Cover;
                        viewModel.Food.FoodId = data.Food.Id;
                        viewModel.Food.FoodIntro = data.Food.Intro;
                        viewModel.Food.FoodName = data.Food.Name;
                    }

                    if (data.Recipe != null)
                    {
                        foreach (var item in data.Recipe.Items)
                        {
                            viewModel.Recipes.Add(new TagRecipeData() { 
                                FoodStuff = item.Stuff,
                                RecipeId = item.RecipeId,
                                LikeNumber = item.LikeCount,
                                ViewNumber = item.ViewCount,
                                RecipeName = item.Title,
                                PreviewImageSource = item.Cover,
                                Card = item.Card
                            });
                        }
                    }

                    if (data.Album != null && data.Album.AlbumItems != null && data.Album.AlbumItems.Length > 0)
                    {
                        var albumData = data.Album.AlbumItems[0];
                        viewModel.Album.AlbumId = int.Parse(albumData.AlbumId);
                        viewModel.Album.AlbumIntro = albumData.Intro;
                        viewModel.Album.AlbumRecipeCount = albumData.RecipeCount;
                        viewModel.Album.AlbumTitle = albumData.Title;
                        viewModel.Album.AlbumViewCount = albumData.ViewCount;
                        viewModel.Album.AlbumLikeCount = albumData.LikeCount;
                        viewModel.Album.AlbumCover = albumData.Cover;
                    }

                    if (data.TopicData != null && data.TopicData.Topics != null && data.TopicData.Topics.Length > 0)
                    {
                        var topicData = data.TopicData.Topics[0];
                        viewModel.Topic.Author = topicData.UserName;
                        viewModel.Topic.CreateTimeDescription = topicData.CreateTime;
                        viewModel.Topic.Id = topicData.TopicId;
                        viewModel.Topic.PreviewContent = topicData.Intro;
                        viewModel.Topic.Title = topicData.Title;
                        viewModel.Topic.Url = topicData.Url;
                        viewModel.Topic.TopicPreviewImageSource = topicData.Cover;
                    }

                }, error => { });
        }

        #endregion

        #region Event

        private void Search_AppbarButton_Click(object sender, RoutedEventArgs e)
        {
            App.Current.RootFrame.Navigate(typeof(SearchInputPage));
        }

        private void Stuff_Tapped(object sender, TappedRoutedEventArgs e)
        {
            StuffInfoPage.StuffInfoPageParams paras = new StuffInfoPage.StuffInfoPageParams();
            paras.Title = viewModel.Food.FoodName;
            paras.Id = viewModel.Food.FoodId;

            App.Current.RootFrame.Navigate(typeof(StuffInfoPage), paras);
        }

        private void RecipeItem_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var dataContext = sender.GetDataContext<TagRecipeData>();

            RecipeInfoPage.RecipeInfoPageParams paras = new RecipeInfoPage.RecipeInfoPageParams();
            paras.RecipeId = dataContext.RecipeId;

            App.Current.RootFrame.Navigate(typeof(RecipeInfoPage), paras);
        }

        private void AlbumItem_Tapped(object sender, TappedRoutedEventArgs e)
        {
            AlbumPage.AlbumPageParams paras = new AlbumPage.AlbumPageParams();
            paras.AlbumId = viewModel.Album.AlbumId;

            App.Current.RootFrame.Navigate(typeof(AlbumPage), paras);
        }

        private void TopicItem_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ArticleViewer.ArticleViewerPageParams paras = new ArticleViewer.ArticleViewerPageParams();
            paras.Url = viewModel.Topic.Url;
            paras.TopicId = viewModel.Topic.Id;

            App.Current.RootFrame.Navigate(typeof(ArticleViewer), paras);
        }

        private void ShowAllRecipes_Tapped(object sender, TappedRoutedEventArgs e)
        {
            TagsPage.TagPageParams tagPageParams = new TagsPage.TagPageParams();
            tagPageParams.TagText = paras.Keyword;
            tagPageParams.FromPage = TagsPage.SourcePage.SEARCH_RESULT;

            App.Current.RootFrame.Navigate(typeof(TagsPage), tagPageParams);
        }

        private void ShowAllAlbums_tapped(object sender, TappedRoutedEventArgs e)
        {
            AlbumListPage.AlbumListPageParams albumListPageParas = new AlbumListPage.AlbumListPageParams();
            albumListPageParas.Keyword = paras.Keyword;

            App.Current.RootFrame.Navigate(typeof(AlbumListPage), albumListPageParas);
        }

        #endregion

        private void ShowAllTopics_Tapped(object sender, TappedRoutedEventArgs e)
        {
            TopicListPage.TopicListPageParams topicListPageParams = new TopicListPage.TopicListPageParams();
            topicListPageParams.CategoryName = paras.Keyword;
            topicListPageParams.SourcePage = TopicListPage.SourcePage.SEARCH_RESULT;

            App.Current.RootFrame.Navigate(typeof(TopicListPage), topicListPageParams);
        }
    }
}
