using System;
using System.Threading.Tasks;
using HaoDouCookBook.HaoDou.DataModels;
using HaoDouCookBook.Common;
using HaoDouCookBook.HaoDou.DataModels.My;

namespace HaoDouCookBook.HaoDou.API
{
    public class DiggAPI
    {
        public const string MODULE = "Digg";

        public static async Task Digg(int id, int uid, int type, int digg, string uuid, string sign, Action<HaodouResultMessage> onSuccess, Action<Error> onFail)
        {
            string methodName = "digg";
            POSTRequestExecuter postRequest = new POSTRequestExecuter(HaoDouApiUrlHelper.GetApiUrl(MODULE, methodName));
            postRequest.AddPostData("id", id.ToString());
            postRequest.AddPostData("uid", uid.ToString());
            postRequest.AddPostData("type", type.ToString());
            postRequest.AddPostData("digg", digg.ToString());
            postRequest.AddPostData("uuid", uuid);
            postRequest.AddPostData("sign", sign);

            HaoDouJsonDataLoader<HaodouResultMessage> loader = new HaoDouJsonDataLoader<HaodouResultMessage>();
            await loader.LoadWithoutCacheAsnyc(postRequest, onSuccess, onFail);
        }

        public static async Task GetDiggUserList(int offset, int limit, int id, int type, Action<DiggUserData> onSuccess, Action<Error> onFail)
        {
 
            string methodName = "getDiggUserList";
            POSTRequestExecuter postRequest = new POSTRequestExecuter(HaoDouApiUrlHelper.GetApiUrl(MODULE, methodName));
            postRequest.AddPostData("id", id.ToString());
            postRequest.AddPostData("type", type.ToString());
            postRequest.AddPostData("offset", offset.ToString());
            postRequest.AddPostData("limit", limit.ToString());

            string cacheFileName = string.Format("{0}-{1}-{2}-{3}", methodName, id.ToString(), offset.ToString(), limit.ToString());
            HaoDouJsonDataLoader<DiggUserData> loader = new HaoDouJsonDataLoader<DiggUserData>();
            await loader.LoadAsync(postRequest, true, MODULE, cacheFileName, onSuccess, onFail);
        }
    }
}
