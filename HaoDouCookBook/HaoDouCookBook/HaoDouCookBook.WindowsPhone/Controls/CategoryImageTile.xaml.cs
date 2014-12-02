using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace HaoDouCookBook.Controls
{
    public sealed partial class CategoryImageTile : UserControl
    {
        #region Dependency Property

        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(CategoryImageTile), new PropertyMetadata(string.Empty));
        public static readonly DependencyProperty ImageSourceProperty = DependencyProperty.Register("ImageSource", typeof(string), typeof(CategoryImageTile), new PropertyMetadata(string.Empty));

        #endregion

        #region CLR　Property　Wrapper

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public string ImageSource
        {
            get { return (string)GetValue(ImageSourceProperty); }
            set { SetValue(ImageSourceProperty, value); }
        }

        #endregion

        #region Lief Cycle

        public CategoryImageTile()
        {
            this.InitializeComponent();
            this.root.DataContext = this;
        }

        #endregion
    }
}
