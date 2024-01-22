using MedicalOffice.Client.Repositories.Inteface;
using MedicalOffice.Shared.DTO;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;

namespace MedicalOffice.Client.Pages.Reservation;



public partial class FormDialogReserveBase : ComponentBase
{
    #region Inject 
    [Inject]
    public IReserveRepository _ReserveRepository { get; set; }

    [CascadingParameter]
    public MudDialogInstance MudDialog { get; set; }

     #endregion

    #region Parameter
    [Parameter]
    public long Id { get; set; }

    [Parameter]
    public string? NationalCode { get; set; }

    [Parameter]
    public string? Mobile { get; set; }

    [Parameter]
    public string? FirstName { get; set; }

    [Parameter]
    public string? LastName { get; set; }

    [Parameter]
    public long TimesReserveId { get; set; }

    [Parameter]
    public long UserId { get; set; }
    #endregion

    #region Fields
    public MultipartFormDataContent MultipartFormData = new();

    public MudTextField<string>? multilineReference;
    public string? ReserveName { get; set; }

    public ReserveDto Reserve = new ReserveDto();

    #endregion

    protected override void OnInitialized()
    {
        if (Id != 0)
        {
            Reserve.Id = Id;
            Reserve.FirstName = FirstName;
            Reserve.LastName = LastName;
            Reserve.NationalCode = NationalCode;
            Reserve.Mobile = Mobile;
            Reserve.TimesReserveId = TimesReserveId;
            Reserve.UserId = UserId;
        }
    }

}



/// <summary>
/// OnValidSubmit
/// </summary>
public partial class FormDialogReserveBase : ComponentBase
{
    public bool success;
    public void Submit() => MudDialog.Close(DialogResult.Ok(true));
    public void Cancel() => MudDialog.Cancel();
    public async Task OnValidSubmit(EditContext context)
    {
        success = true;

        if (Id != 0)
        {
            await _ReserveRepository.UpdateReserve(Reserve);
        }
        else
        {
            await _ReserveRepository.CreateReserve(Reserve);
        }

        StateHasChanged();
        Submit();
    }
}
