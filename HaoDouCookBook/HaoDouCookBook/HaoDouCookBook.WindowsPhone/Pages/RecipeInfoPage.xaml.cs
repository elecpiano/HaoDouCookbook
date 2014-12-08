using HaoDouCookBook.Common;
using HaoDouCookBook.Controls;
using HaoDouCookBook.HaoDou.API;
using HaoDouCookBook.HaoDou.DataModels.ChoicenessPage;
using HaoDouCookBook.Utility;
using HaoDouCookBook.ViewModels;
using Shared.Utility;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;
using DataModels = HaoDouCookBook.HaoDou.DataModels;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace HaoDouCookBook.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RecipeInfoPage : BackablePage
    {
        #region Page Parameters Definition

        public class RecipeInfoPageParams
        {
            public int RecipeId { get; set; }

            public RecipeInfoPageParams()
            {

            }
        }

        #endregion

        #region Field && Property

        private RecipeInfoPageViewModel viewModel = new RecipeInfoPageViewModel();

        #endregion

        #region Life Cycle

        public RecipeInfoPage()
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

            RecipeInfoPageParams paras = e.Parameter as RecipeInfoPageParams;
            if(paras != null)
            {
                viewModel = new RecipeInfoPageViewModel();
                rootScrollViewer.ScrollToVerticalOffset(0);
                DataBinding();
                LoadDataAsync(string.Empty, null, paras.RecipeId);

            }

        }
        #endregion

        #region Data Prepare

        private void DataBinding()
        {
            this.root.DataContext = viewModel;
        }

        private async Task LoadDataAsync(string sign, int? uid, int recipeId)
        {
            await InfoAPI.GetInfo(sign, uid, UserGlobal.Instance.uuid, recipeId, data =>
                {
                    UpdateViewModel(data);
                }, error => { });
        }

        private void UpdateViewModel(InfoPageData data)
        {
            CheckIfAddedToShoppingList(data.Info.RecipeId);

            var infoData = data.Info;

            if (infoData == null)
            {
                return;
            }

            viewModel.RecipeId = infoData.RecipeId;
            viewModel.Cover = infoData.Cover;
            viewModel.Title = infoData.Title;
            viewModel.CookTime = infoData.CookTime;
            viewModel.ReadyTime = infoData.ReadyTime;
            viewModel.Tips = infoData.Tips;
            viewModel.UserName = infoData.UserName;
            viewModel.FavoriteCount = infoData.FavoriteCount;
            viewModel.PhotoCount = infoData.PhotoCount;
            viewModel.ReviewTime = infoData.ReviewTime;
            viewModel.UserId = infoData.UserId;
            viewModel.Avatar = infoData.Avatar;
            viewModel.ViewCount = infoData.ViewCount;
            viewModel.Type = infoData.Type;
            viewModel.UserCount = infoData.UserCount;
            viewModel.LikeCount = infoData.LikeCount;
            viewModel.IsLike = infoData.IsLike == 1 ? true : false;
            viewModel.AdFlag = infoData.AdFlag;
            viewModel.ProductCount = infoData.ProductCount;
            viewModel.IsVip = infoData.Vip == 1 ? true : false;
            viewModel.Intro = infoData.Intro;
            viewModel.CommentCount = string.IsNullOrEmpty(infoData.CommentCount) ? 0 : int.Parse(infoData.CommentCount);

            // Fodd stuff
            //
            if (infoData.Stuff != null)
            {
                foreach (var item in infoData.Stuff)
                {
                    ViewModels.FoodStuff stuff = new ViewModels.FoodStuff();
                    stuff.Category = item.Category;
                    stuff.CategoryId = item.CategoryId;
                    stuff.Id = item.Id;
                    stuff.Name = item.Name;
                    stuff.Type = item.Type;
                    stuff.Weight = item.Weight;
                    stuff.FoodFlag = item.FoodFlag == 1 ? true : false;

                    if (infoData.MainStuff != null)
                    {
                        stuff.IsMainStuff = infoData.MainStuff.Any(s => s.Id == item.Id);
                    }

                    viewModel.Stuffs.Add(stuff);
                }

            }

            // Cook setps
            //
            if (infoData.Steps != null)
            {
                for (int i = 0; i < infoData.Steps.Length; i++)
                {
                    var stepData = infoData.Steps[i];
                    viewModel.CookSteps.Add(new ViewModels.CookStep()
                    {
                        Intro = stepData.Intro,
                        Photo = stepData.StepPhoto,
                        StepNumber = i + 1
                    });

                }
            }

            // ad
            //
            viewModel.Ad.Image = infoData.AD.Image;
            viewModel.Ad.Title = infoData.AD.Title;
            viewModel.Ad.Url = infoData.AD.Url;

            // Tags
            //
            if (infoData.Tags != null)
            {
                foreach (var item in infoData.Tags)
                {
                    viewModel.Tags.Add(new TagItem() { Id = item.Id, Text = item.Name.Trim() });
                }
            }

            // Pohto List
            //
            if (infoData.PhotoList != null)
            {
                foreach (var item in infoData.PhotoList)
                {
                    viewModel.PhotoList.Add(item);
                }
            }

            // comments
            //
            if (infoData.CommentList != null)
            {
                foreach (var item in infoData.CommentList)
                {
                    viewModel.Comments.Add(new ViewModels.Comment() {
                        AtUserId = item.AtUserId,
                        AtUserName =item.AtUserName,
                        UserId = item.UserId,
                        UserName =item.UserName,
                        Avatar = item.Avatar,
                        Content = item.Content,
                        CreateTime = item.CreateTime
                    });
                }
            }

            // Products
            //
            if (infoData.Product != null)
            {
                foreach (var item in infoData.Product)
                {
                    viewModel.Products.Add(new ViewModels.Product() {
                         Content = item.Content,
                         UserId = item.UserId,
                         UserName = item.UserName,
                         Image = item.Image,
                         ProductId = item.Pid
                    });
                    
                }
            }
        }

        private void CheckIfAddedToShoppingList(int recipeId)
        {
            if (ShoppingList.Instance.RecipeExists(recipeId))
            {
                this.addToShoppingListText.Text = "√购买清单";
            }
            else
            {
                this.addToShoppingListText.Text = "+购买清单";
            }
        }

        #endregion

        #region Event

        private void Stuff_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var dataContext = sender.GetDataContext<ViewModels.FoodStuff>();

            if (dataContext.FoodFlag)
            {
                StuffInfoPage.StuffInfoPageParams paras = new StuffInfoPage.StuffInfoPageParams();
                paras.Id = dataContext.Id;
                paras.Title = dataContext.Name;
                App.Current.RootFrame.Navigate(typeof(StuffInfoPage), paras);
            }
        }

        private async void AddToShoppingList_Tapped(object sender, RoutedEventArgs e)
        {
            int recipeId = viewModel.RecipeId;

            if (ShoppingList.Instance.RecipeExists(recipeId))
            {
                this.toast.Show("请勿重复添加");
            }
            else
            {
                string recipeName = viewModel.Title;
                List<DataModels.ChoicenessPage.FoodStuff> sstuff = new List<DataModels.ChoicenessPage.FoodStuff>();

                foreach (var item in viewModel.Stuffs)
                {
                    sstuff.Add(new DataModels.ChoicenessPage.FoodStuff()
                    {
                        Id = item.Id,
                        Name = item.Name,
                        CategoryId = item.CategoryId,
                        Category = item.Category,
                        Weight = item.Weight,
                    });
                }

                await ShoppingList.Instance.AddRecipeAsync(recipeId, recipeName, sstuff);
                this.addToShoppingListText.Text = "√购买清单";
            }

        }

        private void ShowAllProduct_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ProductPage.ProductPageParams paras = new ProductPage.ProductPageParams();
            paras.ProductId = 0;
            paras.TopicId = viewModel.RecipeId;
            paras.Type = 2;

            App.Current.RootFrame.Navigate(typeof(ProductPage), paras);
        }

        private void SingleProduct_Tapped(object sender, TappedRoutedEventArgs e)
        {

            SingleProductViewPage.SingleProductViewPageParams paras = new SingleProductViewPage.SingleProductViewPageParams();
            var dataContext = sender.GetDataContext<ViewModels.Product>();
            paras.ProductId = dataContext.ProductId;

            App.Current.RootFrame.Navigate(typeof(SingleProductViewPage), paras);
        }

        private void Tags_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var dataContext = sender.GetDataContext<TagItem>();
            TagsPage.TagPageParams paras = new TagsPage.TagPageParams();
            paras.TagText = dataContext.Text;
            paras.Id = dataContext.Id;

            App.Current.RootFrame.Navigate(typeof(TagsPage), paras);
        }

        private void ShowAllComment_Tapped(object sender, TappedRoutedEventArgs e)
        {
            CommentListPage.CommentListPageParams paras = new CommentListPage.CommentListPageParams();
            paras.RecipeId = viewModel.RecipeId;
            paras.Type = 0;
            paras.Cid = 0;

            App.Current.RootFrame.Navigate(typeof(CommentListPage), paras);
        }

        #endregion

    }
}
