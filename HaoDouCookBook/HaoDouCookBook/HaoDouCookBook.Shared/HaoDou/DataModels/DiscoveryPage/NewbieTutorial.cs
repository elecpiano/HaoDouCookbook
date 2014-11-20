using System.Runtime.Serialization;

namespace HaoDouCookBook.HaoDou.DataModels.DiscoveryPage
{
    [DataContract]
    public class NewbieTutorial
    {
        [DataMember]
        public string ItemType { get; set; }

        [DataMember]
        public int TopicId { get; set; }

        [DataMember]
        public string Title { get; set; }
        
        [DataMember]
        public int RecipeId { get; set; }

        [DataMember]
        public string RecipeCover { get; set; }
        
        [DataMember]
        public int UserId { get; set; }

        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public string UserAvatar { get; set; }

        [DataMember]
        public string Intro { get; set; }

        [DataMember]
        public string OpenUrl { get; set; }

        [DataMember(Name = "List")]
        public string[] SamllCovers { get; set; }

        public NewbieTutorial()
        {
            ItemType = string.Empty;
            TopicId = -1;
            Title = string.Empty;
            RecipeId = -1;
            RecipeCover = string.Empty;
            UserName = string.Empty;
            UserId = -1;
            UserAvatar = string.Empty;
            Intro = string.Empty;
            OpenUrl = string.Empty;
        }

    }
}
