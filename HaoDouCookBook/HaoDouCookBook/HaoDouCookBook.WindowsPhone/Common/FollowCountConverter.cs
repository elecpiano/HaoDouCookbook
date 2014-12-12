using System;
using Windows.UI.Xaml.Data;

namespace HaoDouCookBook.Common
{
    public class CountToDescriptionStringConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            int count = 0;
            if (int.TryParse(value.ToString(), out count))
            {
                if (count < 10000)
                {
                    return count.ToString();
                }
                else if (count > 10000 && count <= 100000)
                {
                    return (count / 10000.0).ToString("f1") + "万";
                }
                if (count > 100000)
                {
                    return ">10万";
                }
            }

            return count.ToString();

        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
