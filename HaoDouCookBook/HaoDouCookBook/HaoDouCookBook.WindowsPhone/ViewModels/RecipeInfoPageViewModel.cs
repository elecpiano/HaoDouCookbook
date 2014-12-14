using Shared.Infrastructures;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Linq;

namespace HaoDouCookBook.ViewModels
{
    [DataContract]
    public class RecipeInfoPageViewModel: BindableBase
    {
        private int recipeId;

        [DataMember]
        public int RecipeId
        {
            get { return recipeId; }
            set { SetProperty<int>(ref recipeId, value); }
        }

        private string cover;

        [DataMember]
        public string Cover
        {
            get { return cover; }
            set { SetProperty<string>(ref cover, value); }
        }


        private string title;

        [DataMember]
        public string  Title
        {
            get { return title; }
            set { SetProperty<string>(ref title, value); }
        }

        private string intro;

        [DataMember]
        public string Intro
        {
            get { return intro; }
            set { SetProperty<string>(ref intro, value); }
        }

        private string cookTime;

        [DataMember]
        public string CookTime
        {
            get { return cookTime; }
            set { SetProperty<string>(ref cookTime, value); }
        }


        private string readyTime;

        [DataMember]
        public string ReadyTime
        {
            get { return readyTime; }
            set { SetProperty<string>(ref readyTime, value); }
        }

        private string tips;

        [DataMember]
        public string Tips
        {
            get { return tips; }
            set { SetProperty<string>(ref tips, value); }
        }

        private string userName;

        [DataMember]
        public string UserName
        {
            get { return userName; }
            set { SetProperty<string>(ref userName, value); }
        }

        private int favoriteCount;

        [DataMember]
        public int FavoriteCount
        {
            get { return favoriteCount; }
            set { SetProperty<int>(ref favoriteCount, value); }
        }


        private int photoCount;

        [DataMember]
        public int PhotoCount
        {
            get { return photoCount; }
            set { SetProperty<int>(ref photoCount, value); }
        }

        private string reviewTime;

        [DataMember]
        public string ReviewTime
        {
            get { return reviewTime; }
            set { SetProperty<string>(ref reviewTime, value); }
        }

        private int userId;

        [DataMember]
        public int UserId
        {
            get { return userId; }
            set { SetProperty<int>(ref userId, value); }
        }

        private string avatar;

        [DataMember]
        public string Avatar
        {
            get { return avatar; }
            set { SetProperty<string>(ref avatar, value); }
        }

        private int viewCount;

        [DataMember]
        public int ViewCount
        {
            get { return viewCount; }
            set { SetProperty<int>(ref viewCount, value); }
        }

        private int type;

        [DataMember]
        public int Type
        {
            get { return type; }
            set { SetProperty<int>(ref type, value); }
        }

        private string userCount;

        [DataMember]
        public string UserCount
        {
            get { return userCount; }
            set { SetProperty<string>(ref userCount, value); }
        }

        private ObservableCollection<TagItem> tags;

        [DataMember]
        public ObservableCollection<TagItem> Tags
        {
            get { return tags; }
            set { SetProperty<ObservableCollection<TagItem>>(ref tags, value); }
        }

        private int likeCount;

        [DataMember]
        public int LikeCount
        {
            get { return likeCount; }
            set { SetProperty<int>(ref likeCount, value); }
        }

        private bool isLike;

        [DataMember]
        public bool IsLike
        {
            get { return isLike; }
            set { SetProperty<bool>(ref isLike, value); }
        }

        private ObservableCollection<string> photoList;

        [DataMember]
        public ObservableCollection<string> PhotoList
        {
            get { return photoList; }
            set { SetProperty<ObservableCollection<string>>(ref photoList, value); }
        }


        [DataMember]
        private ObservableCollection<CookStep> cookSteps;

        [DataMember]
        public ObservableCollection<CookStep> CookSteps
        {
            get { return cookSteps; }
            set { SetProperty<ObservableCollection<CookStep>>(ref cookSteps, value); }
        }

        private ObservableCollection<FoodStuff> stuffs;

        [DataMember]
        public ObservableCollection<FoodStuff> Stuffs
        {
            get { return stuffs; }
            set { SetProperty<ObservableCollection<FoodStuff>>(ref stuffs, value); }
        }

        private string allStuffs;

        /// <summary>
        /// All stuff strings from Stuffs List, need call the GetAllStuffsString()
        /// to update this property
        /// </summary>
        public string AllStuffs
        {
            get { return allStuffs; }
            set { SetProperty<string>(ref allStuffs, value); }
        }

        private int adFlag;

        [DataMember]
        public int AdFlag
        {
            get { return adFlag; }
            set { SetProperty<int>(ref adFlag, value); }
        }

        private AdItem ad;

        [DataMember]
        public AdItem Ad
        {
            get { return ad; }
            set { SetProperty<AdItem>(ref ad, value); }
        }

        private int commentCount;

