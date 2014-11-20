using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace HaoDouCookBook.HaoDou.DataModels.SquarePage
{
    [DataContract]
    public class CateInfo
    {
        [DataMember]
        public string CateId { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string ImageUrl { get; set; }

        public CateInfo()
        {
            CateId = "-1";
            Name = string.Empty;
            ImageUrl = string.Empty;
        }
    }
}
