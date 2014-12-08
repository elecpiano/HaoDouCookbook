using System.Runtime.Serialization;

namespace HaoDouCookBook.HaoDou.DataModels.My
{

    [DataContract]
    public class UserActivitiesData
    {
        [DataMember(Name = "list")]
        public Activity[] Activities { get; set; }

        public UserActivitiesData()
        {

        }
    }



    [DataContract]
    public class Activity
    {

        [DataMember]
        public int FeedId { get; set; }

        [DataMember]
        public int UserId { get; set; }

        [DataMember]
        public string Avatar { get; set; }

        [DataMember]
        public string Nick { get; set; }

        [DataMember]
        public string CreateTime { get; set; }

        [DataMember]
        public string FormatTime { get; set; }

        [DataMember]
        public int ItemId { get; set; }

        [DataMember]
        public string TopicUrl { get; set; }

        [DataMember]
        public int TopicType { get; set; }

        [DataMember]
        public string Pic { get; set; }

        [DataMember]
        public string Content { get; set; }

        [DataMember]
        public int DiggCnt { get; set; }

        [DataMember]
        public int IsDigg { get; set; }

        [DataMember]
        public int FromUserId { get; set; }

        [DataMember]
        public string FromUserName { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int Type { get; set; }

        public Activity()
        {

        }
    }

}
