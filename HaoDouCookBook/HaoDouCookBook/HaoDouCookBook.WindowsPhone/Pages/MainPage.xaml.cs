using HaoDouCookBook.Common;
using HaoDouCookBook.Pages;
using System;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace HaoDouCookBook
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        #region Page Paramerter Definition
        public class MainPageParams
        {
            public int PivotIndex { get; set; }

            public MainPageParams()
            {
                PivotIndex = 0;
            }
        }
        #endregion

        #region Life Cycle

        public MainPage()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: Prepare page for display here.

            // TODO: If your application contains multiple pages, ensure that you are
            // handling the hardware Back button by registering for the
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
            // If you are using the NavigationHelper provided by some templates,
            // this event is handled for you.

            ShowStatusBarAsync();
            myPage.UpdateViewModel();

            var pageParams = e.Parameter as MainPageParams;
            if(pageParams != null)
            {
                this.mainPivot.SelectedIndex = pageParams.PivotIndex;
            }
        }

        #endregion

        #region BottomAppBar

        private void HandleAppBar()
        {
            switch (mainPivot.SelectedIndex)
            {
                case 0:
                    this.bottomAppBar.Visibility = Visibility.Visible;
                    BuildChocinessPageBottomAppBar();
                    break;
                case 1:
                    this.bottomAppBar.Visibility = Visibility.Visible;
                    BuildDiscoveryPageBottomAppBar();
                    break;
                case 2:
                case 3:
                    this.bottomAppBar.Visibility = Visibility.Collapsed;
                    break;
                default:
                    break;
            }
        }

        private const string CLASSIFICATION_APPBARBUTTON_TAG = "classification";
        private const string PUBLISH_PRODUCTS_APPBARBUTTON_TAG = "publishRecipe";

        private void BuildChocinessPageBottomAppBar()
        {
            this.bottomAppBar.PrimaryCommands.Clear();

            AppBarButton classificationAppbarButton = new AppBarButton();
            classificationAppbarButton.Tag = CLASSIFICATION_APPBARBUTTON_TAG;
            classificationAppbarButton.Label = "分类";
            classificationAppbarButton.Icon = new SymbolIcon(Symbol.List);
            classificationAppbarButton.Click += AppbarButton_Click;

            this.bottomAppBar.PrimaryCommands.Add(classificationAppbarButton);
        }

        private void BuildDiscoveryPageBottomAppBar()
        {
            this.bottomAppBar.PrimaryCommands.Clear();

            AppBarButton publishRecipeAppbarButton = new AppBarButton();
            publishRecipeAppbarButton.Tag = PUBLISH_PRODUCTS_APPBARBUTTON_TAG;
            publishRecipeAppbarButton.Label = "晒一晒";
            publishRecipeAppbarButton.Icon = new SymbolIcon(Symbol.Edit);
            publishRecipeAppbarButton.Click += AppbarButton_Click;

            this.bottomAppBar.PrimaryCommands.Add(publishRecipeAppbarButton);
        }

        void AppbarButton_Click(object sender, RoutedEventArgs e)
        {
            AppBarButton button = sender as AppBarButton;
            if (button != null)
            {
                string tag = button.Tag.ToString();
                if (tag.Equals(CLASSIFICATION_APPBARBUTTON_TAG, StringComparison.OrdinalIgnoreCase))
                {
                    GotoCateogryPage();
                }
                else if (tag.Equals(PUBLISH_PRODUCTS_APPBARBUTTON_TAG, StringComparison.OrdinalIgnoreCase))
                {
                    GotoPublishProductsPage();
                }
            }
        }

        private void GotoPublishProductsPage()
        {
            if (Utilities.SignedIn())
            {
                App.CurrentInstance.RootFrame.Navigate(typeof(PublishProductsPage));
            }
            else
            {
                LoginPage.LoginPageParams paras = new LoginPage.LoginPageParams();
                App.CurrentInstance.RootFrame.Navigate(typeof(LoginPage));
            }
        }

        private void GotoCateogryPage()
        {
            App.CurrentInstance.RootFrame.Navigate(typeof(CategoryPage));
        }

        #endregion

        #region Status Bar

        private async void ShowStatusBarAsync()
        {
            StatusBar statusBar = Windows.UI.ViewManagement.StatusBar.GetForCurrentView();
            statusBar.BackgroundColor = Colors.Black;
            statusBar.ForegroundColor = Colors.White;
            statusBar.BackgroundOpacity = 1;
            await statusBar.ShowAsync();
        }

        #endregion

        #region Pivot Selection Changed
        private void mainPivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (mainPivot.SelectedIndex == 3)
            {
                this.myPage.UpdateViewModel();
            }

            HandleAppBar();
        }


        #endregion

        internal bool Dismissed;
        internal Rect SplashImageRect;

        public void SetExtendedSplashInfo(Rect splashRect, bool dismissStat)
        {
            SplashImageRect = splashRect;
            Dismissed = dismissStat;
        }

    }
}
