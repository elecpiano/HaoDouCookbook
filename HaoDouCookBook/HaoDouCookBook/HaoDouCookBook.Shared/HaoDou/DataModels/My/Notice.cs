using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace HaoDouCookBook.HaoDou.DataModels.My
{
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
