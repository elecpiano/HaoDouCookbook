using HaoDouCookBook.Common;
using HaoDouCookBook.Controls;
using HaoDouCookBook.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.UI.Xaml.Navigation;
using Shared.Utility;
using System.Linq;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Popups;
using System;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace HaoDouCookBook.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ShoppingListPage : BackablePage
    {
        #region Filed && Property

        private ShoppingListPageViewModel viewModel = new ShoppingListPageViewModel();

        #endregion

        #region Life Cycle

        public ShoppingListPage()
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

            stuffCategoryScrollViewer.ScrollToVerticalOffset(0);
            recipesScrollViewer.ScrollToVerticalOffset(0);
            LoadDataAsync();
        }

        #endregion

        #region Data Prepare

        private void DataBinding()
        {
            this.root.DataContext = viewModel;
        }

        private async Task LoadDataAsync()
        {
            await ShoppingList.Instance.LoadDataAsync();

            Dictionary<int, List<StuffItem>> categoryStuffDict = new Dictionary<int, List<StuffItem>>();
            Dictionary<int, string> categoryDict = new Dictionary<int, string>();
            StuffCategory boughtCategory = new StuffCategory();
            boughtCategory.Category = Constants.BOUGHT_STUFFCATEGORY_TITLE;
            boughtCategory.CategoryId = Constants.BOUGHT_STUFFCATEGORY_ID;

            if (ShoppingList.Instance.StuffsDict != null)
            {
                foreach (var stuff in ShoppingList.Instance.StuffsDict.Values)
                {
                    StuffItem sfi = new StuffItem() { Name = stuff.StuffName };

                    if (categoryStuffDict.ContainsKey(stuff.CategoryId))
                    {
                        categoryStuffDict[stuff.CategoryId].Add(sfi);
                    }
                    else
                    {
                        List<StuffItem> list = new List<StuffItem>();
                        list.Add(sfi);
                        categoryStuffDict.Add(stuff.CategoryId, list);
                    }

                    if (!categoryDict.ContainsKey(stuff.CategoryId))
                    {
                        categoryDict.Add(stuff.CategoryId, stuff.CategoryName);
                    }
                }
            }

            viewModel.StuffCategories.Clear();

            foreach (var kv in categoryDict)
            {
                StuffCategory ca = new StuffCategory();
                ca.Category = kv.Value;
                ca.CategoryId = kv.Key;

                if (categoryStuffDict.ContainsKey(kv.Key))
                {
                    foreach (var item in categoryStuffDict[kv.Key])
                    {
                        if (ShoppingList.Instance.StuffsDict[item.Name].IsBought)
                        {
                            boughtCategory.Stuffs.Add(item);
                        }
                        else
                        {
                            ca.Stuffs.Add(item);
                        }
                    }

                    viewModel.StuffCategories.Add(ca);
                }
            }

            viewModel.StuffCategories.Add(boughtCategory);
            SortViewModelStuffCategories();

            viewModel.Recipes.Clear();
            if (ShoppingList.Instance.Recipes != null)
            {
                foreach (var item in ShoppingList.Instance.Recipes)
                {
                    ViewModels.ShoppingListRecipe recipe = new ViewModels.ShoppingListRecipe();
                    recipe.RecipeId = item.RecipeId;
                    recipe.Name = item.RecipeName;

                    foreach (var sf in item.Stuffs)
                    {
                        recipe.Stuffs.Add(new StuffItem() { 
                            Name = sf.StuffName,
                            Weight = sf.Weight
                        });
                    }

                    viewModel.Recipes.Add(recipe);
                }
            }
        }

        #endregion

        #region Event

        private async void StuffItem_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            var dataContext = sender.GetDataContext<StuffItem>();

            // check if exist in "已购买食材"
            //
            if (ExistInBoughtStuffCategory(dataContext.Name))
            {
                // add to normal category
                //
                int realCategoryId = ShoppingList.Instance.StuffsDict[dataContext.Name].CategoryId;
                var category = FindStuffCategoryInViewModelById(realCategoryId);
                if (category != null)
                {
                    category.Stuffs.Add(dataContext);
                }
                else
                {
                    // if don't exist category, create one.
                    // 
                    StuffCategory cate = new StuffCategory();
                    cate.CategoryId = ShoppingList.Instance.StuffsDict[dataContext.Name].CategoryId;
                    cate.Category = ShoppingList.Instance.StuffsDict[dataContext.Name].CategoryName;
                    cate.Stuffs.Add(dataContext);
                    SortViewModelStuffCategories();
                }

                // remove from "已购买食材" 
                //
                RemoveItemFromBoughStuffCategory(dataContext.Name);

                // update local data
                //
                await ShoppingList.Instance.SetStuffBoughtStateAsync(dataContext.Name, false);
            }
            else
            {

                int realCategoryId = ShoppingList.Instance.StuffsDict[dataContext.Name].CategoryId;
                var category = FindStuffCategoryInViewModelById(realCategoryId);

                // remove from NORAML
                //
                category.Stuffs.Remove(dataContext);

                // if "已购买食材" don't contain this stuff, add it into the category
                //
                var boughtCategory = FindStuffCategoryInViewModelById(Constants.BOUGHT_STUFFCATEGORY_ID);
                if (boughtCategory != null)
                {

                    if (boughtCategory.Stuffs.All(s => s.Name != dataContext.Name))
                    {
                        boughtCategory.Stuffs.Add(dataContext);
                    }
                }
                else
                {
                    // if "已购买食材" is not exist, we creat one 
                    //
                    StuffCategory bc = new StuffCategory();
                    bc.CategoryId = Constants.BOUGHT_STUFFCATEGORY_ID;
                    bc.Category = Constants.BOUGHT_STUFFCATEGORY_TITLE;
                    bc.Stuffs.Add(dataContext);
                    viewModel.StuffCategories.Add(bc);
                }

                // update local data
                // 
                await ShoppingList.Instance.SetStuffBoughtStateAsync(dataContext.Name, true);
            }
        }

        private void ShowRecipe_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            FrameworkElement element = sender as FrameworkElement;
            var stuff = sender.GetDataContext<StuffItem>();
            var parent = element.GetParent<SimpleTreeListItem>();
            if (parent != null)
            {
                var stuffLists = parent.ItemsSource as IList<StuffItem>;
                if (stuffLists != null)
                {
                    int index = stuffLists.IndexOf(stuff);
                    if (index != 0)
                    {
                        element.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                    }
                }
            }
        }

        private void ShowRecipe_Tapped(object sender, TappedRoutedEventArgs e)
        {
            FrameworkElement element = sender as FrameworkElement;
            var parent = element.GetParent<SimpleTreeListItem>();
            if (parent != null)
            {
                var dataContext = parent.GetDataContext<ViewModels.ShoppingListRecipe>();
                if (dataContext != null)
                {
                    RecipeInfoPage.RecipeInfoPageParams paras = new RecipeInfoPage.RecipeInfoPageParams();
                    paras.RecipeId = dataContext.RecipeId;

                    App.Current.RootFrame.Navigate(typeof(RecipeInfoPage), paras);
                }
            }
        }
        private void Recipe_Hoding(object sender, HoldingRoutedEventArgs e)
        {
            FrameworkElement senderElement = sender as FrameworkElement;
            if (senderElement == null)
            {
                return;
            }

            FlyoutBase flyoutBase = FlyoutBase.GetAttachedFlyout(senderElement);
            if (flyoutBase != null)
            {
                flyoutBase.ShowAt(senderElement);
            }
        }

        private async void DeleteRecipe_Click(object sender, RoutedEventArgs e)
        {
            var recipe = sender.GetDataContext<ViewModels.ShoppingListRecipe>();

            ContentDialog dialog = new ContentDialog()
            {
                Title = "删除",
                Content = "确定要删除该菜谱吗？",
                FullSizeDesired = false,
                PrimaryButtonText = "确定",
                SecondaryButtonText = "取消"
            };

            var result = await dialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                if (recipe != null)
                {
                    await ShoppingList.Instance.DeleteRecipByIdAsync(recipe.RecipeId);
                    viewModel.Recipes.Remove(recipe);

                    foreach (var stuff in recipe.Stuffs)
                    {
                        bool isReferencedByOthers = false;
                        foreach (var recipeItem in viewModel.Recipes)
                        {
                            if (recipeItem.Stuffs.Any(s => s.Name == stuff.Name))
                            {
                                isReferencedByOthers = true;
                                break;
                            }
                        }

                        if (!isReferencedByOthers)
                        {
                            DeleteStuffInCatergory(ShoppingList.Instance.StuffsDict[stuff.Name].CategoryId, stuff.Name);
                            RemoveItemFromBoughStuffCategory(stuff.Name);
                        }
                    }

                }
            }
        }

        private async void DeleteAllRecipes_Click(object sender, RoutedEventArgs e)
        {
            ContentDialog dialog = new ContentDialog()
            {
                Title = "删除",
                Content = "确定要删除该菜谱吗？",
                FullSizeDesired = false,
                PrimaryButtonText = "确定",
                SecondaryButtonText = "取消"
            };

            var result = await dialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                await ShoppingList.Instance.DeleteAllRecipes();
                viewModel.Recipes.Clear();
                viewModel.StuffCategories.Clear();
            }
        }

        #endregion

        #region Private Methods

        private bool ExistInBoughtStuffCategory(string stuffName)
        {
            var boughtStuffCategory = FindStuffCategoryInViewModelById(Constants.BOUGHT_STUFFCATEGORY_ID);
            if (boughtStuffCategory != null)
            {
                return boughtStuffCategory.FindStuffByName(stuffName) != null;
            }

            return false;
        }

        private void RemoveItemFromBoughStuffCategory(string stuffName)
        {
            DeleteStuffInCatergory(Constants.BOUGHT_STUFFCATEGORY_ID, stuffName);
        }

        private void SortViewModelStuffCategories()
        {
            IEnumerable<StuffCategory> tempList = viewModel.StuffCategories.ToList().OrderBy(sc => sc.CategoryId).ToList();
            viewModel.StuffCategories.Clear();
            foreach (var item in tempList)
            {
                viewModel.StuffCategories.Add(item);
            }
        }

        private StuffCategory FindStuffCategoryInViewModelById(int id)
        {
            return viewModel.StuffCategories.FirstOrDefault(s => s.CategoryId == id);
        }

        private void DeleteStuffInCatergory(int categoryId, string stuffName)
        {
            var category = FindStuffCategoryInViewModelById(categoryId);
            if (category != null)
            {
                var stuffInCate = category.FindStuffByName(stuffName);
                if (stuffInCate != null)
                {
                    category.Stuffs.Remove(stuffInCate);
                }
            }
        }

      

        #endregion
    }
}
