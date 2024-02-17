using MedicalOffice.Shared.DTO;
using MedicalOffice.Shared.Entities;
using MedicalOffice.Shared.Enum;
using MedicalOffice.Ui.Repositories.Inteface;
using MedicalOffice.Ui.Services.Helper;
using MedicalOffice.Ui.Services.Interface;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;

namespace MedicalOffice.Ui.Pages.Components;


/// <summary>
/// Inject
/// </summary>
public partial class ReservationComponentBase : ComponentBase
{
    [Inject]
    public IDaysReserveRepository daysReserveRepository { get; set; }

    [Inject]
    public ITimesRepository timesRepository { get; set; }

    [Inject]
    public IUserRepository userRepository { get; set; }

    [Inject]
    public IReserveRepository reserveRepository { get; set; }

    [Inject]
    public ISmsService smsService { get; set; }

    [Inject]
    public ISnackbar Snackbar { get; set; }

    [Inject]
    public IJSRuntime jSRuntime { get; set; }

}

/// <summary>
/// Parameters
/// </summary>
public partial class ReservationComponentBase : ComponentBase
{
    [Parameter]
    public EventCallback<bool> IsComponentLoading { get; set; }
}

/// <summary>
/// Properties
/// </summary>
public partial class ReservationComponentBase : ComponentBase
{
    public ReserveDto ReserveDto { get; set; } = new();
    public List<DaysReserve> DaysReserves { get; set; } = new();
    public List<IGrouping<long, TimesReserve>> TimesReserves { get; set; } = new();
    private TimesReserve SelectedTime { get; set; } = new();
}

/// <summary>
/// Fields
/// </summary>
public partial class ReservationComponentBase : ComponentBase
{
    public string d_block { get; set; } = "d-block";

    public string d_none { get; set; } = "d-none";

    public string? CurrentNameDay { get; set; }

    public string? CurrentDateDay { get; set; }

    public string FinishInfo { get; set; } = string.Empty;

    public string ButtonContinueReserve { get; set; } = string.Empty;

    public string MessageInfo { get; set; } = string.Empty;

    public string MessageText { get; set; } = string.Empty;

    public string MessageColor { get; set; } = string.Empty;

    public string Selected { get; set; } = string.Empty;

    public bool IsLoader { get; set; } = false;

    public string ShowDivFieldSingleUseCode { get; set; } = string.Empty;
}

/// <summary>
///  Methods
/// </summary>
public partial class ReservationComponentBase : ComponentBase
{
    /// <summary>
    /// OnInitializedAsync
    /// </summary>
    /// <returns></returns>
    protected override async Task OnInitializedAsync()
    {
        var currentDate = DateTime.Now;
        CurrentDateDay = currentDate.ToShamsi();
        CurrentNameDay = currentDate.ToDayShamsi();
        await ShowDays();
        await IsComponentLoading.InvokeAsync(false);
        DoDisplayNone();
    }

    /// <summary>
    /// Show Dates With Times
    /// </summary>
    /// <returns></returns>
    public async Task ShowDays()
    {
        var dates = await daysReserveRepository.GetTimesDayReserve();
        if (dates.Success)
        {
            var res = dates.Response.ToList();
            DaysReserves = res;

            var times = res.SelectMany(x => x.TimesReserves);

            if (times.Any())
            {
                TimesReserves = times.GroupBy(x => x.DaysReserveId).ToList();
            }
        }
    }

    /// <summary>
    /// Set  selectTime
    /// </summary>
    /// <param name="selectTime"></param>
    public void SetSelectedTime(TimesReserve selectTime)
    {
        SelectedTime = selectTime;
        ButtonContinueReserve = d_block;
    }

    /// <summary>
    /// Time Selected and put class  selected
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    public string SelectedClass(TimesReserve time) 
        => time == SelectedTime ? "selected" : string.Empty;

