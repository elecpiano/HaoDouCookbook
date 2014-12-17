using HaoDouCookBook.Common;
using HaoDouCookBook.Controls;
using HaoDouCookBook.HaoDou.API;
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
                rootScrollViewer.ChangeViewExtersion(0, 0, 1.0f);
                DataBinding();
                this.title.Text = paras.TagText;
                LoadFirstPageDataDataAsync(paras.Id, paras.TagText);

                if (!string.IsNullOrEmpty(paras.TagText))
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

        private async Task LoadFirstPageDataDataAsync(int? tagid, string keyword)
        {
            loading.SetState(LoadingState.LOADING);
            await SearchAPI.GetList(0, limit, UserGlobal.Instance.uuid, tagid, keyword,
                success =>
                {
                    if (paras != null && paras.FromPage == SourcePage.SEARCH_RESULT)
                    {
                        viewModel.Count = success.Count;
                    }

                    if (success.Food != null)
                    {
                        viewModel.Food.FoodCover = success.Food.Cover;
                        viewModel.Food.FoodId = success.Food.Id;
                        viewModel.Food.FoodIntro = success.Food.Intro;
                        viewModel.Food.FoodName = success.Food.Name;
                    }

                    if (success.Items != null && success.Items.Length > 0)
                    {
                        if (noResultGrid.Visibility == Visibility.Visible)
                        {
                            noResultGrid.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                        }
                        RemoveLoadMoreControl();
                        foreach (var item in success.Items)
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

                        if(success.Items.Length == limit)
                        {
                            EnsureLoadMoreControl();
                        }
                    }
                    else
                    {
                        this.noResultGrid.Visibility = Windows.UI.Xaml.Visibility.Visible;
                    }
                    page = 1;
                    loading.SetState(LoadingState.SUCCESS);

                }, error =>
                {
                    Utilities.CommonLoadingRetry(loading, async () => await LoadFirstPageDataDataAsync(tagid, keyword));
                });
        }

        #endregion

        #region Event

        private void Recipe_Tapped(object sender, TappedRoutedEventArgs e)
        {
            RecipeInfoPage.RecipeInfoPageParams paras = new RecipeInfoPage.RecipeInfoPageParams();
            paras.RecipeId = sender.GetDataContext<TagRecipeData>().RecipeId;

            App.CurrentInstance.RootFrame.Navigate(typeof(RecipeInfoPage), paras);
        }


        private void Food_Tapped(object sender, TappedRoutedEventArgs e)
        {
            StuffInfoPage.StuffInfoPageParams paras = new StuffInfoPage.StuffInfoPageParams();
            paras.Id = viewModel.Food.FoodId;
            paras.Title = viewModel.Food.FoodName;

            App.CurrentInstance.RootFrame.Navigate(typeof(StuffInfoPage), paras);
        }

        #endregion

        #region Load More

        int page = 1;
        int limit = 20;

        private TagRecipeData loadMoreControlDataContext = new TagRecipeData() { IsLoadMore = true };

        public void EnsureLoadMoreControl()
        {
            if (viewModel.Recipes != null && !viewModel.Recipes.Contains(loadMoreControlDataContext))
            {
                viewModel.Recipes.Add(loadMoreControlDataContext);
            }
        }

        public void RemoveLoadMoreControl()
        {
            if (viewModel.Recipes != null && viewModel.Recipes.Contains(loadMoreControlDataContext))
            {
                viewModel.Recipes.Remove(loadMoreControlDataContext);
            }
        }

        private void loadMore_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Utilities.RegistComonadLoadMoreBehavior(sender,
                  async loadmore =>
                  {
                      loadmore.SetState(LoadingState.LOADING);
                      await SearchAPI.GetList(
                          page * limit,
                          limit,
                          UserGlobal.Instance.uuid,
                          paras.Id,
                          paras.TagText,
                          success =>
                          {
                              if (success.Items != null)
                              {
                                  if (noResultGrid.Visibility == Visibility.Visible)
                                  {
                                      noResultGrid.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                                  }

                                  RemoveLoadMoreControl();
                                  foreach (var item in success.Items)
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

                                  page++;
                                  loadmore.SetState(LoadingState.SUCCESS);
                                  if (success.Items.Length == limit)
                                  {
                                      EnsureLoadMoreControl();
                                  }
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
