using Shared.Infrastructures;
using System.Collections.ObjectModel;
using System.Linq;

namespace HaoDouCookBook.ViewModels
{
    public class ShoppingListPageViewModel : BindableBase
    {
        public ObservableCollection<StuffCategory> StuffCategories { get; set; }

        public ObservableCollection<ShoppingListRecipe> Recipes { get; set; }

        public ShoppingListPageViewModel()
        {
            StuffCategories = new ObservableCollection<StuffCategory>();
            Recipes = new ObservableCollection<ShoppingListRecipe>();
        }
    }



    public class StuffCategory : BindableBase
    {
        private int categoryId;

        public int CategoryId
        {
            get { return categoryId; }
            set { SetProperty<int>(ref categoryId, value); }
        }

        private string category;

        public string Category
        {
            get { return category; }
            set { SetProperty<string>(ref category, value); }
        }

        public ObservableCollection<StuffItem> Stuffs { get; set; }

        public StuffItem FindStuffByName(string Name)
        {
            return Stuffs.FirstOrDefault(s => s.Name == Name);
        }

        public StuffCategory()
        {
            category = string.Empty;
            Stuffs = new ObservableCollection<StuffItem>();
        }
    }

    public class ShoppingListRecipe : BindableBase
    {
        private int recipeId;

        public int RecipeId
        {
            get { return recipeId; }
            set { SetProperty<int>(ref recipeId, value); }
        }

        private string name;

        public string Name
        {
            get { return name; }
            set { SetProperty<string>(ref name, value); }
        }


        public ObservableCollection<StuffItem> Stuffs { get; set; }

        public ShoppingListRecipe()
        {
            recipeId = 0;
            name = string.Empty;
            Stuffs = new ObservableCollection<StuffItem>();
        }
    }


    public class StuffItem : BindableBase
    {
        private string name;

        public string Name
        {
            get { return name; }
            set { SetProperty<string>(ref name, value); }
        }

        private string weight;

        public string Weight
        {
            get { return weight; }
            set { SetProperty<string>(ref weight, value); }
        }

        public StuffItem()
        {
            name = string.Empty;
            weight = string.Empty;
        }
    }
}
