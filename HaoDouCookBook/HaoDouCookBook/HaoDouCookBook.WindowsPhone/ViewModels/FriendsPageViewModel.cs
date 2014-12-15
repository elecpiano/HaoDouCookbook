using Shared.Infrastructures;
using System.Collections.ObjectModel;

namespace HaoDouCookBook.ViewModels
{
    public class FriendsPageViewModel : BindableBase
    {
        private FansInfo info;

        public FansInfo Info
        {
            get { return info; }
            set { SetProperty<FansInfo>(ref info, value); }
        }

        private int allFansCount;

        public int AllFansCount
        {
            get { return allFansCount; }
            set { SetProperty<int>(ref allFansCount, value); }
        }

        private int fansCount;

        public int FansCount
        {
            get { return fansCount; }
            set { SetProperty<int>(ref fansCount, value); }
        }

        public ObservableCollection<FriendsNameCategory> FriendsNameCategories { get; set; }

        public FriendsPageViewModel()
        {
            allFansCount = 0;
            fansCount = 0;
            info = new FansInfo();
            FriendsNameCategories = new ObservableCollection<FriendsNameCategory>();
        }
    }

    public class FriendsNameCategory : BindableBase, ILoadMoreItem
    {
        private string firstWord;

        public string FirstWord
        {
            get { return firstWord; }
            set { SetProperty<string>(ref firstWord, value); }
        }

        public ObservableCollection<Friend> Friends { get; set; }

        public bool IsLoadMore { get; set; }

        public FriendsNameCategory()
        {
            firstWord = string.Empty;
            Friends = new ObservableCollection<Friend>();
            IsLoadMore = false;
        }
    }


    public class Friend : BindableBase
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

        private string avatar;

        public string Avatar
        {
            get { return avatar; }
            set { SetProperty<string>(ref avatar, value); }
        }

        private bool isVip;

        public bool IsVip
        {
            get { return isVip; }
            set { SetProperty<bool>(ref isVip, value); }
        }

        private string description;

        public string Description
        {
            get { return description; }
            set { SetProperty<string>(ref description, value); }
        }


        public Friend()
        {
            userId = 0;
            userName = string.Empty;
            avatar = string.Empty;
            isVip = false;
            description = string.Empty;
        }
    }


    public class FansInfo : BindableBase
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

        private string avatar;

        public string Avatar
        {
            get { return avatar; }
            set { SetProperty<string>(ref avatar, value); }
        }

        private bool isVip;

        public bool IsVip
        {
            get { return isVip; }
            set { SetProperty<bool>(ref isVip, value); }
        }

        public FansInfo()
        {
            userId = 0;
            userName = string.Empty;
            avatar = string.Empty;
            isVip = false;
        }
    }

}
