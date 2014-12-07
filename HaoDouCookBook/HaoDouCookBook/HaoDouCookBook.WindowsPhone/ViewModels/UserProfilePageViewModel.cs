using Shared.Infrastructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaoDouCookBook.ViewModels
{
    public class UserProfilePageViewModel : BindableBase
    {

        private int followCount;

        public int FollowCount 
        {
            get { return followCount; }
            set { SetProperty<int>(ref followCount, value); }
        }

        private int userId;

        public int UserId
        {
            get { return userId; }
            set { SetProperty<int>(ref userId, value); }
        }

        private int fansCount;

        public int FansCount
        {
            get { return fansCount; }
            set { SetProperty<int>(ref fansCount, value); }
        }

        private int coin;

        public int Coin
        {
            get { return coin; }
            set { SetProperty<int>(ref coin, value); }
        }

        private string userAvatar;

        public string UserAvatar
        {
            get { return userAvatar; }
            set { SetProperty<string>(ref userAvatar, value); }
        }

        private string userintro;

        public string UserIntro
        {
            get { return userintro; }
            set { SetProperty<string>(ref userintro, value); }
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


        public UserProfilePageViewModel()
        {
            userName = string.Empty;
            followCount = 0;
            userId = 0;
            fansCount = 0;
            coin = 0;
            userAvatar = string.Empty;
            userintro = string.Empty;
        }
    }
}
