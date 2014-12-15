using Shared.Infrastructures;
using Shared.Utility;

namespace HaoDouCookBook.ViewModels
{
    public class DiggUser : BindableBase, ILoadMoreItem
    {
        private int userId;

        public int UserId
        {
            get { return userId; }
            set { SetProperty<int>(ref userId, value); }
        }

        private string userName;

        public string UserName
        {
            get { return userName; }
            set { SetProperty<string>(ref userName, value); }
        }

        private string description;

        public string Description
        {
            get { return description; }
            set { SetProperty<string>(ref description, value); }
        }

        private bool isVip;

        public bool IsVip
        {
            get { return isVip; }
            set { SetProperty<bool>(ref isVip, value); }
        }

        private string avatar;

        public string Avatar
        {
            get { return avatar; }
            set { SetProperty<string>(ref avatar, value); }
        }

        public bool IsLoadMore { get; set; }

        public DiggUser()
        {
            userId = 0;
            userName = string.Empty;
            description = string.Empty;
            isVip = false;
            avatar = string.Empty;
            IsLoadMore = false;
        }
    }

}
