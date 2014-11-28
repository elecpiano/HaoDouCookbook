using HaoDouCookBook.Controls;
using HaoDouCookBook.HaoDou.API;
using HaoDouCookBook.Utility;
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
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, new Uri(paras.Url));
                request.Headers.Add("Cookies", string.Format("appid={0};uuid={1}", HaoDouApiUrlHelper.APPID, DeviceHelper.GetUniqueDeviceID()));
                webview.NavigateWithHttpRequestMessage(request);
            }
        }

        #endregion

        #region Event


        #endregion
    }
}
