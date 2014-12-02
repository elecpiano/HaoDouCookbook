using System.Runtime.Serialization;

namespace HaoDouCookBook.HaoDou.DataModels.ChoicenessPage
{
    [DataContract]
    public class RankViewPageData
    {
        [DataMember(Name = "info")]
        public RankViewTopicInfo Info { get; set; }

        [DataMember(Name = "list")]
        public RankViewRecipe[] Recipes { get; set; }

        public RankViewPageData()
        {
            Info = new RankViewTopicInfo();
        }
 
    }

    [DataContract]
    public class RankViewTopicInfo
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string Intro { get; set; }

        [DataMember]
        public int Type { get; set; }

        public RankViewTopicInfo()
        {
            Id = -1;
            Title = string.Empty;
            Intro = string.Empty;
            Type = -1;
        }
    }


    [DataContract]
    public class RankViewRecipe
    {
        [DataMember]
        public int UserId { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string Cover { get; set; }

        [DataMember]
        public string ReviewTime { get; set; }

        [DataMember]
        public string LikeCount { get; set; }

        [DataMember]
        public string ViewCount { get; set; }

        [DataMember]
        public string CommentCount { get; set; }

        [DataMember]
        public string IsVideo { get; set; }

        [DataMember]
        public int RecipeId { get; set; }

        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public string UserCover { get; set; }

        [DataMember]
        public int IsFavorited { get; set; }

        [DataMember]
        public int FavoriteCount { get; set; }

        public RankViewRecipe()
        {
            UserId = -1;
            Title = string.Empty;
            Cover = string.Empty;
            ReviewTime = string.Empty;
            LikeCount = "0";
            ViewCount = "0";
            CommentCount = "0";
            IsVideo = "0";
            RecipeId = -1;
            UserName = string.Empty;
            UserCover = string.Empty;
            IsFavorited = 0;
            FavoriteCount = 0;
        }
    }
}
