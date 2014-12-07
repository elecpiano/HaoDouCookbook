using HaoDouCookBook.Common;
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

        public static readonly DependencyProperty UserPhotoProperty = DependencyProperty.Register("UserPhoto", typeof(string), typeof(UserProfileSummary), new PropertyMetadata(Constants.DEFAULT_USER_PHOTO));
        public static readonly DependencyProperty FollowCountProperty = DependencyProperty.Register("FollowCount", typeof(int), typeof(UserProfileSummary), new PropertyMetadata(0));
        public static readonly DependencyProperty FansCountProperty = DependencyProperty.Register("FansCount", typeof(int), typeof(UserProfileSummary), new PropertyMetadata(0));
        public static readonly DependencyProperty CoinProperty = DependencyProperty.Register("Coin", typeof(int), typeof(UserProfileSummary), new PropertyMetadata(0));
        public static readonly DependencyProperty IsSignedInUserProperty = DependencyProperty.Register("IsSignedInUser", typeof(bool), typeof(UserProfileSummary), new PropertyMetadata(false));
        public static readonly DependencyProperty UserIntroProperty = DependencyProperty.Register("UserIntro", typeof(string), typeof(UserProfileSummary), new PropertyMetadata("这个豆子很懒，\n还没有给自己加个人价绍~"));
        public static readonly DependencyProperty UserIdProperty = DependencyProperty.Register("UserId", typeof(int), typeof(UserProfileSummary), new PropertyMetadata(0));

        #endregion

        #region CLR Property Wrapper

        public string UserPhoto
        {
            get { return (string)GetValue(UserPhotoProperty); }
            set { SetValue(UserPhotoProperty, value); }
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

        public bool IsSignedInUser
        {
            get { return (bool)GetValue(IsSignedInUserProperty); }
            set
            {
                SetValue(IsSignedInUserProperty, value);
                if (value)
                {
                    this.checkin.Visibility = Visibility.Visible;
                    this.personal.Visibility = Visibility.Visible;

                    this.chat.Visibility = Visibility.Collapsed;
                    this.follow.Visibility = Visibility.Collapsed;
                }
                else
                {
                    this.checkin.Visibility = Visibility.Collapsed;
                    this.personal.Visibility = Visibility.Collapsed;

                    this.chat.Visibility = Visibility.Visible;
                    this.chat.Visibility = Visibility.Visible;
                }
            }
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

        #endregion

        #region Life Cycle

        public UserProfileSummary()
        {
            this.InitializeComponent();
            this.root.DataContext = this;
        }

        #endregion
    }
}
