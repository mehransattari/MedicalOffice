using System.Globalization;

namespace MedicalOffice.Client.Services.Helper
{
    public static class ConvertDate
    {
        public static string ToShamsi(this string value)
        {
            PersianCalendar pc = new PersianCalendar();
            return pc.GetYear(Convert.ToDateTime(value)) + "/" + pc.GetMonth(Convert.ToDateTime(value)).ToString("00") + "/" +
                   pc.GetDayOfMonth(Convert.ToDateTime(value)).ToString("00");
        }
        public static string ToShamsi(this DateTime value)
        {
            PersianCalendar pc = new PersianCalendar();
            return pc.GetYear((value)) + "/" + pc.GetMonth((value)).ToString("00") + "/" +
                   pc.GetDayOfMonth((value)).ToString("00");
        }
        public static PersianDayOfWeek ToDayShamsi(this DateTime value)
        {
            PersianCalendar pc = new PersianCalendar();
            int dayOfWeek = (int)pc.GetDayOfWeek(value);
            dayOfWeek = (dayOfWeek + 1) % 7;

            return (PersianDayOfWeek)dayOfWeek;
        }
    }
    public enum PersianDayOfWeek
    {
        شنبه,
        یک‌شنبه,
        دو‌شنبه,
        سه‌شنبه,
        چهار‌شنبه,
        پنج‌شنبه,
        جمعه
    }
}
