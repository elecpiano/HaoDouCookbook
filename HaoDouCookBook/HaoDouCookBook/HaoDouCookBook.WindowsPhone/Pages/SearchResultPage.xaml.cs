using HaoDouCookBook.Controls;
using HaoDouCookBook.HaoDou.API;
using HaoDouCookBook.Utility;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace HaoDouCookBook.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SearchResultPage : BackablePage
    {
        #region Page Parameters Definition

        public class SearchResultPageParams
        {
            public string Keyword { get; set; }

            public string TagId { get; set; }

            public SearchResultPageParams()
            {
                Keyword = string.Empty;
            }
        }

        #endregion

        #region Field && Property

        private SearchResultPageViewModel viewModel = new SearchResultPageViewModel();

        #endregion

        #region Life Cycle

        public SearchResultPage()
        {
            this.InitializeComponent();
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

            SearchResultPageParams paras = e.Parameter as SearchResultPageParams;
            if (paras != null)
            {
                viewModel = new SearchResultPageViewModel();
                rootScrollViewer.ScrollToVerticalOffset(0);
                DataBinding();
                LoadDataAsync(paras.Keyword, paras.TagId);
            }
        }

        #endregion

        #region Data Prepare

        private void DataBinding()
        {
            this.root.DataContext = viewModel;
        }

        private async Task LoadDataAsync(string keyword, string tagid)
        {
            await SearchAPI.GetSearchIndex(keyword, tagid, DeviceHelper.GetUniqueDeviceID(), data =>
                {
                    if (data.Food != null)
                    {
                        viewModel.Food.FoodCover = data.Food.Cover;
                        viewModel.Food.FoodId = data.Food.Id;
                        viewModel.Food.FoodIntro = data.Food.Intro;
                        viewModel.Food.FoodName = data.Food.Name;
                    }

                    if (data.Recipe != null)
                    {
                        foreach (var item in data.Recipe.Items)
                        {
                            viewModel.Recipes.Add(new TagRecipeData() { 
                                FoodStuff = item.Stuff,
                                RecipeId = item.RecipeId,
                                LikeNumber = item.LikeCount,
                                ViewNumber = item.ViewCount,
                                RecipeName = item.Title,
                                PreviewImageSource = item.Cover
                            });
                        }
                    }

                    if (data.Album != null && data.Album.AlbumItems != null && data.Album.AlbumItems.Length > 0)
                    {
                        var albumData = data.Album.AlbumItems[0];
                        viewModel.AlbumId = int.Parse(albumData.AlbumId);
                        viewModel.AlbumIntro = albumData.Intro;
                        viewModel.AlbumRecipeCount = albumData.RecipeCount;
                        viewModel.AlbumTitle = albumData.Title;
                        viewModel.AlbumViewCount = albumData.ViewCount;
                        viewModel.AlbumLikeCount = albumData.LikeCount;
                    }

                    if (data.TopicData != null && data.TopicData.Topics != null && data.TopicData.Topics.Length > 0)
                    {
                        var topicData = data.TopicData.Topics[0];
                        viewModel.Topic.Author = topicData.UserName;
                        viewModel.Topic.CreateTimeDescription = topicData.CreateTime;
                        viewModel.Topic.Id = topicData.TopicId.ToString();
                        viewModel.Topic.PreviewContent = topicData.Intro;
                        viewModel.Topic.Title = topicData.Title;
                        viewModel.Topic.Url = topicData.Url;
                        viewModel.Topic.TopicPreviewImageSource = topicData.Cover;
                    }

                }, error => { });
        }

        #endregion
    }
}
