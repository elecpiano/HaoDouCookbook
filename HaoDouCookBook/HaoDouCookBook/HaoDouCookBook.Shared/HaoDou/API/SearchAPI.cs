using HaoDouCookBook.Common;
using HaoDouCookBook.HaoDou.DataModels.Choiceness;
using HaoDouCookBook.HaoDou.DataModels.ChoicenessPage;
using System;
using System.Threading.Tasks;

namespace HaoDouCookBook.HaoDou.API
{
    public class SearchAPI
    {
        public const string MODULE = "Search";

        /// <summary>
        /// Get the data of tag search result 
        /// </summary>
        /// <param name="offset">Offset of data item</param>
        /// <param name="limit">Limit of data item count</param>
        /// <param name="uuid">Device Id</param>
        /// <param name="tagid">Id of Tags</param>
        /// <param name="keyword">Search keyword</param>
        /// <param name="onSuccess">Callback when success</param>
        /// <param name="onFail">Callbacak when fail</param>
        /// <returns></returns>
        public static async Task GetList(int offset, int limit, string uuid, int? tagid, string keyword, Action<TagsPageData> onSuccess, Action<Error> onFail)
        {
            string methodName = "getList";
            POSTRequestExecuter postRequest = new POSTRequestExecuter(HaoDouApiUrlHelper.GetApiUrl(MODULE, methodName));
            postRequest.AddPostData("offset", offset.ToString());
            postRequest.AddPostData("limit", limit.ToString());
            postRequest.AddPostData("sence", "t2");
            postRequest.AddPostData("tagid", tagid.HasValue ? tagid.Value.ToString() : "null");
            postRequest.AddPostData("uuid", uuid);
            postRequest.AddPostData("keyword", Uri.EscapeDataString(keyword.Trim()));

            string cacheFile = string.Format("{0}-{1}-{2}-{3}", methodName, offset, limit, tagid);

            HaoDouJsonDataLoader<TagsPageData> loader = new HaoDouJsonDataLoader<TagsPageData>();
            await loader.LoadAsync(postRequest, true, MODULE, cacheFile, onSuccess, onFail);
        }

        public static async Task GetHotSearch(Action<SearchPageData> onSuccess, Action<Error> onFail)
        {
            string methodName = "getHotSearch";
            GETRequestExecuter getRequest = new GETRequestExecuter(HaoDouApiUrlHelper.GetApiUrl(MODULE, methodName));

            string cacheFileName = methodName;
            HaoDouJsonDataLoader<SearchPageData> loader = new HaoDouJsonDataLoader<SearchPageData>();
            await loader.LoadAsync(getRequest, true, MODULE, cacheFileName, onSuccess, onFail);
        }

        public static async Task GetSuggestion(string keyword, Action<SearchSuggestionData> onSuccess, Action<Error> onFail)
        {
            string methodName = "getSuggestion";
            POSTRequestExecuter postRequest = new POSTRequestExecuter(HaoDouApiUrlHelper.GetApiUrl(MODULE, methodName));
            postRequest.AddPostData("keyword", Uri.EscapeDataString(keyword.Trim()));
            

            HaoDouJsonDataLoader<SearchSuggestionData> loader = new HaoDouJsonDataLoader<SearchSuggestionData>();
            await loader.LoadWithoutCacheAsnyc(postRequest, onSuccess, onFail);
        }

        public static async Task GetSearchIndex(string keyword, string tagid, string uuid, Action<SearchResultPageData> onSuccess, Action<Error> onFail)
        {
            string methodName = "getSearchIndex";
            POSTRequestExecuter postRequset = new POSTRequestExecuter(HaoDouApiUrlHelper.GetApiUrl(MODULE, methodName));
            postRequset.AddPostData("scene", "k1");
            postRequset.AddPostData("keyword", Uri.EscapeDataString(keyword.Trim()));
            postRequset.AddPostData("tagid", tagid);
            postRequset.AddPostData("uuid", uuid);

            string cacheFileName = string.Format("{0}-{1}", methodName, keyword);
            HaoDouJsonDataLoader<SearchResultPageData> loader = new HaoDouJsonDataLoader<SearchResultPageData>();
            await loader.LoadAsync(postRequset, true, MODULE, cacheFileName, onSuccess, onFail);
        }

        public static async Task GetAlbumList(int offset, int limit, string uuid, int? tagid, string keyword, Action<AlbumListPageData> onSuccess, Action<Error> onFail)
        {
            string methodName = "getAlbumList";
            POSTRequestExecuter postRequest = new POSTRequestExecuter(HaoDouApiUrlHelper.GetApiUrl(MODULE, methodName));
            postRequest.AddPostData("offset", offset.ToString());
            postRequest.AddPostData("limit", limit.ToString());
            postRequest.AddPostData("tagid", tagid.HasValue ? tagid.Value.ToString() : "null");
            postRequest.AddPostData("uuid", uuid);
            postRequest.AddPostData("keyword", Uri.EscapeDataString(keyword.Trim()));

            string cacheFile = string.Format("{0}-{1}-{2}-{3}", methodName, offset, limit, tagid);

            HaoDouJsonDataLoader<AlbumListPageData> loader = new HaoDouJsonDataLoader<AlbumListPageData>();
            await loader.LoadAsync(postRequest, true, MODULE, cacheFile, onSuccess, onFail);

        }

        public static async Task GetTopicList(int offset, int limit, string uuid, int? tagid, string keyword, Action<SearchResultTopicListPageData> onSuccess, Action<Error> onFail)
        {
            string methodName = "getTopicList";

            POSTRequestExecuter postRequest = new POSTRequestExecuter(HaoDouApiUrlHelper.GetApiUrl(MODULE, methodName));
            postRequest.AddPostData("offset", offset.ToString());
            postRequest.AddPostData("limit", limit.ToString());
            postRequest.AddPostData("tagid", tagid.HasValue ? tagid.Value.ToString() : "null");
            postRequest.AddPostData("uuid", uuid);
            postRequest.AddPostData("keyword", Uri.EscapeDataString(keyword.Trim()));

            HaoDouJsonDataLoader<SearchResultTopicListPageData> loader = new HaoDouJsonDataLoader<SearchResultTopicListPageData>();
            await loader.LoadWithoutCacheAsnyc(postRequest, onSuccess, onFail);
        }

        public static async Task GetCateList(string uuid, Action<CategoryPageData> onSuccess, Action<Error> onFail)
        {
            string methodName = "getCateList";
            POSTRequestExecuter postRequeset = new POSTRequestExecuter(HaoDouApiUrlHelper.GetApiUrl(MODULE, methodName));
            postRequeset.AddPostData("uuid", uuid);
            postRequeset.AddPostData("offset", "0");

            string cacheFileName = methodName;

            HaoDouJsonDataLoader<CategoryPageData> loader = new HaoDouJsonDataLoader<CategoryPageData>();
            await loader.LoadAsync(postRequeset, true, MODULE, cacheFileName, onSuccess, onFail);
        }
    }
}
