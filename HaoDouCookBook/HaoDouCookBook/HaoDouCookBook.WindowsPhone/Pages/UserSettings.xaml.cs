using HaoDouCookBook.Common;
using HaoDouCookBook.Controls;
using Shared.Infrastructures;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace HaoDouCookBook.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class UserSettings : BackablePage
    {
        #region Life Cycle

        public UserSettings()
        {
            this.InitializeComponent();
            this.root.DataContext = SingletonProvider<UserSettingsConfig>.Instance;
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

            rootScrollViewer.ScrollToVerticalOffset(0);
        }

        #endregion

        #region Event

        #endregion
    }
}
