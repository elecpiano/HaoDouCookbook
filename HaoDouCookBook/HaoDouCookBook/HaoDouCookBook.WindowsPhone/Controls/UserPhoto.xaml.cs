using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace HaoDouCookBook.Controls
{
    public sealed partial class UserPhoto : UserControl
    {

        #region Dependency Property
        public static readonly DependencyProperty ImageSourceProperty = DependencyProperty.Register("ImageSource", typeof(string), typeof(UserPhoto), new PropertyMetadata("../Assets/Images/noavatar_300.jpg"));

        #endregion

        #region CLR Property Wrapper

        public string ImageSource
        {
            get { return (string)GetValue(ImageSourceProperty); }
            set { SetValue(ImageSourceProperty, value); }
        }

        #endregion

        #region Life Cycle

        public UserPhoto()
        {
            this.InitializeComponent();
            this.root.DataContext = this;
        }

        #endregion
    }
}
