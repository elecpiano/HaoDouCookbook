using HaoDouCookBook.Utility;
using System;
using System.Collections.Generic;
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
    public enum TileType
    {
        /// <summary>
        /// Full information which contains preview image, title, author, create time description and preview content.
        /// </summary>
        Full,

        /// <summary>
        /// Only contains preview image, title with text wrapping, and preview conent.
        /// </summary>
        TileWrapAndPreviewContent,

        /// <summary>
        /// Only contains preview image, title, and preview conent with text wrapping.
        /// </summary>
        TileAndPreviewContentWrap
    }

    public sealed partial class TopicTile : UserControl
    {

        #region Dependency Property

        public static readonly DependencyProperty TopicPreviewImageSourceProperty = DependencyProperty.Register("TopicPreviewImageSource", typeof(string), typeof(TopicTile), new PropertyMetadata(string.Empty));
        public static readonly DependencyProperty PreviewContentProperty = DependencyProperty.Register("PreviewContent", typeof(string), typeof(TopicTile), new PropertyMetadata(string.Empty, PreviewContentPropertyChanged));
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(TopicTile), new PropertyMetadata(string.Empty, TitlePropertyChanged));
        public static readonly DependencyProperty TileTypeProperty = DependencyProperty.Register("TileType", typeof(TileType), typeof(TopicTile), new PropertyMetadata(TileType.Full));
        public static readonly DependencyProperty AuthorProperty = DependencyProperty.Register("Author", typeof(string), typeof(TopicTile), new PropertyMetadata(string.Empty));
        public static readonly DependencyProperty CreateTimeDescriptionProperty = DependencyProperty.Register("CreateTimeDescription", typeof(string), typeof(TopicTile), new PropertyMetadata(string.Empty));

        #endregion

        #region CLR Property Wrapper

        public TileType TileType
        {
            get { return (TileType)GetValue(TileTypeProperty); }
            set
            {
                SetTopicTextInfoFashion(value);
                SetValue(TileTypeProperty, value);
            }
        }

        public string TopicPreviewImageSource
        {
            get { return (string)GetValue(TopicPreviewImageSourceProperty); }
            set { SetValue(TopicPreviewImageSourceProperty, value); }
        }

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public string Author
        {
            get { return (string)GetValue(AuthorProperty); }
            set { SetValue(AuthorProperty, value); }
        }

        public string PreviewContent
        {
            get { return (string)GetValue(PreviewContentProperty); }
            set { SetValue(PreviewContentProperty, value); }
        }

        public string CreateTimeDescription
        {
            get { return (string)GetValue(CreateTimeDescriptionProperty); }
            set { SetValue(CreateTimeDescriptionProperty, value); }
        }

        #endregion

        #region Life Cycle

        public TopicTile()
        {
            this.InitializeComponent();
            this.root.DataContext = this;
            SetTopicTextInfoFashion(TileType);
        }

        #endregion

        #region Private Method

        private void SetTopicTextInfoFashion(TileType type)
        {
            switch (type)
            {
                case TileType.Full:
                    this.titleInfo.TextWrapping = TextWrapping.NoWrap;
                    this.previewContentInfo.TextWrapping = TextWrapping.NoWrap;
                    //this.previewContentInfo.Margin = new Thickness(0, 5, 0, 0);
                    this.authorInfo.Visibility = Visibility.Visible;
                    break;
                case TileType.TileWrapAndPreviewContent:
                    this.authorInfo.Visibility = Visibility.Collapsed;
                    this.previewContentInfo.TextWrapping = TextWrapping.NoWrap;
                    //this.previewContentInfo.Margin = new Thickness(0, 12, 0, 0);
                    this.titleInfo.TextWrapping = TextWrapping.Wrap;
                    break;
                case TileType.TileAndPreviewContentWrap:
                    this.authorInfo.Visibility = Visibility.Collapsed;
                    this.previewContentInfo.TextWrapping = TextWrapping.Wrap;
                    //this.previewContentInfo.Margin = new Thickness(0, 6, 0, 0);
                    this.titleInfo.TextWrapping = TextWrapping.NoWrap;
                    break;
                default:
                    break;
            }
        }

        private static void PreviewContentPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null)
            {
                TileType type = (TileType)d.GetValue(TileTypeProperty);
                switch (type)
                {
                    case TileType.Full:
                    case TileType.TileWrapAndPreviewContent:
                        d.SetValue(PreviewContentProperty, StringHelper.GetShortenStyle(e.NewValue.ToString(), 11));
                        break;
                    case TileType.TileAndPreviewContentWrap:
                        d.SetValue(PreviewContentProperty, StringHelper.GetShortenStyle(e.NewValue.ToString(), 22));
                        break;
                    default:
                        break;
                }
            }
        }

        private static void TitlePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null)
            {
                TileType type = (TileType)d.GetValue(TileTypeProperty);
                switch (type)
                {
                    case TileType.TileWrapAndPreviewContent:
                        d.SetValue(TitleProperty, StringHelper.GetShortenStyle(e.NewValue.ToString(), 18));
                        break;
                    case TileType.Full:
                    case TileType.TileAndPreviewContentWrap:
                        d.SetValue(TitleProperty, StringHelper.GetShortenStyle(e.NewValue.ToString(), 9));
                        break;
                    default:
                        break;
                }

            }
        }

        #endregion
    }


}
