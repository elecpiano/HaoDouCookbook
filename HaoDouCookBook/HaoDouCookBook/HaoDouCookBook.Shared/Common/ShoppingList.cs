using Shared.Utility;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Linq;
using HaoDouCookBook.HaoDou.DataModels.ChoicenessPage;
using System;

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

        #region Filed && Property

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

        #endregion

        #region Constructor

        private ShoppingList()
        {
            StuffsDict = new Dictionary<int, ShoppingListStuff>();
        }

        #endregion

        #region Public Methods

        public async Task DeleteRecipByIdAsync(int recipeId)
        {
            var recipe = data.Recipes.Find(r => r.RecipeId == recipeId);
            if (recipe != null)
            {
                data.Recipes.Remove(recipe);
                foreach (var stuff in data.Stuffs)
                {
                    data.Stuffs.RemoveAll(s => s.StuffId == stuff.StuffId);
                }

                await CommitDataAsync();
            }
        }

        public async Task AddRecipeAsync(int recipeId, string recipeName, List<FoodStuff> Stuffs)
        {
            if (recipeId == -1 || string.IsNullOrEmpty(recipeName) || Stuffs == null)
            {
                throw new ArgumentException(); 
            }

            ShoppingListRecipe recipe = new ShoppingListRecipe();
            recipe.RecipeId = recipeId;
            recipe.RecipeName = recipeName;

            foreach (var item in Stuffs)
            {
                recipe.Stuffs.Add(new ShoppingListRecipeStuff()
                {
                    StuffId = item.Id,
                    Weight = item.Weight
                });

                // Add stuff into list of local data
                //
                if (data.Stuffs.All(s => s.StuffId != item.Id))
                {
                    data.Stuffs.Add(new ShoppingListStuff()
                    {
                        StuffId = item.Id,
                        StuffName = item.Name,
                        CategoryId = item.CategoryId,
                        CategoryName = item.Category,
                        IsBought = false
                    });
                }
                else
                {
                    // If the stuff already in local data, we reset the IsBought to false,
                    // because it's in new recipe
                    //
                    var stuff = data.Stuffs.Find(s => s.StuffId == item.Id);
                    stuff.IsBought = false;
                }
            }

            data.Recipes.Add(recipe);

            await CommitDataAsync();
        }

        public bool RecipeExists(int recipeId)
        {
            return data.Recipes.Any(r => r.RecipeId == recipeId);
        }

        public async Task SetStuffBoughtStateAsync(int stuffid, bool isBought)
        {
            var stuff = data.Stuffs.Find(s => s.StuffId == stuffid);
            if (stuff == null)
            {
                throw new Exception("Stuff is not exist");
            }
            else
            {
                stuff.IsBought = isBought;
            }

            await CommitDataAsync(); 
        }

        public async Task CommitDataAsync()
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

        public async Task LoadDataAsync()
        {
            string dataJson = await IsolatedStorageHelper.ReadFileAsync(Constants.LOCAL_USERDATA_FOLDER, SHOPPINGLIST_DATAFILE);
            
            if (!string.IsNullOrEmpty(dataJson))
            {
                data = JsonSerializer.Deserialize<ShoppingListData>(dataJson);
                GetStuffDict();
            }
            else
            {
                // If not exist data file, we create it with empty data
                //
                data = new ShoppingListData();
                await CommitDataAsync();
            }
        }

        #endregion

        #region Private Methods

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

        #endregion
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
