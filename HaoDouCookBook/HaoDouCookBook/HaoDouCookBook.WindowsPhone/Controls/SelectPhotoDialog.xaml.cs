using Windows.Storage.Pickers;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

// The Content Dialog item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace HaoDouCookBook.Controls
{
    public sealed partial class SelectPhotoDialog : ContentDialog
    {
        #region Field && Property

        #endregion

        #region Life Cycle

        public SelectPhotoDialog()
        {
            this.InitializeComponent();
        }

        #endregion

        #region Event

        #region Cannel

        private void Canncel_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Hide();
        }

        #endregion

        #region From Pictures Library
        private void FromPicturesLibraty_Tapped(object sender, TappedRoutedEventArgs e)
        {
            FileOpenPicker filePicker = new FileOpenPicker();
            filePicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            filePicker.ViewMode = PickerViewMode.Thumbnail;
            filePicker.FileTypeFilter.Add(".jpg");
            filePicker.FileTypeFilter.Add(".jpeg");
            filePicker.FileTypeFilter.Add(".png");

            this.Hide();
            filePicker.PickSingleFileAndContinue();
        }

        #endregion

        #endregion
    }
}
