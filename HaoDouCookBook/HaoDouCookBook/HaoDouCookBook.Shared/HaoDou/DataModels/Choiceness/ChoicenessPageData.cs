using HaoDouCookBook.HaoDou.DataModels.Choiceness;
using System.Runtime.Serialization;

namespace HaoDouCookBook.HaoDou.DataModels.ChoicenessPage
{
    [DataContract]
    public class ChoicenessPageData 
    {
        [DataMember(Name = "ad")]
        public ADItem[] ADs { get; set; }

        [DataMember(Name = "recipe")]
        public RecipeItem[] Recipes { get; set; }

        [DataMember(Name = "album")]
        public AlbumItem[] Album { get; set; }

        [DataMember(Name="wiki")]
        public WikiOrTableItem[] RecipesWiki { get; set; }

        [DataMember(Name = "table")]
        public WikiOrTableItem[] RecipeTables { get; set; }

        [DataMember(Name = "rank")]
        public RankItem[] Rank { get; set; }

        [DataMember(Name = "tag")]
        public TagCategoryItem[] Tags { get; set; }

        public ChoicenessPageData()
        {
        
        }
    }
}
