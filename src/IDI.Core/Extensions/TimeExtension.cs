using System;

namespace IDI.Core.Extensions
{
    public static class TimeExtension
    {
        public static long AsLong(this DateTime time)
        {
            var start = TimeZoneInfo.ConvertTime(new DateTime(1970, 1, 1), TimeZoneInfo.Local);
            TimeSpan now = time.Subtract(start);
            long timeStamp = now.Ticks;
            return long.Parse(timeStamp.ToString().Substring(0, timeStamp.ToString().Length - 4));
        }
    }
}
