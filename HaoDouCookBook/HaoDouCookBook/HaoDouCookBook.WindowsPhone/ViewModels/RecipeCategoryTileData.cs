using Shared.Infrastructures;

namespace HaoDouCookBook.ViewModels
{
    public class RecipeCategoryTileData : BindableBase
    {
        private int id;

        public int Id
        {
            get { return id; }
            set { SetProperty<int>(ref id, value); }
        }


        private string markText;

        public string MarkText
        {
            get { return markText; }
            set { SetProperty<string>(ref markText, value); }
        }

        private string titleIcon;

        public string TitleIcon
        {
            get { return titleIcon; }
            set { SetProperty<string>(ref titleIcon, value); }
        }

        private string title;

        public string Title
        {
            get { return title; }
            set { SetProperty<string>(ref title, value); }
        }

        private string description;

        public string Description
        {
            get { return description; }
            set { SetProperty<string>(ref description, value); }
        }

        private string recipeName;

        public string RecipeName
        {
            get { return recipeName; }
            set { SetProperty<string>(ref recipeName, value); }
        }

        private string authorRecipeComment;

        public string AuthorRecipeComment
        {
            get { return authorRecipeComment; }
            set { SetProperty<string>(ref authorRecipeComment, value); }
        }

        private string recipeDescriptionOnImage;

        public string RecipeDescriptionOnImage
        {
            get { return recipeDescriptionOnImage; }
            set { SetProperty<string>(ref recipeDescriptionOnImage, value); }
        }

        private string author;

        public string Author
        {
            get { return author; }
            set { SetProperty<string>(ref author, value); }
        }

        private string tileImage;

        public string TileImage
        {
            get { return tileImage; }
            set { SetProperty<string>(ref tileImage, value); }
        }

        private string url;

        public string Url
        {
            get { return url; }
            set { SetProperty<string>(ref url, value); }
        }

        public RecipeCategoryTileData()
        {
            markText = string.Empty;
            titleIcon = string.Empty;
            title = string.Empty;
            description = string.Empty;
            recipeName = string.Empty;
            authorRecipeComment = string.Empty;
            recipeDescriptionOnImage = string.Empty;
            author = string.Empty;
            tileImage = string.Empty;
            id = -1;
        }

    }
}
