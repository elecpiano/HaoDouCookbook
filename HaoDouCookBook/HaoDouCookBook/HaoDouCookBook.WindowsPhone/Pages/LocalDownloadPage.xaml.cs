using HaoDouCookBook.Common;
using HaoDouCookBook.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace HaoDouCookBook.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LocalDownloadPage : BackablePage
    {
        public LocalDownloadPage()
        {
            this.InitializeComponent();
            Init();
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

            this.locadDownloadControl.UpdateViewModel();
        }

        private void Init()
        {
            locadDownloadControl.OnFolderTapped = folder =>
            {
                LocalDownloadFolderViewer.LocalDownloadFolderViewerParams paras = new LocalDownloadFolderViewer.LocalDownloadFolderViewerParams();
                paras.ViewModel = folder;

                App.CurrentInstance.RootFrame.Navigate(typeof(LocalDownloadFolderViewer), paras);
            };

            locadDownloadControl.CreatNewAction = () =>
            {
                BigTextBox.BigTextBoxParams paras = new BigTextBox.BigTextBoxParams();
                paras.MaxLength = 20;
                paras.PlaceholderText = "输入分类名称，最多20个字符";
                paras.ConfirmAction = newFolderName =>
                {
                    if (string.IsNullOrEmpty(newFolderName))
                    {
                        toast.Show("分类名不能为空");
                        return;
                    }

                    LocalCateogoryFolder newFolder = new LocalCateogoryFolder() { FolderName = newFolderName };
                    LocalDownloads.Instance.Folders.Add(newFolder);
                    LocalDownloads.Instance.CommitData();
                };

                App.CurrentInstance.RootFrame.Navigate(typeof(BigTextBox), paras);
            };
        }
    }
}
