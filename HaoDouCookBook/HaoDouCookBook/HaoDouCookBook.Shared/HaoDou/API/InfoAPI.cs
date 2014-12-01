using HaoDouCookBook.Common;
using HaoDouCookBook.HaoDou.DataModels.Choiceness;
using HaoDouCookBook.HaoDou.DataModels.ChoicenessPage;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HaoDouCookBook.HaoDou.API
{
    public class InfoAPI
    {
        public const string MODULE = "Info";

        /// <summary>
        /// Get the recipe info page data
        /// </summary>
        /// <param name="sign">Sign of current user</param>
        /// <param name="uid">Id of current user</param>
        /// <param name="uuid">Device id</param>
        /// <param name="recipeId">Recipe id</param>
        /// <param name="onSuccess">Callback when success</param>
        /// <param name="onFail">Callback when fail</param>
        /// <returns></returns>
        public static async Task GetInfo(string sign, int? uid, string uuid, int recipeId, Action<InfoPageData> onSuccess, Action<Error> onFail)
        {
            string methodName = "getInfo";
            POSTRequestExecuter postRequest = new POSTRequestExecuter(HaoDouApiUrlHelper.GetApiUrl(MODULE, methodName));
            postRequest.AddPostData("sign", sign);
            postRequest.AddPostData("uid", uid.HasValue ? uid.Value.ToString() : "0");
            postRequest.AddPostData("uuid", uuid);
            postRequest.AddPostData("rid", recipeId.ToString());
            postRequest.AddPostData("request_id", string.Empty);
            //postRequest.PassLeechedDataDirectly = true;

            string cacheFileName = string.Format("{0}-{1}", methodName, recipeId.ToString());

            HaoDouJsonDataLoader<InfoPageData> loader = new HaoDouJsonDataLoader<InfoPageData>();
            await loader.LoadAsync(postRequest, true, MODULE, cacheFileName, onSuccess, onFail);
 
        }


        public static async Task GetAlbumInfo(int offset, int limit, int albumId, string sign, int? uid, string uuid, Action<AlbumPageData> onSuccess, Action<Error> onFail)
        {
            string methodName = "getAlbumInfo";
            POSTRequestExecuter postRequest = new POSTRequestExecuter(HaoDouApiUrlHelper.GetApiUrl(MODULE, methodName));
            postRequest.AddPostData("offset", offset.ToString());
            postRequest.AddPostData("limit", limit.ToString());
            postRequest.AddPostData("aid", albumId.ToString());
            postRequest.AddPostData("sign", sign);
            postRequest.AddPostData("uid", uid.HasValue ? uid.Value.ToString() : "0");
            postRequest.AddPostData("uuid", uuid);

            string cacheFileName = string.Format("{0}-{1}-{2}-{3}", methodName, albumId, offset, limit);
            HaoDouJsonDataLoader<AlbumPageData> loader = new HaoDouJsonDataLoader<AlbumPageData>();
            await loader.LoadAsync(postRequest, true, MODULE, cacheFileName, onSuccess, onFail);
        }

        public static async Task Shake(string sign, int? uid, string uuid, Action<ShakePageData> onSuccess, Action<Error> onFail)
        {
            string methodName = "shake";
            POSTRequestExecuter postRequest = new POSTRequestExecuter(HaoDouApiUrlHelper.GetApiUrl(MODULE, methodName));
            postRequest.AddPostData("sign", sign);
            postRequest.AddPostData("uid", uid.HasValue ? uid.Value.ToString() : "0");
            postRequest.AddPostData("uuid", uuid.ToString());

            string cacheFileName = methodName;
            HaoDouJsonDataLoader<ShakePageData> loader = new HaoDouJsonDataLoader<ShakePageData>();
            await loader.LoadAsync(postRequest, true, MODULE, methodName, onSuccess, onFail);
        }
    }
}
