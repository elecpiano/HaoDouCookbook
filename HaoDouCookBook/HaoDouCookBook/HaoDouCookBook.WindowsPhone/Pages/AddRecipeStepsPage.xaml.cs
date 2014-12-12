using System;
using HaoDouCookBook.Controls;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml.Navigation;
using HaoDouCookBook.ViewModels;

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
            public RecipeStep Step { get; set; }

            public AddRecipeStepsPageParams()
            {
                Step = new RecipeStep();
            }
        }

        #endregion

        #region Field && Property

        private RecipeStep step = new RecipeStep();


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

            AddRecipeStepsPageParams paras = e.Parameter as  AddRecipeStepsPageParams;
            if (paras != null && paras.Step != null)
            {
                step.Photo = paras.Step.Photo;
                step.Description = paras.Step.Description;
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

        #endregion

        #region Select Picture Result

        public void ContinueFileOpenPicker(FileOpenPickerContinuationEventArgs args)
        {
            if (args.Files != null && args.Files.Count > 0)
            {
                step.Photo = args.Files[0].Path;
            }
        }

        #endregion

    }
}
