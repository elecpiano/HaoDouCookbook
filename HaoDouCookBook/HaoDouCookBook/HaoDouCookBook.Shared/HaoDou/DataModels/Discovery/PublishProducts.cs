using System.Runtime.Serialization;

namespace HaoDouCookBook.HaoDou.DataModels.Discovery
{


    [DataContract]
    public class UploadResultInfo
    {

        [DataMember]
        public int Pid { get; set; }

        [DataMember(Name = "errormsg")]
        public string Message { get; set; }
        public UploadResultInfo()
        {

        }
    }



    [DataContract]
    public class TopicTagsData
    {

        [DataMember(Name = "list")]
        public TopicTag[] Tags { get; set; }

        public TopicTagsData()
        {

        }
    }

    [DataContract]
    public class TopicTag
    {
        [DataMember]
        public string Id { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public int Select { get; set; }

        public TopicTag()
        {
            Id = "0";
            Title = string.Empty;
            Select = 0;
        }
    }

}
