using HaoDouCookBook.ViewModels;
using Shared.Infrastructures;
using Shared.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HaoDouCookBook.Common
{
    [DataContract]
    public class LocalCateogoryFolder : BindableBase
	{
        [IgnoreDataMember]
        public const string SYSTEM_FOLDER_NAME = "默认分类";

        private string folderName;

        [DataMember]
        public string FolderName
        {
            get { return folderName; }
            set { SetProperty<string>(ref folderName, value); }
        }

        private string cover;

        [DataMember]
        public string Cover
        {
            get { return cover; }
            set { SetProperty<string>(ref cover, value); }
        }

        private bool visible;

        /// <summary>
        /// 用来控制显示与隐藏
        /// </summary>
        [DataMember]
        public bool Visible
        {
            get { return visible; }
            set { SetProperty<bool>(ref visible, value); }
        }

        public void UpdateAllStuffsStrings()
        {
            foreach (var recipe in Recipes)
            {
                recipe.GetAllStuffsString();
            }
        }

        [DataMember]
        public ObservableCollection<RecipeInfoPageViewModel> Recipes { get; set; }

		public LocalCateogoryFolder()
		{
            Recipes = new ObservableCollection<RecipeInfoPageViewModel>();
            folderName = string.Empty;
            cover = Constants.DEFAULT_IMAGE_SMALL;
            Visible = true;
		}
	}

    [DataContract]
    public class LocalDownloads
    {
        private static readonly string FILENAME = "LocalDownloads.json";

        #region Singleton

        [IgnoreDataMember]
        public static object _lockObject = new object();

        private static LocalDownloads _instance;

        [IgnoreDataMember]
        public static LocalDownloads Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lockObject)
                    {
                        if (_instance == null)
                        {
                            _instance = new LocalDownloads();
                        }
                    }
                }

                return _instance;
            }
        }

        public bool IsDownloaded(int recipeId)
        {
            return Folders.Any(f => f.Recipes.Any(r => r.RecipeId == recipeId));
        }

        #endregion

        private LocalDownloads()
        {
            Folders = new ObservableCollection<LocalCateogoryFolder>();
            Folders.Add(new LocalCateogoryFolder() {
                FolderName = LocalCateogoryFolder.SYSTEM_FOLDER_NAME
            });
        }

        [DataMember]
        public ObservableCollection<LocalCateogoryFolder> Folders { get; set; }

        public async void LoadData()
        {
            string json = await IsolatedStorageHelper.ReadFileAsync(Constants.LOCAL_USERDATA_FOLDER, FILENAME);
            if(!string.IsNullOrEmpty(json))
            {
                var data = JsonSerializer.Deserialize<LocalDownloads>(json);
                if(data != null && data.Folders != null)
                {
                    Folders.Clear();
                    foreach (var folder in data.Folders)
                    {
                        Folders.Add(folder);
                    }
                }
            }
            else
            {
                CommitData();
            }
        }

        public async void CommitData()
        {
            string json = JsonSerializer.Serialize<LocalDownloads>(_instance);
            await IsolatedStorageHelper.WriteToFileAsync(Constants.LOCAL_USERDATA_FOLDER, FILENAME, json);
        }

    }
}
