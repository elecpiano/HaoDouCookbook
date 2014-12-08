using HaoDouCookBook.Common;
using HaoDouCookBook.HaoDou.DataModels.My;
using System;
using System.Threading.Tasks;

namespace HaoDouCookBook.HaoDou.API
{
    public class UserFeedAPI
    {
        public const string MODULE = "Userfeed";

        public static async Task GetFollowUserFeed(int offset, int limit, int userId, int uid, string sign, Action<UserActivitiesData> OnSuccess, Action<Error> onFail)
        { 
            string methodName = "getFollowUserFeed";
            POSTRequestExecuter postRequest = new POSTRequestExecuter(HaoDouApiUrlHelper.GetApiUrl(MODULE, methodName));
            postRequest.AddPostData("uid", uid.ToString());
            postRequest.AddPostData("sign", sign);
            postRequest.AddPostData("v_uid", userId.ToString());
            postRequest.AddPostData("offst", offset.ToString());
            postRequest.AddPostData("limit", limit.ToString());

            string cacheFileName = string.Format("{0}-{1}-{2}-{3}", methodName, userId.ToString(), offset.ToString(), limit.ToString());
            HaoDouJsonDataLoader<UserActivitiesData> loader = new HaoDouJsonDataLoader<UserActivitiesData>();
            await loader.LoadAsync(postRequest, true, MODULE, cacheFileName, OnSuccess, onFail);
        }
    }
}
