using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace HaoDouCookBook.Controls
{
    public sealed partial class SettingGroupTitle : UserControl
    {
        #region Dependency Property

        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(SettingGroupTitle), new PropertyMetadata(string.Empty));

        #endregion

        #region CLR Property Wrapper

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }
        #endregion

        #region Life Cycle
        public SettingGroupTitle()
        {
            this.InitializeComponent();
            this.root.DataContext = this;
        }
        #endregion
    }
}