        [DataMember]
        public int CommentCount
        {
            get { return commentCount; }
            set { SetProperty<int>(ref commentCount, value); }
        }

        private ObservableCollection<Comment> comments;

        [DataMember]
        public ObservableCollection<Comment> Comments
        {
            get { return comments; }
            set { SetProperty<ObservableCollection<Comment>>(ref comments, value); }
        }

        private int productCount;

        [DataMember]
        public int ProductCount
        {
            get { return productCount; }
            set { SetProperty<int>(ref productCount, value); }
        }

        private ObservableCollection<Product> products;

        [DataMember]
        public ObservableCollection<Product> Products
        {
            get { return products; }
            set { SetProperty<ObservableCollection<Product>>(ref products, value); }
        }

        private bool isVip;

        [DataMember]
        public bool IsVip
        {
            get { return isVip; }
            set { SetProperty<bool>(ref isVip, value); }
        }

        public void GetAllStuffsString()
        {
            var nameList = Stuffs.Select(s => s.Name).ToArray();
            AllStuffs =  string.Join("、", nameList);
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
            allStuffs = string.Empty;
        }
    }

    [DataContract]
    public class CookStep : BindableBase
    {
        private string photo;

        [DataMember]
        public string Photo
        {
            get { return photo; }
            set { SetProperty<string>(ref photo, value); }
        }

        private string intro;

        [DataMember]
        public string Intro
        {
            get { return intro; }
            set { SetProperty<string>(ref intro, value); }
        }

        private int stepNumber;

        [DataMember]
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

    [DataContract]
    public class FoodStuff : BindableBase
    {
        private bool isMainStuff;

        [DataMember]
        public bool IsMainStuff
        {
            get { return isMainStuff; }
            set { SetProperty<bool>(ref isMainStuff, value); }
        }


        private string name;

        [DataMember]
        public string Name
        {
            get { return name; }
            set { SetProperty<string>(ref name, value); }
        }

        private string weight;

        [DataMember]
        public string Weight
        {
            get { return weight; }
            set { SetProperty<string>(ref weight, value); }
        }

        private int id;

        [DataMember]
        public int Id
        {
            get { return id; }
            set { SetProperty<int>(ref id, value); }
        }

        private int type;

        [DataMember]
        public int Type
        {
            get { return type; }
            set { SetProperty<int>(ref type, value); }
        }


        private int categoryId;

        [DataMember]
        public int CategoryId
        {
            get { return categoryId; }
            set { SetProperty<int>(ref categoryId, value); }
        }

        private string category;

        [DataMember]
        public string Category
        {
            get { return category; }
            set { SetProperty<string>(ref category, value); }
        }

        private bool foodFlag;

        [DataMember]
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

    [DataContract]
    public class AdItem : BindableBase
    {
        private string title;

        [DataMember]
        public string Title
        {
            get { return title; }
            set { SetProperty<string>(ref title, value); }
        }

        private string url;

        [DataMember]
        public string Url
        {
            get { return url; }
            set { SetProperty<string>(ref url, value); }
        }

        private string image;

        [DataMember]
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

    [DataContract]
    public class Comment : BindableBase
    {
        private int userId;

        [DataMember]
        public int UserId
        {
            get { return userId; }
            set { SetProperty<int>(ref userId, value); }
        }

        private string avatar;

        [DataMember]
        public string Avatar
        {
            get { return avatar; }
            set { SetProperty<string>(ref avatar, value); }
        }

        private string userName;

        [DataMember]
        public string UserName
        {
            get { return userName; }
            set { SetProperty<string>(ref userName, value); }
        }

        private int atUserId;

        [DataMember]
        public int AtUserId
        {
            get { return atUserId; }
            set { SetProperty<int>(ref atUserId, value); }
        }

        private string atUserName;

        [DataMember]
        public string AtUserName
        {
            get { return atUserName; }
            set { SetProperty<string>(ref atUserName, value); }
        }

        private string content;

        [DataMember]
        public string Content
        {
            get { return content; }
            set { SetProperty<string>(ref content, value); }
        }

        private string createTime;

        [DataMember]
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

    [DataContract]
    public class Product : BindableBase
    {
        private string image;

        [DataMember]
        public string Image
        {
            get { return image; }
            set { SetProperty<string>(ref image, value); }
        }

        private string userName;

        [DataMember]
        public string UserName
        {
            get { return userName; }
            set { SetProperty<string>(ref userName, value); }
        }

        private string content;

        [DataMember]
        public string Content
        {
            get { return content; }
            set { SetProperty<string>(ref content, value); }
        }

        private int userId;

        [DataMember]
        public int UserId
        {
            get { return userId; }
            set { SetProperty<int>(ref userId, value); }
        }

        private int productId;

        [DataMember]
        public int ProductId
        {
            get { return productId; }
            set { SetProperty<int>(ref productId, value); }
        }


        public Product()
        {
            Image = string.Empty;
            UserId = -1;
            UserName = string.Empty;
            Content = string.Empty;
            productId = -1;
        }
    }

}
