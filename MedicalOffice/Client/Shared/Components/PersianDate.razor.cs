using Microsoft.AspNetCore.Components;
using System.Globalization;
using System.Reflection;

namespace MedicalOffice.Client.Shared.Components;

public class PersianDateBase : ComponentBase
{
    [Parameter]
    public Action<DateTime> OnValueChanged { get; set; }

    //[Parameter]
    //public DateTime ReceiveDate { get; set; } = DateTime.Now;

    public DateTime? date = DateTime.Now;
    //protected override void OnInitialized()
    //{
    //    //date = ReceiveDate;
    //}
    //protected override void OnAfterRender(bool firstRender)
    //{
    //    // date = ReceiveDate;

    //}
    //protected override Task OnAfterRenderAsync(bool firstRender)
    //{
    //    if(date.HasValue)
    //    {
    //        OnValueChanged?.Invoke(date.Value);
    //    }
    //    return base.OnAfterRenderAsync(firstRender);
    //}
    public CultureInfo GetPersianCulture()
    {
        var culture = new CultureInfo("fa-IR");
        DateTimeFormatInfo formatInfo = culture.DateTimeFormat;
        formatInfo.AbbreviatedDayNames = new[] { "ی", "د", "س", "چ", "پ", "ج", "ش" };
        formatInfo.DayNames = new[]
        { "یکشنبه", "دوشنبه", "سه شنبه", "چهار شنبه", "پنجشنبه", "جمعه", "شنبه" };
        var monthNames = new[]
        {
            "فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور", "مهر", "آبان", "آذر", "دی", "بهمن",
            "اسفند",
            "",
        };
        formatInfo.AbbreviatedMonthNames =
            formatInfo.MonthNames =
                formatInfo.MonthGenitiveNames = formatInfo.AbbreviatedMonthGenitiveNames = monthNames;
        formatInfo.AMDesignator = "ق.ظ";
        formatInfo.PMDesignator = "ب.ظ";
        formatInfo.ShortDatePattern = "yyyy/MM/dd";
        formatInfo.LongDatePattern = "dddd, dd MMMM,yyyy";
        formatInfo.FirstDayOfWeek = DayOfWeek.Saturday;
        Calendar cal = new PersianCalendar();
        FieldInfo fieldInfo = culture.GetType()
            .GetField("calendar", BindingFlags.NonPublic | BindingFlags.Instance);
        if (fieldInfo != null)
            fieldInfo.SetValue(culture, cal);
        FieldInfo info = formatInfo.GetType().GetField("calendar", BindingFlags.NonPublic | BindingFlags.Instance);
        if (info != null)
            info.SetValue(formatInfo, cal);
        culture.NumberFormat.NumberDecimalSeparator = "/";
        culture.NumberFormat.DigitSubstitution = DigitShapes.NativeNational;
        culture.NumberFormat.NumberNegativePattern = 0;

        if (date.HasValue)
        {
            OnValueChanged?.Invoke(date.Value);
        }
        return culture;
      
    }
}
