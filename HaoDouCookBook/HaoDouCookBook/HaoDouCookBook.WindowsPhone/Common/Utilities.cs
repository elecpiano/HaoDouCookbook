﻿using HaoDouCookBook.Controls;
using HaoDouCookBook.Utility;
using Shared.Utility;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace HaoDouCookBook.Common
{
    public class Utilities
    {
        public static bool IsMatchNetworkFail(int errorCode)
        {
            if (errorCode == Constants.ERRORCODE_JSON_PARSE_FAILED // parse json data failed
                || errorCode == Constants.ERRORCODE_METAJSON_PARSE_FAILED //parse the original json data from haodou failed
                || errorCode == Constants.ERRORCODE_REMOTE_SERVER_UNAVAILABLE) // remote server unavaiable
            {
                return true;
            }

            return false;
        }

        public static void ReloadWithDelay(Action action)
        {
            DelayHelper.Delay(TimeSpan.FromSeconds(0.7), action);
        }

        public static void RegistComonadLoadMoreBehavior<T>(object LoadMoreControl, Action<LoadMoreControl> loadMoreAction) where T : class
        {
            Utilities.ElementInItemsControl_SetVisibleAtEnd<T>(LoadMoreControl, true);

            LoadMoreControl loadmoreControl = LoadMoreControl as LoadMoreControl;
            if (loadmoreControl != null)
            {
                loadmoreControl.LoadMoreAction = loadMoreAction;
            }
        }

        public static void CommondLoadMoreErrorBehavoir(LoadMoreControl loadMoreControl, Error error)
        {
            if (IsMatchNetworkFail(error.ErrorCode))
            {
                loadMoreControl.SetState(LoadingState.NETWORK_UNAVAILABLE);
            }
            else
            {
                loadMoreControl.SetState(LoadingState.DONE);
            }
        }


        /// <summary>
        /// Only show the network unaviable page
        /// </summary>
        /// <param name="loading"></param>
        /// <param name="retryAction"></param>
        public static void CommonLoadingRetry(NetworkLoading loading, Action retryAction)
        {
            Utilities.ReloadWithDelay(() =>
            {
                loading.RetryAction = retryAction;
                loading.SetState(LoadingState.NETWORK_UNAVAILABLE);
            });
        }

        public static void CommonLoadingRetry(NetworkLoading loading, Error error, Action retryAction)
        {
            if (IsMatchNetworkFail(error.ErrorCode))
            {
                Utilities.ReloadWithDelay(() =>
                {
                    loading.RetryAction = retryAction;
                    loading.SetState(LoadingState.NETWORK_UNAVAILABLE);
                });
            }
            else
            {
                loading.SetState(LoadingState.DONE);
            }
        }

        public static bool SignedIn()
        {
            return !string.IsNullOrEmpty(UserGlobal.Instance.UserInfo.Sign);
        }

        public static bool IsSignedInUser(int userId)
        {
            if (string.IsNullOrEmpty(UserGlobal.Instance.UserInfo.UserId)
                || userId <= 0)
            {
                return false;
            }

            return userId.ToString().Equals(UserGlobal.Instance.UserInfo.UserId);
        }

        public static int GetInt32UserId(string userId)
        {
            int uid = 0;
            int.TryParse(UserGlobal.Instance.UserInfo.UserId, out uid);
            return uid;
        }

        public static void ElementInItemsControl_SetVisibleAtEnd<T>(object sender, bool visible) where T : class
        {
            FrameworkElement fe = sender as FrameworkElement;

            if (fe != null)
            {
                var parent = fe.GetParent<ItemsControl>();
                if (parent != null)
                {
                    var collections = parent.ItemsSource as IList<T>;
                    if (collections == null)
                    {
                        return;
                    }

                    int index = collections.IndexOf(fe.GetDataContext<T>());
                    if (index == collections.Count - 1)
                    {
                        fe.Visibility = visible ? Visibility.Visible : Visibility.Collapsed;
                    }
                    else
                    {
                        fe.Visibility = visible ? Visibility.Collapsed : Visibility.Visible;
                    }
                }
            }
        }

        public static void ElementInItemsControl_SetVisibleAtBegin<T>(object sender, bool visible) where T : class
        {
            FrameworkElement fe = sender as FrameworkElement;

            if (fe != null)
            {
                var parent = fe.GetParent<ItemsControl>();
                if (parent != null)
                {
                    var collections = parent.ItemsSource as IList<T>;
                    if (collections == null)
                    {
                        return;
                    }

                    int index = collections.IndexOf(fe.GetDataContext<T>());
                    if (index == 0)
                    {
                        fe.Visibility = visible ? Visibility.Visible : Visibility.Collapsed;
                    }
                    else
                    {
                        fe.Visibility = visible ? Visibility.Collapsed : Visibility.Visible;
                    }
                }
            }
        }

        public static async Task ShowOKCancelDialog(string title, string content, Action okAction, Action cancelAction)
        {
            ContentDialog dialog = new ContentDialog()
            {
                Title = title,
                Content = content,
                PrimaryButtonText = "确定",
                SecondaryButtonText = "取消"
            };

            var result = await dialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                if (okAction != null)
                {
                    okAction.Invoke();
                }
                return;
            }

            if (result == ContentDialogResult.Secondary)
            {
                if (cancelAction != null)
                {
                    cancelAction.Invoke();
                }

                return;
            }
        }

        public static void PickPicturesFromPicturesLibrary()
        {
            FileOpenPicker filePicker = new FileOpenPicker();
            filePicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            filePicker.ViewMode = PickerViewMode.Thumbnail;
            filePicker.FileTypeFilter.Add(".jpg");
            filePicker.FileTypeFilter.Add(".jpeg");
            filePicker.FileTypeFilter.Add(".png");

            filePicker.PickSingleFileAndContinue();
        }
    }
}
