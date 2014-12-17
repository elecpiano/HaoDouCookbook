using HaoDouCookBook.Common;
using HaoDouCookBook.HaoDou.DataModels.Discovery;
using System;
using System.Threading.Tasks;

namespace HaoDouCookBook.HaoDou.API
{
    public class CommentAPI
    {
        public const string MODULE = "Comment";

        public static async Task GetList(int offset, int limit, int type, int cid, int recipeId, Action<CommentListPageData> onSuccess, Action<Error> onFail)
        {
            string methodName = "getList";

            POSTRequestExecuter postRequest = new POSTRequestExecuter(HaoDouApiUrlHelper.GetApiUrl(MODULE, methodName));
            postRequest.AddPostData("offset", offset.ToString());
            postRequest.AddPostData("limit", limit.ToString());
            postRequest.AddPostData("type", type.ToString());
            postRequest.AddPostData("cid", cid.ToString());
            postRequest.AddPostData("rid", recipeId.ToString());

            string cacheFileName = string.Format("{0}-{1}-{2}-{3}-{4}-{5}", methodName, cid, recipeId, type, offset, limit);

            HaoDouJsonDataLoader<CommentListPageData> loader = new HaoDouJsonDataLoader<CommentListPageData>();
            await loader.LoadAsync(postRequest, true, MODULE, cacheFileName, onSuccess, onFail);
        }

        public static async Task AddComment(string content, int uid, string sign, int rid, int type, Action<AddCommentResult> onSuccess, Action<Error> onFail)
        {
            string methodName = "addComment";

            POSTRequestExecuter postRequest = new POSTRequestExecuter(HaoDouApiUrlHelper.GetApiUrl(MODULE, methodName));
            postRequest.AddPostData("type", type.ToString());
            postRequest.AddPostData("rid", rid.ToString());
            postRequest.AddPostData("sign", sign);
            postRequest.AddPostData("content", content);
            postRequest.AddPostData("type", type.ToString());
            postRequest.AddPostData("rrid", string.Empty);
            postRequest.AddPostData("atuid", string.Empty);
            postRequest.AddPostData("uid", uid.ToString());

            HaoDouJsonDataLoader<AddCommentResult> loader = new HaoDouJsonDataLoader<AddCommentResult>();
            await loader.LoadWithoutCacheAsnyc(postRequest, onSuccess, onFail);

        }
    }
}
