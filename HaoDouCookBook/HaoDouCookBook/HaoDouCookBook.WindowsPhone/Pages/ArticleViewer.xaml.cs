using HaoDouCookBook.Common;
using HaoDouCookBook.Controls;
using HaoDouCookBook.HaoDou.API;
using HaoDouCookBook.Utility;
using System;
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

            public ArticleViewerPageParams()
            {
                Url = string.Empty;
            }
        }

        #endregion

        #region Life Cycle

        public ArticleViewer()
        {
            this.InitializeComponent();
            this.webview.ScriptNotify += webview_ScriptNotify;
        }

        void webview_ScriptNotify(object sender, NotifyEventArgs e)
        {

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
                ArticleViewerPageParams paras = e.Parameter as  ArticleViewerPageParams;
                if(!string.IsNullOrEmpty(paras.Url))
                {
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, new Uri(paras.Url));
                    request.Headers.Add("Cookies", string.Format("appid={0};uuid={1}", HaoDouApiUrlHelper.APPID, UserGlobal.Instance.uuid));
                    webview.NavigateWithHttpRequestMessage(request);
                }
            }
        }

        #endregion

        #region Event


        #endregion
    }
}
