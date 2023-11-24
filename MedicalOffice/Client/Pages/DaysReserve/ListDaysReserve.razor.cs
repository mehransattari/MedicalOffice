using MedicalOffice.Client.Repositories.Inteface;
using MedicalOffice.Shared.DTO;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using MedicalOffice.Client.Pages.DaysReserve;
using MedicalOffice.Shared.Helper.Mapper;
using System.Globalization;
using System.Reflection;
using MedicalOffice.Client.Services.Helper;

namespace MedicalOffice.Client.Pages.DaysReserves;


/// <summary>
/// Main
/// </summary>
public partial class ListDaysReserveBase : ComponentBase
{

    #region Inject
    [Inject]
    public IDaysReserveRepository _DaysReserveRepository { get; set; }
    [Inject]
    public ISnackbar Snackbar { get; set; }
    [Inject]
    public IDialogService DialogService { get; set; }
    #endregion

    #region Properties
    public List<DaysReserveDto> listDaysReserve { get; set; } = new List<DaysReserveDto>();
    public IEnumerable<DaysReserveDto> enumDaysReserve = new List<DaysReserveDto>();
    public IEnumerable<DaysReserveDto> pagedData = new List<DaysReserveDto>();
    public MultipartFormDataContent MultipartFormData = new MultipartFormDataContent();
    public MudTable<DaysReserveDto>? DaysReserveTable;

    public bool disableButtonAdd { get; set; } = false;

    #endregion

    #region ServerReload
    public async Task<TableData<DaysReserveDto>> ServerReload(TableState state)
    {
        if (EditedDaysReserve.Id != 0)
        {
            MultipartFormData.Add(new StringContent(EditedDaysReserve.Day.ToString()), "Day");           
            var result = await _DaysReserveRepository.UpdateDaysReserve(MultipartFormData);
            if (result.Response)
            {
                StateHasChanged();
                EditedDaysReserve.Id = 0;
            }
        }

        var resultQuery = await _DaysReserveRepository.GetDaysReserve();

        if (resultQuery.Success)
        {
            var res = resultQuery.Response.ToArray();
            enumDaysReserve = res.Mapper();
            disableButtonAdd = enumDaysReserve.Any();
            this.StateHasChanged();
        }

        pagedData = enumDaysReserve.ToArray();

        return new TableData<DaysReserveDto>() { TotalItems = 0, Items = pagedData };
    }
    #endregion
}

/// <summary>
/// Add
/// </summary>
public partial class ListDaysReserveBase : ComponentBase
{  
    public async Task OpenAddDialogAdd()
    {
        DialogOptions closeOnEscapeKey = new DialogOptions()
        {
            CloseOnEscapeKey = true,
            MaxWidth = MaxWidth.Medium,
            FullWidth = true,
        };

        var result = await DialogService.Show<FormDialogDaysReserve>("افزودن روز های رزرو ", closeOnEscapeKey).Result;

        if (!result.Canceled)
        {
            StateHasChanged();
            await DaysReserveTable.ReloadServerData();
        }
    }
}


/// <summary>
/// Edit
/// </summary>
public partial class ListDaysReserveBase : ComponentBase
{
    /// <summary>
    /// باز شدن مدال برای ویرایش
    /// </summary>
    /// <returns></returns>
    public async Task OpenAddDialogEdit()
    {
        var parameters = new DialogParameters<FormDialogDaysReserveBase>();
        var Id = selectedItems.Select(x => x.Id).FirstOrDefault();
        var Day = selectedItems.Select(x => x.Day).FirstOrDefault();
      
        parameters.Add(x => x.Id, Id);
        parameters.Add(x => x.Day, Day);

        DialogOptions closeOnEscapeKey = new DialogOptions()
        {
            CloseOnEscapeKey = true,
            MaxWidth = MaxWidth.Medium,
            FullWidth = true,
        };

        var result = await DialogService.Show<FormDialogDaysReserve>("ویرایش  روز های رزرو ", parameters, closeOnEscapeKey).Result;

        if (!result.Canceled)
        {
            StateHasChanged();
            await DaysReserveTable.ReloadServerData();
        }
    }
}


