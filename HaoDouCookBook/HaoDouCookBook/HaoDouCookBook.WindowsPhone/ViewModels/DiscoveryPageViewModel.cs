using Shared.Infrastructures;
using System.Collections.ObjectModel;

namespace HaoDouCookBook.ViewModels
{
    public class DiscoveryPageViewModel : BindableBase
    {
        public ObservableCollection<UserData> StarredUsers { get; set; }
        public ObservableCollection<Meal> DailyMeals { get; set; }

        public ObservableCollection<Cate> Cates { get; set; }

        public ObservableCollection<CookMaster> Masters { get; set; }

        private NewbieTutorial tutorial;

        public NewbieTutorial Tutorial
        {
            get { return tutorial; }
            set { SetProperty<NewbieTutorial>(ref tutorial, value); }
        }



        public DiscoveryPageViewModel()
        {
            DailyMeals = new ObservableCollection<Meal>();
            StarredUsers = new ObservableCollection<UserData>();
            Masters = new ObservableCollection<CookMaster>();
            tutorial = new NewbieTutorial();
            Cates = new ObservableCollection<Cate>();
        }
    }

    public class Cate : BindableBase
    {
        public ObservableCollection<DishTileData> Dishes { get; set; }

        private string title;

        public string Title
        {
            get { return title; }
            set { SetProperty<string>(ref title, value); }
        }


        public Cate()
        {
            Dishes = new ObservableCollection<DishTileData>();
            title = string.Empty;
        }
    }
}
