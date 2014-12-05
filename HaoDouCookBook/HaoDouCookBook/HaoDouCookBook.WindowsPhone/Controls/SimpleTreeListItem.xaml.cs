using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media.Imaging;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace HaoDouCookBook.Controls
{
    public sealed partial class SimpleTreeListItem : UserControl
    {
        public static readonly DependencyProperty ItemsPanelProperty = DependencyProperty.Register("ItemsPanel", typeof(ItemsPanelTemplate), typeof(SimpleTreeListItem), new PropertyMetadata(null));
        public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register("ItemsSource", typeof(object), typeof(SimpleTreeListItem), new PropertyMetadata(null));
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(SimpleTreeListItem), new PropertyMetadata(string.Empty));
        public static readonly DependencyProperty ItemDataTemplateProperty = DependencyProperty.Register("ItemDataTemplate", typeof(DataTemplate), typeof(SimpleTreeListItem), new PropertyMetadata(null));

        public ItemsPanelTemplate ItemsPanel
        {
            get { return (ItemsPanelTemplate)GetValue(ItemsPanelProperty); }
            set { SetValue(ItemsPanelProperty, value); }
        }

        public DataTemplate ItemDataTemplate
        {
            get { return (DataTemplate)GetValue(ItemDataTemplateProperty); }
            set { SetValue(ItemDataTemplateProperty, value); }
        }

        public object ItemsSource
        {
            get { return (object)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public SimpleTreeListItem()
        {
            this.InitializeComponent();

            this.stuffsList.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            root.DataContext = this;
        }

        private void Header_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (this.stuffsList.Visibility == Windows.UI.Xaml.Visibility.Collapsed)
            {
                this.stuffsList.Visibility = Windows.UI.Xaml.Visibility.Visible;
                this.arrowImage.Source = new BitmapImage(new Uri("ms-appx:///../assets/images/up_arrow.png", UriKind.RelativeOrAbsolute));
            }
            else
            {
                this.stuffsList.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                this.arrowImage.Source = new BitmapImage(new Uri("ms-appx:///../assets/images/down_arrow.png", UriKind.RelativeOrAbsolute));
            }
        }
    }
}
