using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace HaoDouCookBook.HaoDou.DataModels
{
    [DataContract]
    public class ADItem
    {
        [DataMember]
        public string Cover { get; set; }

        [DataMember]
        public string Url { get; set; }

        [DataMember]
        public string Type { get; set; }

        [DataMember]
        public int SortIndex { get; set; }

        public ADItem()
        {
            Cover = string.Empty;
            Url = string.Empty;
            Type = string.Empty;
            SortIndex = -1;
        }
    }
} 