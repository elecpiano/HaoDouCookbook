using System.Runtime.Serialization;

namespace HaoDouCookBook.HaoDou.DataModels.ChoicenessPage
{
    [DataContract]
    public class RecipeRecommend
    {
        [DataMember]
        public string Cover { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public int LikeCount { get; set; }

        [DataMember]
        public int RecipeId { get; set; }

        [DataMember]
        public int IsLike { get; set; }

        [DataMember]
        public RecipeRecommendTag[] Tags { get; set; }

        public string GetTagsString()
        {
            if (Tags == null)
            {
                return string.Empty;
            }

            string[] allTags = new string[Tags.Length];
            for (int i = 0; i < Tags.Length; i++)
            {
                allTags[i] = Tags[i].Name;
            }

            return string.Join("  ", allTags);
        }

        public RecipeRecommend()
        {
            Cover = string.Empty;
            Title = string.Empty;
            UserName = string.Empty;
            LikeCount = 0;
            RecipeId = -1;
            IsLike = -1;
        }
    }
    
    [DataContract]
    public class RecipeRecommendTag
    {

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        public RecipeRecommendTag()
        {
            Id = -1;
            Name = string.Empty;
        }
    }

    [DataContract]
    public class RecipeRecommendPageData
    {
        [DataMember(Name = "list")]
        public RecipeRecommend[] Recipes { get; set; }
    }
}
