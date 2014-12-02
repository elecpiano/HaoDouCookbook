using Shared.Infrastructures;
using System.Collections.ObjectModel;

namespace HaoDouCookBook.ViewModels
{
    public class StuffInfoViewModel : BindableBase
    {
        private string intro;

        public string Intro
        {
            get { return intro; }
            set { SetProperty<string>(ref intro, value); }
        }

        private string cover;

        public string Cover
        {
            get { return cover; }
            set { SetProperty<string>(ref cover, value); }
        }

        private string effect;

        public string Effect
        {
            get { return effect; }
            set { SetProperty<string>(ref effect, value); }
        }

        private string pick;

        public string Pick
        {
            get { return pick; }
            set { SetProperty<string>(ref pick, value); }
        }

        private string skill;

        public string Skill
        {
            get { return skill; }
            set { SetProperty<string>(ref skill, value); }
        }

        private string storage;

        public string Storage
        {
            get { return storage; }
            set { SetProperty<string>(ref storage, value); }
        }

        public ObservableCollection<StuffRecipe> Recipes { get; set; }

        private string stuffName;

        public string StuffName
        {
            get { return stuffName; }
            set { SetProperty<string>(ref stuffName, value); }
        }


        public StuffInfoViewModel()
        {
            stuffName = string.Empty;
            intro = string.Empty;
            cover = string.Empty;
            effect = string.Empty;
            pick = string.Empty;
            skill = string.Empty;
            storage = string.Empty;
            Recipes = new ObservableCollection<StuffRecipe>();
        }

    }

    public class StuffRecipe : BindableBase
    {
        private string title;

        public string Title
        {
            get { return title; }
            set { SetProperty<string>(ref title, value); }
        }

        private int recipeId;

        public int RecipeId
        {
            get { return recipeId; }
            set { SetProperty<int>(ref recipeId, value); }
        }

        private string cover;

        public string Cover
        {
            get { return cover; }
            set { SetProperty<string>(ref cover, value); }
        }

        public StuffRecipe()
        {
            title = string.Empty;
            recipeId = -1;
            cover = string.Empty;
        }
    }
}
