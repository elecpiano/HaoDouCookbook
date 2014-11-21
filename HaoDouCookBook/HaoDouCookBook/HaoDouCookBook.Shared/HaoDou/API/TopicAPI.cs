using HaoDouCookBook.Common;
using HaoDouCookBook.HaoDou.DataModels.SquarePage;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HaoDouCookBook.HaoDou.API
{
    public class TopicAPI
    {
        public const string MODULE = "Topic";

        /// <summary>
        /// Get data of Square page
        /// </summary>
        /// <param name="offset">Offset of data item</param>
        /// <param name="limit">Limit of data item cout</param>
        /// <param name="onSuccess">Callback when success</param>
        /// <param name="onFail">Callback when fail</param>
        /// <returns></returns>
        public static async Task GetGroupIndexData(int offset, int limit, Action<SquarePageData> onSuccess, Action<Error> onFail)
        {
            string methodName = "getGroupIndexData";
            POSTRequestExecuter postRequest = new POSTRequestExecuter(HaoDouApiUrlHelper.GetApiUrl(MODULE, methodName));
            postRequest.AddPostData("offset", offset.ToString());
            postRequest.AddPostData("limit", limit.ToString());

            string fileName = string.Format("{0}-{1}-{2}", methodName, offset.ToString(), limit.ToString());

            HaoDouJsonDataLoader<SquarePageData> loader = new HaoDouJsonDataLoader<SquarePageData>();
            await loader.LoadAsync(postRequest, true, MODULE, fileName, onSuccess, onFail);
        }

        /// <summary>
        /// Get a list of data of recipe category
        /// </summary>
        /// <param name="offset">Offset of data item</param>
        /// <param name="limit">Limit of data item count</param>
        /// <param name="cateId">Category Id</param>
        /// <param name="uid">user id</param>
        /// <param name="onSuccess">Callback when success</param>
        /// <param name="onFail">Callback when fail</param>
        /// <returns></returns>
        public static async Task GetList(int offset, int limit, int cateId, int? uid, Action<TopicCollection> onSuccess, Action<Error> onFail)
        {
            string methodName = "getList";
            POSTRequestExecuter postRequest = new POSTRequestExecuter(HaoDouApiUrlHelper.GetApiUrl(MODULE, methodName));
            postRequest.AddPostData("offset", offset.ToString());
            postRequest.AddPostData("limit", limit.ToString());
            postRequest.AddPostData("cate_id", cateId.ToString());
            postRequest.AddPostData("uid", uid.HasValue ? uid.Value.ToString() : string.Empty);

            HaoDouJsonDataLoader<TopicCollection> loader = new HaoDouJsonDataLoader<TopicCollection>();

            string fileName = string.Format("{0}-{1}-{2}-{3}", methodName, offset.ToString(), limit.ToString(), cateId.ToString());

            await loader.LoadAsync(postRequest, true, MODULE, fileName, onSuccess, onFail);

        }
    }
}
