using HaoDouCookBook.Common;
using HaoDouCookBook.Controls;
using HaoDouCookBook.HaoDou.API;
using HaoDouCookBook.ViewModels;
using System.Threading.Tasks;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;
using System.Linq;

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
        SingleProductViewPageParams pageParams;

        #endregion

        #region Life Cycle

        public SingleProductViewPage()
        {
            this.InitializeComponent();
            this.input.SendAction = Comment;
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

            pageParams = e.Parameter as SingleProductViewPageParams;

            if(pageParams != null)
            {
                viewModel = new SingleProductViewPageViewModel();
                rootScrollViewer.ChangeViewExtersion(0, 0, 1.0f);
                DataBinding();
                LoadFirstPageDataAsync(pageParams.ProductId);
            }

        }

        #endregion

        #region Data Prepare

        private void DataBinding()
        {
            this.root.DataContext = viewModel;
        }

        private async Task LoadFirstPageDataAsync(int id)
        {
            loading.SetState(LoadingState.LOADING);
            await RecipePhotoAPI.GetPhotoView(0, limit, id, UserGlobal.Instance.GetInt32UserId(),
                success =>
                {
                    viewModel.ProductId = success.Info.Pid;
                    viewModel.UserAvatar = success.Info.Avatar;
                    viewModel.UserName = success.Info.UserName;
                    viewModel.UserId = success.Info.UserId;
                    viewModel.TimeStr = success.Info.TimeStr;
                    viewModel.Cover = success.Info.Cover;
                    viewModel.DiggCount = int.Parse(success.Info.DiggCount);
                    viewModel.Intro = success.Info.Intro;
                    viewModel.Title = success.Info.Title;
                    viewModel.Position = success.Info.Position;
                    viewModel.TopicId = success.Info.TopicId;
                    viewModel.TopicName = success.Info.TopicName;
                    viewModel.RecipeId = success.Info.RecipeId;
                    viewModel.RecipeName = success.Info.RecipeTitle;
                    viewModel.IsDigg = success.Info.IsDigg == 1 ? true : false;

                    // digg list
                    //
                    if (success.Info.DiggList != null)
                    {
                        int diggCount = 0;
                        int.TryParse(success.Info.DiggCount, out diggCount);

                        foreach (var item in success.Info.DiggList)
                        {
                            viewModel.DiggList.Add(new ProductViewPageDigg() { 
                                    UserAvatar = item.Avatar,
                                    UserId = int.Parse(item.UserId),
                                    UserName = item.UserName,
                                    DiggCount = diggCount
                            });
                        }
                    }


                    // comments
                    //
                    if (success.CommentList != null)
                    {
                        RemoveLoadMoreControl();
                        foreach (var item in success.CommentList)
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

                        if (viewModel.Comments.Count == limit)
                        {
                            EnusureLoadMoreControl();
                        }
                    }

                    page = 1;
                    loading.SetState(LoadingState.SUCCESS);
                }, 
                error => {
                    Utilities.CommonLoadingRetry(loading, error, async () => await LoadFirstPageDataAsync(id));
                });
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

        private void DiggCount_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Utilities.ElementInItemsControl_SetVisibleAtEnd<ProductViewPageDigg>(sender, true);
        }

        private async void Undigg_click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            await DiggAPI.Digg(
                viewModel.ProductId,
                UserGlobal.Instance.GetInt32UserId(),
                1,
                0,  // undigg
                UserGlobal.Instance.uuid,
                UserGlobal.Instance.UserInfo.Sign,
                success =>
                {
                    if (success.Message.Contains("成功"))
                    {
                        viewModel.IsDigg = false;

                        var userPhoto = viewModel.DiggList.FirstOrDefault(user => user.UserId == UserGlobal.Instance.GetInt32UserId());
                        if(userPhoto != null)
                        {
                            viewModel.DiggList.Remove(userPhoto);
                        }

                        foreach (var item in viewModel.DiggList)
                        {
                            item.DiggCount--;
                        }
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
            await DiggAPI.Digg(
                viewModel.ProductId,
                UserGlobal.Instance.GetInt32UserId(),
                1,
                1, //digg
                UserGlobal.Instance.uuid,
                UserGlobal.Instance.UserInfo.Sign,
                success =>
                {
                    if (success.Message.Contains("成功"))
                    {
                        viewModel.IsDigg = true;


                        var userPhoto = viewModel.DiggList.FirstOrDefault(user => user.UserId == UserGlobal.Instance.GetInt32UserId());
                        if (userPhoto == null)
                        {
                            viewModel.DiggList.Insert(0, new ProductViewPageDigg() { 
                                UserAvatar = UserGlobal.Instance.UserInfo.Avatar,
                                UserId = UserGlobal.Instance.GetInt32UserId(),
                                DiggCount = viewModel.DiggCount,
                                UserName = UserGlobal.Instance.UserInfo.Name
                            });
                        }

                        foreach (var item in viewModel.DiggList)
                        {
                            item.DiggCount++;
                        }
                    }

                    toast.Show(success.Message);
                },
               error =>
               {
                   toast.Show(error.Message);
               });
        }

        private async void Comment(string comment)
        {
            await CommentAPI.AddComment(
                comment,
                UserGlobal.Instance.GetInt32UserId(),
                UserGlobal.Instance.UserInfo.Sign,
                viewModel.ProductId,
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

                        viewModel.Comments.Insert(0, new Comment() {
                            Avatar = UserGlobal.Instance.UserInfo.Avatar,
                            Content = comment,
                            CreateTime = "刚刚",
                            UserName = UserGlobal.Instance.UserInfo.Name,
                            UserId = UserGlobal.Instance.GetInt32UserId()
                        });
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
        }

        #endregion

        #region Load More

        int page = 1;
        int limit = 20;

        private Comment loadMoreControlDataContext = new Comment() { IsLoadMore = true };

        public void EnusureLoadMoreControl()
        {
            if (viewModel.Comments != null && !viewModel.Comments.Contains(loadMoreControlDataContext))
            {
                viewModel.Comments.Add(loadMoreControlDataContext);
            }
        }

        public void RemoveLoadMoreControl()
        {
            if (viewModel.Comments != null && viewModel.Comments.Contains(loadMoreControlDataContext))
            {
                viewModel.Comments.Remove(loadMoreControlDataContext);
            }
        }

        private void loadMore_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Utilities.RegistComonadLoadMoreBehavior(sender,
                 async loadmore =>
                 {
                     loadmore.SetState(LoadingState.LOADING);
                     await RecipePhotoAPI.GetPhotoView(
                         page * limit,
                         limit,
                         pageParams.ProductId,
                         UserGlobal.Instance.GetInt32UserId(),
                         success =>
                         {
                             if (success.CommentList != null)
                             {
                                 RemoveLoadMoreControl();
                                 foreach (var item in success.CommentList)
                                 {
                                     viewModel.Comments.Add(new Comment()
                                     {
                                         AtUserId = item.AtUserId,
                                         UserId = item.AtUserId,
                                         AtUserName = item.AtUserName,
                                         UserName = item.UserName,
                                         Avatar = item.Avatar,
                                         Content = item.Content,
                                         CreateTime = item.CreateTime
                                     });
                                 }

                                 if (viewModel.Comments.Count == limit)
                                 {
                                     EnusureLoadMoreControl();
                                 }

                                 page++;
                                 loadmore.SetState(LoadingState.SUCCESS);
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
