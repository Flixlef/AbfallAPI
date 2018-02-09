using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace AbfallAPI.Models
{
    public static class Helpers
    {
        public static DateTime GetNextDay(DateTime currentDate, DayOfWeek day, Boolean evenWeek)
        {
            Boolean correctWeek = (WeekOfYear(currentDate) % 2 == 0) == evenWeek;
            Boolean correctDay = currentDate.DayOfWeek == day;

            if (correctDay && correctWeek)
            {
                return currentDate;
            }
            else
            {
                return GetNextDay(currentDate.AddDays(1), day, evenWeek);
            }
        }

        public static DateTime GetNextDay(DateTime currentDate, DayOfWeek day)
        {
            Boolean correctDay = currentDate.DayOfWeek == day;

            if(correctDay)
            {
                return currentDate;
            } else
            {
                return GetNextDay(currentDate.AddDays(1), day);
            }
        }

        public static int WeekOfYear(DateTime date)
        {
            DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
            Calendar cal = dfi.Calendar;

            return cal.GetWeekOfYear(date, dfi.CalendarWeekRule, dfi.FirstDayOfWeek);
        }
    }
}
