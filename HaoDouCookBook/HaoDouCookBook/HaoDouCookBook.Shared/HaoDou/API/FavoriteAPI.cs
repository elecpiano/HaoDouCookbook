using HaoDouCookBook.Common;
using HaoDouCookBook.HaoDou.DataModels;
using HaoDouCookBook.HaoDou.DataModels.My;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HaoDouCookBook.HaoDou.API
{
    public class FavoriteAPI
    {
        public const string MODULE = "Favorite";

        public static async Task AddMyAlbum(int uid, string sign, string title, Action<HaodouResultMessage> onSuccess, Action<Error> onFail)
        {
            string methodName = "addMyAlbum";
            POSTRequestExecuter postRequest = new POSTRequestExecuter(HaoDouApiUrlHelper.GetApiUrl(MODULE, methodName));
            postRequest.AddPostData("uid", uid.ToString());
            postRequest.AddPostData("sign", sign);
            postRequest.AddPostData("title", title);

            HaoDouJsonDataLoader<HaodouResultMessage> loader = new HaoDouJsonDataLoader<HaodouResultMessage>();
            await loader.LoadWithoutCacheAsnyc(postRequest, onSuccess, onFail);
        }

        public static async Task DeleteMyAlbum(int uid, int albumId, string sign, string uuid, Action<HaodouResultMessage> onSuccess, Action<Error> onFail)
        {
            string methodName = "delMyAlbum";
            POSTRequestExecuter postRequest = new POSTRequestExecuter(HaoDouApiUrlHelper.GetApiUrl(MODULE, methodName));
            postRequest.AddPostData("uid", uid.ToString());
            postRequest.AddPostData("sign", sign);
            postRequest.AddPostData("albumid", albumId.ToString());
            postRequest.AddPostData("uuid", uuid);

            HaoDouJsonDataLoader<HaodouResultMessage> loader = new HaoDouJsonDataLoader<HaodouResultMessage>();
            await loader.LoadWithoutCacheAsnyc(postRequest, onSuccess, onFail);
        }

        public static async Task RenameMyAlbum(int albumId, int uid, string title, string sign, Action<HaodouResultMessage> onSuccess, Action<Error> onFail)
        {
            string methodName = "renameMyAlbum"; 

            POSTRequestExecuter postRequest = new POSTRequestExecuter(HaoDouApiUrlHelper.GetApiUrl(MODULE, methodName));
            postRequest.AddPostData("uid", uid.ToString());
            postRequest.AddPostData("sign", sign);
            postRequest.AddPostData("albumid", albumId.ToString());
            postRequest.AddPostData("title", title);

            HaoDouJsonDataLoader<HaodouResultMessage> loader = new HaoDouJsonDataLoader<HaodouResultMessage>();
            await loader.LoadWithoutCacheAsnyc(postRequest, onSuccess, onFail);
        }

        public static async Task GetMyAlbum(int offset, int limit, int uid, string sign, string uuid, Action<FavoriteAlbumsData> onSuccess, Action<Error> onFail)
        {
            await LoadDataAsync<FavoriteAlbumsData>("getMyAlbum", offset, limit, uid, sign, uuid, onSuccess, onFail);
        }
        public static async Task GetCollectList(int offset, int limit, int uid, string sign, string uuid, Action<FavoriteAblumsData> onSuccess, Action<Error> onFail)
        {
            await LoadDataAsync<FavoriteAblumsData>("getCollectList", offset, limit, uid, sign, uuid, onSuccess, onFail);
        }
        public static async Task GetGroupList(int offset, int limit, int uid, string sign, string uuid, Action<FavoriteTopicsData> onSuccess, Action<Error> onFail)
        {
            await LoadDataAsync<FavoriteTopicsData>("getGroupList", offset, limit, uid, sign, uuid, onSuccess, onFail);
        }


        private static async Task LoadDataAsync<T>(
            string methodName, 
            int offset, 
            int limit, 
            int uid, 
            string sign, 
            string uuid, 
            Action<T> onSuccess, 
            Action<Error> onFail) where T : class
        {
            POSTRequestExecuter postRequest = new POSTRequestExecuter(HaoDouApiUrlHelper.GetApiUrl(MODULE, methodName));
            postRequest.AddPostData("offset", offset.ToString());
            postRequest.AddPostData("limit", limit.ToString());
            postRequest.AddPostData("uid", uid.ToString());
            postRequest.AddPostData("sign", sign);
            postRequest.AddPostData("uuid", uuid);

            string cacheFileName = string.Format("{0}-{1}-{2}-{3}", methodName, uid.ToString(), offset.ToString(), limit.ToString());
            HaoDouJsonDataLoader<T> loader = new HaoDouJsonDataLoader<T>();
            await loader.LoadAsync(postRequest, true, MODULE, cacheFileName, onSuccess, onFail);
        }

    }
}
