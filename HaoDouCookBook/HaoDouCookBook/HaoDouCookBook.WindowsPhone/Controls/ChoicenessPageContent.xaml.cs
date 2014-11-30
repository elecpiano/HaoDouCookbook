using HaoDouCookBook.Common;
using HaoDouCookBook.ViewModels;
using HaoDouCookBook.Pages;
using Shared.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using HaoDouCookBook.HaoDou.API;
using System.Threading.Tasks;
using HaoDouCookBook.HaoDou.DataModels.ChoicenessPage;

using ViewModels = HaoDouCookBook.ViewModels;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace HaoDouCookBook.Controls
{
    public sealed partial class ChoicenessPageContent : UserControl
    {
        #region Field && Property

        private TagsListData tags = new TagsListData();
        private SpecialRecipeAlbumData specialRecipeAlbumData = new SpecialRecipeAlbumData();
        private ObservableCollection<RankItemData> ranklistData = new ObservableCollection<RankItemData>();
        private RecipeCategoryTileData chuFangBaoDianData = new RecipeCategoryTileData();
        private ObservableCollection<RecipeCategoryTileData> recipes = new ObservableCollection<RecipeCategoryTileData>();
        private RecipeCategoryTileData yingYangCanZhuoData = new RecipeCategoryTileData();

        #endregion

        #region Life Cycle

        public ChoicenessPageContent()
        {
            this.InitializeComponent();
            DataBiding();
            LoadDataAsync();
            this.Loaded += ChoicenessPageContent_Loaded;
            searchText.Text = "搜索菜谱、专题、话题";

        }

        void ChoicenessPageContent_Loaded(object sender, RoutedEventArgs e)
        {

        }

        #endregion

        #region Data Prepare

        private void DataBiding()
        {
            BindRankList();
            BindRecipeCategories();
        }

        private void BindRecipeCategories()
        {
            this.SpecialRecipeAlbum.DataContext = specialRecipeAlbumData;
            this.chuFangBiaoDian.DataContext = chuFangBaoDianData;
            this.yingYangCanZhuo.DataContext = yingYangCanZhuoData;
            this.recipesList.ItemsSource = recipes;
            this.TagsList.DataContext = tags;
        }

        private void BindRankList()
        {
            this.rankList.ItemsSource = ranklistData;
        }

        private async Task LoadDataAsync()
        {
            await RecipeAPI.GetCollectList(1, 6, null, async (data) =>
                          {
                              await UpdatePageDataAsync(data);
                          }, (error) =>
                          {

                          });
        }

        private async Task UpdatePageDataAsync(ChoicenessPageData data)
        {
            // Tags
            //
            if (data.Tags != null && data.Tags.Length == 8)
            {
                tags.Tag1.Icon = data.Tags[0].Icon;
                tags.Tag1.Text = data.Tags[0].Title;

                tags.Tag2.Icon = data.Tags[1].Icon;
                tags.Tag2.Text = data.Tags[1].Title;

                tags.Tag3.Icon = data.Tags[2].Icon;
                tags.Tag3.Text = data.Tags[2].Title;

                tags.Tag4.Icon = data.Tags[3].Icon;
                tags.Tag4.Text = data.Tags[3].Title;

                tags.Tag5.Icon = data.Tags[4].Icon;
                tags.Tag5.Text = data.Tags[4].Title;

                tags.Tag6.Icon = data.Tags[5].Icon;
                tags.Tag6.Text = data.Tags[5].Title;

                tags.Tag7.Icon = data.Tags[6].Icon;
                tags.Tag7.Text = data.Tags[6].Title;

                tags.Tag8.Icon = data.Tags[7].Icon;
                tags.Tag8.Text = data.Tags[7].Title;
            }


            // Ad
            //
            this.Ads.DataContext = data.ADs[0].Cover;

            //Special Recipe AlbumData
            //
            if (data.Album != null && data.Album.Length > 0)
            {
                var albumData = data.Album[0];
                specialRecipeAlbumData.Description = albumData.Intro;
                specialRecipeAlbumData.Title = albumData.Title;
                specialRecipeAlbumData.MainImageSource = albumData.Cover;
                specialRecipeAlbumData.Id = albumData.Id;
                foreach (var smallCover in albumData.SmallCover)
                {
                    specialRecipeAlbumData.DetailsImageSources.Add(smallCover);
                }
                specialRecipeAlbumData.AlbumMarkImageSource = albumData.Icon;
            }

            //Chu Fang Biao Dian 
            //
            if (data.RecipesWiki != null && data.RecipesWiki.Length > 0)
            {
                var baoDianData = data.RecipesWiki[0];
                chuFangBaoDianData.RecipeDescriptionOnImage = baoDianData.Intro;
                chuFangBaoDianData.Title = baoDianData.Title;
                chuFangBaoDianData.MarkText = baoDianData.Icon;
                chuFangBaoDianData.RecipeDescriptionOnImage = baoDianData.Intro;
                chuFangBaoDianData.TileImage = baoDianData.Cover;
                chuFangBaoDianData.Url = baoDianData.Url;
                chuFangBaoDianData.Id = baoDianData.Id;
            }

            // recipes
            //
            if (data.Recipes != null)
            {
                foreach (var item in data.Recipes)
                {
                    recipes.Add(new RecipeCategoryTileData()
                        {
                            Id = item.Id,
                            TileImage = item.Cover,
                            TitleIcon = item.Icon,
                            Title = item.Title,
                            Description = item.Intro,
                            Author = item.UserName,
                            AuthorRecipeComment = item.Desc,
                            RecipeName = item.RTitle,
                            Url = item.Url
                        });
                }
            }


            // Ying Yao Can Zhuo
            //
            if (data.RecipeTables != null && data.RecipeTables.Length > 0)
            {
                var tableData = data.RecipeTables[0];
                yingYangCanZhuoData.Id = tableData.Id;
                yingYangCanZhuoData.MarkText = tableData.Icon;
                yingYangCanZhuoData.TileImage = tableData.Cover;
                yingYangCanZhuoData.Title = tableData.Title;
                yingYangCanZhuoData.Description = tableData.Intro;
                yingYangCanZhuoData.Url = tableData.Url;
            }

            // Rank
            //
            if (data.Rank != null)
            {
                foreach (var rankItem in data.Rank)
                {
                    ranklistData.Add(new RankItemData()
                    {
                        CoverImage = rankItem.Cover,
                        Description = rankItem.Intro,
                        Title = rankItem.Title,
                        Id = rankItem.Id,
                        Type = rankItem.RankType
                    });
                }
            }

        }

        #endregion

        #region Envent

        private void rankListMore_Tapped(object sender, TappedRoutedEventArgs e)
        {
            App.Current.RootFrame.Navigate(typeof(RankListPage));
        }

        private void Tag_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var dataContext = sender.GetDataContext<ViewModels.TagItem>();
            TagsPage.TagPageParams paras = new TagsPage.TagPageParams();
            paras.Id = dataContext.Id;
            paras.TagText = dataContext.Text;

            App.Current.RootFrame.Navigate(typeof(TagsPage), paras);

        }

        private void RecipeCategoryTile_Tapped(object sender, TappedRoutedEventArgs e)
        {
            RecipeCategoryDetailPage.RecipeCategoryDefailPageParams paras = new RecipeCategoryDetailPage.RecipeCategoryDefailPageParams();
            var dataContext = sender.GetDataContext<ViewModels.RecipeCategoryTileData>();
            paras.Id = dataContext.Id;
            paras.Title = dataContext.Title;

            App.Current.RootFrame.Navigate(typeof(RecipeCategoryDetailPage), paras);
        }


        private void JustToWebPageRecipeTile_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var tiledata = sender.GetDataContext<RecipeCategoryTileData>();
            if (tiledata != null)
            {
                string searchMark = "&url=";
                int index = tiledata.Url.IndexOf(searchMark);

                if (index == -1)
                {
                    return;
                }

                string haodouUrlPrefix = string.Format("haodourecipe://haodou.com/wiki/info/?id={0}&url=", tiledata.Id);
                string targetUrl = tiledata.Url.Substring(index + searchMark.Length);
                App.Current.RootFrame.Navigate(typeof(ArticleViewer), new ArticleViewer.ArticleViewerPageParams() { Url = targetUrl });
            }

        }
        private void RankItemGrid_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var rankItemData = sender.GetDataContext<RankItemData>();
            RankViewPage.RankViewPageParams paras = new RankViewPage.RankViewPageParams();
            paras.Id = rankItemData.Id;
            paras.Type = rankItemData.Type;

            App.Current.RootFrame.Navigate(typeof(RankViewPage), paras);
        }


        private void SpecialRecipeAlbum_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var dataContext = sender.GetDataContext<SpecialRecipeAlbumData>();
            AlbumPage.AlbumPageParams paras = new AlbumPage.AlbumPageParams();
            paras.AlbumId = dataContext.Id;
            App.Current.RootFrame.Navigate(typeof(AlbumPage), paras);
        }

        private void searchBox_Tapped(object sender, TappedRoutedEventArgs e)
        {
            App.Current.RootFrame.Navigate(typeof(SearchPage));
        }

        #endregion

    }
}
