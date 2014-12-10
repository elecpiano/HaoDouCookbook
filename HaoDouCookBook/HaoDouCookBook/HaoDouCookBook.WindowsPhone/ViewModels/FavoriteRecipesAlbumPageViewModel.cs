using Shared.Infrastructures;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    public class FavoriteRecipe : BindableBase
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

        public FavoriteRecipe()
        {
            recipeId = 0;
            likeNumber = 0;
            viewNumber = 0;
            title = string.Empty;
            cover = string.Empty;
        }
    }

    
}
