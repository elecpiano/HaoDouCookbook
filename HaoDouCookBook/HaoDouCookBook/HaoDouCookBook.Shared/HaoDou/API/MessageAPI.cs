using HaoDouCookBook.Common;
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
    }
}
