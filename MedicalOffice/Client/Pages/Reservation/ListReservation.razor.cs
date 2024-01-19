using MedicalOffice.Client.Repositories.Inteface;
using MedicalOffice.Shared.DTO;
using MedicalOffice.Shared.Helper.Mapper;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace MedicalOffice.Client.Pages.Reservation;


/// <summary>
/// Inject
/// </summary>
public partial class ListReservationBase : ComponentBase
{
    [Inject]
    public ISnackbar Snackbar { get; set; }
    [Inject]
    public IDialogService DialogService { get; set; }
    [Inject]
    public IReserveRepository _reserveRepository { get; set; }
}

/// <summary>
/// Fields
/// </summary>
public partial class ListReservationBase : ComponentBase
{
    public MudTable<ReserveDto>? ReserveTable;
    public bool disableButtonAdd { get; set; } = false;
}

/// <summary>
/// Parameters
/// </summary>
public partial class ListReservationBase : ComponentBase
{
    public List<ReserveDto> listReserve { get; set; } = new List<ReserveDto>();
    public MultipartFormDataContent MultipartFormData = new();
    public IEnumerable<ReserveDto> enumReserve = new List<ReserveDto>();
    public IEnumerable<ReserveDto> pagedData = new List<ReserveDto>();
    public ReserveDto ReserveBeforeEdit { get; set; } = new ();
    public ReserveDto EditedReserve { get; set; } = new ();
}

/// <summary>
/// Methods
/// </summary>
public partial class ListReservationBase : ComponentBase
{
    #region ServerReload
    public async Task<TableData<ReserveDto>> ServerReload(TableState state)
    {
        if (EditedReserve.Id != 0)
        {
            var result = await _reserveRepository.UpdateReserve(EditedReserve);
            if (result.Response)
            {
                StateHasChanged();
                EditedReserve.Id = 0;
            }
        }

        var resultQuery = await _reserveRepository.GetAllReserves();

        if (resultQuery.Success)
        {
            var res = resultQuery.Response.ToArray();
           // enumReserve = res.Mapper();
            disableButtonAdd = enumReserve.Count() > 0 ? true : false;
            this.StateHasChanged();
        }

        pagedData = enumReserve.ToArray();

        return new TableData<ReserveDto>() { TotalItems = 0, Items = pagedData };
    }
    #endregion
}

/// <summary>
/// Private Methods
/// </summary>
public partial class ListReservationBase : ComponentBase
{

}
