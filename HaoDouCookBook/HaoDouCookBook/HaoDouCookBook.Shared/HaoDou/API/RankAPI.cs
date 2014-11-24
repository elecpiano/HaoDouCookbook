using HaoDouCookBook.Common;
using HaoDouCookBook.HaoDou.DataModels.ChoicenessPage;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HaoDouCookBook.HaoDou.API
{
    public class RankAPI
    {
        public const string MODULE = "Rank";

        public static async Task GetRankList(int offset, int limit, int? sign, int? uid, string uuid, Action<RankPageData> onSuccess, Action<Error> onFail)
        {
            string methodName = "getRankList";
            POSTRequestExecuter postRequest = new POSTRequestExecuter(HaoDouApiUrlHelper.GetApiUrl(MODULE, methodName));
            postRequest.AddPostData("offset", offset.ToString());
            postRequest.AddPostData("limit", limit.ToString());
            postRequest.AddPostData("sign", sign.HasValue ? sign.Value.ToString() : string.Empty);
            postRequest.AddPostData("uid", uid.HasValue ? uid.Value.ToString() : string.Empty);
            postRequest.AddPostData("uuid", uuid);

            string cacheFileName = string.Format("{0}-{1}-{2}", methodName, offset, limit);

            HaoDouJsonDataLoader<RankPageData> loader = new HaoDouJsonDataLoader<RankPageData>();
            await loader.LoadAsync(postRequest, true, MODULE, cacheFileName, onSuccess, onFail);
        }

        public static async Task GetRankView(int id, string sign, int? uid, string uuid, int type, Action<RankViewPageData> onSuccess, Action<Error> onFail)
        {
            string methodName = "getRankView";
            POSTRequestExecuter postRequest = new POSTRequestExecuter(HaoDouApiUrlHelper.GetApiUrl(MODULE, methodName));
            postRequest.AddPostData("id", id.ToString());
            postRequest.AddPostData("sign", sign);
            postRequest.AddPostData("uid", uid.HasValue ? uid.Value.ToString() : "0");
            postRequest.AddPostData("uuid", uuid);
            postRequest.AddPostData("type", type.ToString());

            string cacheFileName = string.Format("{0}-{1}-{2}", methodName, id, type);
            HaoDouJsonDataLoader<RankViewPageData> loader = new HaoDouJsonDataLoader<RankViewPageData>();
            await loader.LoadAsync(postRequest, true, MODULE, cacheFileName, onSuccess, onFail);

        }
    }
}
