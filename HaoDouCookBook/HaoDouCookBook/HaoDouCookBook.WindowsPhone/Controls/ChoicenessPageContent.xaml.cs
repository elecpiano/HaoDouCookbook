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

        private RecipeCategoryTileData yingYangCanZhuoData = new RecipeCategoryTileData()
        {
            MarkText = "营养餐桌",
            Title = "冬日食坚果 健康一冬天",
            Description = "冬季是吃坚果的最好时节，小小一颗不仅营养丰富，味道还很甘美，可说是冬季健康小零嘴的首选食品。"
        };

        #endregion

        #region Life Cycle

        public ChoicenessPageContent()
        {
            this.InitializeComponent();
            DataBiding();
            LoadDataAsync();
            this.Loaded += ChoicenessPageContent_Loaded;

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
                            RecipeName = item.RTitle
                        });
                }
            }


            // Ying Yao Can Zhuo
            //
            if (data.RecipeTables != null && data.RecipeTables.Length > 0)
            {
                var tableData = data.RecipeTables[0];
                yingYangCanZhuoData.MarkText = tableData.Icon;
                yingYangCanZhuoData.TileImage = tableData.Cover;
                yingYangCanZhuoData.Title = tableData.Title;
                yingYangCanZhuoData.Description = tableData.Intro;
            }

            // Rank
            //
            if (data.Rank != null)
            {
                foreach (var rankItem in data.Rank)
                {
                    ranklistData.Add(new RankItemData() { CoverImage =rankItem.Cover, Description = rankItem.Intro, Title = rankItem.Title }); 
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
            App.Current.RootFrame.Navigate(typeof(TagsPage), sender.GetDataContext<ViewModels.TagItem>());

        }

        private void RecipeCategoryTile_Tapped(object sender, TappedRoutedEventArgs e)
        {
            App.Current.RootFrame.Navigate(typeof(RecipeCategoryDetailPage), Utils.GetDataContext<ViewModels.RecipeCategoryTileData>(sender));
        }

        #endregion

    }
}
