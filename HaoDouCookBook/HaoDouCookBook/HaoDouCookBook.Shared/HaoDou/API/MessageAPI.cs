using HaoDouCookBook.Common;
using HaoDouCookBook.HaoDou.DataModels;
using HaoDouCookBook.HaoDou.DataModels.My;
using System;
using System.Threading.Tasks;
namespace HaoDouCookBook.HaoDou.API
{
    public class MessageAPI
    {
        public const string MODULE = "Message";

        public static async Task GetListByUid(int offset, int limit, int uid, string uuid, string sign, Action<MessagePageData> onSuccess, Action<Error> onFail)
        { 
            string methodName = "getListByUid";
            POSTRequestExecuter postRequest = new POSTRequestExecuter(HaoDouApiUrlHelper.GetApiUrl(MODULE, methodName));
            postRequest.AddPostData("uid", uid.ToString());
            postRequest.AddPostData("sign", sign);
            postRequest.AddPostData("uuid", uuid);
            postRequest.AddPostData("offst", offset.ToString());
            postRequest.AddPostData("limit", limit.ToString());

            string cacheFileName = string.Format("{0}-{1}-{2}-{3}", methodName, uid.ToString(), offset.ToString(), limit.ToString());

            HaoDouJsonDataLoader<MessagePageData> loader = new HaoDouJsonDataLoader<MessagePageData>();
            await loader.LoadAsync(postRequest, true, MODULE, cacheFileName, onSuccess, onFail);
        }

        public static async Task GetMessageList(int uid, int userid, string sign, string mid, string uuid, int offset, Action<IMMessageList> onSuccess, Action<Error> onFail)
        {
            string methodName = "getMessageList";
            POSTRequestExecuter postRequest = new POSTRequestExecuter(HaoDouApiUrlHelper.GetApiUrl(MODULE, methodName));
            postRequest.AddPostData("userid", userid.ToString());
            postRequest.AddPostData("uid", uid.ToString());
            postRequest.AddPostData("sign", sign);
            postRequest.AddPostData("uuid", uuid);
            postRequest.AddPostData("offst", offset.ToString());
            postRequest.AddPostData("mid", mid);

            string cacheFileName = string.Format("{0}-{1}-{2}-{3}-{4}", methodName, uid.ToString(), offset.ToString(), userid.ToString(), mid);

            HaoDouJsonDataLoader<IMMessageList> loader = new HaoDouJsonDataLoader<IMMessageList>();
            await loader.LoadAsync(postRequest, true, MODULE, cacheFileName, onSuccess, onFail);

        }

        public static async Task SendMessage(string content, int userId, int uid, string sign, string uuid, Action<HaodouResultMessage> onSuccess, Action<Error> onFail)
        {
            string methodName = "sendMsg";
            POSTRequestExecuter postRequest = new POSTRequestExecuter(HaoDouApiUrlHelper.GetApiUrl(MODULE, methodName));
            postRequest.AddPostData("userid", userId.ToString());
            postRequest.AddPostData("uid", uid.ToString());
            postRequest.AddPostData("sign", sign);
            postRequest.AddPostData("uuid", uuid);
            postRequest.AddPostData("content", content);

            HaoDouJsonDataLoader<HaodouResultMessage> loader = new HaoDouJsonDataLoader<HaodouResultMessage>();
            await loader.LoadWithoutCacheAsnyc(postRequest, onSuccess, onFail);
        }

        public static async Task ClearOneMessageList(int userId, int uid, string mid, string sign, Action<HaodouResultMessage> onSuccess, Action<Error> onFail)
        {
            string methodName = "clearOneMessageList";
            POSTRequestExecuter postRequest = new POSTRequestExecuter(HaoDouApiUrlHelper.GetApiUrl(MODULE, methodName));
            postRequest.AddPostData("userid", userId.ToString());
            postRequest.AddPostData("uid", uid.ToString());
            postRequest.AddPostData("sign", sign);
            postRequest.AddPostData("mid", mid);

            HaoDouJsonDataLoader<HaodouResultMessage> loader = new HaoDouJsonDataLoader<HaodouResultMessage>();
            await loader.LoadWithoutCacheAsnyc(postRequest, onSuccess, onFail);
        }
    }
}
