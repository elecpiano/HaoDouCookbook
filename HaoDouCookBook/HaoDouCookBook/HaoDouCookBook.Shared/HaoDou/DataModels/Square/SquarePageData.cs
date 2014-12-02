using System.Runtime.Serialization;

namespace HaoDouCookBook.HaoDou.DataModels.Square
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
