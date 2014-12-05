using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace HaoDouCookBook.Common
{
    public class ListCountToNoItemsVisiblityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            bool visible = false;

            if (value is string)
            {
                visible = string.IsNullOrEmpty(value.ToString());
            }
            if (value is int)
            {
                visible = int.Parse(value.ToString()) == 0;
            }


            return visible ? Visibility.Visible : Visibility.Collapsed;

            
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
