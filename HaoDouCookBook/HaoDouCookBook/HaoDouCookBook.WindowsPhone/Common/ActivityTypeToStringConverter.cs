using System;
using Windows.UI.Xaml.Data;

namespace HaoDouCookBook.Common
{
    public class ActivityTypeToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null)
            {
                return string.Empty;
            }

            int val = 0;
            if (int.TryParse(value.ToString(), out val))
            {
                switch (val)
                {
                    case 10:
                        return "产品";
                    case 30:
                        return "菜谱";
                    default:
                        break;
                }
            }

            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
