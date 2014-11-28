using System.Runtime.Serialization;

namespace HaoDouCookBook.HaoDou.DataModels.ChoicenessPage
{
    [DataContract]
    public class TagCategoryItem
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string Url { get; set; }

        [DataMember]
        public string Type { get; set; }

        [DataMember]
        public string Icon { get; set; }

        [DataMember]
        public int SortIndex { get; set; }

        public TagCategoryItem()
        {
            Id = -1;
            Title = string.Empty;
            Url = string.Empty;
            Type = string.Empty;
            Icon = string.Empty;
            SortIndex = -1;
        }

    }

    [DataContract]
    public class TagListItem
    {
        [DataMember]
        public int RecipeId { get; set; }

        [DataMember]
        public int ViewCount { get; set; }

        [DataMember]
        public string Cover { get; set; }

        [DataMember]
        public int LikeCount { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string Stuff { get; set; }

        [DataMember]
        public string Card { get; set; }

        public TagListItem()
        {
            RecipeId = -1;
            ViewCount = 0;
            LikeCount = 0;
            Title = string.Empty;
            Stuff = string.Empty;
            Card = string.Empty;
        }
    }

    [DataContract]
    public class FoodInfo
    {
        [DataMember]
        public string Cover { get; set; }

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Intro { get; set; }

        public FoodInfo()
        {
            Cover = string.Empty;
            Id = -1;
            Name = string.Empty;
            Intro = string.Empty;
        }
    }


    [DataContract]
    public class TagsPageData
    {
        [DataMember(Name = "food")]
        public FoodInfo Food { get; set; }

        [DataMember(Name = "list")]
        public TagListItem[] Items { get; set; }

        public TagsPageData()
        {

        }
    }

}
