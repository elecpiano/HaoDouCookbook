using HaoDouCookBook.Common;
using HaoDouCookBook.HaoDou.DataModels;
using HaoDouCookBook.HaoDou.DataModels.My;
using System;
using System.Collections.Generic;
using System.Text;
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
        public static async Task GetFans(int offset, int limit, int userId, int uid, string sign, Action<UserFollowersData> onSuccess, Action<Error> onFail)
        {
            string methodName = "getFans";
            POSTRequestExecuter postRequest = new POSTRequestExecuter(HaoDouApiUrlHelper.GetApiUrl(MODULE, methodName));
            postRequest.AddPostData("uid", uid.ToString());
            postRequest.AddPostData("sign", sign);
            postRequest.AddPostData("userid", userId.ToString());
            postRequest.AddPostData("offst", offset.ToString());
            postRequest.AddPostData("limit", limit.ToString());

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

    }
}
