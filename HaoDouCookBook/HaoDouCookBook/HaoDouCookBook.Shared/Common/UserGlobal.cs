using HaoDouCookBook.HaoDou.API;
using HaoDouCookBook.HaoDou.DataModels.My;
using HaoDouCookBook.Utility;
using Shared.Utility;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HaoDouCookBook.Common
{
    /// <summary>
    /// Singleton class to handle user behaviors
    /// </summary>
    [DataContract]
    public class UserGlobal
    {
        #region Singleton

        public static object _lockObject = new object();

        private static UserGlobal _instance;
        public static UserGlobal Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lockObject)
                    {
                        if (_instance == null)
                        {
                            _instance = new UserGlobal();
                        }
                    }
                }

                return _instance;
            }
        }

        #endregion

        [DataMember]
        public PassportLoginResultData UserInfo { get; set; }

        [DataMember]
        public string uuid { get; set; }

        [IgnoreDataMember]
        private const string FILE_NAME = "user.json";

        private UserGlobal()
        {
            uuid = DeviceHelper.GetUniqueDeviceID();
            UserInfo = new PassportLoginResultData();
        }

        #region Public Methods

        public int GetInt32UserId()
        {
            if (string.IsNullOrEmpty(UserInfo.UserId))
            {
                return 0;
            }

            return int.Parse(UserInfo.UserId);
        }

        public async Task Login(string username, string password, Action onSuccess, Action<Error> onFail)
        {
            await AccountAPI.Login(username, password, async data =>
                {
                    UserInfo = data;

                    if (onSuccess != null)
                    {
                        onSuccess.Invoke();
                    }

                    await CommitDataAsync();

                }, error =>
                {
                    if (onFail != null)
                    {
                        onFail.Invoke(error);
                    }
                });
        }

        public async Task Logout(Action onSuccess, Action<Error> onFail)
        {
            if (string.IsNullOrEmpty(UserInfo.Sign))
            {
                if (onFail != null)
                {
                    onFail.Invoke(new Error() { ErrorCode = int.MaxValue - 1, Message = "Not login"});
                }
            }

            await PassportAPI.Logout(uuid, UserInfo.Sign,
                async () =>
                {
                    // clear data by assign to new instance.
                    //
                    UserInfo = new PassportLoginResultData();
                    await CommitDataAsync();

                    if (onSuccess != null)
                    {
                        onSuccess.Invoke();
                    }

                }, onFail);
        }

        public async Task CommitDataAsync()
        {
            string dataJson = JsonSerializer.Serialize<UserGlobal>(_instance);
            if (!string.IsNullOrEmpty(dataJson))
            {
                await IsolatedStorageHelper.WriteToFileAsync(Constants.LOCAL_USERDATA_FOLDER, FILE_NAME, dataJson);
            }
        }
        public async Task LoadDataAsync()
        {
            string dataJson = await IsolatedStorageHelper.ReadFileAsync(Constants.LOCAL_USERDATA_FOLDER, FILE_NAME);
            if (string.IsNullOrEmpty(dataJson))
            {
                await CommitDataAsync();
            }
            else
            {
                _instance = JsonSerializer.Deserialize<UserGlobal>(dataJson);
            }
        }
        #endregion

        #region Private methods

        

        #endregion
    }
}
