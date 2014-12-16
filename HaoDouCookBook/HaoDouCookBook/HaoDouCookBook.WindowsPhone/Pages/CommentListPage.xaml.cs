using HaoDouCookBook.Common;
using HaoDouCookBook.Controls;
using HaoDouCookBook.HaoDou.API;
using HaoDouCookBook.ViewModels;
using System.Threading.Tasks;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace HaoDouCookBook.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CommentListPage : BackablePage
    {
        #region Page Parameters Definition

        public enum SourcePage
        {
            NORMAL,
            NOTICE_PAGE
        }

        public class CommentListPageParams
        {
            public int Type { get; set; }

            public int Cid { get; set; }

            public int RecipeId { get; set; }

            public int NoticeType { get; set; }

            public SourcePage SourcePage { get; set; }

            public CommentListPageParams()
            {
                Type = 1;
                Cid = 0;
                RecipeId = 0;
                NoticeType = 0;
                SourcePage = CommentListPage.SourcePage.NORMAL;
            }

        }

        #endregion

        #region Field && Property

        private CommentListPageViewModel viewModel = new CommentListPageViewModel();
        private CommentListPageParams pageParams;

        #endregion

        #region Life Cycle


        public CommentListPage()
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

            pageParams = e.Parameter as CommentListPageParams;
            if (pageParams != null)
            {
                viewModel = new CommentListPageViewModel();
                rootScrollViewer.ChangeViewExtersion(0, 0, 1.0f);
                if (pageParams.SourcePage == SourcePage.NOTICE_PAGE)
                {
                    this.recipeHeader.Visibility = Windows.UI.Xaml.Visibility.Visible;
                }
                else
                {
                    this.recipeHeader.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                }

                DataBinding();
                LoadDataAsync(pageParams.Type, pageParams.Cid, pageParams.RecipeId);
            }
        }

        #endregion

        #region Data Prepare

        private void DataBinding()
        {
            this.root.DataContext = viewModel;
        }

        private async Task LoadDataAsync(int type, int cid, int recipeId)
        {
            loading.SetState(LoadingState.LOADING);
            await CommentAPI.GetList(0, limit, type, cid, recipeId, 
                success => {
                    UpdatePageData(success);
                    page = 1;
                    loading.SetState(LoadingState.SUCCESS);

                },
                error => {
                    Utilities.CommonLoadingRetry(loading, error, async () => await LoadDataAsync(type, cid, recipeId));
                });
        }

        private void UpdatePageData(HaoDou.DataModels.Discovery.CommentListPageData data)
        {
            if(data.Info != null)
            {
                viewModel.UserId = data.Info.UserId;
                viewModel.Image = data.Info.Img;
                viewModel.Info.Image = data.Info.Img;
                viewModel.Info.Title = data.Info.Title;
                viewModel.Info.Type = data.Info.Type;
                viewModel.Info.UserId = data.Info.UserId;
            }

            if (data.Comments != null)
            {
                RemoveLoadMoreControl();
                foreach (var item in data.Comments)
                {
                    string content = item.Content;
                    if(!string.IsNullOrEmpty(item.AtUserName))
                    {
                        string prefix = string.Format("@{0}:", item.AtUserName);
                        if (content.StartsWith(prefix))
                        {
                            content = content.Substring(prefix.Length);
                        }
                    }

                    viewModel.Comments.Add(new CommentListComment() { 
                            AtUserId = item.AtUserId,
                            AtUserName = item.AtUserName,
                            Avatar = item.Avatar,
                            Cid = item.Cid,
                            Content = content,
                            CreateTime = item.CreateTime,
                            PhotoFlag = item.PhotoFlag,
                            Photold = item.Photold,
                            PhotoUrl = item.PhotoUrl,
                            UserId = item.UserId,
                            UserName = item.UserName
                    });
                }

                if(data.Comments.Length == limit)
                {
                    EnusureLoadMoreControl();
                }
            }
        }

        #endregion

        #region Event

        private void Recipe_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            switch (pageParams.NoticeType)
            {
                case 101:
                case 104:
                    ProductPage.ProductPageParams pparas = new ProductPage.ProductPageParams();
                    pparas.ProductId = pageParams.RecipeId;
                    pparas.TopicId = pageParams.RecipeId;
                    pparas.Type = 3;
                    App.Current.RootFrame.Navigate(typeof(ProductPage), pparas);
                    break;
                case 3:
                    RecipeInfoPage.RecipeInfoPageParams rparas = new RecipeInfoPage.RecipeInfoPageParams();
                    rparas.RecipeId = pageParams.RecipeId;
                    App.Current.RootFrame.Navigate(typeof(RecipeInfoPage), rparas);
                    break;

            }
            
        }

        #endregion

        #region Load More

        int page = 1;
        int limit = 20;

        private CommentListComment loadMoreControlDataContext = new CommentListComment() { IsLoadMore = true };

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
                     await CommentAPI.GetList(
                         page * limit,
                         limit,
                         pageParams.Type,
                         pageParams.Cid,
                         pageParams.RecipeId,
                         success =>
                         {
                             if (success.Comments != null)
                             {
                                 RemoveLoadMoreControl();
                                 foreach (var item in success.Comments)
                                 {
                                     string content = item.Content;
                                     if (!string.IsNullOrEmpty(item.AtUserName))
                                     {
                                         string prefix = string.Format("@{0}:", item.AtUserName);
                                         if (content.StartsWith(prefix))
                                         {
                                             content = content.Substring(prefix.Length);
                                         }
                                     }

                                     viewModel.Comments.Add(new CommentListComment()
                                     {
                                         AtUserId = item.AtUserId,
                                         AtUserName = item.AtUserName,
                                         Avatar = item.Avatar,
                                         Cid = item.Cid,
                                         Content = content,
                                         CreateTime = item.CreateTime,
                                         PhotoFlag = item.PhotoFlag,
                                         Photold = item.Photold,
                                         PhotoUrl = item.PhotoUrl,
                                         UserId = item.UserId,
                                         UserName = item.UserName
                                     });
                                 }

                                 page++;
                                 loadmore.SetState(LoadingState.SUCCESS);
                                 if (success.Comments.Length == limit)
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
