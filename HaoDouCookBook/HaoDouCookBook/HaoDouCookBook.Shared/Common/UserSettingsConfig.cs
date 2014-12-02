using Shared.Infrastructures;

namespace HaoDouCookBook.Common
{
    public class UserSettingsConfig : BindableBase
    {
        private bool downloadImageIn2Gor3G;

        public bool DownloadImageIn2Gor3G
        {
            get { return downloadImageIn2Gor3G; }
            set { SetProperty<bool>(ref downloadImageIn2Gor3G, value); }
        }

        public UserSettingsConfig()
        {
            downloadImageIn2Gor3G = true;
        }
    }
}
