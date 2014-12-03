using HaoDouCookBook.Common;
using Shared.Infrastructures;
using Shared.Utility;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace HaoDouCookBook.ViewModels
{
    [DataContract]
    public class SettingsPageViewModel : BindableBase
    {
        private const string FILE_NAME = "Settings.json";

        #region Singleton

        public static object _lockObject = new object();

        private static SettingsPageViewModel _instance;
        public static SettingsPageViewModel Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lockObject)
                    {
                        if (_instance == null)
                        {
                            _instance = new SettingsPageViewModel();
                        }
                    }
                }

                return _instance;
            }
        }

        #endregion

        private bool shakeWithVibration;

        [DataMember]
        public bool ShakeWithVibration
        {
            get { return shakeWithVibration; }
            set { SetProperty<bool>(ref shakeWithVibration, value); }
        }

        private bool pushMessage;

        [DataMember]
        public bool PushMessage
        {
            get { return pushMessage; }
            set { SetProperty<bool>(ref pushMessage, value); }
        }

        private string cacheSize;

        [IgnoreDataMember]
        public string CacheSize
        {
            get { return cacheSize; }
            set { SetProperty<string>(ref cacheSize, value); }
        }

        private string version;

        [IgnoreDataMember]
        public string Version
        {
            get { return version; }
            set { SetProperty<string>(ref version, value); }
        }

        private SettingsPageViewModel()
        {
            shakeWithVibration = false;
            pushMessage = true;
            version = "1.0.0";

            LoadDataAsync();
        }


        public async Task LoadDataAsync()
        {
            string json = await IsolatedStorageHelper.ReadFileAsync(Constants.LOCAL_USERDATA_FOLDER, FILE_NAME);
            if (!string.IsNullOrEmpty(json))
            {
                var data = JsonSerializer.Deserialize<SettingsPageViewModel>(json);
                ShakeWithVibration = data.ShakeWithVibration;
                PushMessage = data.PushMessage;
            }
            else 
            {
               await SaveDataAsync();
            }
        }

        public async Task SaveDataAsync()
        {
            string json = JsonSerializer.Serialize(_instance);
            await IsolatedStorageHelper.WriteToFileAsync(Constants.LOCAL_USERDATA_FOLDER, FILE_NAME, json);
        }
    }
}
