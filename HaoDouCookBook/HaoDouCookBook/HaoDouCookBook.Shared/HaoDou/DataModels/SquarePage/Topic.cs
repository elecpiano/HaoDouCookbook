using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace HaoDouCookBook.HaoDou.DataModels.SquarePage
{
    [DataContract]
    public class Topic
    {
        [DataMember]
        public string PreviewContent { get; set; }

        [DataMember]
        public string ImageUrl { get; set; }

        [DataMember]
        public string TopicId { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Url { get; set; }

        [DataMember]
        public string LastPostTime { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string UserName { get; set; }

        public Topic()
        {
            PreviewContent = string.Empty;
            ImageUrl = string.Empty;
            TopicId = "-1";
            Name = string.Empty;
            Url = string.Empty;
            LastPostTime = string.Empty;
            Title = string.Empty;
            UserName = string.Empty;
        }
    }


    [DataContract]
    public class TopicCollection
    {
        [DataMember(Name = "list")]
        public Topic[] Items { get; set; }

        public TopicCollection()
        {
                
        }
    }
}
