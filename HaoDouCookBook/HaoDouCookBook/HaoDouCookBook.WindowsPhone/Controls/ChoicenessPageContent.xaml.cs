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

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace HaoDouCookBook.Controls
{
    public sealed partial class ChoicenessPageContent : UserControl
    {
        #region Field && Property

        private TagsListData tags = new TagsListData();
        private SpecialRecipeAlbumData specialRecipeAlbumData = new SpecialRecipeAlbumData();
        private ObservableCollection<TopicModel> ranklistData = new ObservableCollection<TopicModel>();
        private RecipeCategoryTileData chuFangBaoDianData = new RecipeCategoryTileData()
        {
            MarkText = "厨房宝典"
        };

        private RecipeCategoryTileData reMenCaiPuData = new RecipeCategoryTileData()
        {
            Title = "热门菜谱",
            Description = "500道超高人气菜谱随便挑"
        };

        private RecipeCategoryTileData siRenCaiPuData = new RecipeCategoryTileData()
        {
            Title = "私人定制",
            Description = "特别为您定制100道菜，可能您会喜欢",
            TitleIcon = "../Assets/Images/zhiding.png"
        };

        public RecipeCategoryTileData shiLingJiaYaoData = new RecipeCategoryTileData() 
        {
            Title = "时令佳肴",
            Description = "当季的，就是最好的"
        };

        public RecipeCategoryTileData daRenCaiPuData = new RecipeCategoryTileData()
        {
            Title = "达人菜谱",
            Description = "达人们常在这里做菜"
        };

        public RecipeCategoryTileData zuiXinCaiPuData = new RecipeCategoryTileData() 
        {
            Title = "最新菜谱",
            Description = "豆子露一手"
        };

        private RecipeCategoryTileData kuaiLeDeHongBeiData = new RecipeCategoryTileData()
        {
            Title = "快乐的烘焙",
            Description = "有种快乐叫烘焙",
            TitleIcon = "../Assets/Images/hongbei.png"
        };

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
            this.SpecialRecipeAlbum.DataContext = specialRecipeAlbumData;
            this.chuFangBiaoDian.DataContext = chuFangBaoDianData;
            this.reMenCaiPu.DataContext = reMenCaiPuData;
            this.siRenDingZhi.DataContext = siRenCaiPuData;
            this.shiLingJiaYao.DataContext = shiLingJiaYaoData;
            this.daRenCaiPu.DataContext = daRenCaiPuData;
            this.zuiXinCaiPu.DataContext = zuiXinCaiPuData;
            this.kuaiLeDeHongBei.DataContext = kuaiLeDeHongBeiData;
            this.yingYangCanZhuo.DataContext = yingYangCanZhuoData;
            this.TagsList.DataContext = tags;
        }

        private void BindRankList()
        {
            this.rankList.ItemsSource = ranklistData;
        }

        #endregion

        #region Test

        private void Test()
        {
            tags.Tag1 = "川菜";
            tags.Tag2 = "早餐";
            tags.Tag3 = "烤箱";
            tags.Tag4 = "排骨";
            tags.Tag5 = "驱寒";
            tags.Tag6 = "补血";
            tags.Tag7 = "养胃";
            tags.Tag8 = "感冒";

            // recipe category
            //
            specialRecipeAlbumData.MainImageSource = Constants.DEFAULT_TOPIC_IMAGE;
            specialRecipeAlbumData.Title = "让人哈喇子直流的东北菜";
            specialRecipeAlbumData.Description = "量大味足是很多人对东北菜的第一印象。量大味足是很多人对东北菜的第一印象。量大味足是很多人对东北菜的第一印象。量大味足是很多人对东北菜的第一印象。量大味足是很多人对东北菜的第一印象。";
            specialRecipeAlbumData.DetailsImageSources.Add(Constants.DEFAULT_USER_PHOTO);
            specialRecipeAlbumData.DetailsImageSources.Add(Constants.DEFAULT_TOPIC_IMAGE);
            specialRecipeAlbumData.DetailsImageSources.Add(Constants.DEFAULT_USER_PHOTO);
            specialRecipeAlbumData.DetailsImageSources.Add(Constants.DEFAULT_TOPIC_IMAGE);
            specialRecipeAlbumData.DetailsImageSources.Add(Constants.DEFAULT_USER_PHOTO);
            specialRecipeAlbumData.DetailsImageSources.Add(Constants.DEFAULT_TOPIC_IMAGE);
            specialRecipeAlbumData.DetailsImageSources.Add(Constants.DEFAULT_USER_PHOTO);

            chuFangBaoDianData.RecipeDescriptionOnImage = "市场上的餐具琳琅满目，如何挑选餐具是大家感到困惑的事情？小便为各位介绍一些选购玩具的小姐窍门，学起来～";
            chuFangBaoDianData.TileImage = Constants.DEFAULT_TOPIC_IMAGE;
            chuFangBaoDianData.Title = "你必须知道的买碗技巧";

            reMenCaiPuData.RecipeName = "芝麻酥饼";
            reMenCaiPuData.Author = "美美家的厨房";
            reMenCaiPuData.AuthorRecipeComment = "因为害怕黄油长肉肉，所以哦哦";
            reMenCaiPuData.TileImage = Constants.DEFAULT_TOPIC_IMAGE;

            siRenCaiPuData.TileImage = Constants.DEFAULT_TOPIC_IMAGE;
            siRenCaiPuData.RecipeName = "红焖养肉山药";
            siRenCaiPuData.Author = "尚食之文";
            siRenCaiPuData.AuthorRecipeComment = "山药可以提供人体大量的能量啊啊啊啊啊";


            shiLingJiaYaoData.TileImage = Constants.DEFAULT_TOPIC_IMAGE;
            shiLingJiaYaoData.RecipeName = "红焖养肉山药";
            shiLingJiaYaoData.Author = "尚食之文";
            shiLingJiaYaoData.AuthorRecipeComment = "山药可以提供人体大量的能量啊啊啊啊啊";

            daRenCaiPuData.TileImage = Constants.DEFAULT_TOPIC_IMAGE;
            daRenCaiPuData.RecipeName = "红焖养肉山药";
            daRenCaiPuData.Author = "尚食之文";
            daRenCaiPuData.AuthorRecipeComment = "山药可以提供人体大量的能量啊啊啊啊啊";

            zuiXinCaiPuData.TileImage = Constants.DEFAULT_TOPIC_IMAGE;
            zuiXinCaiPuData.RecipeName = "红焖养肉山药";
            zuiXinCaiPuData.Author = "尚食之文";
            zuiXinCaiPuData.AuthorRecipeComment = "山药可以提供人体大量的能量啊啊啊啊啊";

            kuaiLeDeHongBeiData.TileImage = Constants.DEFAULT_TOPIC_IMAGE;
            kuaiLeDeHongBeiData.RecipeName = "红焖养肉山药";
            kuaiLeDeHongBeiData.Author = "尚食之文";
            kuaiLeDeHongBeiData.AuthorRecipeComment = "山药可以提供人体大量的能量啊啊啊啊啊";

            yingYangCanZhuoData.TileImage = Constants.DEFAULT_TOPIC_IMAGE;
            yingYangCanZhuoData.RecipeName = "红焖养肉山药";
            yingYangCanZhuoData.Author = "尚食之文";
            yingYangCanZhuoData.AuthorRecipeComment = "山药可以提供人体大量的能量啊啊啊啊啊";


            // rank list
            //
            RankListTest();

            DataLoaderTest();

        }

        private void DataLoaderTest()
        {
            POSTExecuter post = new POSTExecuter(HaoDouApiUrlHelper.GetApiUrl("Recipe", "getCollectList"));
            post.AddPostData("offset", "1");
            post.AddPostData("limit", "6");
            post.AddPostData("type", "1");

            post.RunAsync((result) =>
                {
 
                }, (fail) =>
                {
 
                });

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

        private void Tag_Tapped(object sender, TappedRoutedEventArgs e)
        {
            App.Current.RootFrame.Navigate(typeof(TagsPage), sender.GetDataContext());

        }
        private void RecipeCategoryTile_Tapped(object sender, TappedRoutedEventArgs e)
        {
            App.Current.RootFrame.Navigate(typeof(RecipeCategoryDetailPage), Utils.GetDataContext<RecipeCategoryTileData>(sender));
        }

        #endregion

    }
}
