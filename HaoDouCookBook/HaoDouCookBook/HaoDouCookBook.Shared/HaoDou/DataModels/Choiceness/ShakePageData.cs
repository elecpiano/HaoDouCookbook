using HaoDouCookBook.HaoDou.DataModels.ChoicenessPage;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace HaoDouCookBook.HaoDou.DataModels.Choiceness
{

    [DataContract]
    public class ShakePageData
    {

        [DataMember(Name = "list")]
        public TagListItem[] Items { get; set; }

        public ShakePageData()
        {

        }
    }
 
}
