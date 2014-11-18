using HaoDouCookBook.Common;
using HaoDouCookBook.ViewModels;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace HaoDouCookBook.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RankListPage : Page
    {

        #region Field && Property

        private ObservableCollection<TopicModel> rankListData = new ObservableCollection<TopicModel>();

        #endregion

        #region Life Cycle

        public RankListPage()
        {
            this.InitializeComponent();
            this.ranklist.ItemsSource = rankListData;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Windows.Phone.UI.Input.HardwareButtons.BackPressed += HardwareButtons_BackPressed;
            Test();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            Windows.Phone.UI.Input.HardwareButtons.BackPressed -= HardwareButtons_BackPressed;
        }

        #endregion

        #region Event

        void HardwareButtons_BackPressed(object sender, Windows.Phone.UI.Input.BackPressedEventArgs e)
        {
            App.Current.RootFrame.GoBack();

            // Need add this line, otherwise it will not back to last page.
            //
            e.Handled = true;
        }

        #endregion

        #region Test

        private void Test()
        {
            rankListData.Add(new TopicModel() { TopicPreviewImageSource = Constants.DEFAULT_TOPIC_IMAGE, Title = "制作清汤火锅的小窍门", PreviewContent = "昨天为大家推荐了番茄鱼火锅，大家都很喜欢，" });
            rankListData.Add(new TopicModel() { TopicPreviewImageSource = Constants.DEFAULT_TOPIC_IMAGE, Title = "好豆里那些优秀的“国际范”", PreviewContent = "上个月fishmama给大家介绍了很多咱们好豆里优秀的豆亲，受到很多豆亲的关注，" });
            rankListData.Add(new TopicModel() { TopicPreviewImageSource = Constants.DEFAULT_TOPIC_IMAGE, Title = "酥脆的酒鬼花生也能自己做", PreviewContent = "草儿喜欢在做事的时候打开收音机或者手机，听听音乐看看视屏" });
            rankListData.Add(new TopicModel() { TopicPreviewImageSource = Constants.DEFAULT_TOPIC_IMAGE, Title = "制作清汤火锅的小窍门", PreviewContent = "昨天为大家推荐了番茄鱼火锅，大家都很喜欢，" });
            rankListData.Add(new TopicModel() { TopicPreviewImageSource = Constants.DEFAULT_TOPIC_IMAGE, Title = "好豆里那些优秀的“国际范”", PreviewContent = "上个月fishmama给大家介绍了很多咱们好豆里优秀的豆亲，受到很多豆亲的关注，" });
            rankListData.Add(new TopicModel() { TopicPreviewImageSource = Constants.DEFAULT_TOPIC_IMAGE, Title = "酥脆的酒鬼花生也能自己做", PreviewContent = "草儿喜欢在做事的时候打开收音机或者手机，听听音乐看看视屏" });
            rankListData.Add(new TopicModel() { TopicPreviewImageSource = Constants.DEFAULT_TOPIC_IMAGE, Title = "制作清汤火锅的小窍门", PreviewContent = "昨天为大家推荐了番茄鱼火锅，大家都很喜欢，" });
            rankListData.Add(new TopicModel() { TopicPreviewImageSource = Constants.DEFAULT_TOPIC_IMAGE, Title = "好豆里那些优秀的“国际范”", PreviewContent = "上个月fishmama给大家介绍了很多咱们好豆里优秀的豆亲，受到很多豆亲的关注，" });
            rankListData.Add(new TopicModel() { TopicPreviewImageSource = Constants.DEFAULT_TOPIC_IMAGE, Title = "酥脆的酒鬼花生也能自己做", PreviewContent = "草儿喜欢在做事的时候打开收音机或者手机，听听音乐看看视屏" });
            rankListData.Add(new TopicModel() { TopicPreviewImageSource = Constants.DEFAULT_TOPIC_IMAGE, Title = "制作清汤火锅的小窍门", PreviewContent = "昨天为大家推荐了番茄鱼火锅，大家都很喜欢，" });
            rankListData.Add(new TopicModel() { TopicPreviewImageSource = Constants.DEFAULT_TOPIC_IMAGE, Title = "好豆里那些优秀的“国际范”", PreviewContent = "上个月fishmama给大家介绍了很多咱们好豆里优秀的豆亲，受到很多豆亲的关注，" });
            rankListData.Add(new TopicModel() { TopicPreviewImageSource = Constants.DEFAULT_TOPIC_IMAGE, Title = "酥脆的酒鬼花生也能自己做", PreviewContent = "草儿喜欢在做事的时候打开收音机或者手机，听听音乐看看视屏" });
            rankListData.Add(new TopicModel() { TopicPreviewImageSource = Constants.DEFAULT_TOPIC_IMAGE, Title = "制作清汤火锅的小窍门", PreviewContent = "昨天为大家推荐了番茄鱼火锅，大家都很喜欢，" });
            rankListData.Add(new TopicModel() { TopicPreviewImageSource = Constants.DEFAULT_TOPIC_IMAGE, Title = "好豆里那些优秀的“国际范”", PreviewContent = "上个月fishmama给大家介绍了很多咱们好豆里优秀的豆亲，受到很多豆亲的关注，" });
            rankListData.Add(new TopicModel() { TopicPreviewImageSource = Constants.DEFAULT_TOPIC_IMAGE, Title = "酥脆的酒鬼花生也能自己做", PreviewContent = "草儿喜欢在做事的时候打开收音机或者手机，听听音乐看看视屏" });
            rankListData.Add(new TopicModel() { TopicPreviewImageSource = Constants.DEFAULT_TOPIC_IMAGE, Title = "制作清汤火锅的小窍门", PreviewContent = "昨天为大家推荐了番茄鱼火锅，大家都很喜欢，" });
            rankListData.Add(new TopicModel() { TopicPreviewImageSource = Constants.DEFAULT_TOPIC_IMAGE, Title = "好豆里那些优秀的“国际范”", PreviewContent = "上个月fishmama给大家介绍了很多咱们好豆里优秀的豆亲，受到很多豆亲的关注，" });
            rankListData.Add(new TopicModel() { TopicPreviewImageSource = Constants.DEFAULT_TOPIC_IMAGE, Title = "酥脆的酒鬼花生也能自己做", PreviewContent = "草儿喜欢在做事的时候打开收音机或者手机，听听音乐看看视屏" });
        }

        #endregion
    }
}
