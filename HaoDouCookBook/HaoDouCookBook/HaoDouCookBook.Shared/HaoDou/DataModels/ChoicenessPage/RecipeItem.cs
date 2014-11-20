﻿using System.Runtime.Serialization;

namespace HaoDouCookBook.HaoDou.DataModels
{
    [DataContract]
    public class RecipeItem
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string RTitle { get; set; }

        [DataMember]
        public string Intro { get; set; }

        [DataMember]
        public string Cover { get; set; }

        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public string Desc { get; set; }

        [DataMember]
        public string Url { get; set; }

        [DataMember]
        public string Icon { get; set; }

        [DataMember]
        public string Type { get; set; }

        [DataMember]
        public string KeyWord { get; set; }

        [DataMember]
        public int SortIndex { get; set; }

        public RecipeItem()
        {
            Id = -1;
            Title = string.Empty;
            RTitle = string.Empty;
            Intro = string.Empty;
            Cover = string.Empty;
            UserName = string.Empty;
            Desc = string.Empty;
            Url = string.Empty;
            Icon = string.Empty;
            Type = string.Empty;
            KeyWord = string.Empty;
            SortIndex = -1;
        }
    }
}
