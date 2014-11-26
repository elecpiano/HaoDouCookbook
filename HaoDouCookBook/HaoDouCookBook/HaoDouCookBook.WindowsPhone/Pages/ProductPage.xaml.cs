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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace HaoDouCookBook.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ProductPage : Page
    {
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
            Windows.Phone.UI.Input.HardwareButtons.BackPressed += HardwareButtons_BackPressed;

            int topicId = int.MinValue;
            int productId = int.MinValue;
            int type = 1;

            if (e.Parameter is DishTileData)
            {
                var data = e.Parameter as DishTileData;
                topicId = data.Id;
                productId = data.ProductId;
            }
            else if (e.Parameter is ViewModels.Meal)
            {
                var data = e.Parameter as ViewModels.Meal;
                topicId = data.Id;
                productId = data.Id;
            }

            if (topicId != int.MinValue && productId != int.MinValue)
            {
                LoadDataAsync(0, 20, productId, topicId, type, string.Empty, null);
            }
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            Windows.Phone.UI.Input.HardwareButtons.BackPressed -= HardwareButtons_BackPressed;
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
            viewModel.Content = data.Info.Content;
            viewModel.Title = data.Info.Title;
            viewModel.Cover = data.Info.Cover;
            viewModel.Count = data.Info.Count;

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

                    if (item.Comment != null)
                    {
                        foreach (var comment in item.Comment)
                        {

                            recipeProduct.Comments.Add(new ViewModels.ProductPageComment() {
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

        void HardwareButtons_BackPressed(object sender, Windows.Phone.UI.Input.BackPressedEventArgs e)
        {
            App.Current.RootFrame.GoBack();
            e.Handled = true;
        }

        private void Recipe_Tapped(object sender, TappedRoutedEventArgs e)
        {
            FrameworkElement fe = sender as FrameworkElement;
            if (fe != null)
            {
                string recipeId = fe.Tag.ToString();
                if(!String.IsNullOrEmpty(recipeId))
                {
                    App.Current.RootFrame.Navigate(typeof(RecipeInfoPage), recipeId);
                }
            }
        }
        
        #endregion

      

    }
}
