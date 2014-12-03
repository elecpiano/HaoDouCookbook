using Shared.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace HaoDouCookBook.Common
{
    public class HaoDouSearchHelper
    {
        public const string SEARCH_HISTORY_FILENAME = "serachLog.json";

        public static async Task AddSearchKeywordAsync(string keyword)
        {
            if (string.IsNullOrEmpty(keyword))
            {
                return;
            }

            var searchLogs = await GetAllKeywordsAsync();
            if(searchLogs.All(log => !log.Equals(keyword, StringComparison.OrdinalIgnoreCase)))
            {
                searchLogs.Add(keyword);
                string json = JsonSerializer.Serialize(new Data() { SearchLogs = searchLogs });
                await IsolatedStorageHelper.WriteToFileAsync(Constants.LOCAL_USERDATA_FOLDER, SEARCH_HISTORY_FILENAME, json);
            }
        }

        public static async Task ClearAllKeywordsAsync()
        {
            string emptyJson = JsonSerializer.Serialize(new Data());
            await IsolatedStorageHelper.WriteToFileAsync(Constants.LOCAL_USERDATA_FOLDER, SEARCH_HISTORY_FILENAME, emptyJson);
        }


        public static async Task<List<string>> GetAllKeywordsAsync()
        {
            string logJson = await IsolatedStorageHelper.ReadFileAsync(Constants.LOCAL_USERDATA_FOLDER, SEARCH_HISTORY_FILENAME);
            if (string.IsNullOrEmpty(logJson))
            {
                return new List<string>();
            }
            else
            {
                Data data = JsonSerializer.Deserialize<Data>(logJson);
                if (data != null)
                {
                    return data.SearchLogs;
                }
                else
                {
                    return new List<string>();
                }
            }
        }

        [DataContract]
        private class Data
        {
            [DataMember(Name = "keywords")]
            public List<string> SearchLogs { get; set; }

            public Data()
            {
                SearchLogs = new List<string>();
            }
        }
    }
}
