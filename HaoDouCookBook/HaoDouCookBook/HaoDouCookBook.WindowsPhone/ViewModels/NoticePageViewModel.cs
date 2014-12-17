using Shared.Infrastructures;
using Shared.Utility;
using System.Collections.ObjectModel;

namespace HaoDouCookBook.ViewModels
{
    public class NoticePageViewModel : BindableBase
    {
        public ObservableCollection<NoticeItem> Notices { get; set; }

        public NoticePageViewModel()
        {
            Notices = new ObservableCollection<NoticeItem>(); 
        }
    }


    public class NoticeItem : BindableBase, ILoadMoreItem
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

        private int type;

        public int Type
        {
            get { return type; }
            set { SetProperty<int>(ref type, value); }
        }

        private string avatar;

        public string Avatar
        {
            get { return avatar; }
            set { SetProperty<string>(ref avatar, value); }
        }

        private string content;

        public string Content
        {
            get { return content; }
            set { SetProperty<string>(ref content, value); }
        }

        private string time;

        public string Time
        {
            get { return time; }
            set { SetProperty<string>(ref time, value); }
        }

        private int contentId;

        public int ContentId
        {
            get { return contentId; }
            set { SetProperty<int>(ref contentId, value); }
        }

        public bool IsLoadMore { get; set; }

        public NoticeItem()
        {
            userId = 0;
            userName = string.Empty;
            type = 0;
            avatar = string.Empty;
            content = string.Empty;
            time = string.Empty;
            IsLoadMore = false;


        }
    }

}
