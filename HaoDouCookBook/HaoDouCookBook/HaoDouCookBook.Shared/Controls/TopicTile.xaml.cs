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
    public sealed partial class TopicTile : UserControl
    {

        #region Dependency Property

        public static readonly DependencyProperty TopicPreviewImageSourceProperty = DependencyProperty.Register("TopicPreviewImageSource", typeof(string), typeof(TopicTile), new PropertyMetadata(string.Empty));
        public static readonly DependencyProperty PreviewContentProperty = DependencyProperty.Register("PreviewContent", typeof(string), typeof(TopicTile), new PropertyMetadata(string.Empty));
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(TopicTile), new PropertyMetadata(string.Empty));
        public static readonly DependencyProperty AuthorProperty = DependencyProperty.Register("Author", typeof(string), typeof(TopicTile), new PropertyMetadata(string.Empty));
        public static readonly DependencyProperty CreateTimeDescriptionProperty = DependencyProperty.Register("CreateTimeDescription", typeof(string), typeof(TopicTile), new PropertyMetadata(string.Empty));

        #endregion

        #region CLR Property Wrapper

        public string TopicPreviewImageSource
        {
            get { return (string)GetValue(TopicPreviewImageSourceProperty); }
            set { SetValue(TopicPreviewImageSourceProperty, value); }
        }

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set
            {
                string title = value;
                if(value.Length > 13)
                {
                    title = title.Substring(0, 13) + "...";

                }

                SetValue(TitleProperty, title);
            }
        }

        public string Author
        {
            get { return (string)GetValue(AuthorProperty); }
            set { SetValue(AuthorProperty, value); }
        }

        public string PreviewContent
        {
            get { return (string)GetValue(PreviewContentProperty); }
            set
            {
                string content = value;
                if (value.Length > 13)
                {
                    content = content.Substring(0, 13) + "...";
                }
                SetValue(PreviewContentProperty, content);
            }
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
            this.DataContext = this;
            this.InitializeComponent();
        }

        #endregion
    }
}
