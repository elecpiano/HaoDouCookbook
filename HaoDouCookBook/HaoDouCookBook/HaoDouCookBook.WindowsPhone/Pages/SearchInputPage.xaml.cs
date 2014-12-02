using HaoDouCookBook.Controls;
using HaoDouCookBook.HaoDou.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Shared.Utility;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace HaoDouCookBook.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SearchInputPage : BackablePage
    {
        #region Field && Property

        private ObservableCollection<string> Keywords = new ObservableCollection<string>();
        private DateTime lastTextChangedTime = DateTime.Now;

        #endregion

        #region Life Cycle

        public SearchInputPage()
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

            Keywords.Clear();
            rootScrollViewer.ScrollToVerticalOffset(0);
        }

        #endregion

        #region Data Prepare

        private void DataBinding()
        {
            this.keywordsList.ItemsSource = Keywords;
        }

        private async Task LoadSuggestionAsync(string keyword)
        {
            await SearchAPI.GetSuggestion(keyword, data =>
                {
                    if (data.Keywords != null)
                    {
                        foreach (var item in data.Keywords)
                        {
                            Keywords.Add(item);
                        }
                    }

                }, error => { });
        }

        #endregion

        #region Event

        private void Suggestion_Tapped(object sender, TappedRoutedEventArgs e)
        {
            SearchResultPage.SearchResultPageParams paras = new SearchResultPage.SearchResultPageParams();
            paras.Keyword = sender.GetDataContext<string>();
            paras.TagId = string.Empty;

            App.Current.RootFrame.Navigate(typeof(SearchResultPage), paras);
        }

        private async void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            DateTime now = DateTime.Now;
            if(now - lastTextChangedTime < TimeSpan.FromSeconds(0.7))
            {
                return;
            }

            TextBox senderTextBox = sender as TextBox;
            if (senderTextBox != null)
            {
                string kw = senderTextBox.Text.Trim();
                Keywords.Clear();

                if (!string.IsNullOrEmpty(kw))
                {
                    lastTextChangedTime = now;
                    await LoadSuggestionAsync(kw);
                }
            }
        }

        #endregion

    }
}
