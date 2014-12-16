using HaoDouCookBook.Common;
using HaoDouCookBook.Controls;
using HaoDouCookBook.HaoDou.API;
using HaoDouCookBook.HaoDou.DataModels.Discovery;
using HaoDouCookBook.ViewModels;
using Shared.Utility;
using System.Threading.Tasks;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

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
        private ProductPageParams pageParams;

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

            pageParams = e.Parameter as ProductPageParams;
            if (pageParams != null)
            {
                viewModel = new ProductPageViewModel();
                rooScrollViewer.ChangeViewExtersion(0, 0, 1.0f);
                LoadFirstPageDataAsync(pageParams.ProductId, pageParams.TopicId, pageParams.Type);
            }
            
        }

        #endregion

        #region Data Prepare

        private void DataBinding()
        {
            this.root.DataContext = viewModel;
        }

        private async Task LoadFirstPageDataAsync(int productId, int topicId, int type)
        {
            loading.SetState(LoadingState.LOADING);
            await RecipePhotoAPI.GetProdcuts(0, limit, type, productId, topicId,
                UserGlobal.Instance.UserInfo.Sign, 
                UserGlobal.Instance.GetInt32UserId(),
                UserGlobal.Instance.uuid,
                success => {
                    UpdateData(success);
                    loading.SetState(LoadingState.SUCCESS);
                }, error => {
                    Utilities.CommonLoadingRetry(loading, error, async () => await LoadFirstPageDataAsync(productId, topicId, type));
                });
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
                RemoveLoadMoreControl();
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
                    recipeProduct.IsDigg = item.IsDigg == 1 ? true : false;

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

                if(data.Recipes.Length == limit)
                {
                    EnusureLoadMoreControl();
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

        private void publish_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            App.Current.RootFrame.Navigate(typeof(PublishProductsPage));
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

        private void comment_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.input.Visibility = Windows.UI.Xaml.Visibility.Visible;
            this.input.Focus(Windows.UI.Xaml.FocusState.Keyboard);
            var dataContext = sender.GetDataContext<ViewModels.ProductPageRecipe>();
            input.SendAction = async comment =>
            {
                await CommentAPI.AddComment(
                    comment,
                    UserGlobal.Instance.GetInt32UserId(),
                    UserGlobal.Instance.UserInfo.Sign,
                    dataContext.ProductId,
                    2,
                    success =>
                    {
                        if (success.Message.Contains("成功"))
                        {
                            var newComment = new ViewModels.ProductPageComment()
                            {
                                Content = comment,
                                UserId = UserGlobal.Instance.GetInt32UserId(),
                                UserName = UserGlobal.Instance.UserInfo.Name
                            };

                            dataContext.Comments.Insert(0, newComment);
                            dataContext.CommentCount++;
                            this.input.ClearText();
                        }

                        this.input.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                        toast.Show(success.Message);
                    },
                    error =>
                    {
                        toast.Show(error.Message);
                        this.input.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                    });
            };
        }

        private async void Undigg_click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var dataContext = sender.GetDataContext<ViewModels.ProductPageRecipe>();
            await DiggAPI.Digg(
                dataContext.ProductId,
                UserGlobal.Instance.GetInt32UserId(),
                1,
                0,  // undigg
                UserGlobal.Instance.uuid,
                UserGlobal.Instance.UserInfo.Sign,
                success =>
                {
                    if (success.Message.Contains("成功"))
                    {
                        dataContext.IsDigg = false;
                        dataContext.LikeNumber--;
                    }

                    toast.Show(success.Message);


                },
               error =>
               {
                   toast.Show(error.Message);
               });
        }

        private async void Digg_click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var dataContext = sender.GetDataContext<ViewModels.ProductPageRecipe>();
            await DiggAPI.Digg(
                dataContext.ProductId,
                UserGlobal.Instance.GetInt32UserId(),
                1,
                1, //digg
                UserGlobal.Instance.uuid,
                UserGlobal.Instance.UserInfo.Sign,
                success =>
                {
                    if (success.Message.Contains("成功"))
                    {
                        dataContext.IsDigg = true;
                        dataContext.LikeNumber++;
                    }

                    toast.Show(success.Message);


                },
               error =>
               {
                   toast.Show(error.Message);
               });
        }

        #endregion

        #region Load More

        int page = 1;
        int limit = 20;

        private ViewModels.ProductPageRecipe loadMoreControlDataContext = new ViewModels.ProductPageRecipe() { IsLoadMore = true };

        public void EnusureLoadMoreControl()
        {
            if (viewModel.Products != null && !viewModel.Products.Contains(loadMoreControlDataContext))
            {
                viewModel.Products.Add(loadMoreControlDataContext);
            } 
        }

        public void RemoveLoadMoreControl()
        {
            if (viewModel.Products != null && viewModel.Products.Contains(loadMoreControlDataContext))
            {
                viewModel.Products.Remove(loadMoreControlDataContext);
            }
        }

        private void loadMore_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Utilities.RegistComonadLoadMoreBehavior(sender,
                async loadmore =>
                {
                    loadmore.SetState(LoadingState.LOADING);
                    await RecipePhotoAPI.GetProdcuts(
                        page * limit,
                        limit,
                        pageParams.Type,
                        pageParams.ProductId,
                        pageParams.TopicId,
                        UserGlobal.Instance.UserInfo.Sign,
                        UserGlobal.Instance.GetInt32UserId(),
                        UserGlobal.Instance.uuid,
                        success => {
                            if (success.Recipes != null)
                            {
                                RemoveLoadMoreControl();
                                foreach (var item in success.Recipes)
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
                                    recipeProduct.IsDigg = item.IsDigg == 1 ? true : false;

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

                                page++;
                                loadmore.SetState(LoadingState.SUCCESS);
                                if(success.Recipes.Length == limit)
                                {
                                    EnusureLoadMoreControl();
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
