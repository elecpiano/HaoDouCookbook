using HaoDouCookBook.Infrastructures;
using Shared.Utility;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HaoDouCookBook.Common
{
    public class HaoDouJsonDataLoader<T> where T: class
    {
        /// <summary>
        /// Load data async without cache
        /// </summary>
        /// <param name="leechExecuter"></param>
        /// <param name="onSuccess"></param>
        /// <param name="onFail"></param>
        /// <returns></returns>
        public async Task LoadWithoutCacheAsnyc(ILeechExecuter<string, Error> leechExecuter, Action<T> onSuccess, Action<Error> onFail)
        {
            await LoadAsync(leechExecuter, false, string.Empty, string.Empty, onSuccess, onFail);
        }


        /// <summary>
        /// Load data async
        /// </summary>
        /// <param name="leechExecuter">The executer which is a role to leech data</param>
        /// <param name="cacheData">Cache data or not</param>
        /// <param name="moduleName">The folder which the cache file binplace to</param>
        /// <param name="cacheFileName">The filename want to load/save</param>
        /// <param name="onSuccess">Callback when success</param>
        /// <param name="onFail">Callback when failed</param>
        /// <returns></returns>
        public async Task LoadAsync(ILeechExecuter<string, Error> leechExecuter, bool cacheData, string moduleName, string cacheFileName, Action<T> onSuccess, Action<Error> onFail)
        {
            try
            {

                if (cacheData && string.IsNullOrEmpty(cacheFileName))
                {
                    return;
                }

                // if no internet connection, load cache file
                //
                if (!NetworkHelper.Current.IsInternetConnectionAvaiable)
                {
                    //load cache
                    //
                    if (cacheData)
                    {
                        try
                        {
                            var cachedJson = await IsolatedStorageHelper.ReadFile(moduleName, cacheFileName);
                            T dataObject = JsonSerializer.Deserialize<T>(cachedJson);
                            if (dataObject != null)
                            {
                                App.Current.RunAsync(() =>
                                {
                                    if (onSuccess != null)
                                    {
                                        onSuccess.Invoke(dataObject);
                                    }
                                });
                            }
                        }
                        catch (Exception ex)
                        {
                            if (onFail != null)
                            {
                                onFail.Invoke(new Error() { ErrorCode = ex.HResult, Message = ex.Message });
                            }
                        }
                    }
                    return;
                }


                // if we have internet connection, load data form internet
                //
                await leechExecuter.RunAsync((json) =>
                    {
                        T dataObject = JsonSerializer.Deserialize<T>(json);
                        if (dataObject != null)
                        {
                            App.Current.RunAsync(async () =>
                                {
                                    if (onSuccess != null)
                                    {
                                        onSuccess.Invoke(dataObject);
                                    }

                                    // save to cache if want to cache
                                    //
                                    if (cacheData)
                                    {
                                        await IsolatedStorageHelper.WriteToFile(moduleName, cacheFileName, json);
                                    }
                                });
                        }

                    }, onFail);
            }
            catch (Exception e)
            {
                if (onFail != null)
                {
                    onFail.Invoke(new Error() { ErrorCode = e.HResult, Message = e.Message });
                }
            }
        }
    }
}
