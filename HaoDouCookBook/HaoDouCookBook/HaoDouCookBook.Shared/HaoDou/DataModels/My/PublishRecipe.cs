using System.Runtime.Serialization;

namespace HaoDouCookBook.HaoDou.DataModels.My
{

    [DataContract]
    public class SaveInfo
    {
        [DataMember]
        public int RecipeId { get; set; }


        [DataMember(Name = "errormsg")]
        public string Message { get; set; }

        public SaveInfo()
        {
            RecipeId = 0;
            Message = string.Empty;
        }
    }

}
