using HaoDouCookBook.Common;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

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
