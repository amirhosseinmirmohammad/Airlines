using System.Globalization;

namespace WebApp.Utilities
{
    public static class DateTimeConverter
    {
        public static string ToPersianDateTimeWithPeriod(DateTime date)
        {
            PersianCalendar pc = new PersianCalendar();
            string period = GetDayPeriod(date.Hour);

            string persianDate = $"{pc.GetYear(date)}/{pc.GetMonth(date):00}/{pc.GetDayOfMonth(date):00} " +
                                 $"{date:HH:mm} {period}";

            return persianDate;
        }

        private static string GetDayPeriod(int hour)
        {
            if (hour >= 0 && hour < 6)
                return "نیمه‌شب";
            else if (hour >= 6 && hour < 12)
                return "صبح";
            else if (hour >= 12 && hour < 18)
                return "ظهر";
            else
                return "شب";
        }
    }
}
