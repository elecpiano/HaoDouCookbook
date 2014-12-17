using HaoDouCookBook.Common;
using HaoDouCookBook.HaoDou.DataModels;
using HaoDouCookBook.HaoDou.DataModels.My;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HaoDouCookBook.HaoDou.API
{

    //content	继续测试
    //uid	6760854
    //soft_version	4.5.0
    //device_type	GT-I9100G
    //appid	2
    //net_type	WIFI
    //uuid	0afeebe25847670e36e6bd56b0ecb207
    //channel_type	wandoujia_v450
    //system_version	4.0.4
    public class FeedbackAPI
    {
        public const string MODULE = "FeedBack";

        public static async Task Add(string content, int uid, string uuid, Action<HaodouResultMessage> onSuccess, Action<Error> onFail)
        {
            string methodName = "add";
            POSTRequestExecuter postRequest = new POSTRequestExecuter(HaoDouApiUrlHelper.GetApiUrl(MODULE, methodName));
            postRequest.AddPostData("content", content);
            postRequest.AddPostData("uid", uid.ToString());
            postRequest.AddPostData("uuid", uuid);
            postRequest.AddPostData("appid", HaoDouApiUrlHelper.APPID);

            HaoDouJsonDataLoader<HaodouResultMessage> loader = new HaoDouJsonDataLoader<HaodouResultMessage>();
            await loader.LoadWithoutCacheAsnyc(postRequest, onSuccess, onFail);
        }

        public static async Task Clear(string uuid, Action<HaodouResultMessage> onSuccess, Action<Error> onFail)
        {
            string methodName = "clear";
            POSTRequestExecuter postRequest = new POSTRequestExecuter(HaoDouApiUrlHelper.GetApiUrl(MODULE, methodName));
            postRequest.AddPostData("uuid", uuid);

            HaoDouJsonDataLoader<HaodouResultMessage> loader = new HaoDouJsonDataLoader<HaodouResultMessage>();
            await loader.LoadWithoutCacheAsnyc(postRequest, onSuccess, onFail);
        }

        public static async Task GetList(int offset, int limit, string uuid, Action<FeedbackList> onSuccess, Action<Error> onFail)
        {
            string methodName = "getList";
            POSTRequestExecuter postRequest = new POSTRequestExecuter(HaoDouApiUrlHelper.GetApiUrl(MODULE, methodName));
            postRequest.AddPostData("uuid", uuid);
            postRequest.AddPostData("offset", offset.ToString());
            postRequest.AddPostData("limit", limit.ToString());

            string cacheFileName = string.Format("{0}-{1}-{2}", methodName, offset, limit);

            HaoDouJsonDataLoader<FeedbackList> loader = new HaoDouJsonDataLoader<FeedbackList>();
            await loader.LoadAsync(postRequest, true, MODULE, cacheFileName, onSuccess, onFail);
        }
    }
}
