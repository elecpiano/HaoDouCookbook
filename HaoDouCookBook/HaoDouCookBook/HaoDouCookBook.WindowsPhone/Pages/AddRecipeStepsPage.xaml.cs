using System;
using HaoDouCookBook.Controls;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml.Navigation;
using HaoDouCookBook.ViewModels;
using Shared.Utility;
using HaoDouCookBook.Common;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace HaoDouCookBook.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AddRecipeStepsPage : BackablePage, IFileOpenPickerContinuable
    {
        #region Page Parameter Definition

        public class AddRecipeStepsPageParams
        {
            public PublishRecipeStep Step { get; set; }

            public Action<PublishRecipeStep> CompletedAction { get; set; }

            public AddRecipeStepsPageParams()
            {
                Step = new PublishRecipeStep();
            }
        }

        #endregion

        #region Field && Property

        private PublishRecipeStep step = new PublishRecipeStep();
        private AddRecipeStepsPageParams pageParas;

        #endregion

        #region Life Cycle
        public AddRecipeStepsPage()
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

            pageParas = e.Parameter as AddRecipeStepsPageParams;
            if (pageParas != null && pageParas.Step != null)
            {
                DataBinding();
                step.StepPhoto = pageParas.Step.StepPhoto;
                step.StepIntro = pageParas.Step.StepIntro;
            }
        }

        #endregion

        #region Data

        private void DataBinding()
        {
            this.root.DataContext = step;
        }

        #endregion

        #region Event

        private async void SelectPhoto_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            SelectPhotoDialog dialog = new SelectPhotoDialog();
            await dialog.ShowAsync();
        }

        private void Completed_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(step.StepIntro.Trim()))
            {
                toast.Show("描述不能为空");
                return;
            }

            if (pageParas != null && pageParas.CompletedAction != null)
            {
                pageParas.CompletedAction.Invoke(step);
            }

            App.Current.RootFrame.GoBack();
        }

        #endregion

        #region Select Picture Result

        public async void ContinueFileOpenPicker(FileOpenPickerContinuationEventArgs args)
        {
            if (args.Files != null && args.Files.Count > 0)
            {
                var file = args.Files[0];
                step.StepPhoto = await IsolatedStorageHelper.CopyFromFileAsync(file, Constants.PUBLISH_RECIPES_TEMP_FOLDER);
            }
        }

        #endregion

    }
}
