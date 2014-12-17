using HaoDouCookBook.Common;
using Shared.Infrastructures;

namespace HaoDouCookBook.ViewModels
{
    public class UserData : BindableBase
    {

        private string userPhoto;

        public string UserPhoto
        {
            get { return userPhoto; }
            set { SetProperty<string>(ref userPhoto, value); }
        }

        private int userid;

        public int UserID 
        {
            get { return userid; }
            set { SetProperty<int>(ref userid, value); }
        }

        private string name;

        public string Name
        {
            get { return name; }
            set { SetProperty<string>(ref name, value); }
        }

        private string archiveDescription;

        public string ArchiveDescription
        {
            get { return archiveDescription; }
            set { SetProperty<string>(ref archiveDescription, value); }
        }

        public UserData()
        {
            userPhoto = Constants.DEFAULT_USER_PHOTO;
            userid = 0;
            name = string.Empty;
            archiveDescription = string.Empty;

        }
    }
}
