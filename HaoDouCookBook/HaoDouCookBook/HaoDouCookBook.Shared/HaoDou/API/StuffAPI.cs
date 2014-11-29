using HaoDouCookBook.Common;
using HaoDouCookBook.HaoDou.DataModels.Choiceness;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HaoDouCookBook.HaoDou.API
{
    public class StuffAPI
    {
        public const string MODULE = "Stuff";

        public static async Task GetStuffInfo(int id, Action<StuffData> onSuccess, Action<Error> onFail)
        {
            string methodName = "getStuffInfo";
            POSTRequestExecuter postRequest = new POSTRequestExecuter(HaoDouApiUrlHelper.GetApiUrl(MODULE, methodName));
            postRequest.AddPostData("id", id.ToString());

            string cacheFileName = string.Format("{0}-{1}", methodName, id);

            HaoDouJsonDataLoader<StuffData> loader = new HaoDouJsonDataLoader<StuffData>();
            await loader.LoadAsync(postRequest, true, MODULE, cacheFileName, onSuccess, onFail);

        }

    }
}
