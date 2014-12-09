using HaoDouCookBook.Common;
using HaoDouCookBook.HaoDou.DataModels.My;
using System;
using System.Collections.Generic;
using System.Text;
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

    }
}
