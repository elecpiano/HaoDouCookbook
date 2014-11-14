using System;
using System.Collections.Generic;
using System.Text;

namespace HaoDouCookBook.Utility
{
    public class StringHelper
    {
        public static string GetShortenStyle(string src, int maxLength)
        {
            if(src.Length > maxLength)
            {
                return string.Format("{0}...", src.Substring(0, maxLength));
            }

            return src;
        }

    }
}
