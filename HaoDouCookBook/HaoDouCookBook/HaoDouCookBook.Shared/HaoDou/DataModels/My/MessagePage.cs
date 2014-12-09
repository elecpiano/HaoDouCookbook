using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace HaoDouCookBook.HaoDou.DataModels.My
{

    [DataContract]
    public class MessagePageData
    {

        [DataMember(Name = "list")]
        public MessageItem[] Messages { get; set; }

        [DataMember(Name = "notice")]
        public MessagePageNoticeInfo Notice { get; set; }

        public MessagePageData()
        {

        }
    }


    [DataContract]
    public class MessageItem
    {

        [DataMember]
        public int MessageId { get; set; }

        [DataMember]
        public int UserId { get; set; }

        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public int LastUserId { get; set; }

        [DataMember]
        public int ContactId { get; set; }

        [DataMember]
        public string ContactName { get; set; }

        [DataMember]
        public string Avatar { get; set; }

        [DataMember]
        public string LastMsg { get; set; }

        [DataMember]
        public int MsgCount { get; set; }

        [DataMember]
        public int UnreadCount { get; set; }

        [DataMember]
        public int IsRead { get; set; }

        [DataMember]
        public string UpdateTime { get; set; }

        public MessageItem()
        {
            MessageId = 0;
            UserId = 0;
            UserName = string.Empty;
            LastUserId = 0;
            ContactId = 0;
            ContactName = string.Empty;
            Avatar = string.Empty;
            LastMsg = string.Empty;
            MsgCount = 0;
            UnreadCount = 0;
            IsRead = 0;
            UpdateTime = string.Empty;
        }
    }


    [DataContract]
    public class MessagePageNoticeInfo
    {

        [DataMember]
        public string Id { get; set; }

        [DataMember]
        public string SubType { get; set; }

        [DataMember]
        public string Content { get; set; }

        [DataMember]
        public int UnreadCount { get; set; }

        [DataMember]
        public string Uid { get; set; }

        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public string Avatar { get; set; }

        public MessagePageNoticeInfo()
        {
            Id = string.Empty;
            SubType = string.Empty;
            Content = string.Empty;
            UnreadCount = 0;
            Uid = string.Empty;
            UserName = string.Empty;
            Avatar = string.Empty;
        }
    }

}
