using HaoDouCookBook.Common;
using HaoDouCookBook.Controls;
using HaoDouCookBook.HaoDou.API;
using HaoDouCookBook.Utility;
using HaoDouCookBook.ViewModels;
using Shared.Utility;
using System;
using System.Threading.Tasks;
using Windows.Phone.Devices.Notification;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace HaoDouCookBook.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SearchPage : BackablePage
    {
        #region Filed && Property

        private SearchPageViewModel viewModel = new SearchPageViewModel();

        #endregion

        #region Life Cycle

        public SearchPage()
        {
            this.InitializeComponent();
            DataBinding();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            LoadSearchLogsAsync();
            ShakeGesture.StartListenning(ShakenTracking);

            if (e.NavigationMode == NavigationMode.Back)
            {
                return;
            }

            pivot.SelectedIndex = 0;
            historyScrollViewer.ChangeViewExtersion(0, 0, 1.0f);
            hotSearchScrollViewer.ChangeViewExtersion(0, 0, 1.0f);

            LoadHotSearchDataAsync();

        }

        #endregion

        #region Data Prepare

        private void DataBinding()
        {
            this.root.DataContext = viewModel;
        }

        private async Task LoadSearchLogsAsync()
        {
            var seachLogs = await HaoDouSearchHelper.GetAllKeywordsAsync();
            viewModel.SearchLogs.Clear();

            foreach (var item in seachLogs)
            {
                viewModel.SearchLogs.Add(item);
            }
        }

        private async Task LoadHotSearchDataAsync()
        {
            await SearchAPI.GetHotSearch(data =>
            {
                if (data.HotSearchKeywords != null)
                {
                    viewModel.HotSearchKeywords.Clear();
                    foreach (var item in data.HotSearchKeywords)
                    {
                        viewModel.HotSearchKeywords.Add(item);
                    }
                }

            }, error => { });
        }


        #endregion

        #region Event

        private void ShakenTracking()
        {
            App.CurrentInstance.RunAsync(() =>
                {
                    // Only do somethings when the pivot item is "摇一摇"
                    //
                    if (this.pivot.SelectedIndex == 2)
                    {
                        ShakeGesture.StopListenning();

                        // Shake with vibration
                        //
                        if (SettingsPageViewModel.Instance.ShakeWithVibration)
                        {
                            VibrationDevice vd = VibrationDevice.GetDefault();
                            if(vd != null)
                            {
                                vd.Vibrate(TimeSpan.FromSeconds(0.3));
                            }
                        }

                        App.CurrentInstance.RootFrame.Navigate(typeof(ShakePage));
                    }
                });
        }

        private void ClearSearchLogs_Tapped(object sender, TappedRoutedEventArgs e)
        {
            viewModel.SearchLogs.Clear();
            HaoDouSearchHelper.ClearAllKeywordsAsync();
        }

        private void SearchLogs_Tapped(object sender, TappedRoutedEventArgs e)
        {
            SearchResultPage.SearchResultPageParams paras = new SearchResultPage.SearchResultPageParams();
            paras.Keyword = sender.GetDataContext<string>();
            paras.TagId = string.Empty;

            App.CurrentInstance.RootFrame.Navigate(typeof(SearchResultPage), paras);

        }

        private void Search_AppbarButton_Click(object sender, RoutedEventArgs e)
        {
            App.CurrentInstance.RootFrame.Navigate(typeof(SearchInputPage));
        }

        #endregion

    }
}
