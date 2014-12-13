using System;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace HaoDouCookBook.Controls
{
    public sealed partial class MessageInput : UserControl
    {
        public Action<string> SendAction { get; set; }

        public MessageInput()
        {
            this.InitializeComponent();
        }

        private void Send_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            if(SendAction != null)
            {
                SendAction.Invoke(this.textbox.Text);
            }
        }
    }
}
