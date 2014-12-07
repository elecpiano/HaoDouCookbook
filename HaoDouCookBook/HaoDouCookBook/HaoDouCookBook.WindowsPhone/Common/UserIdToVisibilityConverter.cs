using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace HaoDouCookBook.Common
{
    public class SignedInUserIdToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null)
            {
                return Visibility.Collapsed;
            }

            int id = 0;
            if (int.TryParse(value.ToString(), out id))
            {
                return Utilities.IsSignedInUser(id) ? Visibility.Visible : Visibility.Collapsed;
            }

            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    public class NotSignedInUserIdToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null)
            {
                return Visibility.Collapsed;
            }

            int id = 0;
            if (int.TryParse(value.ToString(), out id))
            {
                return Utilities.IsSignedInUser(id) ? Visibility.Collapsed : Visibility.Visible;
            }

            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
