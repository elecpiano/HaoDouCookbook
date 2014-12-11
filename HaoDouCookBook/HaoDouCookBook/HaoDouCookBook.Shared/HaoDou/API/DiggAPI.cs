using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HaoDouCookBook.HaoDou.DataModels;
using HaoDouCookBook.Common;

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
    }
}
