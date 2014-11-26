using Shared.Infrastructures;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaoDouCookBook.ViewModels
{
    public class ProductPageViewModel : BindableBase
    {
        private string title;

        public string Title
        {
            get { return title; }
            set { SetProperty<string>(ref title, value); }
        }

        private string content;

        public string Content
        {
            get { return content; }
            set { SetProperty<string>(ref content, value); }
        }


        private int count;

        public int Count
        {
            get { return count; }
            set { SetProperty<int>(ref count, value); }
        }

        private string cover;

        public string Cover
        {
            get { return cover; }
            set { SetProperty<string>(ref cover, value); }
        }

        public ObservableCollection<ProductPageRecipe> Products { get; set; }

        public ProductPageViewModel()
        {
            title = string.Empty;
            count = 0;
            content = string.Empty;
            cover = string.Empty;
            Products = new ObservableCollection<ProductPageRecipe>();
        }

    }


    public class ProductPageRecipe : BindableBase
    {
        private string title;

        public string Title
        {
            get { return title; }
            set { SetProperty<string>(ref title, value); }
        }

        private int productId;

        public int ProductId
        {
            get { return productId; }
            set { SetProperty<int>(ref productId, value); }
        }

        private int recipeId;

        public int RecipeId
        {
            get { return recipeId; }
            set { SetProperty<int>(ref recipeId, value); }
        }

        private string photoUrl;

        public string PhotoUrl
        {
            get { return photoUrl; }
            set { SetProperty<string>(ref photoUrl, value); }
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

        private int userId;

        public int UserId
        {
            get { return userId; }
            set { SetProperty<int>(ref userId, value); }
        }

        private int likeNumber;

        public int LikeNumber
        {
            get { return likeNumber; }
            set { SetProperty<int>(ref likeNumber, value); }
        }


        private string createTIme;

        public string CreatTime
        {
            get { return createTIme; }
            set { SetProperty<string>(ref createTIme, value); }
        }

        private string position;

        public string Position
        {
            get { return position; }
            set { SetProperty<string>(ref position, value); }
        }

        private string intro;

        public string Intro
        {
            get { return intro; }
            set { SetProperty<string>(ref intro, value); }
        }

        private string timeStr;

        public string TimeStr
        {
            get { return timeStr; }
            set { SetProperty<string>(ref timeStr, value); }
        }


        public ObservableCollection<ProductPageComment> Comments { get; set; }

        public ProductPageRecipe()
        {
            title = string.Empty;
            productId = -1;
            recipeId = -1;
            photoUrl = string.Empty;
            userAvatar = string.Empty;
            userName = string.Empty;
            position = string.Empty;
            intro = string.Empty;
            Comments = new ObservableCollection<ProductPageComment>();
            timeStr = string.Empty;
        }


    }

    public class ProductPageComment : BindableBase 
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

        private string content;

        public string Content
        {
            get { return content; }
            set { SetProperty<string>(ref content, value); }
        }

        public ProductPageComment()
        {
            userId = -1;
            userName = string.Empty;
            content = string.Empty;
        }
    }
}
