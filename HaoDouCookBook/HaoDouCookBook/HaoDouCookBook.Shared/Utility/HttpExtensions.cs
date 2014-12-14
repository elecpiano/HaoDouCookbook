using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Utility
{
    public static class HttpExtensions
    {
        public static Task<HttpWebResponse> GetResponseAsync(this HttpWebRequest request)
        {
            var taskComplete = new TaskCompletionSource<HttpWebResponse>();
            request.BeginGetResponse(asyncResponse =>
            {
                try
                {
                    HttpWebRequest responseRequest = (HttpWebRequest)asyncResponse.AsyncState;
                    HttpWebResponse someResponse =
                       (HttpWebResponse)responseRequest.EndGetResponse(asyncResponse);
                    taskComplete.TrySetResult(someResponse);
                }
                catch (WebException webExc)
                {
                    HttpWebResponse failedResponse = (HttpWebResponse)webExc.Response;
                    taskComplete.TrySetResult(failedResponse);
                }
            }, request);
            return taskComplete.Task;
        }
    }

    public class HttpRequestResult
    {
        public string Content { get; set; }
        public WebHeaderCollection ResponseHeaders { get; set; }

        public HttpRequestResult()
        {
            Content = string.Empty;
            ResponseHeaders = new WebHeaderCollection();
        }
    }

    public class Http
    {
        public const string MULTIPART_BOUNDARY = "6CUS_DxALdoqFbRCOr86dQnoOjTVpFFR";

        public static async Task POSTAsync(string url, 
                                           string data, 
                                           Dictionary<string, string> additionalHttpReqHeaders = null,
                                           Action<HttpRequestResult> callback = null, bool multipart = false)
        {
            try
            {
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(new Uri(url));
                httpWebRequest.Method = "POST";
                httpWebRequest.ContentType = "application/x-www-form-urlencoded";
                if (multipart)
                {
                    httpWebRequest.ContentType = string.Format("multipart/form-data; boundary={0}", MULTIPART_BOUNDARY);
                }

                if (additionalHttpReqHeaders != null)
                {
                    foreach (var kv in additionalHttpReqHeaders)
                    {
                        httpWebRequest.Headers[kv.Key] = kv.Value;
                    }
                }

                if (data != null)
                {
                    byte[] bytes = Encoding.UTF8.GetBytes(data);
                    using (Stream requestStream = await httpWebRequest.GetRequestStreamAsync())
                    {
                        await requestStream.WriteAsync(bytes, 0, bytes.Length);
                    }
                }

                HttpWebResponse response = (HttpWebResponse)await httpWebRequest.GetResponseAsync();
                using (Stream respStream = response.GetResponseStream())
                {
                    HttpRequestResult result = new HttpRequestResult();
                    result.Content = (new StreamReader(respStream)).ReadToEnd();
                    result.ResponseHeaders = response.Headers;
                    if (callback != null)
                    {
                        callback.Invoke(result);
                    }
                }
            }
            catch
            {
                throw;
            }
        }


        public static async Task GETAsync(string url,
                                          Dictionary<string, string> additionalHttpReqHeaders = null,
                                          Action<HttpRequestResult> callback = null)
        {
            try
            {
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(new Uri(url));
                httpWebRequest.Method = "GET";

                if (additionalHttpReqHeaders != null)
                {
                    foreach (var kv in additionalHttpReqHeaders)
                    {
                        httpWebRequest.Headers[kv.Key] = kv.Value;
                    }
                }

                HttpWebResponse response = (HttpWebResponse)await httpWebRequest.GetResponseAsync();
                using (Stream respStream = response.GetResponseStream())
                {
                    HttpRequestResult result = new HttpRequestResult();
                    result.Content = (new StreamReader(respStream)).ReadToEnd();
                    result.ResponseHeaders = response.Headers;
                    if (callback != null)
                    {
                        callback.Invoke(result);
                    }
                }

            }
            catch
            {
                throw;
            }
        }

    }
}
