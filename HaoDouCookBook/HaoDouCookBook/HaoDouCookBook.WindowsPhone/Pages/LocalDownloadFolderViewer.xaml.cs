using HaoDouCookBook.Common;
using HaoDouCookBook.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;
using Shared.Utility;
using HaoDouCookBook.ViewModels;
using System;

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

        LocalDownloadFolderViewerParams pageParams;

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

            pageParams = e.Parameter as LocalDownloadFolderViewerParams;
            if(pageParams!=null)
            {
                rootScrollViewer.ScrollToVerticalOffset(0);
                pageParams.ViewModel.UpdateAllStuffsStrings();
                this.root.DataContext = pageParams.ViewModel;
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

        private async void deleteAll_click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            await Utilities.ShowOKCancelDialog("温馨提示：", "您确定要删除该分类下的所有菜谱吗？",
                () => {
                    if (pageParams == null
                        || pageParams.ViewModel == null
                        || pageParams.ViewModel.Recipes == null)
                    {
                        return;
                    }

                    pageParams.ViewModel.Recipes.Clear();
                    LocalDownloads.Instance.CommitData();
                }, null);
        }

        private void Recipe_Holding(object sender, HoldingRoutedEventArgs e)
        {
            sender.ShowFlayout();
        }

        private async void deleteSingle_click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var dataContext = sender.GetDataContext<RecipeInfoPageViewModel>();
            await Utilities.ShowOKCancelDialog("温馨提示", "您确定要删除选中的菜谱吗？",
                () => {

                    if (pageParams == null
                        || pageParams.ViewModel == null
                        || pageParams.ViewModel.Recipes == null)
                    {
                        return;
                    }

                    pageParams.ViewModel.Recipes.Remove(dataContext);
                }, null);
        }

        private async void moveTo_click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if(pageParams == null 
                || pageParams.ViewModel == null 
                || pageParams.ViewModel.Recipes == null)
            {
                return;
            }

            var dataContext = sender.GetDataContext<RecipeInfoPageViewModel>();

            pageParams.ViewModel.Visible = false;
            
            AddRecipeToLocalDownloadDialog dialog = new AddRecipeToLocalDownloadDialog();
            dialog.onFolderTapped = folder =>
            {
                pageParams.ViewModel.Recipes.Remove(dataContext);
                folder.Recipes.Add(dataContext);

                LocalDownloads.Instance.CommitData();
                dialog.Hide();
            };

            await dialog.ShowAsync();
            pageParams.ViewModel.Visible = true;
        }
    }
}
