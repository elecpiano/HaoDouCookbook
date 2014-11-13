using HaoDouCookBook.Common;
using HaoDouCookBook.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public sealed partial class DiscoveryPageContent : UserControl
    {
        #region Life Cycle

        public DiscoveryPageContent()
        {
            this.InitializeComponent();
            Test();
        }

        #endregion

        #region Test
        
        private void Test()
        {
            ObservableCollection<UserData> usersdata = new ObservableCollection<UserData>();
            this.userMasterList.ItemsSource = usersdata;

            usersdata.Add(new UserData() { UserPhoto = Constants.DEFAULT_USER_PHOTO, UserID = "1" });
            usersdata.Add(new UserData() { UserPhoto = Constants.DEFAULT_USER_PHOTO, UserID = "1" });
            usersdata.Add(new UserData() { UserPhoto = Constants.DEFAULT_USER_PHOTO, UserID = "1" });
            usersdata.Add(new UserData() { UserPhoto = Constants.DEFAULT_USER_PHOTO, UserID = "1" });
            usersdata.Add(new UserData() { UserPhoto = Constants.DEFAULT_USER_PHOTO, UserID = "1" });
            usersdata.Add(new UserData() { UserPhoto = Constants.DEFAULT_USER_PHOTO, UserID = "1" });
            usersdata.Add(new UserData() { UserPhoto = Constants.DEFAULT_USER_PHOTO, UserID = "1" });
            usersdata.Add(new UserData() { UserPhoto = Constants.DEFAULT_USER_PHOTO, UserID = "1" });
            usersdata.Add(new UserData() { UserPhoto = Constants.DEFAULT_USER_PHOTO, UserID = "1" });
            usersdata.Add(new UserData() { UserPhoto = Constants.DEFAULT_USER_PHOTO, UserID = "1" });
            usersdata.Add(new UserData() { UserPhoto = Constants.DEFAULT_USER_PHOTO, UserID = "1" });
        }

        #endregion

    }
}
