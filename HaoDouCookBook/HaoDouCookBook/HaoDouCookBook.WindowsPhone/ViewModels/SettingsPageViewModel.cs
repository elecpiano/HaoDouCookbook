using Shared.Infrastructures;
using Shared.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HaoDouCookBook.ViewModels
{
    [DataContract]
    public class SettingsPageViewModel : BindableBase
    {
        private const string MODULE = "SETTINGS";
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
            string json = await IsolatedStorageHelper.ReadFile(MODULE, FILE_NAME);
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
            await IsolatedStorageHelper.WriteToFile(MODULE, FILE_NAME, json);
        }
    }
}
