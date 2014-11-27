using Shared.Infrastructures;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaoDouCookBook.ViewModels
{
    public class SingleProductViewPageViewModel : BindableBase
    {
        private int productId;

        public int ProductId
        {
            get { return productId; }
            set { SetProperty<int>(ref productId, value); }
        }

        private string title;

        public string Title
        {
            get { return title; }
            set { SetProperty<string>(ref title, value); }
        }

        private int userId;

        public int UserId
        {
            get { return userId; }
            set { SetProperty<int>(ref userId, value); }
        }

        private string cover;

        public string Cover
        {
            get { return cover; }
            set { SetProperty<string>(ref cover, value); }
        }

        private string userName;

        public string UserName
        {
            get { return userName; }
            set { SetProperty<string>(ref userName, value); }
        }

        private string userAvatar;

        public string UserAvatar
        {
            get { return userAvatar; }
            set { SetProperty<string>(ref userAvatar, value); }
        }

        private string timeStr;

        public string TimeStr
        {
            get { return timeStr; }
            set { SetProperty<string>(ref timeStr, value); }
        }

        private int diggCount;

        public int DiggCount
        {
            get { return diggCount; }
            set { SetProperty<int>(ref diggCount, value); }
        }


        private string intro;

        public string Intro
        {
            get { return intro; }
            set { SetProperty<string>(ref intro, value); }
        }

        private string position;

        public string Position
        {
            get { return position; }
            set { SetProperty<string>(ref position, value); }
        }


        public ObservableCollection<ProductViewPageDigg> DiggList { get; set; }

        public ObservableCollection<Comment> Comments { get; set; }

        public SingleProductViewPageViewModel()
        {
            productId = -1;
            title = string.Empty;
            userId = -1;
            userName = string.Empty;
            userAvatar = string.Empty;
            timeStr = string.Empty;
            diggCount = 0;
            intro = string.Empty;
            DiggList = new ObservableCollection<ProductViewPageDigg>();
            Comments = new ObservableCollection<Comment>();
        }


    }

    public class ProductViewPageDigg : BindableBase
    {
        private int userId;

        public int UserId
        {
            get { return userId; }
            set { SetProperty<int>(ref userId, value); }
        }

        private string userAvatar;

        public string UserAvatar
        {
            get { return userAvatar; }
            set { SetProperty<string>(ref userAvatar, value); }
        }

        private string userName;

        public string UserName
        {
            get { return userName; }
            set { SetProperty<string>(ref userName, value); }
        }

        public ProductViewPageDigg()
        {
            userId = -1;
            userAvatar = string.Empty;
            UserName = string.Empty;
        }

    }

}
