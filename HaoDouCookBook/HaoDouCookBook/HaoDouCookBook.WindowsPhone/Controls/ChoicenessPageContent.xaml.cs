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

        public ObservableCollection<TopicModel> RanklistData = new ObservableCollection<TopicModel>();

        #endregion

        #region Life Cycle

        public ChoicenessPageContent()
        {
            this.InitializeComponent();
            this.rankList.ItemsSource = RanklistData;
            RankListTest();
        }

        #endregion

        #region Test
        private void RankListTest()
        {
            RanklistData.Clear();
            RanklistData.Add(new TopicModel() { TopicPreviewImageSource = Constants.DEFAULT_TOPIC_IMAGE, Title = "制作清汤火锅的小窍门", PreviewContent = "昨天为大家推荐了番茄鱼火锅，大家都很喜欢，" });
            RanklistData.Add(new TopicModel() { TopicPreviewImageSource = Constants.DEFAULT_TOPIC_IMAGE, Title = "好豆里那些优秀的“国际范”", PreviewContent = "上个月fishmama给大家介绍了很多咱们好豆里优秀的豆亲，受到很多豆亲的关注，" });
            RanklistData.Add(new TopicModel() { TopicPreviewImageSource = Constants.DEFAULT_TOPIC_IMAGE, Title = "酥脆的酒鬼花生也能自己做", PreviewContent = "草儿喜欢在做事的时候打开收音机或者手机，听听音乐看看视屏" });
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
