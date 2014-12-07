using Shared.Infrastructures;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaoDouCookBook.ViewModels
{
    public class UserFollowersPageViewModel : BindableBase
    {
        private string title;

        public string Title
        {
            get { return title; }
            set { SetProperty<string>(ref title, value); }
        }

        public ObservableCollection<UserFollower> Followers { get; set; }

        public UserFollowersPageViewModel()
        {
            Followers = new ObservableCollection<UserFollower>();
        }
    }

    public class UserFollower : BindableBase
    {
        private int userId;

        public int UserId
        {
            get { return userId; }
            set { SetProperty<int>(ref userId, value); }
        }

        private string avatar;

        public string Avatar
        {
            get { return avatar; }
            set { SetProperty<string>(ref avatar, value); }
        }

        private string userName;

        public string UserName
        {
            get { return userName; }
            set { SetProperty<string>(ref userName, value); }
        }

        private bool canFollow;

        public bool CanFollow
        {
            get { return canFollow; }
            set { SetProperty<bool>(ref canFollow, value); }
        }

        private bool isVip;

        public bool IsVip
        {
            get { return isVip; }
            set { SetProperty<bool>(ref isVip, value); }
        }



        public UserFollower()
        {
            canFollow = true;
            userId = 0;
            userName = string.Empty;
            avatar = string.Empty;
        }
    }

}
