﻿using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;


// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace HaoDouCookBook.Controls
{
    public sealed partial class Toast : UserControl
    {
        private Storyboard showAnimation;

        public Toast()
        {
            this.InitializeComponent();
            this.root.Opacity = 0;
            showAnimation = this.Resources["show"] as Storyboard;
        }


        public void Show(string Text)
        {
            this.content.Text = Text;
            if (show != null)
            {

                show.Stop();
                show.Begin();
            }
        }

    }
}
