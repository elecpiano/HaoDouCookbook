using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace HaoDouCookBook.Controls
{
    public class SmartGrid : Grid
    {
        public static readonly DependencyProperty HoverOnBackgroundProperty = DependencyProperty.Register("HoverOnBackground", typeof(Brush), typeof(SmartGrid), new PropertyMetadata(new SolidColorBrush(Colors.Transparent)));

        public Brush HoverOnBackground
        {
            get { return (Brush)GetValue(HoverOnBackgroundProperty); }
            set { SetValue(HoverOnBackgroundProperty, value); }
        }

        public SmartGrid()
        {
            this.PointerPressed += SmartGrid_PointerPressed;
            this.PointerReleased += SmartGrid_PointerReleased;
            this.PointerExited += SmartGrid_PointerExited;
            this.PointerCaptureLost += SmartGrid_PointerCaptureLost;

            this.IsHitTestVisible = true;
            this.Background = new SolidColorBrush(Colors.Transparent);
        }

        void SmartGrid_PointerCaptureLost(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            this.Background = new SolidColorBrush(Colors.Transparent);
        }

        void SmartGrid_PointerReleased(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            this.Background = new SolidColorBrush(Colors.Transparent);
        }

        void SmartGrid_PointerPressed(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            this.Background = HoverOnBackground;
        }

        void SmartGrid_PointerExited(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            this.Background = new SolidColorBrush(Colors.Transparent);
        }
    }
}