/// <summary>
/// Row Edit
/// </summary>
public partial class ListDaysReserveBase : ComponentBase
{
    #region Row Edit
    public DaysReserveDto DaysReserveBeforeEdit { get; set; } = new DaysReserveDto();
    public DaysReserveDto EditedDaysReserve { get; set; } = new DaysReserveDto();

    /// <summary>
    /// هنگام کلیک روی دکمه ویرایش سطری
    /// </summary>
    /// <param name="DaysReserve"></param>
    public void BackupItem(object DaysReserve)
    {
        DaysReserveBeforeEdit = new()
        {
            Id = ((DaysReserveDto)DaysReserve).Id,
            Day = ((DaysReserveDto)DaysReserve).Day,
           
        };
        date = ((DaysReserveDto)DaysReserve).Day;
    }

    /// <summary>
    /// ذخیره اطلاعات در هنگام ویرایش سطری
    /// </summary>
    /// <param name="DaysReserve"></param>
    public void ItemHasBeenCommitted(object DaysReserve)
    {        
        var res = (DaysReserveDto)DaysReserve;

        if(date.HasValue)
        {
            res.Day = date.Value;
        }

        EditedDaysReserve = res;
        DaysReserveTable.ReloadServerData();
    }

    /// <summary>
    /// کنسل کردن  اطلاعات در هنگام ویرایش سطری
    /// </summary>
    /// <param name="DaysReserve"></param>
    public void ResetItemToOriginalValues(object DaysReserve)
    {
        ((DaysReserveDto)DaysReserve).Id = DaysReserveBeforeEdit.Id;
        ((DaysReserveDto)DaysReserve).Day = DaysReserveBeforeEdit.Day;
    }
    #endregion
}


/// <summary>
/// Selected Row
/// </summary>
public partial class ListDaysReserveBase : ComponentBase
{
    #region Selected Row
    public HashSet<DaysReserveDto> selectedItems = new HashSet<DaysReserveDto>();
    public bool _selectOnRowClick = true;
    public DaysReserveDto _selectedItem = new DaysReserveDto();
    public void OnRowClick(TableRowClickEventArgs<DaysReserveDto> args)
    {
        _selectedItem = args.Item;
    }
    #endregion
}


/// <summary>
/// Delete Message Box
/// </summary>
public partial class ListDaysReserveBase : ComponentBase
{
    public MudMessageBox? mbox { get; set; }

    public async void OnButtonClicked()
    {
        bool? result = await mbox.Show();
        StateHasChanged();
    }
    public async Task DeleteDaysReserves()
    {
        var ids = selectedItems.Select(x => x.Id);
        await _DaysReserveRepository.DeleteDaysReserveByIds(ids);
        await DaysReserveTable.ReloadServerData();
    }

}


/// <summary>
/// Persian Date
/// </summary>
public partial class ListDaysReserveBase : ComponentBase
{
    public DateTime? date { get; set; } = DateTime.Now;

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


/// <summary>
/// ShowTimesReserve
/// </summary>
public partial class ListDaysReserveBase : ComponentBase
{
    public async Task ShowTimesReserve()
    {
        var parameters = new DialogParameters<FormDialogShowTimesReserveBase>();
        var dayId = selectedItems.Select(x => x.Id).FirstOrDefault();

        parameters.Add(x => x.dayId, dayId);

        DialogOptions closeOnEscapeKey = new DialogOptions()
        {
            CloseOnEscapeKey = true,
            MaxWidth = MaxWidth.ExtraExtraLarge,
            FullWidth = true,          
        };

        var day=await _DaysReserveRepository.GetDaysReserveById(dayId); 
        
        if(day.Success)
        {
            var result = await DialogService
                               .Show<FormDialogShowTimesReserve>
                                  ($"ساعات رزرو شده - {day.Response.Day.ToShamsi()}",
                                parameters, closeOnEscapeKey)
                               .Result;

            if (!result.Canceled)
            {
                StateHasChanged();
                await DaysReserveTable.ReloadServerData();
            }

        }             
    }
}