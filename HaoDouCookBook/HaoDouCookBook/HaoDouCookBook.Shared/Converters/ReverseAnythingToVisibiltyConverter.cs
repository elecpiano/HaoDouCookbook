using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace HaoDouCookBook.Converters
{
    public class ReverseAnythingToVisibiltyConverter : IValueConverter
    {


        public object Convert(object value, Type targetType, object parameter, string language)
        {
            bool result = false;

            if (value == null)
            {
                result = true;
            }
            else if (value is bool)
            {
                result = !(bool)value;
            }
            else if (value is string)
            {
                result = string.IsNullOrEmpty(value.ToString().Trim());
            }
            else if (value is int)
            {
                result = int.Parse(value.ToString()) == 0;
            }
            else if (value is DateTime)
            {
                result = (DateTime)value < DateTime.Now;
            }
            else
            {

            }

            return result ? Visibility.Visible : Visibility.Collapsed;

        } 

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
