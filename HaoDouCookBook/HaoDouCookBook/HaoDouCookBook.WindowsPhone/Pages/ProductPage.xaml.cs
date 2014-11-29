using HaoDouCookBook.HaoDou.API;
using HaoDouCookBook.HaoDou.DataModels.Discovery;
using HaoDouCookBook.Utility;
using HaoDouCookBook.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Shared.Utility;
using HaoDouCookBook.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace HaoDouCookBook.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ProductPage : BackablePage
    {
        #region Page Parameters Definition

        public class ProductPageParams
        {
            public int TopicId { get; set; }

            public int ProductId { get; set; }

            public int Type { get; set; }

            public ProductPageParams()
            {

            }
        }

        #endregion

        #region Field && Property

        private ProductPageViewModel viewModel = new ProductPageViewModel();


        #endregion

        #region Life Cycle

        public ProductPage()
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

            ProductPageParams paras = e.Parameter as ProductPageParams;
            if (paras != null)
            {
                viewModel = new ProductPageViewModel();
                rooScrollViewer.ScrollToVerticalOffset(0);
                LoadDataAsync(0, 20, paras.ProductId, paras.TopicId, paras.Type, string.Empty, null);
            }
            
        }

        #endregion

        #region Data Prepare

        private void DataBinding()
        {
            this.root.DataContext = viewModel;
        }

        private async Task LoadDataAsync(int offset, int limit, int productId, int topicId, int type, string sign, int? uid)
        {
            await RecipePhotoAPI.GetProdcuts(offset, limit, type, productId, topicId, sign, uid, DeviceHelper.GetUniqueDeviceID(), data =>
                {
                    UpdateData(data);

                }, error => { });
        }

        private void UpdateData(ProductPageData data)
        {
            DataBinding();

            viewModel.Content = data.Info.Content;
            viewModel.Title = data.Info.Title;
            viewModel.Cover = data.Info.Cover;
            viewModel.Count = data.Info.Count;
            viewModel.RecipeId = data.Info.RecipeId;
            viewModel.RecipeTitle = data.Info.RecipeTitle;

            if (data.Recipes != null)
            {
                foreach (var item in data.Recipes)
                {
                    ViewModels.ProductPageRecipe recipeProduct = new ViewModels.ProductPageRecipe();
                    recipeProduct.Title = item.Title;
                    recipeProduct.CreatTime = item.CreateTime;
                    recipeProduct.TimeStr = item.TimeStr;
                    recipeProduct.ProductId = item.Pid;
                    recipeProduct.RecipeId = item.Rid;
                    recipeProduct.UserAvatar = item.Avatar;
                    recipeProduct.UserName = item.UserName;
                    recipeProduct.UserId = item.UserId;
                    recipeProduct.LikeNumber = item.Digg;
                    recipeProduct.PhotoUrl = item.PhotoUrl;
                    recipeProduct.Intro = item.Intro;
                    recipeProduct.Position = item.Position;
                    recipeProduct.CommentCount = item.CommentCount;

                    if (item.CommentCount > 1)
                    {
                        recipeProduct.ShowAllCommentsTextVisible = true;
                    }


                    if (item.Comment != null)
                    {
                        foreach (var comment in item.Comment)
                        {

                            recipeProduct.Comments.Add(new ViewModels.ProductPageComment()
                            {
                                Content = comment.Content,
                                UserId = int.Parse(comment.UserId),
                                UserName = comment.UserName
                            });
                        }
                    }

                    viewModel.Products.Add(recipeProduct);
                }
            }
        }

        #endregion

        #region Event

        private void Recipe_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ViewModels.ProductPageRecipe recipeData = sender.GetDataContext<ViewModels.ProductPageRecipe>();

            // if recipeId is 0, it will just to TagsPage
            //
            if (0 == recipeData.RecipeId)
            {
                TagsPage.TagPageParams paras = new TagsPage.TagPageParams();
                paras.Id = null;
                paras.TagText = recipeData.Title;

                App.Current.RootFrame.Navigate(typeof(TagsPage), paras);
            }
            else // if recipeId is not equal to 0, just to RecipeInfoPage
            {
                RecipeInfoPage.RecipeInfoPageParams paras = new RecipeInfoPage.RecipeInfoPageParams();
                paras.RecipeId = recipeData.RecipeId;

                App.Current.RootFrame.Navigate(typeof(RecipeInfoPage), paras);
            }
        }



        private void RecipeImage_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ViewModels.ProductPageRecipe recipeData = sender.GetDataContext<ViewModels.ProductPageRecipe>();
            SingleProductViewPage.SingleProductViewPageParams paras = new SingleProductViewPage.SingleProductViewPageParams();
            paras.ProductId = recipeData.ProductId;

            App.Current.RootFrame.Navigate(typeof(SingleProductViewPage), paras);
        }


        private void ShowAllComment_Tapped(object sender, TappedRoutedEventArgs e)
        {
            CommentListPage.CommentListPageParams paras = new CommentListPage.CommentListPageParams();
            ViewModels.ProductPageRecipe recipeData = sender.GetDataContext<ViewModels.ProductPageRecipe>();

            paras.RecipeId = recipeData.ProductId;
            paras.Type = 2;
            paras.Cid = 0;

            App.Current.RootFrame.Navigate(typeof(CommentListPage), paras);
        }

        #endregion



    }
}
