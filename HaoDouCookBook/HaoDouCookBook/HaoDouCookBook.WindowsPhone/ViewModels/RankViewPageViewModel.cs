using Shared.Infrastructures;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaoDouCookBook.ViewModels
{
    public class RankViewPageViewModel : BindableBase
    {
        private string title;

        public string Title
        {
            get { return title; }
            set { SetProperty<string>(ref title, value); }
        }

        private string intro;

        public string Intro
        {
            get { return intro; }
            set { SetProperty<string>(ref intro, value); }
        }

        public ObservableCollection<RankViewRecipeItem> Recipes { get; set; }

        public RankViewPageViewModel()
        {
            title = string.Empty;
            intro = string.Empty;
            Recipes = new ObservableCollection<RankViewRecipeItem>();
        }

    }


    public class RankViewRecipeItem : BindableBase
    {
        private int recipeId;

        public int RecipeId
        {
            get { return recipeId; }
            set { SetProperty<int>(ref recipeId, value); }
        }

        private bool isFavorited;

        public bool IsFavorited
        {
            get { return isFavorited; }
            set { SetProperty<bool>(ref isFavorited, value); }
        }

        private string userName;

        public string UserName
        {
            get { return userName; }
            set { SetProperty<string>(ref userName, value); }
        }

        private int favoriteCount;

        public int FavoriteCount
        {
            get { return favoriteCount; }
            set { SetProperty<int>(ref favoriteCount, value); }
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

        private int rank;

        public int Rank
        {
            get { return rank; }
            set { SetProperty<int>(ref rank, value); }
        }

        private string rankIcon;

        public string RankIcon
        {
            get { return rankIcon; }
            set { SetProperty<string>(ref rankIcon, value); }
        }


        public RankViewRecipeItem()
        {
            userName = string.Empty;
            rank = -1;
            title = string.Empty;
            favoriteCount = 0;
            cover = string.Empty;
            isFavorited = false;
            recipeId = -1;
            rankIcon = string.Empty;

        }

    }
}
