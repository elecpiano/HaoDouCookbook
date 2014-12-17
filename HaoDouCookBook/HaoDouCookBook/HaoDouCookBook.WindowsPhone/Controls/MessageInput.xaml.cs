using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace HaoDouCookBook.Controls
{
    public sealed partial class MessageInput : UserControl
    {
        public static readonly DependencyProperty AutoHideWhenLostFocusWithEmptyTextProperty = DependencyProperty.Register("AutoHideWhenLostFocusWithEmptyText", typeof(bool), typeof(MessageInput), new PropertyMetadata(true));

        public Action<string> SendAction { get; set; }

        public bool AutoHideWhenLostFocusWithEmptyText
        {
            get { return (bool)GetValue(AutoHideWhenLostFocusWithEmptyTextProperty); }
            set { SetValue(AutoHideWhenLostFocusWithEmptyTextProperty, value); }
        }

        public string Text
        {
            get { return this.textbox.Text; }
            set { this.textbox.Text = value; }
        }

        public MessageInput()
        {
            this.InitializeComponent();
            this.LostFocus += input_LostFocus;
        }

        private void Send_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            if (SendAction != null)
            {
                SendAction.Invoke(this.textbox.Text);
            }
        }

        public void ClearText()
        {
            this.textbox.Text = string.Empty;
        }

        //Message Input失去焦点时隐藏
        //
        private void input_LostFocus(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if(AutoHideWhenLostFocusWithEmptyText)
            {
                if (string.IsNullOrEmpty(this.textbox.Text))
                {
                    this.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                }
            }
        }
    }
}
