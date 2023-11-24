using MedicalOffice.Client.Repositories.Inteface;
using MedicalOffice.Client.Services;
using MedicalOffice.Shared.DTO;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using System.Globalization;
using System.Reflection;

namespace MedicalOffice.Client.Pages.DaysReserves;

public partial class FormDialogDaysReserveBase : ComponentBase
{
    #region Inject 
    [Inject]
    public IDaysReserveRepository _DaysReserveRepository { get; set; }

    [CascadingParameter]
    public MudDialogInstance MudDialog { get; set; }

    [Inject]
    public IFileUpload fileUpload { get; set; }
    #endregion

    #region Parameter
    [Parameter]
    public long Id { get; set; }

    [Parameter]
    public DateTime Day { get; set; } = DateTime.Now;
    #endregion

    #region Fields
    public MultipartFormDataContent MultipartFormData = new MultipartFormDataContent();

    public MudTextField<string>? multilineReference;
    public string? DaysReserveName { get; set; }

    public DaysReserveDto DaysReserve = new DaysReserveDto();

    public DateTime? date { get; set; } = DateTime.Now;

    #endregion

    protected override void OnInitialized()
    {
        if (Id != 0)
        {
            DaysReserve.Id = Id;
            DaysReserve.Day = Day;
            date = Day;
        }
    }

}


/// <summary>
/// OnValidSubmit
/// </summary>
public partial class FormDialogDaysReserveBase : ComponentBase
{
    public bool success;
    public void Submit() => MudDialog.Close(DialogResult.Ok(true));
    public void Cancel() => MudDialog.Cancel();
    public async Task OnValidSubmit(EditContext context)
    {
        success = true;

        MultipartFormData.Add(new StringContent(DaysReserve.Id.ToString()), "Id");
        MultipartFormData.Add(new StringContent(date.ToString()), "Day");

        if (Id != 0)
        {
            await _DaysReserveRepository.UpdateDaysReserve(MultipartFormData);
        }
        else
        {
            await _DaysReserveRepository.CreateDaysReserve(MultipartFormData);
        }

        StateHasChanged();
        Submit();
    }
}


/// <summary>
/// Persian Date
/// </summary>
public partial class FormDialogDaysReserveBase : ComponentBase
{
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
       
        return culture;
    }

}