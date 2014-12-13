using HaoDouCookBook.Common;
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
    }
}
