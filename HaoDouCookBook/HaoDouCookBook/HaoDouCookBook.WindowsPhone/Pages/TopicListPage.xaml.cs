using HaoDouCookBook.HaoDou.API;
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
    public sealed partial class TopicListPage : BackablePage
    {
        #region Field && Property

        private ObservableCollection<TopicModel> topics = new ObservableCollection<TopicModel>();
        private int pageOffset = 0;

        #endregion

        #region Life Cycle

        public TopicListPage()
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
            
            topics.Clear();
            if (e.Parameter != null)
            {
                topics.Clear();
                rootScrollViewer.ScrollToVerticalOffset(0);
                CategoryTileData category = e.Parameter as CategoryTileData;
                this.title.Text = category.Title;
                LoadDataAsync(int.Parse(category.Id));
            }

        }

        #endregion

        #region Data Prepare

        private void DataBinding()
        {
            this.topicList.ItemsSource = topics;
        }

        private async Task LoadDataAsync(int cateId)
        {
            await TopicAPI.GetList(pageOffset, 20, cateId, null, data =>
                {
                    if (data.Items != null)
                    {
                        foreach (var item in data.Items)
                        {
                            topics.Add(new TopicModel()
                            {
                                Id = item.TopicId,
                                Url = item.Url,
                                TopicPreviewImageSource = item.ImageUrl,
                                Title = item.Title,
                                PreviewContent = item.PreviewContent,
                                Author = item.UserName,
                                CreateTimeDescription = item.LastPostTime,
                            });
                        }
                    }

                }, error =>
                {
                });
        }


        #endregion

        #region Event

        private void TopicTile_Tapped(object sender, TappedRoutedEventArgs e)
        {
            App.Current.RootFrame.Navigate(typeof(ArticleViewer), sender.GetDataContext<TopicModel>().Url);
        }

        #endregion
    }
}
