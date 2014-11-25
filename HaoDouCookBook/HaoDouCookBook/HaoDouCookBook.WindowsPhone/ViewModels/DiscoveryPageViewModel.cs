using Shared.Infrastructures;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaoDouCookBook.ViewModels
{
    public class DiscoveryPageViewModel : BindableBase
    {
        public ObservableCollection<UserData> StarredUsers { get; set; }
        public ObservableCollection<Meal> DailyMeals { get; set; }

        public ObservableCollection<Cate> Cates { get; set; }

        private CookMaster master;

        public CookMaster Master
        {
            get { return master; }
            set { SetProperty<CookMaster>(ref master, value); }
        }

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
            master = new CookMaster();
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
