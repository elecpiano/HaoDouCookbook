using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace HaoDouCookBook.HaoDou.DataModels.Choiceness
{
    [DataContract]
    public class SearchPageData
    {
        [DataMember(Name = "list")]
        public string[] HotSearchKeywords { get; set; }

        public SearchPageData()
        {

        }
    }
}
