﻿using HaoDouCookBook.Common;
using HaoDouCookBook.HaoDou.DataModels;
using HaoDouCookBook.HaoDou.DataModels.My;
using System;
using System.Threading.Tasks;

namespace HaoDouCookBook.HaoDou.API
{
    public class PassportAPI
    {
        public const string MODULE = "Passport";

        public static async Task Login(string userName, string password, string uuid, Action<PassportLoginResultData> onSuccess, Action<Error> onFail)
        {
            string methodName = "login";
            POSTRequestExecuter postRequest = new POSTRequestExecuter(HaoDouApiUrlHelper.GetApiUrl(MODULE, methodName));
            postRequest.AddPostData("name", userName);
            postRequest.AddPostData("pwd", password);
            postRequest.AddPostData("uuid", uuid);

            HaoDouJsonDataLoader<PassportLoginResultData> loader = new HaoDouJsonDataLoader<PassportLoginResultData>();
            await loader.LoadWithoutCacheAsnyc(postRequest, onSuccess, onFail);
        }

        public static async Task Logout(string uuid, string sign, Action onSuccess, Action<Error> onFail)
        {
            string methodName = "logout";
            POSTRequestExecuter postRequest = new POSTRequestExecuter(HaoDouApiUrlHelper.GetApiUrl(MODULE, methodName));
            postRequest.AddPostData("uuid", uuid);
            postRequest.AddPostData("sign", sign);

            HaoDouJsonDataLoader<HaodouResultMessage> loader = new HaoDouJsonDataLoader<HaodouResultMessage>();
            await loader.LoadWithoutCacheAsnyc(postRequest, success =>
                {
                    if (onSuccess != null)
                    {
                        onSuccess.Invoke();
                    }

                }, error =>
                {
                    if (onFail != null)
                    {
                        onFail.Invoke(error);
                    }
                });
        }
    }
    public class AccountAPI
    {
        public const string MODULE = "Account";

        public static async Task Login(string userName, string password, Action<PassportLoginResultData> onSuccess, Action<Error> onFail)
        {
            string methodName = "login";
            POSTRequestExecuter postRequest = new POSTRequestExecuter(HaoDouApiUrlHelper.GetApiUrl(MODULE, methodName));
            postRequest.AddPostData("name", userName);
            postRequest.AddPostData("pwd", password);

            HaoDouJsonDataLoader<PassportLoginResultData> loader = new HaoDouJsonDataLoader<PassportLoginResultData>();
            await loader.LoadWithoutCacheAsnyc(postRequest, onSuccess, onFail);
        }

        public static async Task Checkin(int uid, string sign, Action<CheckinData> onSuccess, Action<Error> onFail)
        {
            string methodName = "checkin";
            POSTRequestExecuter postRequest = new POSTRequestExecuter(HaoDouApiUrlHelper.GetApiUrl(MODULE, methodName));
            postRequest.AddPostData("uid", uid.ToString());
            postRequest.AddPostData("sign", sign);

            HaoDouJsonDataLoader<CheckinData> loader = new HaoDouJsonDataLoader<CheckinData>();
            await loader.LoadWithoutCacheAsnyc(postRequest, onSuccess, onFail);
        }

        public static async Task ChangeIntro(string content, string sign, int uid, Action<HaodouResultMessage> onSuccess, Action<Error> onFail)
        {
            string methodName = "changeIntro";
            POSTRequestExecuter postRequest = new POSTRequestExecuter(HaoDouApiUrlHelper.GetApiUrl(MODULE, methodName));
            postRequest.AddPostData("content", Uri.EscapeDataString(content));
            postRequest.AddPostData("sign", sign);
            postRequest.AddPostData("uid", uid.ToString());

            HaoDouJsonDataLoader<HaodouResultMessage> loader = new HaoDouJsonDataLoader<HaodouResultMessage>();
            await loader.LoadWithoutCacheAsnyc(postRequest, onSuccess, onFail);
        }

        public static async Task RegByEmail(string email, string nickname, string password, Action<PassportLoginResultData> onSuccess, Action<Error> onFail)
        {
            string methodName = "reg";
            POSTRequestExecuter postRequest = new POSTRequestExecuter(HaoDouApiUrlHelper.GetApiUrl(MODULE, methodName));
            postRequest.AddPostData("pwd", password);
            postRequest.AddPostData("pwd2", password);
            postRequest.AddPostData("nick", nickname);
            postRequest.AddPostData("name", email);

            HaoDouJsonDataLoader<PassportLoginResultData> loader = new HaoDouJsonDataLoader<PassportLoginResultData>();
            await loader.LoadWithoutCacheAsnyc(postRequest, onSuccess, onFail);

        }
        public static async Task RegByPhone(string phone, string code, string nickname, string password, Action<PassportLoginResultData> onSuccess, Action<Error> onFail)
        {
            string methodName = "reg";
            POSTRequestExecuter postRequest = new POSTRequestExecuter(HaoDouApiUrlHelper.GetApiUrl(MODULE, methodName));
            postRequest.AddPostData("pwd", password);
            postRequest.AddPostData("pwd2", password);
            postRequest.AddPostData("nick", nickname);
            postRequest.AddPostData("name", phone);
            postRequest.AddPostData("code", code);

            HaoDouJsonDataLoader<PassportLoginResultData> loader = new HaoDouJsonDataLoader<PassportLoginResultData>();
            await loader.LoadWithoutCacheAsnyc(postRequest, onSuccess, onFail);

        }
    }
}
