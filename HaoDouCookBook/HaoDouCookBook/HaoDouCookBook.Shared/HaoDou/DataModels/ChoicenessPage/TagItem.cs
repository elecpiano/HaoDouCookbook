using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace HaoDouCookBook.HaoDou.DataModels.ChoicenessPage
{
    [DataContract]
    public class TagItem
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string  Url { get; set; }

        [DataMember]
        public string Type { get; set; }

        [DataMember]
        public string Icon { get; set; }

        [DataMember]
        public int SortIndex { get; set; }

        public TagItem()
        {
            Id = -1;
            Title = string.Empty;
            Url = string.Empty;
            Type = string.Empty;
            Icon = string.Empty;
            SortIndex = -1;
        }

    }
}
