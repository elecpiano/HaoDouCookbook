using System.Runtime.Serialization;

namespace HaoDouCookBook.HaoDou.DataModels.My
{

    [DataContract]
    public class DiggUserData
    {

        [DataMember(Name = "list")]
        public DiggUser[] DiggUsers { get; set; }
        
        public DiggUserData()
        {

        }
    }


    [DataContract]
    public class DiggUser
    {

        [DataMember]
        public string UserId { get; set; }

        [DataMember]
        public string Avatar { get; set; }

        [DataMember]
        public string Nick { get; set; }

        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public int Vip { get; set; }

        [DataMember]
        public string Intro { get; set; }

        public DiggUser()
        {
            UserId = "0";
            Avatar = string.Empty;
            Nick = string.Empty;
            UserName = string.Empty;
            Vip = 0;
            Intro = string.Empty;
        }
    }

}
