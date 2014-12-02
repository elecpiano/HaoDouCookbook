using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace HaoDouCookBook.Controls
{
    public sealed partial class DishTile : UserControl
    {

        #region Denpendency Property

        public static readonly DependencyProperty ImageSourceProperty = DependencyProperty.Register("ImageSource", typeof(string), typeof(DishTile), new PropertyMetadata(string.Empty));
        public static readonly DependencyProperty AuthorProperty = DependencyProperty.Register("Author", typeof(string), typeof(DishTile), new PropertyMetadata(string.Empty));
        public static readonly DependencyProperty SupportNumberProperty = DependencyProperty.Register("SupportNumber", typeof(int), typeof(DishTile), new PropertyMetadata(0));

        #endregion

        #region CLR Property Wrapper

        public string ImageSource
        {
            get { return (string)GetValue(ImageSourceProperty); }
            set { SetValue(ImageSourceProperty, value); }
        }

        public string Author
        {
            get { return (string)GetValue(AuthorProperty); }
            set { SetValue(AuthorProperty, value); }
        }


        public int SupportNumber
        {
            get { return (int)GetValue(SupportNumberProperty); }
            set { SetValue(SupportNumberProperty, value); }
        }

        #endregion
        
        #region Life Cycle
        
        public DishTile()
        {
            this.InitializeComponent();
            this.root.DataContext = this;
        }

        #endregion

        #region Public Methods

        public void SetImageHight(double height) 
        {
            this.imageRect.Height = height;
        }

        #endregion
    }
}
