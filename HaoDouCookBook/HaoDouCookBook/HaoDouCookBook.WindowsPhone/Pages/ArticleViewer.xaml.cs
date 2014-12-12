using HaoDouCookBook.Common;
using HaoDouCookBook.Controls;
using HaoDouCookBook.HaoDou.API;
using System;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.Web.Http;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace HaoDouCookBook.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ArticleViewer : BackablePage
    {
        #region Page Parameter Definition

        public class ArticleViewerPageParams
        {
            public string Url { get; set; }

            public int TopicId { get; set; }

            public ArticleViewerPageParams()
            {
                Url = string.Empty;
            }
        }

        #endregion

        #region Field && Property

        private ArticleViewerPageParams pageParams;

        #endregion

        #region Life Cycle

        public ArticleViewer()
        {
            this.InitializeComponent();
            this.webview.DOMContentLoaded += webview_DOMContentLoaded;
            this.favorite.Visibility = Windows.UI.Xaml.Visibility.Visible;
            this.removeFavorite.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        }

        void webview_DOMContentLoaded(WebView sender, WebViewDOMContentLoadedEventArgs args)
        {
            webview.Visibility = Windows.UI.Xaml.Visibility.Visible;
            loading.SetState(LoadingState.DONE);
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

            if (e.Parameter != null)
            {
                pageParams = e.Parameter as  ArticleViewerPageParams;
                if (!string.IsNullOrEmpty(pageParams.Url))
                {
                    loading.SetState(LoadingState.LOADING);
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, new Uri(pageParams.Url));
                    request.Headers.Add("Cookies", string.Format("appid={0};uuid={1}", HaoDouApiUrlHelper.APPID, UserGlobal.Instance.uuid));
                    webview.NavigateWithHttpRequestMessage(request);
                    webview.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                    CheckIsFavorite(pageParams.TopicId);
                }
            }
        }

        #endregion

        #region Private Methods

        private async Task CheckIsFavorite(int id)
        {
            await FavoriteAPI.IsFav(UserGlobal.Instance.GetInt32UserId(), 3, id, UserGlobal.Instance.uuid, UserGlobal.Instance.UserInfo.Sign,
                success => {
                    if(success.Flag == 1)
                    {
                        this.favorite.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                        this.removeFavorite.Visibility = Windows.UI.Xaml.Visibility.Visible;
                    }
                }, error => {
                    toast.Show(error.Message);
                });
        }

        #endregion

        #region Event

        private async void Favorite_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (pageParams != null)
            {
                await FavoriteAPI.Add(UserGlobal.Instance.GetInt32UserId(), 3, pageParams.TopicId, UserGlobal.Instance.uuid, UserGlobal.Instance.UserInfo.Sign,
                    success =>
                    {
                        toast.Show(success.Message);
                        this.favorite.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                        this.removeFavorite.Visibility = Windows.UI.Xaml.Visibility.Visible;
                    },
                    error =>
                    {
                        toast.Show(error.Message);
                    });
            }
        }

        private async void removeFavorite_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (pageParams != null)
            {
                await FavoriteAPI.Del(UserGlobal.Instance.GetInt32UserId(), 3, pageParams.TopicId.ToString(), UserGlobal.Instance.uuid, UserGlobal.Instance.UserInfo.Sign, false,
                    success =>
                    {
                        toast.Show(success.Message);
                        this.favorite.Visibility = Windows.UI.Xaml.Visibility.Visible;
                        this.removeFavorite.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                    },
                    error =>
                    {
                        toast.Show(error.Message);
                    });
            }
        }

        private void Comments_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (pageParams != null)
            {
                CommentListPage.CommentListPageParams paras = new CommentListPage.CommentListPageParams();
                paras.RecipeId = pageParams.TopicId;
                paras.Type = 3;

                App.Current.RootFrame.Navigate(typeof(CommentListPage), paras);
            }
        }

        #endregion
    }
}
