using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace HaoDouCookBook.HaoDou.DataModels
{
    public class AlbumItem
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string Intro { get; set; }

        [DataMember]
        public string Cover { get; set; }

        [DataMember]
        public string[] SmallCover { get; set; }

        [DataMember]
        public string Url { get; set; }

        [DataMember]
        public string UrlToList { get; set; }

        [DataMember]
        public string Icon { get; set; }

        [DataMember]
        public string Type { get; set; }

        [DataMember]
        public int SortIndex { get; set; }

        public AlbumItem()
        {
            Id = -1;
            Title = string.Empty;
            Intro = string.Empty;
            Cover = string.Empty;
            SmallCover = null;
            Url = string.Empty;
            UrlToList = string.Empty;
            Icon = string.Empty;
            Type = string.Empty;
            SortIndex = -1;
        }
    }
}
