using System;

namespace com.bitscopic.reportcore.utils
{
    static class DateTimeExtensions {
        public static string ToReportDate(this DateTime dateTime, bool includeTime) {
            if (includeTime && dateTime.TimeOfDay.TotalSeconds > 0)
                return dateTime.ToString(("M/d/yy h:mm"));
            else
                return dateTime.ToString("M/d/yy");
        }
    }
}