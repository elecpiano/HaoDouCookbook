using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace HaoDouCookBook.Controls
{
    public sealed partial class AlbumTile : UserControl
    {
        #region Dependency Property

        public static readonly DependencyProperty AlbumTitleProperty = DependencyProperty.Register("AlbumTitle", typeof(string), typeof(AlbumTile), new PropertyMetadata(string.Empty));
        public static readonly DependencyProperty AlbumCoverProperty = DependencyProperty.Register("AlbumCover", typeof(string), typeof(AlbumTile), new PropertyMetadata(string.Empty));
        public static readonly DependencyProperty AlbumRecipeCountProperty = DependencyProperty.Register("AlbumRecipeCount", typeof(int), typeof(AlbumTile), new PropertyMetadata(0));
        public static readonly DependencyProperty AlbumLikeCountProperty = DependencyProperty.Register("AlbumLikeCount", typeof(int), typeof(AlbumTile), new PropertyMetadata(0));
        public static readonly DependencyProperty AlbumViewCountProperty = DependencyProperty.Register("AlbumViewCount", typeof(int), typeof(AlbumTile), new PropertyMetadata(0));
        public static readonly DependencyProperty AlbumIntroProperty = DependencyProperty.Register("AlbumIntro", typeof(string), typeof(AlbumTile), new PropertyMetadata(string.Empty));

        #endregion

        #region CLR Property Wrapper

        public string AlbumTitle
        {
            get { return (string)GetValue(AlbumTitleProperty); }
            set { SetValue(AlbumTitleProperty, value); }
        }

        public string AlbumCover
        {
            get { return (string)GetValue(AlbumCoverProperty); }
            set { SetValue(AlbumCoverProperty, value); }
        }

        public int AlbumRecipeCount
        {
            get { return (int)GetValue(AlbumRecipeCountProperty); }
            set { SetValue(AlbumRecipeCountProperty, value); }
        }

        public int AlbumLikeCount
        {
            get { return (int)GetValue(AlbumLikeCountProperty); }
            set { SetValue(AlbumLikeCountProperty, value); }
        }

        public int AlbumViewCount
        {
            get { return (int)GetValue(AlbumViewCountProperty); }
            set { SetValue(AlbumViewCountProperty, value); }
        }

        public string AlbumIntro
        {
            get { return (string)GetValue(AlbumIntroProperty); }
            set { SetValue(AlbumIntroProperty, value); }
        }

        #endregion

        #region Life Cycle
        
        public AlbumTile()
        {
            this.InitializeComponent();
            this.root.DataContext = this;
        }

        #endregion
    }
}
