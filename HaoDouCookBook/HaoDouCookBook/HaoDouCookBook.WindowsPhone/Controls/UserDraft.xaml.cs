using HaoDouCookBook.Common;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Shared.Utility;
using HaoDouCookBook.ViewModels;
using HaoDouCookBook.Pages;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace HaoDouCookBook.Controls
{
    public sealed partial class UserDraft : UserControl
    {
        #region Life Cycle

        public UserDraft()
        {
            this.InitializeComponent();
            UpdataViewModel();
            this.noItemText.Text = Constants.I_DONT_HAVE_DRAFT;
        }

        #endregion

        #region Public Methods

        public void UpdataViewModel()
        {
            DataBinding();
        }

        public void ResetScrollViewerToBegin()
        {
            this.rootScrollViewer.ScrollToVerticalOffset(0);
        }
        
        #endregion

        #region Private Methods

        private void DataBinding()
        {
            this.draftslist.ItemsSource = UserRecipeDraft.Instance.Recipes;
            this.noItemsPanel.DataContext = UserRecipeDraft.Instance.Recipes;
        }

        #endregion

        #region Event

        private void draft_Tapped(object sender, TappedRoutedEventArgs e)
        {
            sender.ShowFlayout(); 
        }

        private async void deleteDraft_Click(object sender, RoutedEventArgs e)
        {
            var dataContext = sender.GetDataContext<PublishRecipePageViewModel>();
            UserRecipeDraft.Instance.Recipes.Remove(dataContext);

            // if there are no drafs after delete this draft
            // we clear all cache recipes image published
            //
            await IsolatedStorageHelper.DeleteFolderAsync(Constants.PUBLISH_RECIPES_TEMP_FOLDER);
            UserRecipeDraft.Instance.CommitData();
        }

        private void editDraft_Click(object sender, RoutedEventArgs e)
        {
            var dataContext = sender.GetDataContext<PublishRecipePageViewModel>();
            PublishRecipePage.PublishRecipePageParams paras = new PublishRecipePage.PublishRecipePageParams();
            paras.ViewModel = dataContext;
            paras.ViewModel.IsInStepOne = false;

            App.Current.RootFrame.Navigate(typeof(PublishRecipePage), paras);
        }

        #endregion
    }
}
