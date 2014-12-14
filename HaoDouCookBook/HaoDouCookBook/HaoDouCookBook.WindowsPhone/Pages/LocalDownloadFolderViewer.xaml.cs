using HaoDouCookBook.Common;
using HaoDouCookBook.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;
using Shared.Utility;
using HaoDouCookBook.ViewModels;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace HaoDouCookBook.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LocalDownloadFolderViewer : BackablePage
    {
        #region Page Parameter Definition

        public class LocalDownloadFolderViewerParams
        {
            public LocalCateogoryFolder ViewModel { get; set; }

            public LocalDownloadFolderViewerParams()
            {
                ViewModel = null;
            }

        }

        #endregion

        #region Life Cycle

        public LocalDownloadFolderViewer()
        {
            this.InitializeComponent();
            noItemText.Text = Constants.NO_RECIPES_IN_LOACL_FOLDER;

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

            LocalDownloadFolderViewerParams paras = e.Parameter as LocalDownloadFolderViewerParams;
            if(paras !=null)
            {
                rootScrollViewer.ScrollToVerticalOffset(0);
                paras.ViewModel.UpdateAllStuffsStrings();
                this.root.DataContext = paras.ViewModel;
            }
        }

        #endregion

        #region Event

        private void Recipe_Tapped(object sender, TappedRoutedEventArgs e)
        {
            RecipeInfoPage.RecipeInfoPageParams paras = new RecipeInfoPage.RecipeInfoPageParams();
            paras.LocalDownloadData = sender.GetDataContext<RecipeInfoPageViewModel>();

            App.Current.RootFrame.Navigate(typeof(RecipeInfoPage), paras);
        }

        #endregion
    }
}
