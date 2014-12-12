using HaoDouCookBook.Utility;
using System;
using System.Collections.Generic;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;

namespace HaoDouCookBook.HaoDou.API
{
    public class HaoDouApiUrlHelper
    {
        public const string DOMAIN = "http://api.hoto.cn/";
        public const string APIPAGE = "index.php";
        public const string APPID = "7";
        public const string APPKEY = "37e66171acbfbbb63db742bd3bfc1142";
        public const string VERSIONCODE = "1";
        public const string VERSIONNAME = "v1.0.0";

        public static string GetApiUrl(string module, string methodName)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("appid", APPID);
            parameters.Add("appkey", APPKEY);
            parameters.Add("vc", VERSIONCODE);
            parameters.Add("vn", VERSIONNAME);
            parameters.Add("method", string.Format("{0}.{1}", module, methodName));
            parameters.Add("deviceid", string.Empty); 
            parameters.Add("format", "json");
            parameters.Add("sessionid", DateTime.Now.GetTimeStamp());
            parameters.Add("uuid", DeviceHelper.GetUniqueDeviceID());

            return string.Format("{0}{1}?{2}", DOMAIN, APIPAGE, CombineURLParameters(parameters));
        }

        public static string CombineURLParameters(Dictionary<string, string> parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentException("parameters is null");
            }

            List<string> paras = new List<string>();
            foreach (var kv in parameters)
            {
                paras.Add(string.Format("{0}={1}", kv.Key, kv.Value));
            }

            return string.Join("&", paras.ToArray());
        }
    }
}
