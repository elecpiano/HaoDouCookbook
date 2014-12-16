using HaoDouCookBook.Controls;
using HaoDouCookBook.ViewModels;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;
using Shared.Utility;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace HaoDouCookBook.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CategoryTagsPage : BackablePage
    {
        #region Page Parameters Definition

        public class CategoryTagsPageParams
        {
            public string Title { get; set; }

            public ObservableCollection<CategoryTag> Tags { get; set; }

            public CategoryTagsPageParams()
            {
                Title = string.Empty;
                Tags = new ObservableCollection<CategoryTag>();
            }
        }

        #endregion

        #region Life Cycle

        public CategoryTagsPage()
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
            if (e.NavigationMode == NavigationMode.Back)
            {
                return;
            }

            CategoryTagsPageParams paras = e.Parameter as CategoryTagsPageParams;

            if(paras != null)
            {
                rootScrollViewer.ChangeViewExtersion(0, 0, 1.0f);
                this.title.Text = paras.Title;
                this.tagsList.ItemsSource = paras.Tags;
            }
        }

        #endregion

        #region Event

        private void Tag_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var dataContext = sender.GetDataContext<CategoryTag>();
            TagsPage.TagPageParams paras = new TagsPage.TagPageParams();
            paras.Id = dataContext.Id;
            paras.TagText = dataContext.Name;

            App.Current.RootFrame.Navigate(typeof(TagsPage), paras);
        }

        #endregion
    }
}
