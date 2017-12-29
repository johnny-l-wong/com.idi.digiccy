using System;
using System.Globalization;

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

        public static DateTime AsDateTime(this string s)
        {
            var time = new DateTime(1970, 1, 1);

            if (DateTime.TryParse(s, out time))
                return time;

            return time;
        }

        public static int WeekOfYear(this DateTime time)
        {
            return new GregorianCalendar().GetWeekOfYear(time, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
        }

        public static DateTime FirstDayOfMonth(this DateTime time)
        {
            return new DateTime(time.Year, time.Month, 1);
        }

        public static DateTime FirstDayOfWeek(this DateTime time)
        {
            int dayOfWeek = (int)time.DayOfWeek;

            dayOfWeek = (dayOfWeek == 0 ? (7 - 1) : (dayOfWeek - 1));

            return time.AddDays((-1) * dayOfWeek).Date;
        }
    }
}
