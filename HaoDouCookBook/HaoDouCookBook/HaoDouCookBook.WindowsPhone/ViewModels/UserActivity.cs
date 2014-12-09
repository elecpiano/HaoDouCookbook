using Shared.Infrastructures;
using System.Collections.ObjectModel;

namespace HaoDouCookBook.ViewModels
{
    public class UserActivitiesGroup : BindableBase
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

        public UserActivitiesGroup()
        {
            key = string.Empty;
            day = string.Empty;
            month = string.Empty;
            Activities = new ObservableCollection<UserActivityItem>();
        }
    }


    public class UserActivityItem : BindableBase
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

        private string type;

        public string Type
        {
            get { return type; }
            set { SetProperty<string>(ref type, value); }
        }

        private int typeId;

        public int TypeId
        {
            get { return typeId; }
            set { SetProperty<int>(ref typeId, value); }
        }

        public UserActivityItem()
        {
            name = string.Empty;
            content = string.Empty;
            image = string.Empty;
            createTime = string.Empty;
            productId = 0;
            typeId = 0;
            type = string.Empty;
        }
    }

}
