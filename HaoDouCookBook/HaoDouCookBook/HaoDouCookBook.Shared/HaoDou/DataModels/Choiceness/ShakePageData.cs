using HaoDouCookBook.HaoDou.DataModels.ChoicenessPage;
using System.Runtime.Serialization;

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
