using HaoDouCookBook.HaoDou.API;
using HaoDouCookBook.HaoDou.DataModels;
using Shared.Infrastructures;
using Shared.Utility;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;

namespace HaoDouCookBook.Common
{
    public class Error
    {
        public int ErrorCode { get; set; }
        public string Message { get; set; }

        public Error()
        {
            ErrorCode = -1;
            Message = string.Empty;
        }
    }

    public class POSTRequestExecuter : ILeechExecuter<string, Error>
    {
        private string apiUrl;
        private Dictionary<string, string> postData;
        private Dictionary<string, string> additionalRequestHeaders;
        public StringBuilder multipartDataBuilder;

        public bool IsMultipart { get; set; }

        /// <summary>
        /// If true, will pass leeched data directly
        /// </summary>
        public bool PassLeechedDataDirectly { get; set; }

        public POSTRequestExecuter()
        {
            PassLeechedDataDirectly = false;
            multipartDataBuilder = new StringBuilder();
            multipartDataBuilder.Append(string.Format("--{0}", Http.MULTIPART_BOUNDARY));
            IsMultipart = false;
        }

        public POSTRequestExecuter(string url)
        {
            this.apiUrl = url;
        }

        public void AddMultipart8BitData(string key, string value)
        {
            multipartDataBuilder.AppendLine("Content-Type: text/plain; charset=UTF-8");
            multipartDataBuilder.AppendLine("Content-Transfer-Encoding: 8bit");
            multipartDataBuilder.AppendLine();
            multipartDataBuilder.AppendLine(value);
            multipartDataBuilder.AppendLine(string.Format("--{0}", Http.MULTIPART_BOUNDARY)); 
        }

        public void AddMultiparFileData(string key, byte[] data)
        {
            multipartDataBuilder.AppendLine(string.Format(@"Content-Disposition: form-data; name=""filedata""; filename=""{0}""", key));
            multipartDataBuilder.AppendLine("Content-Type: application/octet-stream");
            multipartDataBuilder.AppendLine("Content-Transfer-Encoding: binary");
            multipartDataBuilder.AppendLine();
            multipartDataBuilder.AppendLine(Encoding.UTF8.GetString(data, 0, data.Length));
            multipartDataBuilder.AppendLine(string.Format("--{0}", Http.MULTIPART_BOUNDARY)); 
        }

        public void AddRequestHeader(string key, string value)
        {
            if (additionalRequestHeaders == null)
            {
                additionalRequestHeaders = new Dictionary<string, string>();
                additionalRequestHeaders.Add(key, value);
                return;
            }

            if (additionalRequestHeaders.ContainsKey(key))
            {
                additionalRequestHeaders[key] = value;
            }
            else
            {
                additionalRequestHeaders.Add(key, value);
            }
        }

        public void AddPostData(string key, string value)
        {
            if (postData == null)
            {
                postData = new Dictionary<string, string>();
                postData.Add(key, value);
                return;
            }

            if (postData.ContainsKey(key))
            {
                postData[key] = value;
            }
            else
            {
                postData.Add(key, value);
            }
        }

        public async Task RunAsync(Action<string> onSuccess, Action<Error> onFail)
        {
            try
            {
                string dataToPost = string.Empty;
                if (IsMultipart)
                {
                    dataToPost = multipartDataBuilder.ToString();
                }
                else
                {
                    dataToPost = HaoDouApiUrlHelper.CombineURLParameters(postData);
                }

                await Http.POSTAsync(apiUrl, dataToPost, additionalRequestHeaders, (result) =>
                    {
                        if (PassLeechedDataDirectly)
                        {
                            onSuccess(result.Content);
                            return;
                        }

                        JsonObject data = JsonObject.Parse(result.Content);

                        if (data == null)
                        {
                            onFail.Invoke(new Error() { ErrorCode = 1, Message = "parse json data failed" });
                            return;
                        }

                        // if the reqesut is not success (status = 200)
                        //
                        int status = int.Parse(data["status"].GetNumber().ToString());
                        if (data["status"].GetNumber() != 200)
                        {
                            HaodouResultMessage resultMessage = JsonSerializer.Deserialize<HaodouResultMessage>(data["result"].GetObject().Stringify());
                            if (resultMessage != null && onFail != null)
                            {
                                onFail.Invoke(new Error() { ErrorCode = status, Message = resultMessage.Message });
                            }
                        }


                        if (onSuccess != null)
                        {
                            onSuccess.Invoke(data["result"].GetObject().Stringify());
                        }
                    }, IsMultipart);
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


    public class GETRequestExecuter : ILeechExecuter<string, Error>
    {
        private string apiUrl;
        private Dictionary<string, string> additionalRequestHeaders;

        public GETRequestExecuter(string url)
        {
            this.apiUrl = url;
        }

        public void AddRequestHeader(string key, string value)
        {
            if (additionalRequestHeaders == null)
            {
                additionalRequestHeaders = new Dictionary<string, string>();
                additionalRequestHeaders.Add(key, value);
                return;
            }

            if (additionalRequestHeaders.ContainsKey(key))
            {
                additionalRequestHeaders[key] = value;
            }
            else
            {
                additionalRequestHeaders.Add(key, value);
            }
        }

        public async Task RunAsync(Action<string> onSuccess, Action<Error> onFail)
        {
            try
            {
                await Http.GETAsync(apiUrl, additionalRequestHeaders, (result) =>
                {
                    JsonObject data = JsonObject.Parse(result.Content);

                    if (data == null)
                    {
                        onFail.Invoke(new Error() { ErrorCode = Constants.ERRORCODE_METAJSON_PARSE_FAILED, Message = "parse json data failed" });
                        return;
                    }

                    // if the reqesut is not success (status = 200)
                    //
                    int status = int.Parse(data["status"].GetNumber().ToString());
                    if (data["status"].GetNumber() != 200)
                    {
                        HaodouResultMessage resultMessage = JsonSerializer.Deserialize<HaodouResultMessage>(data["result"].GetObject().Stringify());
                        if (resultMessage != null && onFail != null)
                        {
                            onFail.Invoke(new Error() { ErrorCode = status, Message = resultMessage.Message });
                        }
                    }


                    if (onSuccess != null)
                    {
                        onSuccess.Invoke(data["result"].GetObject().Stringify());
                    }
                });
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
