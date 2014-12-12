using HaoDouCookBook.Common;
using HaoDouCookBook.HaoDou.DataModels.My;
using System;
using System.Threading.Tasks;

namespace HaoDouCookBook.HaoDou.API
{
    public class NoticeAPI
    {
        public const string MODULE = "Notice";

        public static async Task GetCount(int uid, string sign, Action<NoticeInfoData> onSuccess, Action<Error> onFail)
        {
            string methodName = "getCount";
            POSTRequestExecuter postRequest = new POSTRequestExecuter(HaoDouApiUrlHelper.GetApiUrl(MODULE, methodName));
            postRequest.AddPostData("uid", uid.ToString());
            postRequest.AddPostData("sign", sign);

            HaoDouJsonDataLoader<NoticeInfoData> loader = new HaoDouJsonDataLoader<NoticeInfoData>();
            await loader.LoadWithoutCacheAsnyc(postRequest, onSuccess, onFail);
        }

        public static async Task GetUserNotice(int offset, int limit, int uid, string uuid, int status, string subtype, string sign, Action<NoticPageData> onSuccess, Action<Error> onFail)
        {
            string methodName = "getUserNotice";
            POSTRequestExecuter postRequest = new POSTRequestExecuter(HaoDouApiUrlHelper.GetApiUrl(MODULE, methodName));
            postRequest.AddPostData("offset", offset.ToString());
            postRequest.AddPostData("limit", limit.ToString());
            postRequest.AddPostData("uid", uid.ToString());
            postRequest.AddPostData("uuid", uuid);
            postRequest.AddPostData("sign", sign);
            postRequest.AddPostData("subtype", subtype.ToString());
            postRequest.AddPostData("status", status.ToString());

            string cacheFileName = string.Format("{0}-{1}-{2}-{3}-{4}-{5}", methodName, uid.ToString(), status.ToString(), subtype.ToString(), offset.ToString(), limit.ToString());
            HaoDouJsonDataLoader<NoticPageData> loader = new HaoDouJsonDataLoader<NoticPageData>();
            await loader.LoadAsync(postRequest, true, MODULE, cacheFileName, onSuccess, onFail);
        }
    }
}
