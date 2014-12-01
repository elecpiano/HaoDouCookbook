using HaoDouCookBook.Common;
using HaoDouCookBook.Controls;
using HaoDouCookBook.HaoDou.API;
using HaoDouCookBook.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Shared.Utility;
using HaoDouCookBook.Utility;

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
            historyScrollViewer.ScrollToVerticalOffset(0);
            hotSearchScrollViewer.ScrollToVerticalOffset(0);

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
            App.Current.RunAsync(() =>
                {
                    // Only do somethings when the pivot item is "摇一摇"
                    //
                    if (this.pivot.SelectedIndex == 2)
                    {
                        ShakeGesture.StopListenning();
                        App.Current.RootFrame.Navigate(typeof(ShakePage));
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
            
        }

        private void Search_AppbarButton_Click(object sender, RoutedEventArgs e)
        {
            App.Current.RootFrame.Navigate(typeof(SearchInputPage));
        }

        #endregion

    }
}
