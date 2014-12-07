using HaoDouCookBook.Common;
using HaoDouCookBook.Pages;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Shared.Utility;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace HaoDouCookBook.Controls
{
    public sealed partial class UserPhoto : UserControl
    {

        #region Dependency Property

        public static readonly DependencyProperty ImageSourceProperty = DependencyProperty.Register("ImageSource", typeof(string), typeof(UserPhoto), new PropertyMetadata("../Assets/Images/noavatar_300.jpg"));
        public static readonly DependencyProperty UserIdProperty = DependencyProperty.Register("UserId", typeof(int), typeof(UserPhoto), new PropertyMetadata(0));

        #endregion

        #region CLR Property Wrapper

        public int UserId
        {
            get { return (int)GetValue(UserIdProperty); }
            set { SetValue(UserIdProperty, value); }
        }

        public string ImageSource
        {
            get { return (string)GetValue(ImageSourceProperty); }
            set { SetValue(ImageSourceProperty, value); }
        }

        #endregion

        #region Life Cycle

        public UserPhoto()
        {
            this.InitializeComponent();
            this.root.DataContext = this;
        }

        #endregion

        #region Event

        private void UserControl_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            var userData = this.root.GetDataContext<UserPhoto>();

            UserProfilePage.UserProfilePageParams paras = new UserProfilePage.UserProfilePageParams();
            paras.UserId = userData.UserId;

            App.Current.RootFrame.Navigate(typeof(UserProfilePage), paras);
        }

        #endregion
    }
}
