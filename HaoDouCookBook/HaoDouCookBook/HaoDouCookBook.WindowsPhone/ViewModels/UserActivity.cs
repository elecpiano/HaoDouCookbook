using Shared.Infrastructures;
using Shared.Utility;
using System.Collections.ObjectModel;

namespace HaoDouCookBook.ViewModels
{
    public class UserActivitiesGroup : BindableBase, ILoadMoreItem
    {
        private string key;

        public string Key
        {
            get { return key; }
            set { SetProperty<string>(ref key, value); }
        }

        private string day;

        public string Day
        {
            get { return day; }
            set { SetProperty<string>(ref day, value); }
        }

        private string month;

        public string Month
        {
            get { return month; }
            set { SetProperty<string>(ref month, value); }
        }

        public ObservableCollection<UserActivityItem> Activities { get; set; }

        public bool IsLoadMore { get; set; }

        public UserActivitiesGroup()
        {
            key = string.Empty;
            day = string.Empty;
            month = string.Empty;
            Activities = new ObservableCollection<UserActivityItem>();
            IsLoadMore = false;
        }
    }


    public class UserActivityItem : BindableBase, ILoadMoreItem
    {
        private string name;

        public string Name
        {
            get { return name; }
            set { SetProperty<string>(ref name, value); }
        }

        private string content;

        public string Content
        {
            get { return content; }
            set { SetProperty<string>(ref content, value); }
        }

        private string image;

        public string Image
        {
            get { return image; }
            set { SetProperty<string>(ref image, value); }
        }

        private string createTime;

        public string CreateTime
        {
            get { return createTime; }
            set { SetProperty<string>(ref createTime, value); }
        }

        private int productId;

        public int ProductId
        {
            get { return productId; }
            set { SetProperty<int>(ref productId, value); }
        }

        private int type;

        public int Type
        {
            get { return type; }
            set { SetProperty<int>(ref type, value); }
        }

        private int diggCount;

        public int DiggCount
        {
            get { return diggCount; }
            set { SetProperty<int>(ref diggCount, value); }
        }

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

        private int activityId;

        public int ActivityId
        {
            get { return activityId; }
            set { SetProperty<int>(ref activityId, value); }
        }

        private bool isDigg;

        public bool IsDigg
        {
            get { return isDigg; }
            set { SetProperty<bool>(ref isDigg, value); }
        }

        public ObservableCollection<Comment> Comments { get; set; }

        private int commentsCount;

        public int CommentsCount
        {
            get { return commentsCount; }
            set { SetProperty<int>(ref commentsCount, value); }
        }


        public bool IsLoadMore { get; set; }

        public UserActivityItem()
        {
            name = string.Empty;
            content = string.Empty;
            image = string.Empty;
            createTime = string.Empty;
            productId = 0;
            type = 0;
            userId = 0;
            avatar = string.Empty;
            isDigg = false;
            IsLoadMore = false;
            Comments = new ObservableCollection<Comment>();
        }
    }

}
