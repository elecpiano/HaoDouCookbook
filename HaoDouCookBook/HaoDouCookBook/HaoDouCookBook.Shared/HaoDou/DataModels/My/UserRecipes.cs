using System.Runtime.Serialization;

namespace HaoDouCookBook.HaoDou.DataModels.My
{


    [DataContract]
    public class UserRecipesData
    {
        [DataMember(Name = "list")]
        public UserRecipeItem[] Recipes { get; set; }

        public UserRecipesData()
        {

        }
    }


    [DataContract]
    public class UserRecipeItem
    {
        [DataMember]
        public int Rid { get; set; }

        [DataMember]
        public int Status { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string Cover { get; set; }

        [DataMember]
        public int Record { get; set; }

        public UserRecipeItem()
        {
            Rid = 0;
            Status = 0;
            Title = string.Empty;
            Cover = string.Empty;
            Record = 0;
        }
    }

}
