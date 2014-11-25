using System.Runtime.Serialization;

namespace HaoDouCookBook.HaoDou.DataModels.Discovery
{
    [DataContract]
    public class Actor
    {
        [DataMember]
        public string ItemType { get; set; }
        
        [DataMember]
        public string ActImg { get; set; }

        [DataMember]
        public int Type { get; set; }

        [DataMember]
        public string OpenUrl { get; set; }

        public Actor()
        {
            ItemType = string.Empty;
            ActImg = string.Empty;
            Type = -1;
            OpenUrl = string.Empty;
        }
    }
}
