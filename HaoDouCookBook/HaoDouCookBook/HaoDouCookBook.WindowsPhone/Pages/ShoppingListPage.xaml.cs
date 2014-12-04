using HaoDouCookBook.Common;
using HaoDouCookBook.Controls;
using HaoDouCookBook.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.UI.Xaml.Navigation;
using Shared.Utility;
using System.Linq;

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
                    StuffItem sfi = new StuffItem() { Id = stuff.StuffId, Name = stuff.StuffName };

                    if (categoryStuffDict.ContainsKey(stuff.CategoryId))
                    {
                        if (stuff.IsBought)
                        {
                            boughtCategory.Stuffs.Add(sfi);
                        }
                        else
                        {
                            categoryStuffDict[stuff.CategoryId].Add(sfi);
                        }
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
                        ca.Stuffs.Add(new StuffItem() { Id = item.Id, Name = item.Name });
                    }

                    viewModel.StuffCategories.Add(ca);
                }
            }

            viewModel.StuffCategories.Add(boughtCategory);
            SortViewModelStuffCategories();
        }

        #endregion

        #region Event

        private async void StuffItem_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            var dataContext = sender.GetDataContext<StuffItem>();

            // check if exist in "已购买食材"
            //
            if (ExistInBoughtStuffCategory(dataContext.Id))
            {
                // add to normal category
                //
                int realCategoryId = ShoppingList.Instance.StuffsDict[dataContext.Id].CategoryId;
                var category = viewModel.StuffCategories.First(c => c.CategoryId == realCategoryId);
                if (category != null)
                {
                    category.Stuffs.Add(dataContext);
                }
                else
                {
                    // if don't exist category, create one.
                    // 
                    StuffCategory cate = new StuffCategory();
                    cate.CategoryId = ShoppingList.Instance.StuffsDict[dataContext.Id].CategoryId;
                    cate.Category = ShoppingList.Instance.StuffsDict[dataContext.Id].CategoryName;
                    cate.Stuffs.Add(dataContext);
                }

                // update local data
                //
                await ShoppingList.Instance.SetStuffBoughtStateAsync(dataContext.Id, false);

                // remove from "已购买食材" 
                //
                RemoveItemFromBoughStuffCategory(dataContext.Id);
            }
            else
            {

                int realCategoryId = ShoppingList.Instance.StuffsDict[dataContext.Id].CategoryId;
                var category = viewModel.StuffCategories.First(c => c.CategoryId == realCategoryId);

                // remove from NORAML
                //
                category.Stuffs.Remove(dataContext);

                // if "已购买食材" don't contain this stuff, add it into the category
                //
                var boughtCategory = viewModel.StuffCategories.FirstOrDefault(c => c.CategoryId == Constants.BOUGHT_STUFFCATEGORY_ID);
                if (boughtCategory != null)
                {

                    if (boughtCategory.Stuffs.All(s => s.Id != dataContext.Id))
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
                await ShoppingList.Instance.SetStuffBoughtStateAsync(dataContext.Id, true);
            }
        }


        #endregion

        #region Private Methods

        private bool ExistInBoughtStuffCategory(int stuffId)
        {
            var boughtStuffCategory = viewModel.StuffCategories.FirstOrDefault(f => f.CategoryId == Constants.BOUGHT_STUFFCATEGORY_ID);
            if (boughtStuffCategory != null)
            {
                return boughtStuffCategory.Stuffs.Any(s => s.Id == stuffId);
            }

            return false;
        }

        private void RemoveItemFromBoughStuffCategory(int stuffId)
        {
            var boughtStuffCategory = viewModel.StuffCategories.First(f => f.CategoryId == Constants.BOUGHT_STUFFCATEGORY_ID);

            if (boughtStuffCategory != null)
            {
                var stuff = boughtStuffCategory.Stuffs.First(s => s.Id == stuffId);
                boughtStuffCategory.Stuffs.Remove(stuff);
            }
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

        #endregion
    }
}
