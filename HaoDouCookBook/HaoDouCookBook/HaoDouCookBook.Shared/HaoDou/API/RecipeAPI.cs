using HaoDouCookBook.Common;
using HaoDouCookBook.HaoDou.DataModels.ChoicenessPage;
using HaoDouCookBook.HaoDou.DataModels.DiscoveryPage;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HaoDouCookBook.HaoDou.API
{
    public class RecipeAPI
    {
        public const string Moudle = "Recipe";

        public static async Task GetCollectList(int offset, int limit, int? type, Action<ChoicenessPageData> OnSuccess, Action<Error> OnFail)
        {
            string methodName = "getCollectList";
            POSTRequestExecuter postRequest = new POSTRequestExecuter(HaoDouApiUrlHelper.GetApiUrl(Moudle, methodName));
            postRequest.AddPostData("offset", offset.ToString());
            postRequest.AddPostData("limit", limit.ToString());

            if (type.HasValue)
            {
                postRequest.AddPostData("type", type.Value.ToString());
            }
            string cacheFileName = string.Format("{0}-{1}-{2}-{3}", methodName, offset, limit, type);

            HaoDouJsonDataLoader<ChoicenessPageData> loader = new HaoDouJsonDataLoader<ChoicenessPageData>();
            await loader.LoadAsync(postRequest, true, Moudle, cacheFileName, OnSuccess, OnFail);
        }

        public static async Task GetDiscoveryData(Action<DiscoveryPageData> onSuccess, Action<Error> onFail)
        {
            string methodName = "getfindRecipe";
            GETRequestExecuter getRequst = new GETRequestExecuter(HaoDouApiUrlHelper.GetApiUrl(Moudle, methodName));
            string cacheFileName = methodName;
            HaoDouJsonDataLoader<DiscoveryPageData> loader = new HaoDouJsonDataLoader<DiscoveryPageData>();
            await loader.LoadAsync(getRequst, true, Moudle, cacheFileName, onSuccess, onFail);
        }
    }
}
