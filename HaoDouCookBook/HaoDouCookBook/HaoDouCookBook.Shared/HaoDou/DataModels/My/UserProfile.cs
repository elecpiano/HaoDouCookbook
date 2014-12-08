using System.Runtime.Serialization;

namespace HaoDouCookBook.HaoDou.DataModels.My
{

    [DataContract]
    public class UserProfile
    {

        [DataMember(Name = "info")]
        public UserSummaryInfo SummaryInfo { get; set; }

        public UserProfile()
        {

        }
    }


    [DataContract]
    public class UserSummaryInfo
    {

        [DataMember]
        public int IsModify { get; set; }


        [DataMember]
        public int UserId { get; set; }


        [DataMember]
        public string UserName { get; set; }


        [DataMember]
        public int Vip { get; set; }


        [DataMember]
        public string Avatar { get; set; }

        [DataMember]
        public string Intro { get; set; }

        [DataMember]
        public int RecipeCnt { get; set; }

        [DataMember]
        public int FollowCnt { get; set; }

        [DataMember]
        public int FansCount { get; set; }

        [DataMember]
        public int CanFollow { get; set; }

        [DataMember]
        public int Wealth { get; set; }

        [DataMember]
        public int Gender { get; set; }

        [DataMember]
        public int FavoriteCnt { get; set; }

        [DataMember]
        public int PhotoCnt { get; set; }

        [DataMember]
        public bool CheckIn { get; set; }

        [DataMember]
        public int NoticeCnt { get; set; }

        [DataMember]
        public int MessageCnt { get; set; }

        [DataMember]
        public Area Area { get; set; }

        public UserSummaryInfo()
        {
            IsModify = 0;
            UserId = 0;
            UserName = string.Empty;
            Vip = 0;
            Avatar = string.Empty;
            Intro = string.Empty;
            RecipeCnt = 0;
            FollowCnt = 0;
            CanFollow = 1;
            FansCount = 0;
            Wealth = 0;
            Gender = 0;
            CheckIn = false;
            PhotoCnt = 0;
            FavoriteCnt = 0;
            MessageCnt = 0;
        }
    }

    [DataContract]
    public class Area
    {

        [DataMember]
        public int ProviceId { get; set; }

        [DataMember]
        public string ProvinceName { get; set; }

        [DataMember]
        public int CityId { get; set; }

        [DataMember]
        public string CityName { get; set; }

        public Area()
        {
            ProviceId = 0;
            ProvinceName = string.Empty;
            CityId = 0;
            CityName = string.Empty;
        }
    }


}
