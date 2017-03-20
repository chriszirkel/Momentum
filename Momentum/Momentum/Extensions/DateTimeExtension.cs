using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Momentum.Extensions
{
    public static class DateTimeExtension
    {
        public static string ToDateString(this DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-dd");
        }

        public static string ToTimeString(this DateTime dateTime)
        {
            return dateTime.ToString("HH:mm:ss");
        }
    }
}
