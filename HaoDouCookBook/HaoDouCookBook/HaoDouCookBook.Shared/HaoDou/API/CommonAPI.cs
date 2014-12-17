using HaoDouCookBook.Common;
using HaoDouCookBook.HaoDou.DataModels;
using System;
using System.Threading.Tasks;

namespace HaoDouCookBook.HaoDou.API
{
    public class CommonAPI
    {
        public const string MODULE = "common";

        public static async Task SendCode(string phone, string uuid, Action<HaodouResultMessage> onSuccess, Action<Error> onFail)
        {
            string methodName = "sendCode";
            POSTRequestExecuter postRequest = new POSTRequestExecuter(HaoDouApiUrlHelper.GetApiUrl(MODULE, methodName));
            postRequest.AddPostData("phone", phone);
            postRequest.AddPostData("uuid", uuid);

            HaoDouJsonDataLoader<HaodouResultMessage> loader = new HaoDouJsonDataLoader<HaodouResultMessage>();
            await loader.LoadWithoutCacheAsnyc(postRequest, onSuccess, onFail);
        }
    }
}
