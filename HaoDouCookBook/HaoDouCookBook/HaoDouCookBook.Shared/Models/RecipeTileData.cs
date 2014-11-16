using Shared.Infrastructures;
using System;
using System.Collections.Generic;
using System.Text;

namespace HaoDouCookBook.Models
{
    public class RecipeTileData : BindableBase
    {
        private string tagsText;

        public string TagsText
        {
            get { return tagsText; }
            set { SetProperty<string>(ref tagsText, value); }
        }

        private string recipeName;

        public string RecipeName
        {
            get { return recipeName; }
            set { SetProperty<string>(ref recipeName, value); }
        }

        private string author;

        public string Author
        {
            get { return author; }
            set { SetProperty<string>(ref author, value); }
        }

        private string supportNumber;

        public string SupportNumber
        {
            get { return supportNumber; }
            set { SetProperty<string>(ref supportNumber, value); }
        }

        private string recommendation;

        public string Recommendation
        {
            get { return recommendation; }
            set { SetProperty<string>(ref recommendation, value); }
        }

        private string recipeImage;

        public string RecipeImage
        {
            get { return recipeImage; }
            set { SetProperty<string>(ref recipeImage, value); }
        }

        public RecipeTileData()
        {
            tagsText = string.Empty;
            recipeImage = string.Empty;
            recipeName = string.Empty;
            author = string.Empty;
            supportNumber = "0";
            recommendation = string.Empty;
        }
        
    }
}
