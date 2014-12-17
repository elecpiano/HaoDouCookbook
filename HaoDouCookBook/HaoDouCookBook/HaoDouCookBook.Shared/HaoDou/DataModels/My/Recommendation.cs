using System.Runtime.Serialization;

namespace HaoDouCookBook.HaoDou.DataModels.My
{


    [DataContract]
    public class RecommendationPageData
    {
        [DataMember(Name = "list")]
        public RecommendationItem[] Items { get; set; }

        public RecommendationPageData()
        {

        }
    }


    [DataContract]
    public class RecommendationItem
    {
        [DataMember]
        public string Title { get; set; }


        [DataMember]
        public string Desc { get; set; }


        [DataMember]
        public string Url { get; set; }


        [DataMember]
        public string Image { get; set; }

        public RecommendationItem()
        {
            Title = string.Empty;
            Desc = string.Empty;
            Url = string.Empty;
            Image = string.Empty;
        }
    }

}
