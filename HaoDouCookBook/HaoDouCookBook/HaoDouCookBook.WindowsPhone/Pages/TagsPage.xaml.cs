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
    public sealed partial class TagsPage : BackablePage
    {
        #region Page Parameters Definition

        public enum SourcePage
        {
            SEARCH_RESULT,
            NORMAL
        }

        public class TagPageParams
        {
            public int? Id { get; set; }

            public string TagText { get; set; }

            public SourcePage FromPage { get; set; }

            public TagPageParams()
            {
                FromPage = SourcePage.NORMAL;
            }

        }

        #endregion

        #region Field && Property

        private TagsPageViewModel viewModel = new TagsPageViewModel();
        private TagPageParams paras;

        #endregion

        #region Life Cycle

        public TagsPage()
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

            paras = e.Parameter as TagPageParams;

            if (paras != null)
            {
                viewModel = new TagsPageViewModel();
                rootScrollViewer.ScrollToVerticalOffset(0);
                DataBinding();
                this.title.Text = paras.TagText;
                LoadDataAsync(0, 10, paras.Id, paras.TagText);

                if(!string.IsNullOrEmpty(paras.TagText))
                {
                    HaoDouSearchHelper.AddSearchKeywordAsync(paras.TagText);
                }

            }
        }

        #endregion

        #region Data Prepare

        private void DataBinding()
        {
            this.root.DataContext = viewModel;
        }

        private async Task LoadDataAsync(int offset, int limit, int? tagid, string keyword)
        {
            await SearchAPI.GetList(offset, limit, UserGlobal.Instance.uuid, tagid, keyword, data =>
                {
                    if(paras != null && paras.FromPage == SourcePage.SEARCH_RESULT)
                    {
                        viewModel.Count = data.Count;
                    }

                    if(data.Food != null)
                    {
                        viewModel.Food.FoodCover = data.Food.Cover;
                        viewModel.Food.FoodId = data.Food.Id;
                        viewModel.Food.FoodIntro = data.Food.Intro;
                        viewModel.Food.FoodName = data.Food.Name;
                    }

                    if (data.Items != null && data.Items.Length > 0)
                    {
                        if (noResultGrid.Visibility == Visibility.Visible)
                        {
                            noResultGrid.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                        }

                        foreach (var item in data.Items)
                        {
                            viewModel.Recipes.Add(new TagRecipeData()
                            {
                                FoodStuff = item.Stuff,
                                LikeNumber = item.LikeCount,
                                ViewNumber = item.ViewCount,
                                PreviewImageSource = item.Cover,
                                RecipeName = item.Title,
                                RecipeId = item.RecipeId,
                                Card = item.Card
                            });
                        }

                    }
                    else
                    {
                        this.noResultGrid.Visibility = Windows.UI.Xaml.Visibility.Visible;
                    }

                }, error => { });
        }

        #endregion

        #region Event

        private void Recipe_Tapped(object sender, TappedRoutedEventArgs e)
        {
            RecipeInfoPage.RecipeInfoPageParams paras = new RecipeInfoPage.RecipeInfoPageParams();
            paras.RecipeId = sender.GetDataContext<TagRecipeData>().RecipeId;

            App.Current.RootFrame.Navigate(typeof(RecipeInfoPage), paras);
        }


        private void Food_Tapped(object sender, TappedRoutedEventArgs e)
        {
            StuffInfoPage.StuffInfoPageParams paras = new StuffInfoPage.StuffInfoPageParams();
            paras.Id = viewModel.Food.FoodId;
            paras.Title = viewModel.Food.FoodName;

            App.Current.RootFrame.Navigate(typeof(StuffInfoPage), paras);
        }

        #endregion

    }
}
