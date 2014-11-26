using HaoDouCookBook.Common;
using HaoDouCookBook.HaoDou.DataModels.Discovery;
using System;
using System.Collections.Generic;
using System.Text;
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
    }
}
