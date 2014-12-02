using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace HaoDouCookBook.Controls
{
    public sealed partial class ShadowTextBlock : UserControl
    {

        #region Dependency Property

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(ShadowTextBlock), new PropertyMetadata(string.Empty));
        public static readonly DependencyProperty TextTrimmingProperty = DependencyProperty.Register("TextTrimming", typeof(TextTrimming), typeof(ShadowTextBlock), new PropertyMetadata(TextTrimming.None));
        public static readonly DependencyProperty TextWrappingProperty = DependencyProperty.Register("TextWrapping", typeof(TextWrapping), typeof(ShadowTextBlock), new PropertyMetadata(TextWrapping.Wrap));

        #endregion

        #region CLR Property Wrapper

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public TextTrimming TextTrimming
        {
            get { return (TextTrimming)GetValue(TextTrimmingProperty); }
            set { SetValue(TextTrimmingProperty, value); }
        }
        public TextWrapping TextWrapping
        {
            get { return (TextWrapping)GetValue(TextWrappingProperty); }
            set { SetValue(TextWrappingProperty, value); }
        }



        public int MaxLines
        {
            get { return (int)GetValue(MaxLinesProperty); }
            set { SetValue(MaxLinesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MaxLines.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MaxLinesProperty =
            DependencyProperty.Register("MaxLines", typeof(int), typeof(ShadowTextBlock), new PropertyMetadata(1));



        #endregion


        #region Life Cycle

        public ShadowTextBlock()
        {
            this.InitializeComponent();
            this.root.DataContext = this;
        }

        #endregion
    }
}
