using System.Runtime.Serialization;

namespace HaoDouCookBook.HaoDou.DataModels.DiscoveryPage
{
    [DataContract]
    public class Cate
    {
        [DataMember]
        public string ItemType { get; set; }
        
        [DataMember]
        public int TopicId { get; set; }

        [DataMember]
        public string TopicName { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string OpenUrl { get; set; }

        [DataMember(Name = "List")]
        public CateItem[] Items { get; set; }

        public Cate()
        {
            ItemType = string.Empty;
            TopicId = -1;
            Title = string.Empty;
            OpenUrl = string.Empty;
        }
    }

    [DataContract]
    public class CateItem
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public string  Cover { get; set; }

        [DataMember]
        public string OpenUrl { get; set; }

        [DataMember]
        public int Count { get; set; }

        public CateItem()
        {
            Id = -1;
            UserName = string.Empty;
            Cover = string.Empty;
            OpenUrl = string.Empty;
            Count = 0;
        }
    }
}