    /// <summary>
    /// Button Save  ContinuePurchaseProcess
    /// </summary>
    public void ContinuePurchaseProcess()
    {
        if (SelectedTime.Id == 0)
        {
            return;
        }
        FinishInfo = d_block;
        ButtonContinueReserve = d_none;
    }

    /// <summary>
    /// cancell reserve and reset fields
    /// </summary>
    public void CancellReserve()
    {
        FinishInfo = d_none;
        ButtonContinueReserve = d_block;
        ReserveDto = new();
        StateHasChanged();
    }

    /// <summary>
    /// Save Form user information and reservation
    /// </summary>
    /// <returns></returns>
    public async Task SaveInfoAndConnectToPay()
    {
        var randomNumber = new Random().Next(10000, 999000);

        IsLoader = !IsLoader;
        
        ReserveDto.TimesReserveId = SelectedTime.Id;
        ReserveDto.Password = ReserveDto.NationalCode;
        ReserveDto.RoleId = (long)RoleEnum.user;
        ReserveDto.Status = StatusEnum.Pending;

        string toMobile = ReserveDto.Mobile;

        if (!string.IsNullOrEmpty(toMobile))
        {
            await GetSmsSend(toMobile, randomNumber);         
        }

        if (ReserveDto.SingleUseCode!=0)
        {
            await GetAddReserve();      
        }   

    }
}

/// <summary>
/// Private Methods
/// </summary>
public partial class ReservationComponentBase : ComponentBase
{
    /// <summary>
    /// Changes for successful operations
    /// </summary>
    private void SuccessAction()
    {
        FinishInfo = d_none;
        ButtonContinueReserve = d_none;
        SelectedTime = new();
        ReserveDto = new();
        MessageText = "رزرو شما با موفقیت ثبت گردید";
        MessageColor = "success";
        MessageInfo = d_block;
        Snackbar.Add(MessageText);
        IsLoader = !IsLoader;
    }

    /// <summary>
    ///  Changes for failure operations
    /// </summary>
    private void FailedAction()
    {
        MessageText = "رزرو شما با شکست مواجه گردید";
        MessageColor = "danger";

        MessageInfo = d_block;
        Snackbar.Add(MessageText);
        IsLoader = !IsLoader;
    }

    /// <summary>
    /// Do None Display For Fields And Button
    /// </summary>
    private void DoDisplayNone()
    {
        FinishInfo = d_none;
        ShowDivFieldSingleUseCode = d_none;
        ButtonContinueReserve = d_none;
        MessageInfo = d_none;
    }

    /// <summary>
    /// Sms Send When Success Return True Else False
    /// </summary>
    /// <param name="to"></param>
    /// <param name="text"></param>
    /// <returns></returns>
    private async Task<bool> SmsSend(string to, string text)
    {
        var sms = new SmsDto()
        {
            To = to,
            Text = text
        };

        var result = await smsService.SendSmsAsync(sms);
        if (result.Success)
            return result.Response;
        return false;
    }

    /// <summary>
    /// Add Reserve and Show Message Success or Failed
    /// </summary>
    /// <returns></returns>
    private async Task GetAddReserve()
    {
        var res = await reserveRepository.AddReserve(ReserveDto);

        if (res.Success && res.Response)
        {
            SuccessAction();
        }
        else
        {
            FailedAction();
        }

        await jSRuntime.InvokeVoidAsync("swalFire", "نتیجه رزرو", MessageText, MessageColor);
    }

    /// <summary>
    /// Sms Send and div SingleUseCode do block
    /// </summary>
    /// <param name="toMobile"></param>
    /// <param name="randomNumber"></param>
    /// <returns></returns>
    private async Task GetSmsSend(string toMobile,int randomNumber)
    {
        string text = $"کد یکبار مصرف رزرو پزشکی : {randomNumber}";

        var resSmsm = await SmsSend(toMobile, text);

        if (resSmsm)
            ShowDivFieldSingleUseCode = d_block;
    }

}
