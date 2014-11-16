using HaoDouCookBook.Common;
using HaoDouCookBook.Models;
using HaoDouCookBook.Pages;
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

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace HaoDouCookBook.Controls
{
    public sealed partial class ChoicenessPageContent : UserControl
    {
        #region Field && Property

        private ObservableCollection<TopicModel> ranklistData = new ObservableCollection<TopicModel>();
        private RecipeCategoryTileData cookGemsData = new RecipeCategoryTileData()
        {
            MarkText = "厨房宝典"
        };

        private RecipeCategoryTileData hotRecipesData = new RecipeCategoryTileData()
        {
            Title = "热门菜谱",
            Description = "500道超高人气菜谱随便挑"
        };

        private RecipeCategoryTileData personalRecipesData = new RecipeCategoryTileData()
        {
            Title = "私人定制",
            Description = "特别为您定制100道菜，可能您会喜欢"
        };

        #endregion

        #region Life Cycle

        public ChoicenessPageContent()
        {
            this.InitializeComponent();
            this.Loaded += ChoicenessPageContent_Loaded;

        }

        void ChoicenessPageContent_Loaded(object sender, RoutedEventArgs e)
        {
            DataBiding();
            Test();
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
            this.cookGemsTile.DataContext = cookGemsData;
            this.hotRecipesTile.DataContext = hotRecipesData;
            this.personalRecipesTile.DataContext = personalRecipesData;
        }

        private void BindRankList()
        {
            this.rankList.ItemsSource = ranklistData;
        }

        #endregion

        #region Test

        private void Test()
        {
            // recipe category
            //
            cookGemsData.RecipeDescriptionOnImage = "市场上的餐具琳琅满目，如何挑选餐具是大家感到困惑的事情？小便为各位介绍一些选购玩具的小姐窍门，学起来～";
            cookGemsData.TileImage = Constants.DEFAULT_TOPIC_IMAGE;
            cookGemsData.Title = "你必须知道的买碗技巧";

            hotRecipesData.RecipeName = "芝麻酥饼";
            hotRecipesData.Author = "美美家的厨房";
            hotRecipesTile.AuthorRecipeComment = "因为害怕黄油长肉肉，所以哦哦";
            hotRecipesData.TileImage = Constants.DEFAULT_TOPIC_IMAGE;

            personalRecipesData.TileImage = Constants.DEFAULT_TOPIC_IMAGE;
            personalRecipesData.RecipeName = "红焖养肉山药";
            personalRecipesData.Author = "尚食之文";
            personalRecipesData.AuthorRecipeComment = "山药可以提供人体大量的能量啊啊啊啊啊";
            personalRecipesData.TitleIcon = Constants.DEFAULT_USER_PHOTO;


            // rank list
            //
            RankListTest();
        }

        private void RankListTest()
        {
            ranklistData.Clear();
            ranklistData.Add(new TopicModel() { TopicPreviewImageSource = Constants.DEFAULT_TOPIC_IMAGE, Title = "制作清汤火锅的小窍门", PreviewContent = "昨天为大家推荐了番茄鱼火锅，大家都很喜欢，" });
            ranklistData.Add(new TopicModel() { TopicPreviewImageSource = Constants.DEFAULT_TOPIC_IMAGE, Title = "好豆里那些优秀的“国际范”", PreviewContent = "上个月fishmama给大家介绍了很多咱们好豆里优秀的豆亲，受到很多豆亲的关注，" });
            ranklistData.Add(new TopicModel() { TopicPreviewImageSource = Constants.DEFAULT_TOPIC_IMAGE, Title = "酥脆的酒鬼花生也能自己做", PreviewContent = "草儿喜欢在做事的时候打开收音机或者手机，听听音乐看看视屏" });
        }
        #endregion

        #region Envent

        private void rankListMore_Tapped(object sender, TappedRoutedEventArgs e)
        {
            App.Current.RootFrame.Navigate(typeof(RankListPage));
        }

        #endregion
    }
}
