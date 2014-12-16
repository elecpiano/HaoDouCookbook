using Shared.Infrastructures;
using System.Collections.ObjectModel;

namespace HaoDouCookBook.ViewModels
{
    public class IMPageViewModel : BindableBase
    {
        public ObservableCollection<IMMessage> Messages { get; set; }

        private string userName;

        public string UserName
        {
            get { return userName; }
            set { SetProperty<string>(ref userName, value); }
        }

        public IMPageViewModel()
        {
            Messages = new ObservableCollection<IMMessage>();
        }
    }

    public class IMMessage : BindableBase
    {
        private int userId;

        public int UserId
        {
            get { return userId; }
            set { SetProperty<int>(ref userId, value); }
        }

        private string message;

        public string Message
        {
            get { return message; }
            set { SetProperty<string>(ref message, value); }
        }

        private string createTime;

        public string CreateTime
        {
            get { return createTime; }
            set { SetProperty<string>(ref createTime, value); }
        }

        private bool isSignedInUser;

        public bool IsSignedInUser
        {
            get { return isSignedInUser; }
            set { SetProperty<bool>(ref isSignedInUser, value); }
        }

        private string avatar;

        public string Avatar
        {
            get { return avatar; }
            set { SetProperty<string>(ref avatar, value); }
        }

        public IMMessage()
        {
            userId = 0;
            message = string.Empty;
            createTime = string.Empty;
            isSignedInUser = false;
            avatar = string.Empty;
        }
    }

}
