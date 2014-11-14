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

        #endregion

        #region Life Cycle 

        public SquarePageConent()
        {
            Test();
            this.InitializeComponent();
            this.topicList.ItemsSource = LatestTopics;
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
