using HaoDouCookBook.Common;
using HaoDouCookBook.HaoDou.API;
using HaoDouCookBook.Pages;
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
    public sealed partial class UserProfileSummary : UserControl
    {

        #region Dependency Property

        public static readonly DependencyProperty FollowCountProperty = DependencyProperty.Register("FollowCount", typeof(int), typeof(UserProfileSummary), new PropertyMetadata(0));
        public static readonly DependencyProperty FansCountProperty = DependencyProperty.Register("FansCount", typeof(int), typeof(UserProfileSummary), new PropertyMetadata(0));
        public static readonly DependencyProperty CoinProperty = DependencyProperty.Register("Coin", typeof(int), typeof(UserProfileSummary), new PropertyMetadata(0));
        public static readonly DependencyProperty UserIntroProperty = DependencyProperty.Register("UserIntro", typeof(string), typeof(UserProfileSummary), new PropertyMetadata(Constants.DEFAULT_USER_INTRO));
        public static readonly DependencyProperty UserIdProperty = DependencyProperty.Register("UserId", typeof(int), typeof(UserProfileSummary), new PropertyMetadata(0));
        public static readonly DependencyProperty AvatarProperty = DependencyProperty.Register("Avatar", typeof(string), typeof(UserProfileSummary), new PropertyMetadata(Constants.DEFAULT_USER_PHOTO));
        public static readonly DependencyProperty CanFollowProperty = DependencyProperty.Register("CanFollow", typeof(bool), typeof(UserProfileSummary), new PropertyMetadata(true));

        #endregion

        #region CLR Property Wrapper


        public string Avatar
        {
            get { return (string)GetValue(AvatarProperty); }
            set { SetValue(AvatarProperty, value); }
        }

        public int FollowCount
        {
            get { return (int)GetValue(FollowCountProperty); }
            set { SetValue(FollowCountProperty, value); }
        }

        public int FansCount
        {
            get { return (int)GetValue(FansCountProperty); }
            set { SetValue(FansCountProperty, value); }
        }

        public int Coin
        {
            get { return (int)GetValue(CoinProperty); }
            set { SetValue(CoinProperty, value); }
        }

        public string UserIntro
        {
            get { return (string)GetValue(UserIntroProperty); }
            set { SetValue(UserIntroProperty, value); }
        }

        public int UserId
        {
            get { return (int)GetValue(UserIdProperty); }
            set { SetValue(UserIdProperty, value); }
        }

        public bool CanFollow
        {
            get { return (bool)GetValue(CanFollowProperty); }
            set { SetValue(CanFollowProperty, value); }
        }

        #endregion

        public Action<UserProfileSummary> FollowAction { get; set; }

        public Action<UserProfileSummary> UnFollowAction { get; set; }

        #region Life Cycle

        public UserProfileSummary()
        {
            this.InitializeComponent();
            this.root.DataContext = this;
        }

        #endregion

        #region Event

        private void Follower_Tapped(object sender, TappedRoutedEventArgs e)
        {
            UserFollowersPage.UserFollowsPageParams paras = new UserFollowersPage.UserFollowsPageParams();
            paras.PageType = UserFollowersPage.PageType.FOLLOW;
            paras.UserId = UserId;

            App.Current.RootFrame.Navigate(typeof(UserFollowersPage), paras);
        }

        private void Fans_Tapped(object sender, TappedRoutedEventArgs e)
        {
            UserFollowersPage.UserFollowsPageParams paras = new UserFollowersPage.UserFollowsPageParams();
            paras.PageType = UserFollowersPage.PageType.FANS;
            paras.UserId = UserId;

            App.Current.RootFrame.Navigate(typeof(UserFollowersPage), paras);

        }
        private void follow_Click(object sender, RoutedEventArgs e)
        {
            if (FollowAction != null)
            {
                FollowAction.Invoke(this);
            }
        }

        private void unFollow_Click(object sender, RoutedEventArgs e)
        {
            if (UnFollowAction != null)
            {
                UnFollowAction.Invoke(this);
            }
        }

        #endregion

       

    }
}
