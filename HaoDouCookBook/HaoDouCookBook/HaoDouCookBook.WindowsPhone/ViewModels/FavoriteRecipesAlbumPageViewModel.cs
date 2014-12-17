using Shared.Infrastructures;
using Shared.Utility;
using System.Collections.ObjectModel;

namespace HaoDouCookBook.ViewModels
{
    public class FavoriteRecipesAlbumPageViewModel : BindableBase
    {
        public ObservableCollection<FavoriteRecipe> Recipes { get; set; }

        public FavoriteRecipesAlbumPageViewModel()
        {
            Recipes = new ObservableCollection<FavoriteRecipe>();
        }
    }

    public class FavoriteRecipe : BindableBase, ILoadMoreItem
    {
        private int recipeId;

        public int RecipeId 
        {
            get { return recipeId; }
            set { SetProperty<int>(ref recipeId, value); }
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

        private string title;

        public string Title
        {
            get { return title; }
            set { SetProperty<string>(ref title, value); }
        }

        private string cover;

        public string Cover
        {
            get { return cover; }
            set { SetProperty<string>(ref cover, value); }
        }

        private string intro;

        public string Intro
        {
            get { return intro; }
            set { SetProperty<string>(ref intro, value); }
        }

        public bool IsLoadMore { get; set; }

        public FavoriteRecipe()
        {
            recipeId = 0;
            likeNumber = 0;
            viewNumber = 0;
            title = string.Empty;
            cover = string.Empty;
            IsLoadMore = false;
        }
    }

    
}
