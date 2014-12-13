using Shared.Infrastructures;

namespace HaoDouCookBook.ViewModels
{
    public class PersonalInfoPageViewModel : BindableBase
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

        private string intro;

        public string Intro
        {
            get { return intro; }
            set { SetProperty<string>(ref intro, value); }
        }

        private string gender;

        public string Gender
        {
            get { return gender; }
            set { SetProperty<string>(ref gender, value); }
        }

        private string cityName;

        public string CityName
        {
            get { return cityName; }
            set { SetProperty<string>(ref cityName, value); }
        }

        private string tags;

        public string Tags
        {
            get { return tags; }
            set { SetProperty<string>(ref tags, value); }
        }

        private bool isModify;

        public bool IsModity
        {
            get { return isModify; }
            set { SetProperty<bool>(ref isModify, value); }
        }

        public PersonalInfoPageViewModel()
        {
            avatar = string.Empty;
            cityName = string.Empty;
            userName = string.Empty;
            intro = string.Empty;
            tags = string.Empty;
            gender = string.Empty;
            isModify = false;
        }
    }
}
