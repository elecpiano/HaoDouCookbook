using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Shared.Infrastructures;
using Windows.Data.Json;
using Shared.Utility;

namespace HaoDouCookBook.HaoDou.DataModels.My
{

    [DataContract]
    public class MessagePageData : CustomJsonSerializableBase
    {

        [DataMember(Name = "list")]
        public List<MessageItem> Messages { get; set; }

        [DataMember(Name = "notice")]
        public List<MessagePageNoticeInfo> Notices { get; set; }

        public MessagePageData()
        {
            Messages = new List<MessageItem>();
            Notices = new List<MessagePageNoticeInfo>();
        }

        public override bool Deserialize(string json)
        {
            try
            {
                Messages.Clear();
                JsonObject jsonObj = JsonObject.Parse(json);
                JsonArray messageList = jsonObj.GetNamedArray("list");

                for (int i = 0; i < messageList.Count; i++)
                {
                    var item = JsonSerializer.Deserialize<MessageItem>(messageList[i].Stringify());
                    Messages.Add(item);
                }

                try
                {
                    JsonArray noticeArray = jsonObj.GetNamedArray("notice");
                    for (int i = 0; i < noticeArray.Count; i++)
                    {
                        var noticeObject = JsonSerializer.Deserialize<MessagePageNoticeInfo>(noticeArray[i].Stringify());
                        Notices.Add(noticeObject);
                    }
                }
                catch
                {
                    JsonObject notice = jsonObj["notice"].GetObject();
                    var noticeObject = JsonSerializer.Deserialize<MessagePageNoticeInfo>(notice.Stringify());
                    Notices.Add(noticeObject);
                }

                return true;

            }
            catch 
            {
                return false;
            }
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
