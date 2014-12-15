using Shared.Infrastructures;
using Shared.Utility;

namespace HaoDouCookBook.ViewModels
{
    public class TagRecipeData : BindableBase, ILoadMoreItem
    {
        private string recipeName;

        public string RecipeName
        {
            get { return recipeName; }
            set { SetProperty<string>(ref recipeName, value); }
        }

        private int likeNumber;

        public int LikeNumber
        {
            get { return likeNumber; }
            set { SetProperty<int>(ref likeNumber, value); }
        }

        private int viewNumber;

        public int ViewNumber
        {
            get { return viewNumber; }
            set { SetProperty<int>(ref viewNumber, value); }
        }

        private string foodStuff;

        public string FoodStuff
        {
            get { return foodStuff; }
            set { SetProperty<string>(ref foodStuff, value); }
        }

        private string previewImageSource;

        public string PreviewImageSource
        {
            get { return previewImageSource; }
            set { SetProperty<string>(ref previewImageSource, value); }
        }

        private int recipeId;

        public int RecipeId
        {
            get { return recipeId; }
            set { SetProperty<int>(ref recipeId, value); }
        }

        private string card;

        public string Card
        {
            get { return card; }
            set { SetProperty<string>(ref card, value); }
        }

        public TagRecipeData()
        {
            recipeName = string.Empty;
            previewImageSource = string.Empty;
            likeNumber = 0;
            viewNumber = 0;
            foodStuff = string.Empty;
            recipeId = -1;
            IsLoadMore = false;
        }

        public bool IsLoadMore { get; set; }
    }
}
