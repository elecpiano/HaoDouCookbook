using System.Runtime.Serialization;

namespace HaoDouCookBook.HaoDou.DataModels.Choiceness
{
    [DataContract]
    public class ADItem
    {
        [DataMember]
        public string Cover { get; set; }

        [DataMember]
        public string Url { get; set; }

        [DataMember]
        public string Type { get; set; }

        [DataMember]
        public int SortIndex { get; set; }

        public ADItem()
        {
            Cover = string.Empty;
            Url = string.Empty;
            Type = string.Empty;
            SortIndex = -1;
        }
    }
} 