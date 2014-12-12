using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage.Pickers;
using Windows.Storage.Pickers.Provider;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

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

            filePicker.PickSingleFileAndContinue();
        }

        #endregion

        #endregion
    }
}
