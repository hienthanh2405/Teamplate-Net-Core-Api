using System;

namespace Utilities.Extensions
{
    public static class StringExtensions
    {
        public static string ToDateFormat(this DateTime dateTime)
            => dateTime.ToString("D");
    }
}