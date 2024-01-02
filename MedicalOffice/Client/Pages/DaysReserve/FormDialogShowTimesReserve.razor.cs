using MedicalOffice.Client.Repositories.Inteface;
using MedicalOffice.Shared.DTO;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;

namespace MedicalOffice.Client.Pages.DaysReserve;

public partial class FormDialogShowTimesReserveBase : ComponentBase
{
    #region Inject 

    [Inject]
    public required ITimesRepository TimeRepository { get; set; }

    [CascadingParameter]
    public MudDialogInstance MudDialog { get; set; }

    #endregion

    #region Parameter

    [Parameter]
    public long dayId { get; set; }


    #endregion

    #region Fields

    public MultipartFormDataContent MultipartFormData = new();

    public MudTextField<string>? multilineReference;
    public string? DaysReserveName { get; set; }

    public List<TimesReserveDto> TimesReserves = new();

    public TimesReserveDto TimesReserve = new();

    public TimeSpan? fromTime { get; set; } = new TimeSpan(12, 00, 00);
    public TimeSpan? toTime { get; set; } = new TimeSpan(13, 00, 00);
    public string MessageForm { get; set; }
    public string FormTitle { get; set; } = "جدید";
    public string FormButton { get; set; } = "ذخیره تغییرات";
    public bool IsFormEdit { get; set; }
    public long EditTimeId { get; set; } = 0;

    #endregion

    protected override async Task OnInitializedAsync()
    {
        await LoadTimesByDay();
    }

    public async Task LoadTimesByDay()
    {
        if (dayId != 0)
        {
            var res = await TimeRepository.GetTimesReserveByDayId(dayId);
            if (res.Success)
            {
                TimesReserves = res.Response.ToList();
            }
        }
    }
}

/// <summary>
/// OnValidSubmit
/// </summary>
public partial class FormDialogShowTimesReserveBase : ComponentBase
{
    public bool success;
    public void Submit() => MudDialog.Close(DialogResult.Ok(true));
    public void Cancel() => MudDialog.Cancel();

    public bool nestedVisible;
    public void OpenNested() => nestedVisible = true;
    public void CloseNested() => nestedVisible = false;
    public async Task OnValidSubmit(EditContext context)
    {
        success = true;
     
        #region Check Duplicate TimesReserve

        var timeReserve = new TimesReserveDto()
        {
            DaysReserveId = dayId,
            FromTime = fromTime.Value,
            ToTime = toTime.Value
        };

        var isDuplicate=  await TimeRepository.CheckDuplicateTimesReserve(timeReserve);

        if(isDuplicate.Success)
        {
            if(isDuplicate.Response)
            {
                MessageForm = "این ساعت برای این روز از قبل رزرو شده است.";
                OpenNested();
            }
            else
            {
                MultipartFormData = new();
                MultipartFormData
                       .Add(new StringContent(dayId.ToString()), "DaysReserveId");

                if (fromTime.HasValue)
                {
                    MultipartFormData
                        .Add(new StringContent(fromTime.Value.ToString()), "FromTime");
                }

                if (toTime.HasValue)
                {
                    MultipartFormData
                        .Add(new StringContent(toTime.Value.ToString()), "ToTime");
                }
                if (IsFormEdit && EditTimeId!=0)
                {
                    MultipartFormData
                       .Add(new StringContent(EditTimeId.ToString()), "Id");

                    await TimeRepository.UpdateTimesReserve(MultipartFormData);
                     ChangeFormToNew();
                }
                else
                {

                    await TimeRepository.CreateTimesReserve(MultipartFormData);
                }
                StateHasChanged();
                //Submit();
                
                await LoadTimesByDay();

            }
        }
        #endregion

     
    }
}

/// <summary>
/// Edit Times
/// </summary>
public partial class FormDialogShowTimesReserveBase : ComponentBase
{

    public async Task ShowEditTimeOnForm(long timeId)
    {
        var time=await TimeRepository.GetTimesReserveById(timeId);
        if(time.Success)
        {
            fromTime=  time.Response.FromTime;
            toTime = time.Response.ToTime;
            FormTitle = "ویرایش";
            FormButton = "ویرایش تغییرات";
            IsFormEdit = true;
            EditTimeId = timeId;
        }
    }

    public void  ChangeFormToNew()
    {
        IsFormEdit = false;
        EditTimeId = 0;
        FormButton = "ذخیره تغییرات";
        FormTitle = "جدید";
        fromTime = new TimeSpan(12, 00, 00);
        toTime = new TimeSpan(13, 00, 00);
    }
    public async Task  DeleteTime(long timeId)
    {
        
        var result = await TimeRepository.DeleteTimesReserveByIds(new List<long> { timeId});
        if(result.Success)
        {
            if(result.Response)
            {
                MessageForm = $" از ساعت  {fromTime} تا ساعت {toTime} حذف گردید.";
                OpenNested();
                await LoadTimesByDay();
            }
        }
        //باید بررسی شود که ایا از این ساعت در رزرو استفاده شده یا نه

    }
}
