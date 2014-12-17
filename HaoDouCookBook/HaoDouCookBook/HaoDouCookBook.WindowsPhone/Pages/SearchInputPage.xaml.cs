using HaoDouCookBook.Controls;
using HaoDouCookBook.HaoDou.API;
using Shared.Utility;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

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
            rootScrollViewer.ChangeViewExtersion(0, int.MaxValue, 1.0f);
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
            GotoResultPage(sender.GetDataContext<string>());
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

        private void TextBox_GotFocus(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            this.searchImage.Opacity = 0;
            this.textBoxline.Source = new BitmapImage(new Uri("ms-appx:///../Assets/images/textboxBlueLine.png", UriKind.RelativeOrAbsolute));
            this.clearSearchImage.Opacity = 1;
        }

        private void TextBox_LostFocus(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            TextBox senderTextBox = sender as TextBox;
            if (senderTextBox != null)
            {
                if (string.IsNullOrEmpty(senderTextBox.Text.Trim()))
                {
                    this.searchImage.Opacity = 1;
                }
            }
            this.textBoxline.Source = new BitmapImage(new Uri("ms-appx:///../Assets/images/textboxGrayLine.png", UriKind.RelativeOrAbsolute));
            this.clearSearchImage.Opacity = 0;
        }

        private void TextBox_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                TextBox senderTextBox = sender as TextBox;
                if (senderTextBox != null)
                {
                    string kw = senderTextBox.Text.Trim();
                    Keywords.Clear();

                    if (!string.IsNullOrEmpty(kw))
                    {
                        GotoResultPage(kw);
                    }
                }
            }
        }

        private void clearSearchImage_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.searchTexBox.Text = string.Empty;
            this.searchImage.Opacity = 1;
        }

        #endregion

        #region Search

        private void GotoResultPage(string keyword)
        {
            SearchResultPage.SearchResultPageParams paras = new SearchResultPage.SearchResultPageParams();
            paras.Keyword = keyword;
            paras.TagId = string.Empty;

            App.CurrentInstance.RootFrame.Navigate(typeof(SearchResultPage), paras); 
        }

        #endregion

    }
}
