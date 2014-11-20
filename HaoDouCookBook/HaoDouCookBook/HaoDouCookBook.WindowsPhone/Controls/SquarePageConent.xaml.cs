using HaoDouCookBook.Common;
using HaoDouCookBook.HaoDou.API;
using HaoDouCookBook.HaoDou.DataModels.SquarePage;
using HaoDouCookBook.Pages;
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

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace HaoDouCookBook.Controls
{
    public sealed partial class SquarePageConent : UserControl
    {
        #region Field && Property

        public ObservableCollection<TopicModel> LatestTopics = new ObservableCollection<TopicModel>();
        public ObservableCollection<CategoryTileData> Categories = new ObservableCollection<CategoryTileData>();

        #endregion

        #region Life Cycle

        public SquarePageConent()
        {
            this.InitializeComponent();
            DataBinding();
            Init();
        }

        #endregion

        #region Data Prepare

        private void DataBinding()
        {
            this.topicList.ItemsSource = LatestTopics;
            this.CategoryList.ItemsSource = Categories;
        }

        private async Task LoadPageDataAsync()
        {
            await TopicAPI.GetGroupIndexData(0, 20, (data) =>
                {
                    UpdatePageData(data);

                }, error =>
                {

                });
        }

        private void UpdatePageData(SquarePageData data)
        {
            // Category
            //
            if (data.CateInfos != null)
            {
                Categories.Clear();
                foreach (var item in data.CateInfos)
                {
                    Categories.Add(new CategoryTileData() { ImageSource = item.ImageUrl, Title = item.Name, Id = item.CateId });
                }
            }

            // Topic
            //


            if (data.Topics != null)
            {
                foreach (var item in data.Topics)
                {
                    LatestTopics.Add(new TopicModel()
                    {
                        Author = item.UserName,
                        CreateTimeDescription = item.LastPostTime,
                        PreviewContent = item.PreviewContent,
                        Title = item.Title,
                        TopicPreviewImageSource = item.ImageUrl
                    });
                }

            }
        }

        #endregion

        #region Private Method
        private void Init()
        {
            LoadPageDataAsync();
        }

        #endregion

        #region Envent

        private void CategoryImageTile_Tapped(object sender, TappedRoutedEventArgs e)
        {
            App.Current.RootFrame.Navigate(typeof(TopicListPage), sender.GetDataContext<CategoryTileData>());
        }

        #endregion
    }
}
