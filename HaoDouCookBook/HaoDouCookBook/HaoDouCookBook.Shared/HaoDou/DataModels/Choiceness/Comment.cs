using System.Runtime.Serialization;

namespace HaoDouCookBook.HaoDou.DataModels.ChoicenessPage
{
    [DataContract]
    public class Comment
    {
        [DataMember]
        public int UserId { get; set; }

        [DataMember]
        public string Avatar { get; set; }

        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public int AtUserId { get; set; }

        [DataMember]
        public string AtUserName { get; set; }

        [DataMember]
        public string Content { get; set; }

        [DataMember]
        public string CreateTime { get; set; }

        public Comment()
        {
            UserId = - 1;
            AtUserId = -1;
            UserName = string.Empty;
            AtUserName = string.Empty;
            Content = string.Empty;
            CreateTime = string.Empty;
            Avatar = string.Empty;
        }

    }
}
