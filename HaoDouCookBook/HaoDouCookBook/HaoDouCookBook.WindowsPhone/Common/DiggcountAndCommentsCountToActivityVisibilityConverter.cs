using HaoDouCookBook.ViewModels;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace HaoDouCookBook.Common
{
    public class DiggcountAndCommentsCountToActivityVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if(value == null)
            {
                return Visibility.Collapsed;
            }

            UserActivityItem uai = value as UserActivityItem;
            if(uai == null)
            {
                return Visibility.Collapsed;
            }

            if(uai.CommentsCount > 0 || uai.DiggCount > 0)
            {
                return Visibility.Visible;
            }
            else
            {
                return Visibility.Collapsed;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
