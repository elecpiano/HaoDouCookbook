using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace HaoDouCookBook.HaoDou.DataModels.SquarePage
{
    [DataContract]
    public class SquarePageData
    {
        [DataMember(Name = "list")]
        public Topic[] Topics { get; set; }

        [DataMember(Name = "cate_infos")]
        public CateInfo[] CateInfos { get; set; }

        public SquarePageData()
        {

        }
    }
}
