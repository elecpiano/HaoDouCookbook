﻿using HaoDouCookBook.Controls;
using System;
using Windows.UI.Xaml.Navigation;
using Windows.Web.Http;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace HaoDouCookBook.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TutorialViewPage : BackablePage
    {
        #region Page Parameters Definition

        public class TutorialViewPageParams
        {
            public string Url { get; set; }

            public TutorialViewPageParams()
            {

            }
        }

        #endregion

        #region Lief Cycle

        public TutorialViewPage()
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

            TutorialViewPageParams paras = e.Parameter as  TutorialViewPageParams;

            if (paras != null)
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, new Uri(paras.Url));
                webview.NavigateWithHttpRequestMessage(request);
            }
        }

        #endregion
    }
}
