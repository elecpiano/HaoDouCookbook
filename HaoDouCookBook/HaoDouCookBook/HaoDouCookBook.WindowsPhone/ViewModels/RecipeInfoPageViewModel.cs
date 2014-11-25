using Shared.Infrastructures;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaoDouCookBook.ViewModels
{
    public class RecipeInfoPageViewModel: BindableBase
    {
        private int recipeId;

        public int RecipeId
        {
            get { return recipeId; }
            set { SetProperty<int>(ref recipeId, value); }
        }

        private string cover;

        public string Cover
        {
            get { return cover; }
            set { SetProperty<string>(ref cover, value); }
        }


        private string title;

        public string  Title
        {
            get { return title; }
            set { SetProperty<string>(ref title, value); }
        }

        private string intro;

        public string Intro
        {
            get { return intro; }
            set { SetProperty<string>(ref intro, value); }
        }

        private string cookTime;

        public string CookTime
        {
            get { return cookTime; }
            set { SetProperty<string>(ref cookTime, value); }
        }


        private string readyTime;

        public string ReadyTime
        {
            get { return readyTime; }
            set { SetProperty<string>(ref readyTime, value); }
        }

        private string tips;

        public string Tips
        {
            get { return tips; }
            set { SetProperty<string>(ref tips, value); }
        }


        private string userName;

        public string UserName
        {
            get { return userName; }
            set { SetProperty<string>(ref userName, value); }
        }

        private int favoriteCount;

        public int FavoriteCount
        {
            get { return favoriteCount; }
            set { SetProperty<int>(ref favoriteCount, value); }
        }


        private int photoCount;

        public int PhotoCount
        {
            get { return photoCount; }
            set { SetProperty<int>(ref photoCount, value); }
        }

        private string reviewTime;

        public string ReviewTime
        {
            get { return reviewTime; }
            set { SetProperty<string>(ref reviewTime, value); }
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

        private int viewCount;

        public int ViewCount
        {
            get { return viewCount; }
            set { SetProperty<int>(ref viewCount, value); }
        }

        private int type;

        public int Type
        {
            get { return type; }
            set { SetProperty<int>(ref type, value); }
        }

        private string userCount;

        public string UserCount
        {
            get { return userCount; }
            set { SetProperty<string>(ref userCount, value); }
        }

        private ObservableCollection<TagItem> tags;

        public ObservableCollection<TagItem> Tags
        {
            get { return tags; }
            set { SetProperty<ObservableCollection<TagItem>>(ref tags, value); }
        }

        private int likeCount;

        public int LikeCount
        {
            get { return likeCount; }
            set { SetProperty<int>(ref likeCount, value); }
        }

        private bool isLike;

        public bool IsLike
        {
            get { return isLike; }
            set { SetProperty<bool>(ref isLike, value); }
        }

        private ObservableCollection<string> photoList;

        public ObservableCollection<string> PhotoList
        {
            get { return photoList; }
            set { SetProperty<ObservableCollection<string>>(ref photoList, value); }
        }


        private ObservableCollection<CookStep> cookSteps;

        public ObservableCollection<CookStep> CookSteps
        {
            get { return cookSteps; }
            set { SetProperty<ObservableCollection<CookStep>>(ref cookSteps, value); }
        }

        private ObservableCollection<FoodStuff> stuffs;

        public ObservableCollection<FoodStuff> Stuffs
        {
            get { return stuffs; }
            set { SetProperty<ObservableCollection<FoodStuff>>(ref stuffs, value); }
        }


        private int adFlag;

        public int AdFlag
        {
            get { return adFlag; }
            set { SetProperty<int>(ref adFlag, value); }
        }

        private AdItem ad;

        public AdItem Ad
        {
            get { return ad; }
            set { SetProperty<AdItem>(ref ad, value); }
        }

        private int commentCount;

        public int CommonCount
        {
            get { return commentCount; }
            set { SetProperty<int>(ref commentCount, value); }
        }

        private ObservableCollection<Comment> comments;

        public ObservableCollection<Comment> Comments
        {
            get { return comments; }
            set { SetProperty<ObservableCollection<Comment>>(ref comments, value); }
        }

        private int productCount;

        public int ProductCount
        {
            get { return productCount; }
            set { SetProperty<int>(ref productCount, value); }
        }

        private ObservableCollection<Product> products;

        public ObservableCollection<Product> Products
        {
            get { return products; }
            set { SetProperty<ObservableCollection<Product>>(ref products, value); }
        }

        private bool isVip;

        public bool IsVip
        {
            get { return isVip; }
            set { SetProperty<bool>(ref isVip, value); }
        }

        public RecipeInfoPageViewModel()
        {
            RecipeId = -1;
            Cover = string.Empty;
            Title = string.Empty;
            Intro = string.Empty;
            CookTime = string.Empty;
            ReadyTime = string.Empty;
            Tips = string.Empty;
            UserName = string.Empty;
            FavoriteCount = 0;
            PhotoCount = 0;
            ReviewTime = string.Empty;
            UserId = -1;
            Avatar = string.Empty;
            ViewCount = 0;
            Type = -1;
            UserCount = string.Empty;
            LikeCount = 0;
            IsLike = false;
            AdFlag = -1;
            ProductCount = 0;
            IsVip = false;
            comments = new ObservableCollection<Comment>();
            products = new ObservableCollection<Product>();
            cookSteps = new ObservableCollection<CookStep>();
            stuffs = new ObservableCollection<FoodStuff>();
            ad = new AdItem();
            tags = new ObservableCollection<TagItem>();
            photoList = new ObservableCollection<string>();
        }
    }

    public class CookStep : BindableBase
    {
        private string photo;

        public string Photo
        {
            get { return photo; }
            set { SetProperty<string>(ref photo, value); }
        }

        private string intro;

        public string Intro
        {
            get { return intro; }
            set { SetProperty<string>(ref intro, value); }
        }

        private int stepNumber;

        public int StepNumber
        {
            get { return stepNumber; }
            set { SetProperty<int>(ref stepNumber, value); }
        }


        public CookStep()
        {
            photo = string.Empty;
            intro = string.Empty;
            stepNumber = -1;
        }
    }

    public class FoodStuff : BindableBase
    {
        private bool isMainStuff;

        public bool IsMainStuff
        {
            get { return isMainStuff; }
            set { SetProperty<bool>(ref isMainStuff, value); }
        }


        private string name;

        public string Name
        {
            get { return name; }
            set { SetProperty<string>(ref name, value); }
        }

        private string weight;

        public string Weight
        {
            get { return weight; }
            set { SetProperty<string>(ref weight, value); }
        }

        private int id;

        public int Id
        {
            get { return id; }
            set { SetProperty<int>(ref id, value); }
        }

        private int type;

        public int Type
        {
            get { return type; }
            set { SetProperty<int>(ref type, value); }
        }


        private int categoryId;

        public int CategoryId
        {
            get { return categoryId; }
            set { SetProperty<int>(ref categoryId, value); }
        }

        private string category;

        public string Category
        {
            get { return category; }
            set { SetProperty<string>(ref category, value); }
        }

        private bool foodFlag;

        public bool FoodFlag
        {
            get { return foodFlag; }
            set { SetProperty<bool>(ref foodFlag, value); }
        }

        public FoodStuff()
        {
            Id = -1;
            Name = string.Empty;
            Weight = string.Empty;
            Type = -1;
            CategoryId = -1;
            Category = string.Empty;
            FoodFlag = false;
            isMainStuff = false;
        }
    }

    public class AdItem : BindableBase
    {
        private string title;

        public string Title
        {
            get { return title; }
            set { SetProperty<string>(ref title, value); }
        }

        private string url;

        public string Url
        {
            get { return url; }
            set { SetProperty<string>(ref url, value); }
        }

        private string image;

        public string Image
        {
            get { return image; }
            set { SetProperty<string>(ref image, value); }
        }

        public AdItem()
        {
            title = string.Empty;
            url = string.Empty;
            image = string.Empty;
        }
 
    }

    public class Comment : BindableBase
    {
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

        private string userName;

        public string UserName
        {
            get { return userName; }
            set { SetProperty<string>(ref userName, value); }
        }

        private int atUserId;

        public int AtUserId
        {
            get { return atUserId; }
            set { SetProperty<int>(ref atUserId, value); }
        }

        private string atUserName;

        public string AtUserName
        {
            get { return atUserName; }
            set { SetProperty<string>(ref atUserName, value); }
        }

        private string content;

        public string Content
        {
            get { return content; }
            set { SetProperty<string>(ref content, value); }
        }

        private string createTime;

        public string CreateTime
        {
            get { return createTime; }
            set { SetProperty<string>(ref createTime, value); }
        }

        public Comment()
        {
            UserId = -1;
            AtUserId = -1;
            UserName = string.Empty;
            AtUserName = string.Empty;
            Content = string.Empty;
            CreateTime = string.Empty;
            Avatar = string.Empty;
        }

    }

    public class Product : BindableBase
    {
        private string image;

        public string Image
        {
            get { return image; }
            set { SetProperty<string>(ref image, value); }
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

        private int userId;

        public int UserId
        {
            get { return userId; }
            set { SetProperty<int>(ref userId, value); }
        }

        public Product()
        {
            Image = string.Empty;
            UserId = -1;
            UserName = string.Empty;
            Content = string.Empty;
        }
    }

}
