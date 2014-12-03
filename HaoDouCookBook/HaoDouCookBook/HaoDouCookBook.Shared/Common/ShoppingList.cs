using Shared.Utility;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Linq;

namespace HaoDouCookBook.Common
{
    public class ShoppingList
    {
        #region Singleton

        public static object lockObject = new object();

        private static ShoppingList _instance;
        public static ShoppingList Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (lockObject)
                    {
                        if (_instance == null)
                        {
                            _instance = new ShoppingList();
                        }
                    }
                }

                return _instance;
            }
        }

        #endregion

        private const string SHOPPINGLIST_DATAFILE = "ShoppingList.json";
        private ShoppingListData data;

        public Dictionary<int, ShoppingListStuff> StuffsDict { get; set; }

        public List<ShoppingListRecipe> Recipes
        {
            get
            {
                return data.Recipes;
            }
        }

        private ShoppingList()
        {
            StuffsDict = new Dictionary<int,ShoppingListStuff>();
        }


        private async Task DeleteRecipByIdAsync(int recipeId)
        {
            var recipe = data.Recipes.Find(r => r.RecipeId == recipeId);
            if (recipe != null)
            {
                data.Recipes.Remove(recipe);
                foreach (var stuff in data.Stuffs)
                {
                    data.Stuffs.RemoveAll(s => s.StuffId == stuff.StuffId);
                }

                await SaveDataAsync();
            }
        }

        private async Task AddRecipeAsync(ShoppingListRecipe recipe)
        {
            if (data.Recipes.All(r => r.RecipeId != recipe.RecipeId))
            {
                data.Recipes.Add(recipe);

                // TODO : Add stuffs data
                //

                await SaveDataAsync();
            }
        }

        private async Task LoadDataAsync()
        {
            string dataJson = await IsolatedStorageHelper.ReadFileAsync(Constants.LOCAL_USERDATA_FOLDER, SHOPPINGLIST_DATAFILE);
            if (!string.IsNullOrEmpty(dataJson))
            {
                data = JsonSerializer.Deserialize<ShoppingListData>(dataJson);
            }

            GetStuffDict();

        }

        private void GetStuffDict()
        {
            if (data == null && data.Stuffs == null)
            {
                return;
            }

            foreach (var item in data.Stuffs)
            {
                if (!StuffsDict.ContainsKey(item.StuffId))
                {
                    StuffsDict.Add(item.StuffId, item);
                }
            }
        }
        
        private async Task SaveDataAsync()
        {
            if (data == null)
            {
                return;
            }

            string dataJson = JsonSerializer.Serialize<ShoppingListData>(data);
            if (!string.IsNullOrEmpty(dataJson))
            {
                await IsolatedStorageHelper.WriteToFileAsync(Constants.LOCAL_USERDATA_FOLDER, SHOPPINGLIST_DATAFILE, dataJson);
            }
        }

    }


    [DataContract]
    public class ShoppingListData
    {

        [DataMember]
        public List<ShoppingListRecipe> Recipes { get; set; }


        [DataMember]
        public List<ShoppingListStuff> Stuffs { get; set; }

        public ShoppingListData()
        {
            Recipes = new List<ShoppingListRecipe>();
            Stuffs = new List<ShoppingListStuff>();
        }
    }

    [DataContract]
    public class ShoppingListRecipe
    {
        [DataMember]
        public int RecipeId { get; set; }

        [DataMember]
        public string RecipeName { get; set; }


        [DataMember]
        public List<ShoppingListRecipeStuff> Stuffs { get; set; }

        public ShoppingListRecipe()
        {
            Stuffs = new List<ShoppingListRecipeStuff>();
            RecipeName = string.Empty;
        }
    }


    [DataContract]
    public class ShoppingListRecipeStuff
    {

        [DataMember]
        public int StuffId { get; set; }


        [DataMember]
        public string Weight { get; set; }

        public ShoppingListRecipeStuff()
        {
            StuffId = -1;
            Weight = string.Empty;
        }
    }


    [DataContract]
    public class ShoppingListStuff
    {
        [DataMember]
        public int StuffId { get; set; }

        [DataMember]
        public string StuffName { get; set; }

        [DataMember]
        public int CategoryId { get; set; }

        [DataMember]
        public string CategoryName { get; set; }


        [DataMember]
        public bool IsBought { get; set; }

        public ShoppingListStuff()
        {
            StuffId = -1;
            StuffName = string.Empty;
            CategoryId = -1;
            CategoryName = string.Empty;
            IsBought = false;
        }
    }

}
