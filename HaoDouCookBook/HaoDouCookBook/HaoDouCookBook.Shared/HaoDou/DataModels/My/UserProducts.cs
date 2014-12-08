using System.Runtime.Serialization;

namespace HaoDouCookBook.HaoDou.DataModels.My
{


    [DataContract]
    public class UserProductsData
    {
        [DataMember(Name = "list")]
        public UserProductItem[] Products { get; set; }

        public UserProductsData()
        {


        }
    }


    [DataContract]
    public class UserProductItem
    {
        [DataMember]
        public int RecipeId { get; set; }

        [DataMember]
        public int RecipePhotoId { get; set; }

        [DataMember]
        public string PhotoUrl { get; set; }

        [DataMember]
        public string Comment { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string CreateTime { get; set; }
        
        public UserProductItem()
        {
            RecipeId = 0;
            RecipePhotoId = 0;
            PhotoUrl = string.Empty;
            Comment = string.Empty;
            Title = string.Empty;
            CreateTime = string.Empty;
        }
    }

}
