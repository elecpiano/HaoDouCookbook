using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace HaoDouCookBook.Controls
{
    public sealed partial class UserPhoto : UserControl
    {

        #region Dependency Property
        public static readonly DependencyProperty ImageSourceProperty = DependencyProperty.Register("UserPhoto", typeof(string), typeof(UserPhoto), new PropertyMetadata("../Assets/Images/noavatar_300.jpg"));

        #endregion

        #region CLR Property Wrapper

        public string ImageSource
        {
            get { return (string)GetValue(ImageSourceProperty); }
            set { SetValue(ImageSourceProperty, value); }
        }

        #endregion

        public UserPhoto()
        {
            this.InitializeComponent();
            this.DataContext = this;
        }
    }
}
