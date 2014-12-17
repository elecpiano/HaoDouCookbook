using HaoDouCookBook.HaoDou.DataModels.ChoicenessPage;
using System.Runtime.Serialization;

namespace HaoDouCookBook.HaoDou.DataModels.Discovery
{
    [DataContract]
    public class SingleProductViewPageData
    {
        [DataMember(Name = "info")]
        public SingleProductViewInfo Info { get; set; }

        [DataMember(Name = "count")]
        public string Count { get; set; }

        [DataMember(Name = "list")]
        public SingleProductViewComment[] CommentList { get; set; }

        public SingleProductViewPageData()
        {
            Count = "0";
        }
    }

    [DataContract]
    public class SingleProductViewInfo
    {
        [DataMember]
        public int Pid { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public int UserId { get; set; }

        [DataMember]
        public string Cover { get; set; }

        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public string Avatar { get; set; }

        [DataMember]
        public string Position { get; set; }

        [DataMember]
        public string TimeStr { get; set; }

        [DataMember]
        public int TopicId { get; set; }

        [DataMember]
        public string TopicName { get; set; }

        [DataMember]
        public int RecipeId { get; set; }

        [DataMember]
        public string RecipeTitle { get; set; }

        [DataMember]
        public string Intro { get; set; }

        [DataMember]
        public string Url { get; set; }

        [DataMember]
        public string DiggCount { get; set; }

        [DataMember]
        public SingleProductViewDigg[] DiggList { get; set; }
        
        [DataMember]
        public int IsDigg { get; set; }

        public SingleProductViewInfo()
        {
            Pid = -1;
            Title = string.Empty;
            UserId = -1;
            Cover = string.Empty;
            UserName = string.Empty;
            Avatar = string.Empty;
            Position = string.Empty;
            TimeStr = string.Empty;
            TopicId = 0;
            TopicName = string.Empty;
            RecipeId = -1;
            RecipeTitle = string.Empty;
            Intro = string.Empty;
            Url = string.Empty;
            DiggCount = "0";
            IsDigg = 0;
        }
    }

    [DataContract]
    public class SingleProductViewDigg
    {
        [DataMember]
        public string UserId { get; set; }

        [DataMember]
        public string Avatar { get; set; }

        [DataMember]
        public string UserName { get; set; }

        public SingleProductViewDigg()
        {
            UserId = "-1";
            Avatar = string.Empty;
            UserName = string.Empty;
        }
    }

    [DataContract]
    public class SingleProductViewComment : Comment
    {
        [DataMember]
        public int Cid { get; set; }

        public SingleProductViewComment()
        {
            Cid = -1;
        }
    }
}
