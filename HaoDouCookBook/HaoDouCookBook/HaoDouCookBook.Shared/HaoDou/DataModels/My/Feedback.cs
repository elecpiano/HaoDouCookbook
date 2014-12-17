using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace HaoDouCookBook.HaoDou.DataModels.My
{


    [DataContract]
    public class FeedbackList
    {

        [DataMember(Name = "list")]
        public FeedbackItem[] Feedbacks { get; set; }

        public FeedbackList()
        {

        }
    }


    [DataContract]
    public class FeedbackItem
    {
        [DataMember]
        public string Id { get; set; }

        [DataMember]
        public string Status { get; set; }

        [DataMember]
        public string Content { get; set; }

        [DataMember]
        public string DelStatus { get; set; }

        [DataMember]
        public string CreateTime { get; set; }

        [DataMember]
        public ReplyInfo[] ReplayInfo { get; set; }

        public FeedbackItem()
        {
            Id = string.Empty;
            Status = string.Empty;
            Content = string.Empty;
            DelStatus = string.Empty;
            CreateTime = string.Empty;
        }
    }



    [DataContract]
    public class ReplyInfo
    {
        public ReplyInfo()
        {

        }
    }


}
