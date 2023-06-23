using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace HBTSA.Libraries.ExtensionMethods
{
    public static class DateTimeExtension
    {
        public static DateTime FirstDayMonthCurrentDate(this DateTime input)
        {
            return new DateTime(input.Year, input.Month, 1);
        }

        public static DateTime LastDayMonthCurrentDate(this DateTime input, bool setEndOfDayTime = false)
        {
            DateTime tmp01 = new DateTime(input.Year, input.Month, 1);
            DateTime tmp02 = tmp01.AddMonths(1).AddDays(-1);
            if (setEndOfDayTime) tmp02 = new DateTime(tmp02.Year, tmp02.Month, tmp02.Day, 23, 59, 59);
            return tmp02;
        }

        public static DateTime AddMonthsSetLastDay(this DateTime input, int Months, int LastDay)
        {
            DateTime tmp01 = input.AddMonths(Months);
            if (LastDay <= tmp01.LastDayMonthCurrentDate().Day) 
                tmp01 = new DateTime(tmp01.Year, tmp01.Month, LastDay);
            return tmp01;
        }
        
        public static DateTime ToYesterdayAtMidnight(this DateTime input)
        {
            DateTime result = input.AddDays(-1);
            return new DateTime(result.Year, result.Month, result.Day, 23, 59, 59);
        }

        public static DateTime ToDayAtMidnight(this DateTime input, bool at23 = false)
        {
            DateTime result = input;
            return at23 ? new DateTime(result.Year, result.Month, result.Day, 23, 59, 59) : new DateTime(result.Year, result.Month, result.Day, 0, 0, 0);
        }

        public static (DateTime start, DateTime end) GetStartEndOfMonth(this DateTime input)
        {
            var bb = ToDayAtMidnight(new DateTime(2020, 10, 10));
            DateTime start = input.FirstDayMonthCurrentDate();
            DateTime end = input.LastDayMonthCurrentDate(true);
            return (start, end);
        }

        public static (string start, string end) GetStartEndOfMonthString(this DateTime input)
        {
            (DateTime start, DateTime end) = input.GetStartEndOfMonth();
            string FromDate = $"{start.Year}-{start.Month}-{start.Day}";
            string ToDate = $"{end.Year}-{end.Month}-{end.Day}";
            return (FromDate, ToDate);
        }

        public static int MonthsDifference(this DateTime date1, DateTime date2)
        {
            return Math.Abs((date1.Month - date2.Month) + 12 * (date1.Year - date2.Year));
        }
    }
}