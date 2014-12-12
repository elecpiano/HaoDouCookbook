using Shared.Infrastructures;
using System.Collections.ObjectModel;

namespace HaoDouCookBook.ViewModels
{
    public class MessagePageViewModel : BindableBase
    {
        private string noticeContent;

        public string NoticeContent
        {
            get { return noticeContent; }
            set { SetProperty<string>(ref noticeContent, value); }
        }

        private string subType;

        public string SubType
        {
            get { return subType; }
            set { SetProperty<string>(ref subType, value); }
        }


        public ObservableCollection<Message> Messages { get; set; }

        public MessagePageViewModel()
        {
            noticeContent = "暂时还没有新的通知";
            subType = string.Empty;
            Messages = new ObservableCollection<Message>();
        }
    }


    public class Message : BindableBase
    {
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

        private string updateTime;

        public string UpdateTime
        {
            get { return updateTime; }
            set { SetProperty<string>(ref updateTime, value); }
        }

        private string content;

        public string Content
        {
            get { return content; }
            set { SetProperty<string>(ref content, value); }
        }

        private int unreadCount;

        public int UnreadCount
        {
            get { return unreadCount; }
            set { SetProperty<int>(ref unreadCount, value); }
        }

        private int userId;

        public int UserId
        {
            get { return userId; }
            set { SetProperty<int>(ref userId, value); }
        }


        public Message()
        {
            avatar = string.Empty;
            userName = string.Empty;
            updateTime = string.Empty;
            content = string.Empty;
            unreadCount = 0;
        }
    }

}
