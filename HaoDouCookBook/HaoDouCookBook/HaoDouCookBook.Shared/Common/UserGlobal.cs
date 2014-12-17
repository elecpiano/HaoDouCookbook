using HaoDouCookBook.HaoDou.API;
using HaoDouCookBook.HaoDou.DataModels.My;
using HaoDouCookBook.Utility;
using Shared.Utility;
using System;
using System.Runtime.Serialization;
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
            await AccountAPI.Login(username, password, data =>
                {
                    if (!string.IsNullOrEmpty(data.Sign))
                    {
                        UserInfo = data;

                        if (onSuccess != null)
                        {
                            onSuccess.Invoke();
                        }

                        CommitDataAsync();
                    }
                    else
                    {
                        if (onFail != null)
                        {
                            onFail.Invoke(new Error() { ErrorCode = Constants.ERROR_LOGIN_FAIL, Message = Constants.ERRORMESSAGE_LOGIN_FAIL });
                        };
                    }

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
                    onFail.Invoke(new Error() { ErrorCode = int.MaxValue - 1, Message = "Not login" });
                }
            }

            await PassportAPI.Logout(uuid, UserInfo.Sign,
               () =>
                {
                    // clear data by assign to new instance.
                    //
                    UserInfo = new PassportLoginResultData();
                    CommitDataAsync();

                    if (onSuccess != null)
                    {
                        onSuccess.Invoke();
                    }

                }, onFail);
        }

        public async void CommitDataAsync()
        {
            string dataJson = JsonSerializer.Serialize<UserGlobal>(_instance);
            if (!string.IsNullOrEmpty(dataJson))
            {
                try
                {
                    await IsolatedStorageHelper.WriteToFileAsync(Constants.LOCAL_USERDATA_FOLDER, FILE_NAME, dataJson);
                }
                catch(UnauthorizedAccessException)
                {}
            }
        }

        public async void LoadData()
        {
            string dataJson = await IsolatedStorageHelper.ReadFileAsync(Constants.LOCAL_USERDATA_FOLDER, FILE_NAME);
            if (string.IsNullOrEmpty(dataJson))
            {
                CommitDataAsync();
            }
            else
            {
                _instance = JsonSerializer.Deserialize<UserGlobal>(dataJson);
            }
        }

        public void UpdateUserInfoBySummary(UserSummaryInfo summary)
        {
            UserInfo.Avatar = summary.Avatar;
            UserInfo.CheckIn = summary.CheckIn;
            UserInfo.MessageCnt = summary.MessageCnt;
            UserInfo.MsgCnt = summary.MessageCnt;
            UserInfo.Name = summary.UserName;
            UserInfo.NoticCnt = summary.NoticeCnt;
            UserInfo.UserId = summary.UserId.ToString();
            UserInfo.Vip = summary.Vip;
        }

        #endregion

        #region Private methods



        #endregion
    }
}
