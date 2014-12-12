using System;
using Windows.UI.Xaml.Data;

namespace HaoDouCookBook.Common
{
    public class ImageToDefaultImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            bool isBig = true;

            if(parameter != null && !string.IsNullOrEmpty(parameter.ToString()))
            {
                if(parameter.ToString() == "small")
                {
                    isBig = false;
                }
            }

            if (value == null)
            {
                return isBig ? Constants.DEFAULT_IMAGE_BIG : Constants.DEFAULT_IMAGE_SMALL;
            }

            if (string.IsNullOrEmpty(value.ToString()))
            {
                return isBig ? Constants.DEFAULT_IMAGE_BIG : Constants.DEFAULT_IMAGE_SMALL;
            }

            return value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
