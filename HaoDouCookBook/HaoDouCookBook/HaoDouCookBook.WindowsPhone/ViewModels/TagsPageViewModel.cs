using Shared.Infrastructures;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaoDouCookBook.ViewModels
{
    public class TagsPageViewModel : BindableBase
    {
        private Food food;

        public Food Food
        {
            get { return food; }
            set { SetProperty<Food>(ref food, value); }
        }

        public ObservableCollection<TagRecipeData> Recipes { get; set; }

        public TagsPageViewModel()
        {
            Recipes = new ObservableCollection<TagRecipeData>();
            food = new Food();
        }
    }

    public class Food : BindableBase
    {
        private int foodId;

        public int FoodId
        {
            get { return foodId; }
            set { SetProperty<int>(ref foodId, value); }
        }


        private string foodName;

        public string FoodName
        {
            get { return foodName; }
            set { SetProperty<string>(ref foodName, value); }
        }

        private string foodCover;

        public string FoodCover
        {
            get { return foodCover; }
            set { SetProperty<string>(ref foodCover, value); }
        }

        private string foodIntro;

        public string FoodIntro
        {
            get { return foodIntro; }
            set { SetProperty<string>(ref foodIntro, value); }
        }

        public Food()
        {
            foodId = -1;
            foodIntro = string.Empty;
            foodName = string.Empty;
            foodCover = string.Empty;
        }
    }




}
