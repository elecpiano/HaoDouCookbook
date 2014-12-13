using HaoDouCookBook.Common;
using HaoDouCookBook.HaoDou.DataModels;
using HaoDouCookBook.HaoDou.DataModels.My;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HaoDouCookBook.HaoDou.API
{
    public class SettingAPI
    {
        public const string MODULE = "setting";

        public static async Task Fond(string sign, int uid, Action<PersonalTagsData> onSuccess, Action<Error> onFail)
        {
            string methodName = "fond";
            POSTRequestExecuter postRequest = new POSTRequestExecuter(HaoDouApiUrlHelper.GetApiUrl(MODULE, methodName));
            postRequest.AddPostData("offset", "0");
            postRequest.AddPostData("limit", "2147483647");
            postRequest.AddPostData("uid", uid.ToString());
            postRequest.AddPostData("sign", sign);

            HaoDouJsonDataLoader<PersonalTagsData> loader = new HaoDouJsonDataLoader<PersonalTagsData>();
            await loader.LoadWithoutCacheAsnyc(postRequest, onSuccess, onFail);
        }

        public static async Task SetFond(string ids, int uid, string sign, Action<HaodouResultMessage> onSuccess, Action<Error> onFail)
        {
            string methodName = "setFond";
            POSTRequestExecuter postRequest = new POSTRequestExecuter(HaoDouApiUrlHelper.GetApiUrl(MODULE, methodName));
            postRequest.AddPostData("uid", uid.ToString());
            postRequest.AddPostData("sign", sign);
            postRequest.AddPostData("id", ids);

            HaoDouJsonDataLoader<HaodouResultMessage> loader = new HaoDouJsonDataLoader<HaodouResultMessage>();
            await loader.LoadWithoutCacheAsnyc(postRequest, onSuccess, onFail);
        }

        public static async Task UpdateUserName(string userName, int uid, string sign, Action<HaodouResultMessage> onSuccess, Action<Error> onFail)
        {
            string methodName = "updateUserName";
            POSTRequestExecuter postRequest = new POSTRequestExecuter(HaoDouApiUrlHelper.GetApiUrl(MODULE, methodName));
            postRequest.AddPostData("uid", uid.ToString());
            postRequest.AddPostData("user_name", userName);
            postRequest.AddPostData("sign", sign);

            HaoDouJsonDataLoader<HaodouResultMessage> loader = new HaoDouJsonDataLoader<HaodouResultMessage>();
            await loader.LoadWithoutCacheAsnyc(postRequest, onSuccess, onFail);
        }
    }
}
