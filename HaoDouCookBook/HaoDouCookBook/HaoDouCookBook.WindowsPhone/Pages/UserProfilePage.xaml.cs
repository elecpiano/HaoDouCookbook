using HaoDouCookBook.Common;
using HaoDouCookBook.Controls;
using HaoDouCookBook.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Shared.Utility;
using HaoDouCookBook.HaoDou.API;
using HaoDouCookBook.Utility;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace HaoDouCookBook.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class UserProfilePage : BackablePage
    {
        #region Page Parameter Definition

        public class UserProfilePageParams
        {
            public int UserId { get; set; }

            public UserProfilePageParams()
            {
                UserId = 0;
            }
        }

        #endregion

        #region Field && Proprety

        private UserProfilePageViewModel viewModel = new UserProfilePageViewModel();
        private UserProfilePageParams pageParams;

        #endregion

        #region Life Cycle

        public UserProfilePage()
        {
            this.InitializeComponent();
            DataBinding();
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

            pageParams = e.Parameter as UserProfilePageParams;
            if (pageParams != null)
            {
                viewModel = new UserProfilePageViewModel();
                DataBinding();
                LoadDataAsync();
            }

        }

        #endregion

        #region Data Prepare

        private void DataBinding()
        {
            this.root.DataContext = viewModel;
        }

        private async Task LoadDataAsync()
        {
            loading.SetState(LoadingState.LOADING);

            int uid = Utilities.GetInt32UserId(UserGlobal.Instance.UserInfo.UserId);

            await RecipeUserAPI.GetUserInfo(pageParams.UserId, uid, UserGlobal.Instance.UserInfo.Sign, data =>
            {
                if (pageParams != null)
                {
                    viewModel.UserId = pageParams.UserId;
                }

                if (data.SummaryInfo != null)
                {
                    viewModel.FollowCount = data.SummaryInfo.FollowCnt;
                    viewModel.FansCount = data.SummaryInfo.FansCount;
                    viewModel.Coin = data.SummaryInfo.Wealth;
                    viewModel.UserAvatar = data.SummaryInfo.Avatar;
                    viewModel.UserIntro = data.SummaryInfo.Intro;
                }

                loading.SetState(LoadingState.SUCCESS);

            }, error =>
            {
                if (Utilities.IsMatchNetworkFail(error.ErrorCode))
                {
                    DelayHelper.Delay(TimeSpan.FromSeconds(0.7), () =>
                    {
                        loading.RetryAction = async () => await LoadDataAsync();
                        loading.SetState(LoadingState.NETWORK_UNAVAILABLE);
                    });
                }
                else
                {
                    loading.SetState(LoadingState.DONE);
                }
            });
        }

        #endregion

    }
}
