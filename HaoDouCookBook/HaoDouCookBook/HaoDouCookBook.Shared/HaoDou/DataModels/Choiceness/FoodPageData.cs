using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace HaoDouCookBook.HaoDou.DataModels.Choiceness
{
    [DataContract]
    public class FoodPageData
    {
        [DataMember(Name = "info")]
        public FoodPageInfo Info { get; set; }

        FoodPageData()
        { 

        }
    }

    [DataContract]
    public class FoodPageInfo
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
        public FoodPageRecipe[] RecipeList { get; set; }

    }

    [DataContract]
    public class FoodPageRecipe
    {
        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string Cover { get; set; }

        [DataMember]
        public int RecipeId { get; set; }

        public FoodPageRecipe()
        {
            Title = string.Empty;
            Cover = string.Empty;
            RecipeId = -1;
        }

    }
}
