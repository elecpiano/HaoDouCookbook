using System.Runtime.Serialization;

namespace HaoDouCookBook.HaoDou.DataModels.Choiceness
{
    [DataContract]
    public class StuffData
    {
        [DataMember(Name = "info")]
        public StuffInfo Info { get; set; }

        public StuffData()
        { 

        }
    }

    [DataContract]
    public class StuffInfo
    {
        [DataMember]
        public int TagId { get; set; }

        [DataMember]
        public string Intro { get; set; }

        [DataMember]
        public string Cover { get; set; }

        [DataMember]
        public string Effect { get; set; }

        [DataMember]
        public string Pick { get; set; }

        [DataMember]
        public string Skill { get; set; }

        [DataMember]
        public string Storage { get; set; }

        [DataMember]
        public StuffRecipe[] RecipeList { get; set; }

        public StuffInfo()
        {
            TagId = -1;
            Intro = string.Empty;
            Cover = string.Empty;
            Effect = string.Empty;
            Pick = string.Empty;
            Skill = string.Empty;
            Storage = string.Empty;
        }
    }

    [DataContract]
    public class StuffRecipe
    {
        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string Cover { get; set; }

        [DataMember]
        public int RecipeId { get; set; }

        public StuffRecipe()
        {
            Title = string.Empty;
            Cover = string.Empty;
            RecipeId = -1;
        }

    }
}
