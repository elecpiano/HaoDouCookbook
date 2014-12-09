using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace HaoDouCookBook.HaoDou.DataModels.My
{

    [DataContract]
    public class FriendsData
    {
        [DataMember(Name = "fanscount")]
        public int FansCount { get; set; }

        [DataMember(Name = "allfanscnt")]
        public int AllFansCount { get; set; }

        [DataMember(Name = "fansinfo")]
        public FansInfo Info { get; set; }

        [DataMember(Name = "list")]
        public FriendsNameCategory[] FriendsNameCategories { get; set; }

        public FriendsData()
        {
            FansCount = 0;
            AllFansCount = 0;
            Info = new FansInfo();
        }
    }


    [DataContract]
    public class FansInfo
    {
        [DataMember]
        public int UserId { get; set; }

        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public string Avatar { get; set; }

        [DataMember]
        public int Vip { get; set; }

        public FansInfo()
        {
            UserId = 0;
            UserName = string.Empty;
            Avatar = string.Empty;
            Vip = 0;
        }
    }


    [DataContract]
    public class FriendsNameCategory
    {
        [DataMember]
        public string FirstWord { get; set; }


        [DataMember(Name = "data")]
        public Friend[] Friends { get; set; }

        public FriendsNameCategory()
        {

        }
    }


    [DataContract]
    public class Friend
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
        public string VipDesc { get; set; }

        public Friend()
        {
            UserId = 0;
            UserName = string.Empty;
            Avatar = string.Empty;
            Vip = 0;
            VipDesc = string.Empty;

        }
    }



}
