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
            await RecipeUserAPI.GetUserRecipeList(0, 21, uid, uid, success =>
            {
                if (success.Recipes != null)
                {
                    Recipes.Clear();
                    foreach (var item in success.Recipes)
                    {
                        Recipes.Add(new UserRecipe()
                        {
                            Id = item.Rid,
                            Cover = item.Cover
                        });
                    }
                }
                loading.SetState(LoadingState.SUCCESS);

            }, error => {
                Utilities.CommonLoadingRetry(loading, error, async () => await LoadFirstPageDataAsync());
            });
        }

        #endregion

        #region Event

        private void Publish_Click(object sender, RoutedEventArgs e)
        {
            App.Current.RootFrame.Navigate(typeof(PublishRecipePage));
        }

        #endregion

    }
}
