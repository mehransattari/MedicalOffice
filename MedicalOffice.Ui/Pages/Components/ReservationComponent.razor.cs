using MedicalOffice.Shared.DTO;
using MedicalOffice.Shared.Entities;
using MedicalOffice.Shared.Enum;
using MedicalOffice.Ui.Repositories.Inteface;
using MedicalOffice.Ui.Services.Helper;
using MedicalOffice.Ui.Services.Interface;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;
using System.Text;

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

    public bool IsReadOnly { get; set; } = false;

    public int RandonNumber { get; set; }
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
        IsReadOnly = false;
        ShowDivFieldSingleUseCode = d_none;
    }

    /// <summary>
    /// Save Form user information and reservation
    /// </summary>
    /// <returns></returns>
    public async Task SaveInfoAndConnectToPay()
    {
        try
        {
            IsLoader = true;

            fillFieldsReserveDto();

            string toMobile = ReserveDto.Mobile;

            //Sms Code
            if (string.IsNullOrEmpty(ReserveDto.SingleUseCode) &&
                !string.IsNullOrEmpty(toMobile))
            {
                await GetSingleUseCodeSmsSend(toMobile);
                IsLoader = false;
            }

            //Add reserve
            if (!string.IsNullOrEmpty(ReserveDto.SingleUseCode) &&
                ReserveDto.SingleUseCode == RandonNumber.ToString())
            {
                await GetAddReserve();
                IsLoader = false;
            }

            if (!string.IsNullOrEmpty(ReserveDto.SingleUseCode) &&
                ReserveDto.SingleUseCode != RandonNumber.ToString())
            {
                await NoValidReciveSingleUseCode();
            }
        }
        catch (Exception ex)
        {

            IsLoader = false;
            IsReadOnly = false;
        }

        #region local functions

        //Fill Fields ReserveDto 
        void fillFieldsReserveDto()
        {
            var randomCode = new Random().Next(10000, 999000);

            ReserveDto.TimesReserveId = SelectedTime.Id;
            ReserveDto.Password = ReserveDto.NationalCode;
            ReserveDto.RoleId = (long)RoleEnum.user;
            ReserveDto.Status = StatusEnum.Pending;
            ReserveDto.Code = randomCode;
        }

        //Show Message When Not Valid Single Use Code
        async Task NoValidReciveSingleUseCode()
        {
            string messageSingleUseCode = "کد وارد شده معتبر نمی باشد.";

            await GetSwalFire("نتیجه رزرو", messageSingleUseCode, "danger");

            IsLoader = false;
        }

        #endregion
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
    private void SuccessReserveAction()
    {
        FinishInfo = d_none;
        ButtonContinueReserve = d_none;
        SelectedTime = new();
        ReserveDto = new();
        MessageText = "رزرو شما با موفقیت ثبت گردید";
        MessageColor = "success";
        MessageInfo = d_block;
        Snackbar.Add(MessageText);
        IsLoader = false;
        IsReadOnly = false;
        ShowDivFieldSingleUseCode = d_none;

    }

    /// <summary>
    ///  Changes for failure operations
    /// </summary>
    private void FailedReserveAction()
    {
        MessageText = "رزرو شما ناموفق بود ";
        MessageColor = "danger";
        MessageInfo = d_block;
        Snackbar.Add(MessageText);
        IsLoader = false;
        IsReadOnly = false;
        ShowDivFieldSingleUseCode = d_none;


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
    /// Add Reserve and Show Message Success or Failed
    /// </summary>
    /// <returns></returns>
    private async Task<bool> GetAddReserve()
    {

        var isCheckDuplicateReservation = await reserveRepository
            .CheckDuplicateReservation(ReserveDto.NationalCode, ReserveDto.TimesReserveId);

        if (isCheckDuplicateReservation.Success && isCheckDuplicateReservation.Response)
        {
            FailedReserveAction();

            await GetSwalFire("نتیجه رزرو", $"این تاریخ برای این کد ملی از قبل ثبت شده است.", "danger");

            return false;
        }

        var res = await reserveRepository.AddReserve(ReserveDto);

        if (res.Success && res.Response)
        {
            await getSendSmsmActiveCodeReserve(ReserveDto);

            SuccessReserveAction();

            await GetSwalFire("نتیجه رزرو", MessageText, MessageColor);

            return true;
        }
        else
        {
            FailedReserveAction();
            MessageText = "برای این کد ملی از قبل برای این تاریخ ثبت شده است.";
            await GetSwalFire(MessageText,"نتیجه رزرو",  MessageColor);

            return false;
        }



        //Send Sms TO User for Active User Code + Time + Date
        async Task getSendSmsmActiveCodeReserve(ReserveDto reserveDto)
        {
            if (reserveDto.TimesReserveId != 0)
            {
                try
                {
                    var dateTime = await reserveRepository.ShowDateAndTimeByTimeReserveId(reserveDto.TimesReserveId);

                    if (dateTime.Success)
                    {
                        var date = dateTime.Response.DaysReserve?.Day.ToShamsi();
                        var day = dateTime.Response.DaysReserve?.Day.ToDayShamsi();
                        var time = dateTime.Response.FromTime.ToString("hh\\:mm") + " - " + dateTime.Response.ToTime.ToString("hh\\:mm");
                        var code = reserveDto.Code;

                        StringBuilder text = new StringBuilder();

                        text.AppendLine($"کد رزرو شما : {code}");
                        text.AppendLine($" روز : {day}");
                        text.AppendLine($" ساعت : {time}");
                        text.AppendLine($" تاریخ : {date}");
                        text.AppendLine("با موفقیت ثبت گردید.");

                        await SmsSend(reserveDto.Mobile, text.ToString());
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Sms :" + ex.Message);
                    throw;
                }

            }
        }
    }

    /// <summary>
    /// Sms Send and div SingleUseCode do block
    /// </summary>
    /// <param name="toMobile"></param>
    /// <param name="randomNumber"></param>
    /// <returns></returns>
    private async Task GetSingleUseCodeSmsSend(string toMobile)
    {
        RandonNumber = new Random().Next(10000, 999000);

        Console.WriteLine("RandonNumber : "+RandonNumber);

        string text = $"کد یکبار مصرف رزرو پزشکی : {RandonNumber}";

        var resSmsm = await SmsSend(toMobile, text);

        if (resSmsm)
        {
            ShowDivFieldSingleUseCode = d_block;
            IsLoader = false;
            IsReadOnly = true;
        }
    }

    /// <summary>
    /// Sms Send When Success Return True Else False
    /// </summary>
    /// <param name="to"></param>
    /// <param name="text"></param>
    /// <returns></returns>
    private async Task<bool> SmsSend(string to, string text)
    {
        //var sms = new SmsDto()
        //{
        //    To = to,
        //    Text = text
        //};

        //var result = await smsService.SendSmsAsync(sms);

        //if (result.Success)
        //{
        //    return result.Response;
        //}

        return true;
    }

    /// <summary>
    /// Call Swal Filre
    /// </summary>
    /// <param name="text"></param>
    /// <param name="title"></param>
    /// <param name="color"></param>
    /// <returns></returns>
    private async Task GetSwalFire(string text, string title, string color) =>
        await jSRuntime.InvokeVoidAsync("swalFire", title, text, color);

}
