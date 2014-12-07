using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace HaoDouCookBook.Common
{
    public class SignedUserToHeaderWidthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null)
            {
                return 110d;
            }

            bool isSignedUser = bool.Parse(value.ToString());
            if (isSignedUser)
            {
                return 80d;
            }
            else
            {
                return 110d;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
