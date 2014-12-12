using System.Runtime.Serialization;

namespace HaoDouCookBook.HaoDou.DataModels.Choiceness
{

    [DataContract]
    public class AllAlbumsData
    {

        [DataMember(Name = "list")]
        public Album[] Albums { get; set;  }

        public AllAlbumsData()
        {

        }
    }


    [DataContract]
    public class Album
    {
        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string Cover { get; set; }

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int UserId { get; set; }

        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public string Avatar { get; set; }

        [DataMember]
        public int RecipeCount { get; set; }

        [DataMember]
        public string Content { get; set; }

        public Album()
        {
            Title = string.Empty;
            Cover = string.Empty;
            Id = 0;
            UserId = 0;
            UserName = string.Empty;
            Avatar = string.Empty;
            RecipeCount = 0;
            Content = string.Empty;
        }
    }

}
