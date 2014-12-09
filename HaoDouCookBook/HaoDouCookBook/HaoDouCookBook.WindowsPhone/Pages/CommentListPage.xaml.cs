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

            public SourcePage SourcePage { get; set; }

            public CommentListPageParams()
            {
                Type = 1;
                Cid = 0;
                RecipeId = 0;
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
                rootScrollViewer.ScrollToVerticalOffset(0);
                if (pageParams.SourcePage == SourcePage.NOTICE_PAGE)
                {
                    this.recipeHeader.Visibility = Windows.UI.Xaml.Visibility.Visible;
                }
                else
                {
                    this.recipeHeader.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                }

                DataBinding();
                LoadDataAsync(0, 20, pageParams.Type, pageParams.Cid, pageParams.RecipeId);
            }
        }

        #endregion

        #region Data Prepare

        private void DataBinding()
        {
            this.root.DataContext = viewModel;
        }

        private async Task LoadDataAsync(int offset, int limit, int type, int cid, int recipeId)
        {
            await CommentAPI.GetList(offset, limit, type, cid, recipeId, data =>
                {
                    UpdatePageData(data);

                }, error => { });
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
            }
        }

        #endregion

        private void Recipe_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            RecipeInfoPage.RecipeInfoPageParams paras = new RecipeInfoPage.RecipeInfoPageParams();
            paras.RecipeId = pageParams.RecipeId;

            App.Current.RootFrame.Navigate(typeof(RecipeInfoPage), paras);
        }
    }
}
