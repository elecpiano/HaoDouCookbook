using HaoDouCookBook.HaoDou.DataModels.ChoicenessPage;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace HaoDouCookBook.HaoDou.DataModels.Discovery
{
    [DataContract]
    public class CommentListPageData
    {
        [DataMember(Name = "info")]
        public CommentListPageInfo Info { get; set; }

        [DataMember(Name = "count")]
        public int Count { get; set; }

        [DataMember(Name = "list")]
        public CommentListPageComment[] Comments { get; set; }
    }

    [DataContract]
    public class CommentListPageInfo
    {
        [DataMember]
        public int UserId { get; set; }

        [DataMember]
        public string Img { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public int Type { get; set; }

        public CommentListPageInfo()
        {
            UserId = -1;
            Img = string.Empty;
            Title = string.Empty;
            Type = -1;
        }
    }

    [DataContract]
    public class CommentListPageComment : Comment
    {
        [DataMember]
        public int Cid { get; set; }

        [DataMember]
        public int PhotoFlag { get; set; }

        [DataMember]
        public int Photold { get; set; }

        [DataMember]
        public string PhotoUrl { get; set; }

        public CommentListPageComment()
        {
            Cid = -1;
            Photold = -1;
            PhotoUrl = string.Empty;
            PhotoFlag = -1;
        }
    }
}
