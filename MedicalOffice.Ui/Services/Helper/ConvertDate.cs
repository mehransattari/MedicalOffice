using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace MedicalOffice.Ui.Services.Helper
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

        public static string ToDayShamsi(this DateTime value)
        {
            PersianCalendar pc = new PersianCalendar();
            int dayOfWeek = (int)pc.GetDayOfWeek(value);
            dayOfWeek = (dayOfWeek + 1) % 7;

            var res = Enum.GetValues(typeof(PersianDayOfWeek)).Cast<PersianDayOfWeek>().ElementAt(dayOfWeek).GetDisplayName();
            return res;
        }

        /// <summary>
        ///Find Name Attribute Display As Enum
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetDisplayName(this Enum value)
        {
            var displayAttribute = value.GetType()
                                        .GetField(value.ToString())
                                        .GetCustomAttributes(typeof(DisplayAttribute), false)
                                        .SingleOrDefault() as DisplayAttribute;

            return displayAttribute?.Name ?? value.ToString();
        }
    }



    public enum PersianDayOfWeek
    {
        [Display(Name = "شنبه")]
        Saturday,

        [Display(Name = "یک‌شنبه")]
        Sunday,

        [Display(Name = "دوشنبه")]
        Monday,

        [Display(Name = "سه‌شنبه")]
        Tuesday,

        [Display(Name = "چهارشنبه")]
        Wednesday,

        [Display(Name = "پنج‌شنبه")]
        Thursday,

        [Display(Name = "جمعه")]
        Friday,
    }

}
