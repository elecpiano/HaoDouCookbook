using System.Runtime.Serialization;

namespace HaoDouCookBook.HaoDou.DataModels.ChoicenessPage
{
    [DataContract]
    public class RankItem
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string Cover { get; set; }

        [DataMember]
        public string Url { get; set; }

        [DataMember]
        public string Type { get; set; }

        [DataMember]
        public int RankType { get; set; }

        [DataMember]
        public string Intro { get; set; }

        [DataMember]
        public int SortIndex { get; set; }

        public RankItem()
        {
            Id = -1;
            Title = string.Empty;
            Cover = string.Empty;
            Url = string.Empty;
            Type = string.Empty;
            RankType = -1;
            Intro = string.Empty;
            SortIndex = -1;
        }
    }
}
