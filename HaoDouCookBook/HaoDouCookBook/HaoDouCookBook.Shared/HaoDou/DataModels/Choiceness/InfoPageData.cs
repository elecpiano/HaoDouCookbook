using System.Runtime.Serialization;

namespace HaoDouCookBook.HaoDou.DataModels.ChoicenessPage
{
    [DataContract]
    public class InfoPageData
    {
        [DataMember(Name = "info")]
        public Info Info { get; set; }

        public InfoPageData()
        {
            Info = new Info();
        }
    }

    [DataContract]
    public class CookStep
    {
        [DataMember]
        public string StepPhoto { get; set; }

        [DataMember]
        public string Intro { get; set; }

        public CookStep()
        {
            StepPhoto = string.Empty;
            Intro = string.Empty;
        }
    }

    [DataContract]
    public class FoodStuff
    {

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "weight")]
        public string Weight { get; set; }

        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "type")]
        public int Type { get; set; }
        
        [DataMember(Name = "cateid")]
        public int CategoryId { get; set; }

        [DataMember(Name = "cate")]
        public string Category { get; set; }

        [DataMember(Name = "food_flag")]
        public int FoodFlag { get; set; }

        public FoodStuff()
        {
            Id = -1;
            Name = string.Empty;
            Weight = string.Empty;
            Type = - 1;
            CategoryId = -1;
            Category = string.Empty;
            FoodFlag = -1;
        }
    }

    [DataContract]
    public class AdData
    {
        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string Url { get; set; }

        [DataMember]
        public string Image { get; set; }

        public AdData()
        {
            Title = string.Empty;
            Url = string.Empty;
            Image = string.Empty;
        }
    }

    [DataContract]
    public class Product
    {
        [DataMember(Name = "Img")]
        public string Image { get; set; }

        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public string Content { get; set; }

        [DataMember]
        public int UserId { get; set; }

        [DataMember]
        public int Pid { get; set; }

        public Product()
        {
            Image = string.Empty;
            UserId = -1;
            UserName = string.Empty;
            Content = string.Empty;
            Pid = -1;
        }
    }

    [DataContract]
    public class Info
    {
        [DataMember]
        public int RecipeId { get; set; }

        [DataMember]
        public string Cover { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string Intro { get; set; }

        [DataMember]
        public string CookTime { get; set; }

        [DataMember]
        public string ReadyTime { get; set; }

        [DataMember]
        public string Tips { get; set; }

        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public int FavoriteCount { get; set; }
        
        [DataMember]
        public int PhotoCount { get; set; }

        [DataMember]
        public string ReviewTime { get; set; }

        [DataMember]
        public int UserId { get; set; }

        [DataMember]
        public string Avatar { get; set; }

        [DataMember]
        public int ViewCount { get; set; }

        [DataMember]
        public int Type { get; set; }

        [DataMember]
        public string UserCount { get; set; }

        [DataMember]
        public RecipeRecommendTag[] Tags { get; set; }

        [DataMember]
        public int LikeCount { get; set; }

        [DataMember]
        public int IsLike { get; set; }

        [DataMember]
        public string[] PhotoList { get; set; }
        
        [DataMember]
        public CookStep[] Steps { get; set; }

        [DataMember]
        public FoodStuff[] Stuff { get; set; }

        [DataMember]
        public FoodStuff[] MainStuff { get; set; }

        [DataMember]
        public FoodStuff[] OtherStuff { get; set; }

        [DataMember(Name = "ad_flag")]
        public int AdFlag { get; set; }

        [DataMember(Name = "ad_data")]
        public AdData AD { get; set; }

        [DataMember]
        public string CommentCount { get; set; }

        [DataMember]
        public Comment[] CommentList { get; set; }

        [DataMember]
        public int ProductCount { get; set; }

        [DataMember]
        public Product[] Product { get; set; }

        [DataMember]
        public int Vip { get; set; }

        public Info()
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
            IsLike = -1;
            AdFlag = -1;
            ProductCount = 0;
            Vip = -1;
            CommentCount = string.Empty;
        }

    }

    
}
