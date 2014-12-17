using HaoDouCookBook.Common;
using HaoDouCookBook.HaoDou.DataModels.Choiceness;
using HaoDouCookBook.HaoDou.DataModels.ChoicenessPage;
using HaoDouCookBook.HaoDou.DataModels.Discovery;
using System;
using System.Threading.Tasks;

namespace HaoDouCookBook.HaoDou.API
{
    public class RecipeAPI
    {
        public const string Moudle = "Recipe";

        /// <summary>
        /// Get dada of Chocineess page
        /// </summary>
        /// <param name="offset">Offset of data item</param>
        /// <param name="limit">Limit of data cout</param>
        /// <param name="type">Type of data</param>
        /// <param name="OnSuccess">Callback when success</param>
        /// <param name="OnFail">Callback when fail</param>
        /// <returns></returns>
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

        /// <summary>
        /// Get data of Discovery page
        /// </summary>
        /// <param name="onSuccess">Callback when success</param>
        /// <param name="onFail">Callback when fail</param>
        /// <returns></returns>
        public static async Task GetDiscoveryData(Action<DiscoveryPageData> onSuccess, Action<Error> onFail)
        {
            string methodName = "getfindRecipe";
            GETRequestExecuter getRequst = new GETRequestExecuter(HaoDouApiUrlHelper.GetApiUrl(Moudle, methodName));
            string cacheFileName = methodName;
            HaoDouJsonDataLoader<DiscoveryPageData> loader = new HaoDouJsonDataLoader<DiscoveryPageData>();
            await loader.LoadAsync(getRequst, true, Moudle, cacheFileName, onSuccess, onFail);
        }

        /// <summary>
        /// Get data of recipe recommend page
        /// </summary>
        /// <param name="offset">Offset of data item</param>
        /// <param name="limit">Limit of data item count</param>
        /// <param name="sign">Sign</param>
        /// <param name="uid">User id</param>
        /// <param name="uuid">Divce id?</param>
        /// <param name="type">Recipe type name</param>
        /// <param name="recipeId">Id of recipe recommend</param>
        /// <param name="onSuccess"></param>
        /// <param name="onFail"></param>
        /// <returns></returns>
        public static async Task GetCollectRecomment(int offset, int limit, string sign, int? uid, string uuid, string type, int recipeId, Action<RecipeRecommendPageData> onSuccess, Action<Error> onFail)
        {
            string methodName = "getCollectRecomment";
            POSTRequestExecuter postRequest = new POSTRequestExecuter(HaoDouApiUrlHelper.GetApiUrl(Moudle, methodName));
            postRequest.AddPostData("offset", offset.ToString());
            postRequest.AddPostData("limit", limit.ToString());
            postRequest.AddPostData("sign", sign);
            postRequest.AddPostData("uid", uid.HasValue ? uid.Value.ToString() : string.Empty);
            postRequest.AddPostData("uuid", uuid);
            postRequest.AddPostData("type", type);
            postRequest.AddPostData("rid", recipeId.ToString());

            string filename = string.Format("{0}-{1}-{2}-{3}", methodName, offset.ToString(), limit.ToString(), recipeId.ToString());

            HaoDouJsonDataLoader<RecipeRecommendPageData> loader = new HaoDouJsonDataLoader<RecipeRecommendPageData>();
            await loader.LoadAsync(postRequest, true, Moudle, filename, onSuccess, onFail);
        }

        public static async Task GetAlbumList(int offset, int limit, string uuid, Action<AllAlbumsData> onSuccess, Action<Error> onFail)
        {
            string methodName = "getAlbumList";
            POSTRequestExecuter postRequest = new POSTRequestExecuter(HaoDouApiUrlHelper.GetApiUrl(Moudle, methodName));
            postRequest.AddPostData("offset", offset.ToString());
            postRequest.AddPostData("limit", limit.ToString());
            postRequest.AddPostData("uuid", uuid);

            string filename = string.Format("{0}-{1}-{2}", methodName, offset.ToString(), limit.ToString());

            HaoDouJsonDataLoader<AllAlbumsData> loader = new HaoDouJsonDataLoader<AllAlbumsData>();
            await loader.LoadAsync(postRequest, true, Moudle, filename, onSuccess, onFail);

        }
    }
}
