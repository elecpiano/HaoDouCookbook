using HaoDouCookBook.Common;
using HaoDouCookBook.Controls;
using HaoDouCookBook.HaoDou.API;
using HaoDouCookBook.ViewModels;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;
using Shared.Utility;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace HaoDouCookBook.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DiggUserListPage : BackablePage
    {
        public class DiggUserListPageParams
        {
            public int ActivityId { get; set; }

            public DiggUserListPageParams()
            {
                ActivityId = 0;
            }
        }

        public ObservableCollection<DiggUser> DiggUsers { get; set; }
        private DiggUserListPageParams pageParams;

        public DiggUserListPage()
        {
            this.InitializeComponent();
            DiggUsers = new ObservableCollection<DiggUser>();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.NavigationMode == NavigationMode.Back)
            {
                return;
            }

            pageParams = e.Parameter as DiggUserListPageParams;
            if (pageParams != null)
            {
                DiggUsers.Clear();
                DataBinding();
                rootScrollViewer.ScrollToVerticalOffset(0);
                DataBinding();
                LoadFirstPageDataAsync();
            }

        }

        #region Data Prepare

        private void DataBinding()
        {
            this.root.DataContext = this;
        }

        private async Task LoadFirstPageDataAsync()
        {
            loading.SetState(LoadingState.LOADING);
            await DiggAPI.GetDiggUserList(0, 20, pageParams.ActivityId, 2,
                success => {
                    if (success.DiggUsers != null)
                    {
                        DiggUsers.Clear();
                        foreach (var item in success.DiggUsers)
                        {
                            DiggUsers.Add(new DiggUser() { 
                                Description = item.Intro,
                                UserId = int.Parse(item.UserId),
                                IsVip = item.Vip == 1 ? true : false,
                                UserName = item.UserName,
                                Avatar = item.Avatar
                            });
                        }
                    }
                    loading.SetState(LoadingState.SUCCESS);
                },
                error => {
                    Utilities.CommonLoadingRetry(loading, error, async () => await LoadFirstPageDataAsync());
                });
        }

        #endregion

        #region Event

        private void User_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var dataContext = sender.GetDataContext<DiggUser>();
            UserProfilePage.UserProfilePageParams paras = new UserProfilePage.UserProfilePageParams();
            paras.UserId = dataContext.UserId;

            App.Current.RootFrame.Navigate(typeof(UserProfilePage), paras);
        }

        #endregion
    }
}
