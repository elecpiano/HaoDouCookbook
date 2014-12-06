using Shared.Infrastructures;

namespace HaoDouCookBook.ViewModels
{
    public class MyPageViewModel : BindableBase
    {
        private bool signedIn;

        public bool SignedIn
        {
            get { return signedIn; }
            set
            {
                ShowLoginNotify = !value;
                SetProperty<bool>(ref signedIn, value);
            }
        }

        private bool showLoginNotify;

        public bool ShowLoginNotify
        {
            get { return showLoginNotify; }
            private set { SetProperty<bool>(ref showLoginNotify, value); }
        }

        private string loginNotifyInfo;

        public string LoginNotifyInfo
        {
            get { return loginNotifyInfo; }
            set { SetProperty<string>(ref loginNotifyInfo, value); }
        }



        public MyPageViewModel()
        {
            SignedIn = false;
            LoginNotifyInfo = " 登陆后，您可：\n 签到领豆币，兑换厨房用具；\n 秀厨艺，与美食达人公共成长；\n 收藏美食菜谱，营养知识。";
        }
    }
}
