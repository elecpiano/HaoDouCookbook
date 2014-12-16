using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace HaoDouCookBook.Common
{
    public class FriendsCirclePageCommentsCountToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if(value == null)
            {
                return Visibility.Collapsed;
            }

            if(string.IsNullOrEmpty(value.ToString()))
            {
                return Visibility.Collapsed;
            }

            int count = 0;
            int.TryParse(value.ToString(), out count);
            return count > 5 ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
