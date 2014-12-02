using System.Runtime.Serialization;

namespace HaoDouCookBook.HaoDou.DataModels.Square
{
    [DataContract]
    public class CateInfo
    {
        [DataMember]
        public string CateId { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string ImageUrl { get; set; }

        public CateInfo()
        {
            CateId = "-1";
            Name = string.Empty;
            ImageUrl = string.Empty;
        }
    }
}
