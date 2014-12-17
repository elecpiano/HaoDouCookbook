using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace HaoDouCookBook.Controls
{
    public sealed partial class StuffControl : UserControl
    {

        public static readonly DependencyProperty StuffNameProperty = DependencyProperty.Register("StuffName", typeof(string), typeof(StuffControl), new PropertyMetadata(string.Empty));
        public static readonly DependencyProperty WeightProperty = DependencyProperty.Register("Weight", typeof(string), typeof(StuffControl), new PropertyMetadata(string.Empty));

        public string StuffName
        {
            get { return (string)GetValue(StuffNameProperty); }
            set { SetValue(StuffNameProperty, value); }
        }

        public string Weight
        {
            get { return (string)GetValue(WeightProperty); }
            set { SetValue(WeightProperty, value); }
        }

        public StuffControl()
        {
            this.InitializeComponent();
            this.root.DataContext = this;
        }
    }
}
