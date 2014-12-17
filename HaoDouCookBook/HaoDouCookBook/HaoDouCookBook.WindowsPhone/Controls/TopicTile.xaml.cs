using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

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
        }

        #endregion

        #region Private Method

        #endregion
    }


}
