using System.Runtime.Serialization;

namespace HaoDouCookBook.HaoDou.DataModels.My
{


    [DataContract]
    public class NoticPageData
    {
        [DataMember(Name = "list")]
        public Notice[] Notices { get; set; }

        public NoticPageData()
        {

        }
    }


    [DataContract]
    public class Notice
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int Type { get; set; }

        [DataMember]
        public int Status { get; set; }

        [DataMember]
        public int Uid { get; set; }

        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public string Avatar { get; set; }

        [DataMember]
        public string Time { get; set; }

        [DataMember]
        public NoticeContent Content { get; set; }

        public Notice()
        {
            Id = 0;
            Type = -1;
            Status = -1;
            Uid = 0;
            UserName = string.Empty;
            Avatar = string.Empty;
            Time = string.Empty;
        }
    }


    [DataContract]
    public class NoticeContent
    {

        [DataMember]
        public string Rid { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Cid { get; set; }

        [DataMember]
        public string Comment { get; set; }

        public NoticeContent()
        {
            Rid = string.Empty;
            Name = string.Empty;
            Cid = string.Empty;
            Comment = string.Empty;
        }
    }




    [DataContract]
    public class NoticeInfoData
    {

        [DataMember(Name = "info")]
        public NoticeInfo Info { get; set; }

        public NoticeInfoData()
        {

        }
    }


    [DataContract]
    public class NoticeInfo
    {
        [DataMember]
        public int NoticeCnt { get; set; }

        [DataMember]
        public int MessageCnt { get; set; }

        [DataMember]
        public int FriendCnt { get; set; }

        [DataMember]
        public int FriendListVer { get; set; }

        [DataMember]
        public int FavoVer { get; set; }

        public NoticeInfo()
        {
            NoticeCnt = 0;
            MessageCnt = 0;
            FriendCnt = 0;
            FriendListVer = 0;
            FavoVer = 0;
        }
    }

}
