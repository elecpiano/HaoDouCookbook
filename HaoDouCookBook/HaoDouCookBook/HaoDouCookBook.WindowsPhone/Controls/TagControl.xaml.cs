using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace HaoDouCookBook.Controls
{
    public sealed partial class TagControl : UserControl
    {

        public static readonly DependencyProperty SelectedProperty = DependencyProperty.Register("Selected", typeof(bool), typeof(TagControl), new PropertyMetadata(false));
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(TagControl), new PropertyMetadata(string.Empty));

        public bool Selected
        {
            get { return (bool)GetValue(SelectedProperty); }
            set { SetValue(SelectedProperty, value); }
        }

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public TagControl()
        {
            this.InitializeComponent();
            this.grid.DataContext = this;

            this.Loaded += TagControl_Loaded;
        }

        void TagControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (Selected)
            {
                SetToSelectedState();
            }
            else
            {
                SetToNotSelectedState();
            }
        }

        private void Grid_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (!Selected)
            {
                SetToSelectedState();
                Selected = true;
            }
            else
            {
                SetToNotSelectedState();
                Selected = false;
            }

        }

        private void SetToSelectedState()
        {
            this.grid.Background = new SolidColorBrush(Color.FromArgb(0xff, 0x74, 0xa5, 0x00));
            this.text.Foreground = new SolidColorBrush(Colors.White);
        }

        private void SetToNotSelectedState()
        {
            this.grid.Background = new SolidColorBrush(Color.FromArgb(0x10, 0x00, 0x00, 0x00));
            this.text.Foreground = new SolidColorBrush(Colors.Gray);
        }
    }
}
