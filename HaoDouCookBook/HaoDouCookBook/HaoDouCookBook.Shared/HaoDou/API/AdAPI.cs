using HaoDouCookBook.Common;
using HaoDouCookBook.HaoDou.DataModels.My;
using System;
using System.Threading.Tasks;

namespace HaoDouCookBook.HaoDou.API
{
    public class AdAPI
    {
        public const string MODULE = "Ad";

        public static async Task GetRecommendAdList(int offset, string uuid, Action<RecommendationPageData> onSuccess, Action<Error> onFail)
        {
            string methodName = "getRecommendAdList";
            POSTRequestExecuter postRequest = new POSTRequestExecuter(HaoDouApiUrlHelper.GetApiUrl(MODULE, methodName));
            postRequest.AddPostData("offset", offset.ToString());
            postRequest.AddPostData("uuid", uuid);

            string cacheFileName = string.Format("{0}-{1}", methodName, offset.ToString());

            HaoDouJsonDataLoader<RecommendationPageData> loader = new HaoDouJsonDataLoader<RecommendationPageData>();
            await loader.LoadAsync(postRequest, true, MODULE, cacheFileName, onSuccess, onFail);
        }
    }
}
