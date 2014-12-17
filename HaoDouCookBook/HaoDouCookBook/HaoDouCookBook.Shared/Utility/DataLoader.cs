using HaoDouCookBook;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.Serialization;

namespace Shared.Utility
{
    public class GenericDataLoader<T> where T : class
    {
        public bool Busy = false;
        public bool Loaded = false;

        private Action<T> onCallback;
        string moduleName = string.Empty;
        string fileName = string.Empty;
        private bool toCacheData = false;

        public void LoadWithoutCaching(string dataURL, Action<T> callback)
        {
            this.Load(dataURL, false, string.Empty, string.Empty, callback);
        }

        /* don't even try to convert this method into an awaitable method, as the callback should be called twice: 
         * first when local cache is loaded,  second when the new data is downloaded. Async-callback approach is better solution
         * than awaitable method for such use case.
        */
        public async void Load(string dataURL, bool cacheData, string module, string file, Action<T> callback)
        {
            if (cacheData && (string.IsNullOrEmpty(module) || string.IsNullOrEmpty(file)))
            {
                return;
            }

            //for callback
            onCallback = callback;
            toCacheData = cacheData;
            moduleName = module;
            fileName = file;

            if (!NetworkHelper.Current.IsInternetConnectionAvaiable)
            {
                //load cache
                if (cacheData)
                {
                    try
                    {
                        var cachedJson = await IsolatedStorageHelper.ReadFileAsync(moduleName, fileName);
                        T obj = JsonSerializer.Deserialize<T>(cachedJson);
                        if (obj != null)
                        {
                            App.CurrentInstance.RunAsync(() =>
                            {
                                onCallback(obj);
                            });
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                }
                return;
            }

            //download new
            try
            {
                HttpWebRequest request = HttpWebRequest.CreateHttp(new Uri(dataURL));
                request.Method = "GET";
                request.BeginGetResponse(GetData_Callback, request);

                Loaded = false;
                Busy = true;
            }
            catch (WebException e)
            {
            }
            catch (Exception e)
            {
            }
        }

        private async void GetData_Callback(IAsyncResult result)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)result.AsyncState;
                WebResponse response = request.EndGetResponse(result);

                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    string json = reader.ReadToEnd();
                    T obj = JsonSerializer.Deserialize<T>(json);
                    if (obj != null)
                    {
                        App.CurrentInstance.RunAsync(() =>
                        {
                            onCallback(obj);
                        });
                    }

                    if (toCacheData)
                    {
                        await IsolatedStorageHelper.WriteToFileAsync(moduleName, fileName, json);
                    }
                }
                Loaded = true;
            }
            catch (Exception)
            {
            }
            finally
            {
                Busy = false;
            }
        }
    }

    public class DataLoader<T> where T : class
    {
        public bool Busy = false;
        public bool Loaded = false;

        private Action<T> onCallback;
        string moduleName = string.Empty;
        string fileName = string.Empty;
        private bool toCacheData = false;

        public void LoadWithoutCaching(string dataURL, Action<T> callback)
        {
            this.Load(dataURL, false, string.Empty, string.Empty, callback);
        }

        /* don't even try to convert this method into an awaitable method, as the callback should be called twice: 
         * first when local cache is loaded,  second when the new data is downloaded. Async-callback approach is better solution
         * than awaitable method for such use case.
        */
        public async void Load(string dataURL, bool cacheData, string module, string file, Action<T> callback)
        {
            if (cacheData && (string.IsNullOrEmpty(module) || string.IsNullOrEmpty(file)))
            {
                return;
            }

            //for callback
            onCallback = callback;
            toCacheData = cacheData;
            moduleName = module;
            fileName = file;

            if (!NetworkHelper.Current.IsInternetConnectionAvaiable)
            {
                //load cache
                if (cacheData)
                {
                    try
                    {
                        var cachedJson = await IsolatedStorageHelper.ReadFileAsync(moduleName, fileName);
                        JsonObjectWrapper<T> wrapper = JsonSerializer.Deserialize<JsonObjectWrapper<T>>(cachedJson);
                        if (wrapper != null && wrapper.data != null)
                        {
                            App.CurrentInstance.RunAsync(() =>
                            {
                                onCallback(wrapper.data);
                            });
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                }
                return;
            }

            //download new
            try
            {
                HttpWebRequest request = HttpWebRequest.CreateHttp(new Uri(dataURL));
                request.Method = "GET";
                request.BeginGetResponse(GetData_Callback, request);

                Loaded = false;
                Busy = true;
            }
            catch (WebException e)
            {
            }
            catch (Exception e)
            {
            }
        }

        private async void GetData_Callback(IAsyncResult result)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)result.AsyncState;
                WebResponse response = request.EndGetResponse(result);

                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    string json = reader.ReadToEnd();
                    JsonObjectWrapper<T> wrapper = JsonSerializer.Deserialize<JsonObjectWrapper<T>>(json);
                    if (wrapper != null && wrapper.data != null)
                    {
                        App.CurrentInstance.RunAsync(() =>
                        {
                            onCallback(wrapper.data);
                        });
                    }

                    if (toCacheData)
                    {
                        await IsolatedStorageHelper.WriteToFileAsync(moduleName, fileName, json);
                    }
                }
                Loaded = true;
            }
            catch (Exception)
            {
            }
            finally
            {
                Busy = false;
            }
        }
    }

    [DataContract]
    public class JsonObjectWrapper<T>
    {
        [DataMember]
        public T data { get; set; }
    }

    public class ListDataLoader<T>
    {
        public bool Busy = false;
        public bool Loaded = false;

        private Action<List<T>> onCallback;
        string moduleName = string.Empty;
        string fileName = string.Empty;
        private bool toCacheData = false;

        //for comparison
        private List<T> _LoadedList = new List<T>();
        private Func<T, T, bool> _Comparison;

        public void Load(string dataURL, Action<List<T>> callback)
        {
            this.Load(dataURL, false, string.Empty, string.Empty, callback);
        }

        public async void Load(string dataURL, bool cacheData, string module, string file, Action<List<T>> callback)
        {
            if (cacheData && (string.IsNullOrEmpty(module) || string.IsNullOrEmpty(file)))
            {
                return;
            }

            //for callback
            onCallback = callback;
            moduleName = module;
            fileName = file;
            toCacheData = cacheData;

            if (!NetworkHelper.Current.IsInternetConnectionAvaiable)
            {
                //load cache
                if (cacheData)
                {
                    try
                    {
                        var cachedJson = await IsolatedStorageHelper.ReadFileAsync(moduleName, fileName);
                        JsonArrayWrapper<T> wrapper = JsonSerializer.Deserialize<JsonArrayWrapper<T>>(cachedJson);
                        if (wrapper != null && wrapper.data != null)
                        {
                           App.CurrentInstance.RunAsync(() =>
                            {
                                List<T> list = new List<T>();
                                for (int i = 0; i < wrapper.data.Length; i++)
                                {
                                    list.Add(wrapper.data[i]);
                                }
                                onCallback(list);
                            });
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                }
                return;
            }

            //download new
            try
            {
                HttpWebRequest request = HttpWebRequest.CreateHttp(new Uri(dataURL));
                request.Method = "GET";
                request.BeginGetResponse(GetData_Callback, request);

                Loaded = false;
                Busy = true;
            }
            catch (Exception e)
            {
            }
        }

        private async void GetData_Callback(IAsyncResult result)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)result.AsyncState;
                WebResponse response = request.EndGetResponse(result);

                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    string json = reader.ReadToEnd();

                    JsonArrayWrapper<T> wrapper = JsonSerializer.Deserialize<JsonArrayWrapper<T>>(json);
                    if (wrapper != null && wrapper.data != null)
                    {
                        App.CurrentInstance.RunAsync(() =>
                        {
                            List<T> list = new List<T>();
                            for (int i = 0; i < wrapper.data.Length; i++)
                            {
                                list.Add(wrapper.data[i]);
                            }
                            onCallback(list);
                        });
                    }

                    if (toCacheData)
                    {
                        await IsolatedStorageHelper.WriteToFileAsync(moduleName, fileName, json);
                    }
                }
                Loaded = true;
            }
            catch (Exception e)
            {
            }
            finally
            {
                Busy = false;
            }
        }

        /// <summary>
        /// Re-Executes the callback method ONLY when the downloaed data listis different from the list loaded from local cache
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="param"></param>
        /// <param name="cacheData"></param>
        /// <param name="module"></param>
        /// <param name="file"></param>
        /// <param name="callback"></param>
        /// <param name="comparison"></param>
        public async void Load(string dataURL, bool cacheData, string module, string file, Action<List<T>> callback, Func<T, T, bool> comparison)
        {
            {
                if (cacheData && (string.IsNullOrEmpty(module) || string.IsNullOrEmpty(file)))
                {
                    return;
                }

                //for callback
                onCallback = callback;
                moduleName = module;
                fileName = file;
                toCacheData = cacheData;

                _Comparison = comparison;

                if (!NetworkHelper.Current.IsInternetConnectionAvaiable)
                {
                    //load cache
                    if (cacheData)
                    {
                        try
                        {
                            var cachedJson = await IsolatedStorageHelper.ReadFileAsync(moduleName, fileName);
                            JsonArrayWrapper<T> wrapper = JsonSerializer.Deserialize<JsonArrayWrapper<T>>(cachedJson);
                            if (wrapper != null && wrapper.data != null)
                            {
                                App.CurrentInstance.RunAsync(() =>
                                {
                                    _LoadedList.Clear();
                                    for (int i = 0; i < wrapper.data.Length; i++)
                                    {
                                        _LoadedList.Add(wrapper.data[i]);
                                    }
                                    onCallback(_LoadedList);
                                });
                            }
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                    return;
                }

                //download new
                try
                {
                    HttpWebRequest request = HttpWebRequest.CreateHttp(new Uri(dataURL));
                    request.Method = "GET";
                    request.BeginGetResponse(GetData_Callback2, request);

                    Loaded = false;
                    Busy = true;
                }
                catch (Exception e)
                {
                }
            }
        }

        private async void GetData_Callback2(IAsyncResult result)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)result.AsyncState;
                WebResponse response = request.EndGetResponse(result);

                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    string json = reader.ReadToEnd();

                    JsonArrayWrapper<T> wrapper = JsonSerializer.Deserialize<JsonArrayWrapper<T>>(json);
                    if (wrapper != null && wrapper.data != null)
                    {
                        App.CurrentInstance.RunAsync(() =>
                        {
                            List<T> list = new List<T>();
                            for (int i = 0; i < wrapper.data.Length; i++)
                            {
                                list.Add(wrapper.data[i]);
                            }

                            //compare list, and execute the callback method only when difference is detected
                            if (CompareList(list, _LoadedList, _Comparison))
                            {
                                onCallback(list);
                            }
                        });
                    }

                    if (toCacheData)
                    {
                        await IsolatedStorageHelper.WriteToFileAsync(moduleName, fileName, json);
                    }
                }
                Loaded = true;
            }
            catch (Exception e)
            {
            }
            finally
            {
                Busy = false;
            }
        }

        private static bool CompareList(List<T> list1, List<T> list2, Func<T, T, bool> comparison)
        {
            bool isEqual = true;

            foreach (var item1 in list1)
            {
                foreach (var item2 in list2)
                {
                    if (!CompareItems(item1, item2, comparison))
                    {
                        isEqual = false;
                        break;
                    }
                }
            }

            foreach (var item2 in list2)
            {
                foreach (var item1 in list1)
                {
                    if (!CompareItems(item1, item2, comparison))
                    {
                        isEqual = false;
                        break;
                    }
                }
            }

            return isEqual;
        }

        private static bool CompareItems(T item1, T item2, Func<T, T, bool> comparison)
        {
            bool isEqual = false;
            if (comparison(item1, item2))
            {
                isEqual = true;
            }
            return isEqual;
        }
    }

    [DataContract]
    public class JsonArrayWrapper<T>
    {
        [DataMember]
        public T[] data { get; set; }
    }
}
