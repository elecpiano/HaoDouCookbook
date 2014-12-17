using HaoDouCookBook.Common;
using HaoDouCookBook.HaoDou.DataModels;
using HaoDouCookBook.HaoDou.DataModels.My;
using System;
using System.Threading.Tasks;

namespace HaoDouCookBook.HaoDou.API
{
    public class RecipeUserAPI
    {
        public const string MODULE = "RecipeUser";

        public static async Task GetUserInfo(int userId, int uid, string sign, Action<UserProfile> onSuccess, Action<Error> onFail)
        {
            string methodName = "getUserInfo";

            POSTRequestExecuter postRequest = new POSTRequestExecuter(HaoDouApiUrlHelper.GetApiUrl(MODULE, methodName));
            postRequest.AddPostData("userid", userId.ToString());
            postRequest.AddPostData("uid", uid.ToString());
            postRequest.AddPostData("sign", sign);

            HaoDouJsonDataLoader<UserProfile> loader = new HaoDouJsonDataLoader<UserProfile>();
            await loader.LoadWithoutCacheAsnyc(postRequest, onSuccess, onFail);
        }

        public static async Task Follow(int friendId, int uid, string sign, Action<HaodouResultMessage> onSuccess, Action<Error> onFail)
        {
            string methodName = "follow";
            POSTRequestExecuter postRequest = new POSTRequestExecuter(HaoDouApiUrlHelper.GetApiUrl(MODULE, methodName));
            postRequest.AddPostData("fid", friendId.ToString());
            postRequest.AddPostData("uid", uid.ToString());
            postRequest.AddPostData("sign", sign);

            HaoDouJsonDataLoader<HaodouResultMessage> loader = new HaoDouJsonDataLoader<HaodouResultMessage>();
            await loader.LoadWithoutCacheAsnyc(postRequest, onSuccess, onFail);
        }

        public static async Task GetFollows(int offset, int limit, int userId, int uid, string sign, Action<UserFollowersData> onSuccess, Action<Error> onFail)
        {
            string methodName = "getFollows";
            POSTRequestExecuter postRequest = new POSTRequestExecuter(HaoDouApiUrlHelper.GetApiUrl(MODULE, methodName));
            postRequest.AddPostData("uid", uid.ToString());
            postRequest.AddPostData("sign", sign);
            postRequest.AddPostData("userid", userId.ToString());
            postRequest.AddPostData("offst", offset.ToString());
            postRequest.AddPostData("limit", limit.ToString());

            HaoDouJsonDataLoader<UserFollowersData> loader = new HaoDouJsonDataLoader<UserFollowersData>();
            await loader.LoadWithoutCacheAsnyc(postRequest, onSuccess, onFail);
 
        }
        public static async Task GetFans(int offset, int limit, int userId, int uid, string uuid, string sign, bool refresh, Action<UserFollowersData> onSuccess, Action<Error> onFail)
        {
            string methodName = "getFans";
            POSTRequestExecuter postRequest = new POSTRequestExecuter(HaoDouApiUrlHelper.GetApiUrl(MODULE, methodName));
            postRequest.AddPostData("uid", uid.ToString());
            postRequest.AddPostData("sign", sign);
            postRequest.AddPostData("userid", userId.ToString());
            postRequest.AddPostData("offst", offset.ToString());
            postRequest.AddPostData("limit", limit.ToString());
            postRequest.AddPostData("uuid", uuid);

            if(refresh)
            {
                postRequest.AddPostData("refresh", "1");
            }

            HaoDouJsonDataLoader<UserFollowersData> loader = new HaoDouJsonDataLoader<UserFollowersData>();
            await loader.LoadWithoutCacheAsnyc(postRequest, onSuccess, onFail);
 
        }

        public static async Task UnFollow(int friendId, int uid, string sign, Action<HaodouResultMessage> onSuccess, Action<Error> onFail)
        {
            string methodName = "unfollow";
            POSTRequestExecuter postRequest = new POSTRequestExecuter(HaoDouApiUrlHelper.GetApiUrl(MODULE, methodName));
            postRequest.AddPostData("fid", friendId.ToString());
            postRequest.AddPostData("uid", uid.ToString());
            postRequest.AddPostData("sign", sign);

            HaoDouJsonDataLoader<HaodouResultMessage> loader = new HaoDouJsonDataLoader<HaodouResultMessage>();
            await loader.LoadWithoutCacheAsnyc(postRequest, onSuccess, onFail);
        }

        public static async Task GetUserRecipeList(int offset, int limit, int userId, int uid, Action<UserRecipesData> onSuccess, Action<Error> onFail)
        {
            string methodName = "getUserRecipeList";
            POSTRequestExecuter postRequest = new POSTRequestExecuter(HaoDouApiUrlHelper.GetApiUrl(MODULE, methodName));
            postRequest.AddPostData("offset", offset.ToString());
            postRequest.AddPostData("limit", limit.ToString());
            postRequest.AddPostData("userid", userId.ToString());
            postRequest.AddPostData("uid", uid.ToString());


            string cacheFilename = string.Format("{0}-{1}-{2}-{3}", methodName, userId.ToString(), offset.ToString(), limit.ToString());
            HaoDouJsonDataLoader<UserRecipesData> loader = new HaoDouJsonDataLoader<UserRecipesData>();
            await loader.LoadAsync(postRequest, true, MODULE, cacheFilename, onSuccess, onFail);
        }

        public static async Task GetFriendList(int offset, int limit, int uid, string sign, Action<FriendsData> onSuccess, Action<Error> onFail)
        { 
            string methodName = "getFriendList";
            POSTRequestExecuter postRequest = new POSTRequestExecuter(HaoDouApiUrlHelper.GetApiUrl(MODULE, methodName));
            postRequest.AddPostData("offset", offset.ToString());
            postRequest.AddPostData("limit", limit.ToString());
            postRequest.AddPostData("uid", uid.ToString());
            postRequest.AddPostData("sign", sign);
            postRequest.AddPostData("is_get_last_fans_count", "1");

            string cacheFilename = string.Format("{0}-{1}-{2}-{3}", methodName, uid.ToString(), offset.ToString(), limit.ToString());
            HaoDouJsonDataLoader<FriendsData> loader = new HaoDouJsonDataLoader<FriendsData>();
            await loader.LoadAsync(postRequest, true, MODULE, cacheFilename, onSuccess, onFail);
        }

    }
}
