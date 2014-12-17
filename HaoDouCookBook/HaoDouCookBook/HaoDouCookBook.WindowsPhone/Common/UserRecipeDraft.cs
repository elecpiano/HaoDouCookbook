using HaoDouCookBook.ViewModels;
using Shared.Utility;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace HaoDouCookBook.Common
{
    [DataContract]
    public class UserRecipeDraft
    {
        private static readonly string FILENAME = string.Format("{0}-draft.json", UserGlobal.Instance.GetInt32UserId());

        #region Singleton

        public static object _lockObject = new object();

        private static UserRecipeDraft _instance;
        public static UserRecipeDraft Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lockObject)
                    {
                        if (_instance == null)
                        {
                            _instance = new UserRecipeDraft();
                        }
                    }
                }

                return _instance;
            }
        }

        #endregion

        [DataMember]
        public ObservableCollection<PublishRecipePageViewModel> Recipes { get; set; }

        public UserRecipeDraft()
        {
            Recipes = new ObservableCollection<PublishRecipePageViewModel>();
            LoadData();
        }

        public async void LoadData()
        {
            string json = await IsolatedStorageHelper.ReadFileAsync(Constants.LOCAL_USERDATA_FOLDER, FILENAME);
            if(!string.IsNullOrEmpty(json))
            {
                var data = JsonSerializer.Deserialize<UserRecipeDraft>(json);
                Recipes.Clear();
                foreach (var item in data.Recipes)
                {
                    Recipes.Add(item);
                }
            }
        }

        public async void CommitData()
        {
            string json = JsonSerializer.Serialize<UserRecipeDraft>(_instance);
            await IsolatedStorageHelper.WriteToFileAsync(Constants.LOCAL_USERDATA_FOLDER, FILENAME, json);
        }
    }
}
