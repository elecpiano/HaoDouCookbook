using HaoDouCookBook.Common;
using HaoDouCookBook.Pages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    public sealed partial class AddRecipeToLocalDownloadDialog : ContentDialog
    {
        public Action<LocalCateogoryFolder> onFolderTapped { get; set; }

        public Action CreateNewFolderAction { get; set; }

        public AddRecipeToLocalDownloadDialog()
        {
            this.InitializeComponent();
            Init();
        }

        private void Init()
        {
            this.localDownloadControl.OnFolderTapped = folder =>
            {
                if (onFolderTapped != null)
                {
                    onFolderTapped.Invoke(folder);
                    this.Hide();
                }
            };

            this.localDownloadControl.CreatNewAction = ()=>
            {
                if(CreateNewFolderAction != null)
                {
                    CreateNewFolderAction.Invoke();
                }
            };
        }


        private void hideButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }
    }
}
