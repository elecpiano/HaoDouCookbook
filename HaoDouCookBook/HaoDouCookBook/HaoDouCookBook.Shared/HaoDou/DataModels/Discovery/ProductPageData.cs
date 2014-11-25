using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace HaoDouCookBook.HaoDou.DataModels.Discovery
{
    public class ProductPageData
    {
    }


    [DataContract]
    public class ProductPageInfo
    {
        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string Content { get; set; }

        [DataMember]
        public string Cover { get; set; }

        [DataMember]
        public string Date { get; set; }
        
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int Count { get; set; }

        public ProductPageInfo()
        {
            Title = string.Empty;
            Content = string.Empty;
            Date = string.Empty;
            Id = -1;
            Count = -1;
        }
    }

    [DataContract]
    public class ProductPageRecipe
    {
        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public int Pid { get; set; }

        [DataMember]
        public int Rid { get; set; }

        [DataMember]
        public string RTitle { get; set; }

        [DataMember]
        public int TopicId { get; set; }

        [DataMember]
        public string TopicName { get; set; }

        [DataMember]
        public string PhotoUrl { get; set; }

        [DataMember]
        public int UserId { get; set; }

        [DataMember]
        public int Digg { get; set; }

        [DataMember]
        public string Avatar { get; set; }

        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public string CreateTime { get; set; }

        [DataMember]
        public int IsDigg { get; set; }

        [DataMember]
        public string TimeStr { get; set; }

        [DataMember]
        public string Position { get; set; }

        [DataMember]
        public string Intro { get; set; }

        [DataMember]
        public int CommentCount { get; set; }

        [DataMember]
        public ProductPageComment[] Comment { get; set; }

        [DataMember]
        public ProdcutPageAtUser AtUser { get; set; }

        public ProductPageRecipe()
        {
            Title = string.Empty;
            RTitle = string.Empty;
            Pid = -1;
            Rid = -1;
            TopicId = -1;
            TopicName = string.Empty;
            PhotoUrl = string.Empty;
            UserId = -1;
            CreateTime = string.Empty;
            IsDigg = 0;
            TimeStr = string.Empty;
            Position = string.Empty;
            Intro = string.Empty;
            CommentCount = 0;
        }

    }

    [DataContract]
    public class ProdcutPageAtUser
    {
        public ProdcutPageAtUser()
        {

        }
    }

    [DataContract]
    public class ProductPageComment
    {
        [DataMember]
        public string UserId { get; set; }
        
        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public string Content { get; set; }

        public ProductPageComment()
        {
            UserId = "-1";
            UserName = string.Empty;
            Content = string.Empty;
        }

    }
}
