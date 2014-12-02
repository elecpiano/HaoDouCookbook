using HaoDouCookBook.Pages;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace HaoDouCookBook.Controls
{
    public sealed partial class MyPageContent : UserControl
    {
        #region Life Cycle

        public MyPageContent()
        {
            this.InitializeComponent();
        }

        #endregion

        #region Event

        private void SimpleSettingItem_Tapped(object sender, TappedRoutedEventArgs e)
        {
            SimpleSettingItem ssi = sender as SimpleSettingItem;
            if (ssi != null)
            {
                HandlePageItemTapped(ssi.Tag.ToString());
            }
        }

        #endregion

        #region Page item tapped logic

        private void HandlePageItemTapped(string tag)
        {
            if(string.IsNullOrEmpty(tag))
            {
                return;
            }

            switch(tag.Trim().ToLower())
            {
                case "settings":
                    GotoSettingsPage();
                    break;
                default:
                    break;
            }
        }

        private void GotoSettingsPage()
        {
            App.Current.RootFrame.Navigate(typeof(UserSettings), "设置");
        }

        #endregion
    }
}
