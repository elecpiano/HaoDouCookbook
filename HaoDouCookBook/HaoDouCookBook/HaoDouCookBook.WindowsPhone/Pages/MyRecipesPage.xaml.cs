using HaoDouCookBook.Common;
using HaoDouCookBook.Controls;
using HaoDouCookBook.HaoDou.API;
using HaoDouCookBook.ViewModels;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace HaoDouCookBook.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MyRecipesPage : BackablePage
    {
        #region Filed && Property

        public ObservableCollection<UserRecipe> Recipes { get; set; }

        #endregion

        #region Life Cycle

        public MyRecipesPage()
        {
            this.InitializeComponent();
            Recipes = new ObservableCollection<UserRecipe>();
            this.noItemsText.Text = Constants.I_DONT_HAVE_RECIPES;
            DataBinding();
            Init();

        }

        private void Init()
        {
            this.recipesList.LoadMoreAction = LoadMore;
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

            Recipes.Clear();
            this.recipesList.ResetScrollViewerToBegin();
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
            int uid = UserGlobal.Instance.GetInt32UserId();

            loading.SetState(LoadingState.LOADING);
            await RecipeUserAPI.GetUserRecipeList(0, limit, uid, uid, success =>
            {
                if (success.Recipes != null)
                {
                    Recipes.Clear();
                    RemoveLoadMoreControl();
                    foreach (var item in success.Recipes)
                    {
                        Recipes.Add(new UserRecipe()
                        {
                            Id = item.Rid,
                            Cover = item.Cover
                        });
                    }

                    if(success.Recipes.Length == limit)
                    {
                        EnsureLoadMoreControl();
                    }
                }

                page = 1;
                loading.SetState(LoadingState.SUCCESS);

            }, error => {
                Utilities.CommonLoadingRetry(loading, error, async () => await LoadFirstPageDataAsync());
            });
        }

        #endregion

        #region Event

        private void Publish_Click(object sender, RoutedEventArgs e)
        {
            App.CurrentInstance.RootFrame.Navigate(typeof(PublishRecipePage));
        }

        #endregion


        #region Load More

        int page = 1;
        int limit = 21;

        private UserRecipe loadMoreControlDataContext = new UserRecipe() { IsLoadMore = true };

        public void EnsureLoadMoreControl()
        {
            if (Recipes != null && !Recipes.Contains(loadMoreControlDataContext))
            {
                Recipes.Add(loadMoreControlDataContext);
            }
        }

        public void RemoveLoadMoreControl()
        {
            if (Recipes != null && Recipes.Contains(loadMoreControlDataContext))
            {
                Recipes.Remove(loadMoreControlDataContext);
            }
        }

        private async void LoadMore(LoadMoreControl loadmore)
        {
            int uid = UserGlobal.Instance.GetInt32UserId();
            loadmore.SetState(LoadingState.LOADING);
            await RecipeUserAPI.GetUserRecipeList(
                page * limit,
                limit,
                uid,
                uid,
                success => {
                    if (success.Recipes != null)
                    {
                        RemoveLoadMoreControl();
                        foreach (var item in success.Recipes)
                        {
                            Recipes.Add(new UserRecipe()
                            {
                                Id = item.Rid,
                                Cover = item.Cover
                            });
                        }

                        if (success.Recipes.Length == limit)
                        {
                            EnsureLoadMoreControl();
                        }
                        page++;
                        loadmore.SetState(LoadingState.SUCCESS);
                    }
                    else
                    {
                        loadmore.SetState(LoadingState.DONE);
                    }
                },
               error => {
                   Utilities.CommondLoadMoreErrorBehavoir(loadmore, error);
               });
        }

        #endregion

    }
}
