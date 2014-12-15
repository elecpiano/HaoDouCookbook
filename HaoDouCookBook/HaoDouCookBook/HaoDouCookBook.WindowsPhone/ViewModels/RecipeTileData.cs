﻿using Shared.Infrastructures;
using Shared.Utility;

namespace HaoDouCookBook.ViewModels
{
    public class RecipeTileData : BindableBase, ILoadMoreItem
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

        private int recipeId;

        public int RecipeId
        {
            get { return recipeId; }
            set { SetProperty<int>(ref recipeId, value); }
        }


        public RecipeTileData()
        {
            tagsText = string.Empty;
            recipeImage = string.Empty;
            recipeName = string.Empty;
            author = string.Empty;
            supportNumber = "0";
            recipeId = -1;
            recommendation = string.Empty;
            IsLoadMore = false;
        }

        public bool IsLoadMore { get; set; }
    }
}
