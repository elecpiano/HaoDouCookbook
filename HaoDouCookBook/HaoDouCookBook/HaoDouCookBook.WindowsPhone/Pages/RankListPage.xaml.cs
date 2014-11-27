using HaoDouCookBook.Common;
using HaoDouCookBook.HaoDou.API;
using HaoDouCookBook.Utility;
using HaoDouCookBook.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using HaoDouCookBook.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace HaoDouCookBook.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RankListPage : BackablePage
    {

        #region Field && Property

        private ObservableCollection<RankItemData> rankListData = new ObservableCollection<RankItemData>();

        #endregion

        #region Life Cycle

        public RankListPage()
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

            if (e.NavigationMode == NavigationMode.Back)
            {
                return;
            }

            rankListData.Clear();
            rootScrollViewer.ScrollToVerticalOffset(0);
            LoadDataAsync(0, 20, null, null);
        }

        #endregion

        #region Data Prepare

        private void DataBinding()
        {
            this.ranklist.ItemsSource = rankListData;
        }

        private async Task LoadDataAsync(int offset, int limit, int? sign, int? uid)
        {
            await RankAPI.GetRankList(offset, limit, sign, uid, DeviceHelper.GetUniqueDeviceID(), data =>
                {
                    if (data.Items != null)
                    {
                        foreach (var item in data.Items)
                        {
                            rankListData.Add(new RankItemData() { 
                                Title = item.Title, 
                                CoverImage = item.Cover, 
                                Description = item.Intro, 
                                Type = item.RankType,
                                Id = int.Parse(item.Id)
                            });
                        }
                    }

                }, error => { });
        }

        #endregion

        #region Event

        private void RecipeItem_Tapped(object sender, TappedRoutedEventArgs e)
        {
            App.Current.RootFrame.Navigate(typeof(RankViewPage), sender.GetDataContext<RankItemData>());
        }

        #endregion

       
    }
}
