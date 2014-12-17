using HaoDouCookBook.Common;
using HaoDouCookBook.HaoDou.DataModels.Discovery;
using HaoDouCookBook.HaoDou.DataModels.My;
using System;
using System.Threading.Tasks;

namespace HaoDouCookBook.HaoDou.API
{
    public class RecipePhotoAPI
    {
        public const string MODULE = "Recipephoto";

        public static async Task GetProdcuts(int offset, int limit, int type, int productId, int topicId, string sign, int? uid, string uuid, Action<ProductPageData> onSuccess, Action<Error> onFail)
        {
            string methodName = "getProducts";
            POSTRequestExecuter postRequest = new POSTRequestExecuter(HaoDouApiUrlHelper.GetApiUrl(MODULE, methodName));
            postRequest.AddPostData("offset", offset.ToString());
            postRequest.AddPostData("limit", limit.ToString());
            postRequest.AddPostData("sort", productId.ToString());
            postRequest.AddPostData("id", topicId.ToString());
            postRequest.AddPostData("type", type.ToString());
            postRequest.AddPostData("sign", sign);
            postRequest.AddPostData("uuid", uuid);
            postRequest.AddPostData("uid", uid.HasValue ? uid.Value.ToString() : "0");

            string cacheFileName = string.Format("{0}-{1}-{2}-{3}-{4}", methodName, productId.ToString(), topicId.ToString(),type.ToString(), offset.ToString(), limit.ToString());

            HaoDouJsonDataLoader<ProductPageData> loader = new HaoDouJsonDataLoader<ProductPageData>();
            await loader.LoadAsync(postRequest, true, MODULE, cacheFileName, onSuccess, onFail);
        }

        public static async Task GetPhotoView(int offset, int limit, int id, int? uid, Action<SingleProductViewPageData> onSuccess, Action<Error> onFail)
        {
            string methodName = "photoView";
            POSTRequestExecuter postRequest = new POSTRequestExecuter(HaoDouApiUrlHelper.GetApiUrl(MODULE, methodName));
            postRequest.AddPostData("id", id.ToString());
            postRequest.AddPostData("uid", uid.HasValue ? uid.Value.ToString() : "0");
            postRequest.AddPostData("offset", offset.ToString());
            postRequest.AddPostData("limit", limit.ToString());

            string cacheFileName = string.Format("{0}-{1}-{2}-{3}", methodName, id, offset, limit);
            HaoDouJsonDataLoader<SingleProductViewPageData> loader = new HaoDouJsonDataLoader<SingleProductViewPageData>();
            await loader.LoadAsync(postRequest, true, MODULE, cacheFileName, onSuccess, onFail);
        }

        public static async Task GetList(int offset, int limit, int userId, int uid, string sign, Action<UserProductsData> onSuccess, Action<Error> onFail)
        { 
            string methodName = "getList";
            POSTRequestExecuter postRequest = new POSTRequestExecuter(HaoDouApiUrlHelper.GetApiUrl(MODULE, methodName));
            postRequest.AddPostData("offset", offset.ToString());
            postRequest.AddPostData("limit", limit.ToString());
            postRequest.AddPostData("userid", userId.ToString());
            postRequest.AddPostData("uid", uid.ToString());
            postRequest.AddPostData("sign", sign);

            string cacheFilename = string.Format("{0}-{1}-{2}-{3}", methodName, userId.ToString(), offset.ToString(), limit.ToString());
            HaoDouJsonDataLoader<UserProductsData> loader = new HaoDouJsonDataLoader<UserProductsData>();
            await loader.LoadAsync(postRequest, true, MODULE, cacheFilename, onSuccess, onFail);
        }

        public static async Task GetTopicList(Action<TopicTagsData> onSuccess, Action<Error> onFail)
        {
            string methodName = "getTopicList";
            GETRequestExecuter getRequest = new GETRequestExecuter(HaoDouApiUrlHelper.GetApiUrl(MODULE, methodName));

            HaoDouJsonDataLoader<TopicTagsData> loader = new HaoDouJsonDataLoader<TopicTagsData>();
            await loader.LoadWithoutCacheAsnyc(getRequest, onSuccess, onFail);
        }

        public static async Task Upload(
            string position,
            int uid, 
            int topicid,
            string topicName,
            string intro,
            string title,
            string sign,
            string uuid,
            string siteid,
            string picName,
            ulong lng,
            string picData,
            Action<UploadResultInfo> onSuccess,
            Action<Error> onFail)
        {
            string methodName = "upload";
            POSTRequestExecuter postRequest = new POSTRequestExecuter(HaoDouApiUrlHelper.GetApiUrl(MODULE, methodName));
            postRequest.IsMultipart = true;

            postRequest.AddMultipartData("position", position);
            postRequest.AddMultipartData("uid", uid.ToString());
            postRequest.AddMultipartData("topicId", topicid != 0 ? topicid.ToString() : string.Empty);
            postRequest.AddMultipartData("topicName", topicName);
            postRequest.AddMultipartData("rtitle", title);
            postRequest.AddMultipartData("sign", sign);
            postRequest.AddMultipartData("uuid", uuid);
            postRequest.AddMultipartData("lng", (lng / 1024.0).ToString());
            postRequest.AddMultipartData("siteid", siteid);
            postRequest.AddMultipartData("lat", "38");
            postRequest.AddMultiparFileData("pic", picName, picData);

            HaoDouJsonDataLoader<UploadResultInfo> loader = new HaoDouJsonDataLoader<UploadResultInfo>();
            await loader.LoadWithoutCacheAsnyc(postRequest, onSuccess, onFail);
        }

    }
}
