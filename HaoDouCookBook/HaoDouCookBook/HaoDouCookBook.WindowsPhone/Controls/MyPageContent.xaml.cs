using HaoDouCookBook.Common;
using HaoDouCookBook.HaoDou.API;
using HaoDouCookBook.Pages;
using HaoDouCookBook.ViewModels;
using System.Threading.Tasks;
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

        public async Task UpdateViewModel()
        {
            viewModel.SignedIn = Utilities.SignedIn();
            if(viewModel.SignedIn)
            {
                int uid = UserGlobal.Instance.GetInt32UserId();
                await RecipeUserAPI.GetUserInfo(uid, uid, UserGlobal.Instance.UserInfo.Sign, data =>
                    {
                        UserGlobal.Instance.UserInfo.Name = data.SummaryInfo.UserName;
                        viewModel.UserName = data.SummaryInfo.UserName;

                        viewModel.UserCover = data.SummaryInfo.Avatar;
                        UserGlobal.Instance.UserInfo.Avatar = data.SummaryInfo.Avatar;

                        viewModel.Coin = data.SummaryInfo.Wealth;

                        UserGlobal.Instance.CommitDataAsync();

                    }, error => { });
            }
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
        private void personalInfo_Tapped(object sender, TappedRoutedEventArgs e)
        {
            UserProfilePage.UserProfilePageParams paras = new UserProfilePage.UserProfilePageParams();
            paras.UserId = int.Parse(UserGlobal.Instance.UserInfo.UserId);

            App.Current.RootFrame.Navigate(typeof(UserProfilePage), paras);
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
