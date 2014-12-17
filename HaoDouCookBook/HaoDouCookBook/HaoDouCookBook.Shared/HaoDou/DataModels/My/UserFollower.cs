using System.Runtime.Serialization;

namespace HaoDouCookBook.HaoDou.DataModels.My
{


    [DataContract]
    public class UserFollowersData
    {
        [DataMember(Name = "list")]
        public Follower[] Followers { get; set; }

        public UserFollowersData()
        {
            
        }
    }


    [DataContract]
    public class Follower
    {

        [DataMember]
        public int UserId { get; set; }

        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public string Avatar { get; set; }

        [DataMember]
        public int Vip { get; set; }

        [DataMember]
        public int CanFollow { get; set; }

        public Follower()
        {
            UserId = 0;
            UserName = string.Empty;
            Avatar = string.Empty;
            Vip = 0;
            CanFollow = 1;
        }
    }

}
