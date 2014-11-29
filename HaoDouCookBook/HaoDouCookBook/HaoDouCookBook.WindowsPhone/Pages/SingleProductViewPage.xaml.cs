using HaoDouCookBook.Controls;
using HaoDouCookBook.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
using ViewModels = HaoDouCookBook.ViewModels;
using System.Threading.Tasks;
using HaoDouCookBook.HaoDou.API;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace HaoDouCookBook.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SingleProductViewPage : BackablePage
    {
        #region Page Parameters Definition

        public class SingleProductViewPageParams
        {
            public int ProductId { get; set; }

            public SingleProductViewPageParams()
            {

            }
        }


        #endregion

        #region Field && Property

        private SingleProductViewPageViewModel viewModel = new SingleProductViewPageViewModel();

        #endregion

        #region Life Cycle

        public SingleProductViewPage()
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

            SingleProductViewPageParams paras = e.Parameter as SingleProductViewPageParams;

            if(paras != null)
            {
                viewModel = new SingleProductViewPageViewModel();
                rootScollViewer.ScrollToVerticalOffset(0);
                DataBinding();
                LoadDataAsync(paras.ProductId, null);
            }

        }

        #endregion

        #region Data Prepare

        private void DataBinding()
        {
            this.root.DataContext = viewModel;
        }

        private async Task LoadDataAsync(int id, int? uid)
        {
            await RecipePhotoAPI.GetPhotoView(id, uid, data =>
                {
                    viewModel.ProductId = data.Info.Pid;
                    viewModel.UserAvatar = data.Info.Avatar;
                    viewModel.UserName = data.Info.UserName;
                    viewModel.UserId = data.Info.UserId;
                    viewModel.TimeStr = data.Info.TimeStr;
                    viewModel.Cover = data.Info.Cover;
                    viewModel.DiggCount = int.Parse(data.Info.DiggCount);
                    viewModel.Intro = data.Info.Intro;
                    viewModel.Title = data.Info.Title;
                    viewModel.Position = data.Info.Position;
                    viewModel.TopicId = data.Info.TopicId;
                    viewModel.TopicName = data.Info.TopicName;
                    viewModel.RecipeId = data.Info.RecipeId;
                    viewModel.RecipeName = data.Info.RecipeTitle;


                    // digg list
                    //
                    if (data.Info.DiggList != null)
                    {
                        foreach (var item in data.Info.DiggList)
                        {
                            viewModel.DiggList.Add(new ProductViewPageDigg() { 
                                    UserAvatar = item.Avatar,
                                    UserId = int.Parse(item.UserId),
                                    UserName = item.UserName
                            });
                        }
                    }


                    // comments
                    //
                    if (data.CommentList != null)
                    {
                        foreach (var item in data.CommentList)
                        {
                            viewModel.Comments.Add(new Comment() {
                                    AtUserId = item.AtUserId,
                                    UserId = item.AtUserId,
                                    AtUserName = item.AtUserName,
                                    UserName = item.UserName,
                                    Avatar = item.Avatar,
                                    Content = item.Content,
                                    CreateTime = item.CreateTime
                            });
                        }
                    }

                }, error => { });
        }

        #endregion

        #region Event

        private void RecipeName_Tapped(object sender, TappedRoutedEventArgs e)
        {
            // if recipeId is 0, it will just to TagsPage
            //
            if (0 == viewModel.RecipeId)
            {
                TagsPage.TagPageParams paras = new TagsPage.TagPageParams();
                paras.Id = null;
                paras.TagText = viewModel.RecipeName;

                App.Current.RootFrame.Navigate(typeof(TagsPage), paras);
            }
            else // if recipeId is not equal to 0, just to RecipeInfoPage
            {
                RecipeInfoPage.RecipeInfoPageParams paras = new RecipeInfoPage.RecipeInfoPageParams();
                paras.RecipeId = viewModel.RecipeId;

                App.Current.RootFrame.Navigate(typeof(RecipeInfoPage), paras);
            }
        }

        private void TopicName_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ProductPage.ProductPageParams paras = new ProductPage.ProductPageParams();
            paras.ProductId = viewModel.ProductId;
            paras.TopicId = viewModel.TopicId;
            paras.Type = 1;

            App.Current.RootFrame.Navigate(typeof(ProductPage), paras);
        }

        #endregion
    }
}
