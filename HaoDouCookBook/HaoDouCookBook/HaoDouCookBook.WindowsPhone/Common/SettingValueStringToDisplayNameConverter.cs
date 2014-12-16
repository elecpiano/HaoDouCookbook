using System;
using Windows.UI.Xaml.Data;

namespace HaoDouCookBook.Common
{
    public class SettingValueStringToDisplayNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if(value == null || string.IsNullOrEmpty(value.ToString()))
            {
                return "未填写";
            }

            return value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
