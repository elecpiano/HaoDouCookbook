using System.Runtime.Serialization;

namespace HaoDouCookBook.HaoDou.DataModels.ChoicenessPage
{
    [DataContract]
    public class RankListItem
    {
        [DataMember]
        public string Id { get; set; }

        [DataMember]
        public int RankType { get; set; }

        [DataMember]
        public string Cover { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string Intro { get; set; }

        public RankListItem()
        {
            Id = "-1";
            RankType = -1;
            Cover = string.Empty;
            Title = string.Empty;
            Intro = string.Empty;
        }
    }

    [DataContract]
    public class RankPageData
    {
        [DataMember(Name = "list")]
        public RankListItem[] Items { get; set; }

        public RankPageData()
        {

        }
    }
}
