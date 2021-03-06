﻿using HaoDouCookBook.Common;
using HaoDouCookBook.Controls;
using HaoDouCookBook.ViewModels;
using Shared.Utility;
using System;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
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

            rootScrollViewer.ChangeViewExtersion(0, 0, 1.0f);
            LoadDataAsync();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            SettingsPageViewModel.Instance.CommitDataAsync();
        }

        #endregion

        #region Data Prepare

        private void LoadDataAsync()
        {
            SettingsPageViewModel.Instance.SignedIn = Utilities.SignedIn();
            GetCacheSizeAsync(() =>
                {
                    SettingsPageViewModel.Instance.CacheSize = "正在获取缓存大小...";
                }, result =>
                {
                    SettingsPageViewModel.Instance.CacheSize = result;
                });
        }

        #endregion

        #region Event

        private void Feedback_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            App.CurrentInstance.RootFrame.Navigate(typeof(FeedbackPage));
        }

        private async void ClearCache_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            ContentDialog dialog = new ContentDialog()
            {
                Title = "清理缓存提示",
                Content = "清除缓存之后，收藏的菜谱将无法离线\n继续查看，确认清除？",
                FullSizeDesired = false,
                PrimaryButtonText = "确定",
                SecondaryButtonText = "取消"
            };

            var result = await dialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                await ClearCacheAsync(() =>
                {
                    SettingsPageViewModel.Instance.CacheSize = "缓存清理中...";
                },
                () =>
                {
                    SettingsPageViewModel.Instance.CacheSize = "无缓存";
                });
            }
        }
        private void About_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            App.CurrentInstance.RootFrame.Navigate(typeof(AboutPage));
        }

        private async void Logout_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            ContentDialog dialog = new ContentDialog()
            {
                Title = "提示",
                Content = "您确定要退出登录吗？",
                FullSizeDesired = false,
                PrimaryButtonText = "确定",
                SecondaryButtonText = "取消"
            };

            var result = await dialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                await UserGlobal.Instance.Logout(() =>
                {
                    App.CurrentInstance.RootFrame.Navigate(typeof(MainPage));

                }, error => { });
            }
        }

        #endregion

        #region Private Method

        private async Task GetCacheSizeAsync(Action onStart, Action<string> onCompleted)
        {
            if (onStart != null)
            {
                App.CurrentInstance.RunAsync(onStart);
            }

            ulong size = await IsolatedStorageHelper.GetUserDataSizeAsync(Constants.LOCAL_USERDATA_FOLDER, Constants.PUBLISH_RECIPES_TEMP_FOLDER);
            string sizeDesc = "无缓存";

            if (size == 0)
            {
                if (onCompleted != null)
                {
                    App.CurrentInstance.RunAsync(() => onCompleted.Invoke(sizeDesc));
                }
                return;
            }
            if (size > 0)
            {
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
            }

            if (onCompleted != null)
            {
                App.CurrentInstance.RunAsync(() => onCompleted.Invoke(sizeDesc));
            }
        }

        private async Task ClearCacheAsync(Action onStart, Action onCompleted)
        {
            if (onStart != null)
            {
                App.CurrentInstance.RunAsync(onStart);
            }

            await IsolatedStorageHelper.ClearUserDataAsync(Constants.LOCAL_USERDATA_FOLDER, Constants.PUBLISH_RECIPES_TEMP_FOLDER);

            if (onCompleted != null)
            {
                App.CurrentInstance.RunAsync(onCompleted);
            }
        }


        #endregion


    }
}
