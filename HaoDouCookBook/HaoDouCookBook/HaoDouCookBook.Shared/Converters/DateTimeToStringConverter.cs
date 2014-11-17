using System;
using Windows.UI.Xaml.Data;

namespace Shared.Converters
{
    public class DateTimeToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value != null)
            {
                DateTime dt = (DateTime)value;
                string format = string.Empty;
                if (parameter != null)
                {
                    format = (string)parameter;
                }
                return dt.ToString(format);
            }
            else
            {
                return string.Empty;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
