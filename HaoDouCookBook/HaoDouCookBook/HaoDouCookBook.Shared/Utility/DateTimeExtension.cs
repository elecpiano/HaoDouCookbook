using System;

namespace HaoDouCookBook.Utility
{
    public static class DateTimeExtension
    {
        /// <summary>
        /// Get time stamp since 1970/1/1 00:00:00
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string GetTimeStamp(this DateTime dateTime)
        {
            TimeSpan span = dateTime - new DateTime(1970, 1, 1, 0, 0, 0).ToUniversalTime();
            return ((long)span.TotalSeconds).ToString();
        }
    }
}
