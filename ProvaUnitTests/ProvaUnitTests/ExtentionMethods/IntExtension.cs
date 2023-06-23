using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBTSA.Libraries.ExtensionMethods
{
    public static class IntExtension
    {
        public static DateTime ToDateFromExcelFormatNumber(this long value)
        {
            int MAGIC_NUMBER = 25569;
            long TO_MILLISECONDS = 24 * 60 * 60 * 1000;
            long ticks = (value - MAGIC_NUMBER) * TO_MILLISECONDS;
            TimeSpan time = TimeSpan.FromMilliseconds(ticks);
            return new DateTime(1970, 1, 1) + time;
        }
    }
}
