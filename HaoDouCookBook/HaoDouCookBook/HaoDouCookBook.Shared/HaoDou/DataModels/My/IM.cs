using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace HaoDouCookBook.HaoDou.DataModels.My
{

    [DataContract]
    public class IMMessageList
    {

        [DataMember(Name = "list")]
        public IMMessage[] Messages { get; set; }

        public IMMessageList()
        {

        }
    }


    [DataContract]
    public class IMMessage
    {

        [DataMember]
        public string ReplayId { get; set; }

        [DataMember]
        public string UserId { get; set; }

        [DataMember]
        public string Avatar { get; set; }

        [DataMember]
        public string ParentId { get; set; }

        [DataMember]
        public string Msg { get; set; }

        [DataMember]
        public string CreateTime { get; set; }

        public IMMessage()
        {
            ReplayId = string.Empty;
            UserId = string.Empty;
            Avatar = string.Empty;
            ParentId = string.Empty;
            Msg = string.Empty;
            CreateTime = string.Empty;
        }
    }

}
