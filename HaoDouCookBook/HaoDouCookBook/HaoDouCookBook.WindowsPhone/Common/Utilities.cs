using Shared.Utility;
using System.Collections.Generic;
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

            if(fe != null)
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
                }
            }
        }

        public static void ElementInItemsControl_SetVisibleAtBegin<T>(object sender, bool visible) where T : class
        {
            FrameworkElement fe = sender as FrameworkElement;

            if(fe != null)
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
                }
            }
        }
    }
}
