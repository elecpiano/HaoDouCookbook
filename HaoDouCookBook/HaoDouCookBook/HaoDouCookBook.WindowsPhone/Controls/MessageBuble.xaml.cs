using System;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace HaoDouCookBook.Controls
{
    public enum MessageBubleFromSide
    {
        LEFT,
        RIGHT
    }

    public class MessageBubleSideToVisibiltyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null)
            {
                return Visibility.Collapsed;
            }

            MessageBubleFromSide side;
            Enum.TryParse<MessageBubleFromSide>(value.ToString(), out side);
            bool isLeft = true;

            if (parameter != null)
            {
                if(parameter.ToString().Equals("right", StringComparison.OrdinalIgnoreCase))
                {
                    isLeft = false;
                }
            }

            if (isLeft)
            {
                return side == MessageBubleFromSide.LEFT ? Visibility.Visible : Visibility.Collapsed;
            }
            else
            {
                return side == MessageBubleFromSide.RIGHT ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }


    public sealed partial class MessageBuble : UserControl
    {

        public static readonly DependencyProperty FromSideProperty = DependencyProperty.Register("FromSide", typeof(MessageBubleFromSide), typeof(MessageBuble), new PropertyMetadata(MessageBubleFromSide.LEFT));
        public static readonly DependencyProperty MessageProperty = DependencyProperty.Register("Message", typeof(string), typeof(MessageBuble), new PropertyMetadata(string.Empty));
        public static readonly DependencyProperty TextForegroundProperty = DependencyProperty.Register("TextForeground", typeof(SolidColorBrush), typeof(MessageBuble), new PropertyMetadata(new SolidColorBrush(Colors.White)));
        public static readonly DependencyProperty BubleBackgroundProperty = DependencyProperty.Register("BubleBackground", typeof(SolidColorBrush), typeof(MessageBuble), new PropertyMetadata(new SolidColorBrush(Color.FromArgb(0xff, 0x74, 0xa6, 0x00))));

        public MessageBubleFromSide FromSide
        {
            get { return (MessageBubleFromSide)GetValue(FromSideProperty); }
            set { SetValue(FromSideProperty, value); }
        }

        public SolidColorBrush BubleBackground
        {
            get { return (SolidColorBrush)GetValue(BubleBackgroundProperty); }
            set { SetValue(BubleBackgroundProperty, value); }
        }
        

        public SolidColorBrush TextForeground
        {
            get { return (SolidColorBrush)GetValue(TextForegroundProperty); }
            set { SetValue(TextForegroundProperty, value); }
        }

        public string Message
        {
            get { return (string)GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }

        public MessageBuble()
        {
            this.InitializeComponent();
            this.Loaded += MessageBuble_Loaded;
        }

        void MessageBuble_Loaded(object sender, RoutedEventArgs e)
        {
            this.root.DataContext = this;
        }

    }
}
