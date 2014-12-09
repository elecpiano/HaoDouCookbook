using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace HaoDouCookBook.Controls
{
    public sealed partial class SimpleSettingItem : UserControl
    {

        #region Denpendency Property

        public static readonly DependencyProperty WithMoreTagProperty = DependencyProperty.Register("WithMoreTag", typeof(bool), typeof(SimpleSettingItem), new PropertyMetadata(true));
        public static readonly DependencyProperty IconProperty = DependencyProperty.Register("Icon", typeof(string), typeof(SimpleSettingItem), new PropertyMetadata(string.Empty));
        public static readonly DependencyProperty ItemTextProperty = DependencyProperty.Register("ItemText", typeof(string), typeof(SimpleSettingItem), new PropertyMetadata(string.Empty));
        public static readonly DependencyProperty CountProperty = DependencyProperty.Register("Count", typeof(int), typeof(SimpleSettingItem), new PropertyMetadata(0));

        #endregion

        #region CLR Property Wrapper

        public string Icon
        {
            get { return (string)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        public string ItemText
        {
            get { return (string)GetValue(ItemTextProperty); }
            set { SetValue(ItemTextProperty, value); }
        }

        public bool WithMoreTag
        {
            get { return (bool)GetValue(WithMoreTagProperty); }
            set { SetValue(WithMoreTagProperty, value); }
        }

        public int Count
        {
            get { return (int)GetValue(CountProperty); }
            set { SetValue(CountProperty, value); }
        }

        #endregion

        #region Life Cycle

        public SimpleSettingItem()
        {
            this.InitializeComponent();
            this.root.DataContext = this;
        }

        #endregion
    }
}
