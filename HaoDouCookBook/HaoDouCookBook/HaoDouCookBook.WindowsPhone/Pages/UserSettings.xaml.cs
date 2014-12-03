using HaoDouCookBook.Common;
using HaoDouCookBook.Controls;
using HaoDouCookBook.ViewModels;
using Shared.Infrastructures;
using Shared.Utility;
using System;
using System.Threading.Tasks;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace HaoDouCookBook.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class UserSettings : BackablePage
    {
        #region Life Cycle

        public UserSettings()
        {
            this.InitializeComponent();
            this.root.DataContext = SettingsPageViewModel.Instance;
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

            rootScrollViewer.ScrollToVerticalOffset(0);
            LoadDataAsync();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            SettingsPageViewModel.Instance.SaveDataAsync();
        }

        #endregion

        #region Data Prepare

        private void LoadDataAsync()
        {
            GetCacheSizeAsync(() =>
                {
                    SettingsPageViewModel.Instance.CacheSize = "努力计算中...";
                }, result =>
                {
                    SettingsPageViewModel.Instance.CacheSize = result;
                });
        }

        #endregion

        #region Event

        private void ClearCache_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            ClearCacheAsync(() => {
                SettingsPageViewModel.Instance.CacheSize = "缓存清理中...";
            },
            () => {
                SettingsPageViewModel.Instance.CacheSize = "0 B";
            });
        }

        #endregion

        #region Private Method

        private async Task GetCacheSizeAsync(Action onStart, Action<string> onCompleted)
        {
            if (onStart != null)
            {
                App.Current.RunAsync(onStart);
            }

            ulong size = await IsolatedStorageHelper.GetUserDataSizeAsync(Constants.LOCAL_USERDATA_FOLDER);
            string sizeDesc = "0 B";

            if (size < 1024d)
            {
                sizeDesc = size.ToString() + " B";
            }
            else if (size < 1048576d)
            {
                sizeDesc = Math.Round(((double)size / 1024d), 2).ToString() + " KB";
            }
            else
            {
                sizeDesc = Math.Round(((double)size / 1048576d), 2).ToString() + " MB"; 
            }

            if (onCompleted != null)
            {
                App.Current.RunAsync(() => onCompleted.Invoke(sizeDesc));
            }
        }

        private async Task ClearCacheAsync(Action onStart, Action onCompleted)
        {
            if (onStart != null)
            {
                App.Current.RunAsync(onStart);
            }

            await IsolatedStorageHelper.ClearUserDataAsync(Constants.LOCAL_USERDATA_FOLDER);

            if (onCompleted != null)
            {
                App.Current.RunAsync(onCompleted);
            }
        }


        #endregion

    }
}
