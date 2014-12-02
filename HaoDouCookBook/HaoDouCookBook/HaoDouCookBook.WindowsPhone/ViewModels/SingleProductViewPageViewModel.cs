using Shared.Infrastructures;
using System.Collections.ObjectModel;

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

        private int topicId;

        public int TopicId
        {
            get { return topicId; }
            set { SetProperty<int>(ref topicId, value); }
        }

        private string topicName;

        public string TopicName
        {
            get { return topicName; }
            set { SetProperty<string>(ref topicName, value); }
        }

        private int recipeId;

        public int RecipeId
        {
            get { return recipeId; }
            set { SetProperty<int>(ref recipeId, value); }
        }

        private string recipeName;

        public string RecipeName
        {
            get { return recipeName; }
            set { SetProperty<string>(ref recipeName, value); }
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
            topicId = -1;
            topicName = string.Empty;
            recipeId = -1;
            recipeName = string.Empty;
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
