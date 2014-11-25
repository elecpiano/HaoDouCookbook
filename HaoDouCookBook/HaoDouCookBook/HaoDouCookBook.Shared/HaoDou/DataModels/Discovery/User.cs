using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace HaoDouCookBook.HaoDou.DataModels.Discovery
{
    [DataContract]
    public class User
    {
        [DataMember]
        public int UserId { get; set; }

        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public string Avatar { get; set; }

        [DataMember]
        public string OpenUrl { get; set; }

        public User()
        {
            UserId = -1;
            UserName = string.Empty;
            Avatar = string.Empty;
            OpenUrl = string.Empty;
        }
    }

    [DataContract]
    public class StarredUser
    {
        [DataMember]
        public string ItemType { get; set; }

        [DataMember(Name = "List")]
        public User[] Users { get; set; }

        public StarredUser()
        {
            ItemType = string.Empty;
        }
    }
}
