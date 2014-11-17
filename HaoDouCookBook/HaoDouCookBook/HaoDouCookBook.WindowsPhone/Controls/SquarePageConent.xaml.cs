using HaoDouCookBook.Common;
using HaoDouCookBook.Models;
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
            this.topicList.ItemsSource = LatestTopics;
            this.CategoryList.ItemsSource = Categories;
            Init();
        }

        #endregion

        #region Private Method
        private void Init()
        {
            PrepareCategory();
            Test();
        }

        private void PrepareCategory()
        {
            Categories.Add(new CategoryTileData() { ImageSource = Constants.DEFAULT_TOPIC_IMAGE, Title = "乐在厨房" });
            Categories.Add(new CategoryTileData() { ImageSource = Constants.DEFAULT_TOPIC_IMAGE, Title = "健康营养" });
            Categories.Add(new CategoryTileData() { ImageSource = Constants.DEFAULT_TOPIC_IMAGE, Title = "好好生活" });
            Categories.Add(new CategoryTileData() { ImageSource = Constants.DEFAULT_TOPIC_IMAGE, Title = "游山玩水" });
        }
        #endregion

        #region Test

        private void Test()
        {
            LatestTopics.Clear();
            LatestTopics.Add(new TopicModel() { Title = "红烧肉青红椒", Author = "爱跳舞的老太", CreateTimeDescription = "1小时前", PreviewContent = "本话题已参加活动：好豆菜谱无哈哈", TopicPreviewImageSource = "../Assets/Images/DefaultTopicImage.jpg" });
            LatestTopics.Add(new TopicModel() { Title = "红烧肉青红椒", Author = "爱跳舞的老太", CreateTimeDescription = "1小时前", PreviewContent = "本话题已参加活动：好豆菜谱无哈哈", TopicPreviewImageSource = "../Assets/Images/DefaultTopicImage.jpg" });
            LatestTopics.Add(new TopicModel() { Title = "红烧肉青红椒红烧肉青红椒红烧肉青红椒", Author = "爱跳舞的老太", CreateTimeDescription = "1小时前", PreviewContent = "本话题已参加活动：好豆菜谱无哈哈", TopicPreviewImageSource = "../Assets/Images/DefaultTopicImage.jpg" });
            LatestTopics.Add(new TopicModel() { Title = "红烧肉青红椒", Author = "爱跳舞的老太", CreateTimeDescription = "1小时前", PreviewContent = "本话题已参加活动：好豆菜谱无哈哈", TopicPreviewImageSource = "../Assets/Images/DefaultTopicImage.jpg" });
            LatestTopics.Add(new TopicModel() { Title = "红烧肉青红椒", Author = "爱跳舞的老太", CreateTimeDescription = "1小时前", PreviewContent = "本话题已参加活动：好豆菜谱无哈哈好豆菜谱无哈哈", TopicPreviewImageSource = "../Assets/Images/DefaultTopicImage.jpg" });
            LatestTopics.Add(new TopicModel() { Title = "红烧肉青红椒", Author = "爱跳舞的老太", CreateTimeDescription = "1小时前", PreviewContent = "本话题已参加活动：好豆菜谱无哈哈", TopicPreviewImageSource = "../Assets/Images/DefaultTopicImage.jpg" });
            LatestTopics.Add(new TopicModel() { Title = "红烧肉青红椒", Author = "爱跳舞的老太", CreateTimeDescription = "1小时前", PreviewContent = "本话题已参加活动：好豆菜谱无哈哈", TopicPreviewImageSource = "../Assets/Images/DefaultTopicImage.jpg" });
            LatestTopics.Add(new TopicModel() { Title = "红烧肉青红椒", Author = "爱跳舞的老太", CreateTimeDescription = "1小时前", PreviewContent = "本话题已参加活动：好豆菜谱无哈哈", TopicPreviewImageSource = "../Assets/Images/DefaultTopicImage.jpg" });
            LatestTopics.Add(new TopicModel() { Title = "红烧肉青红椒", Author = "爱跳舞的老太", CreateTimeDescription = "1小时前", PreviewContent = "本话题已参加活动：好豆菜谱无哈哈", TopicPreviewImageSource = "../Assets/Images/DefaultTopicImage.jpg" });
            LatestTopics.Add(new TopicModel() { Title = "红烧肉青红椒", Author = "爱跳舞的老太", CreateTimeDescription = "1小时前", PreviewContent = "本话题已参加活动：好豆菜谱无哈哈", TopicPreviewImageSource = "../Assets/Images/DefaultTopicImage.jpg" });
            LatestTopics.Add(new TopicModel() { Title = "红烧肉青红椒", Author = "爱跳舞的老太", CreateTimeDescription = "1小时前", PreviewContent = "本话题已参加活动：好豆菜谱无哈哈", TopicPreviewImageSource = "../Assets/Images/DefaultTopicImage.jpg" });
            LatestTopics.Add(new TopicModel() { Title = "红烧肉青红椒", Author = "爱跳舞的老太", CreateTimeDescription = "1小时前", PreviewContent = "本话题已参加活动：好豆菜谱无哈哈", TopicPreviewImageSource = "../Assets/Images/DefaultTopicImage.jpg" });
            LatestTopics.Add(new TopicModel() { Title = "红烧肉青红椒", Author = "爱跳舞的老太", CreateTimeDescription = "1小时前", PreviewContent = "本话题已参加活动：好豆菜谱无哈哈", TopicPreviewImageSource = "../Assets/Images/DefaultTopicImage.jpg" });
            LatestTopics.Add(new TopicModel() { Title = "红烧肉青红椒", Author = "爱跳舞的老太", CreateTimeDescription = "1小时前", PreviewContent = "本话题已参加活动：好豆菜谱无哈哈", TopicPreviewImageSource = "../Assets/Images/DefaultTopicImage.jpg" });
        }

        #endregion
    }
}
