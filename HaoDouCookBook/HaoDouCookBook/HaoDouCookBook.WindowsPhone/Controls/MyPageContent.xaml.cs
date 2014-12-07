using HaoDouCookBook.Common;
using HaoDouCookBook.Pages;
using HaoDouCookBook.ViewModels;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace HaoDouCookBook.Controls
{
    public sealed partial class MyPageContent : UserControl
    {
        #region Field && Property

        private MyPageViewModel viewModel = new MyPageViewModel();

        #endregion

        #region Life Cycle

        public MyPageContent()
        {
            this.InitializeComponent();
            DataBinding();
        }

        #endregion

        #region Data Prepare

        private void DataBinding()
        {
            this.root.DataContext = viewModel; 
        }

        public void UpdateViewModel()
        {
            viewModel.SignedIn = Utilities.SignedIn();
        }

        #endregion

        #region Event

        private void GotoLogin_Tapped(object sender, TappedRoutedEventArgs e)
        {
            App.Current.RootFrame.Navigate(typeof(LoginPage));
        }

        private void SimpleSettingItem_Tapped(object sender, TappedRoutedEventArgs e)
        {
            SimpleSettingItem ssi = sender as SimpleSettingItem;
            if (ssi != null && ssi.Tag != null)
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
                case "shoppinglist":
                    GotoShoppingListPage();
                    break;
                case "settings":
                    GotoSettingsPage();
                    break;
                case "adrecommendation":
                    GotoAdRecommendationPage();
                    break;
                default:
                    break;
            }
        }

        private void GotoAdRecommendationPage()
        {
            App.Current.RootFrame.Navigate(typeof(AdRecommendationPage));
        }

        private void GotoShoppingListPage()
        {
            App.Current.RootFrame.Navigate(typeof(ShoppingListPage));
        }

        private void GotoSettingsPage()
        {
            App.Current.RootFrame.Navigate(typeof(UserSettings), "设置");
        }

        #endregion

    }
}
