using MedicalOffice.Client.Repositories.Inteface;
using MedicalOffice.Shared.DTO;
using MedicalOffice.Shared.Helper.Mapper;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace MedicalOffice.Client.Pages.Reservation;



public partial class ListReserveBase : ComponentBase
{

    #region Inject
    [Inject]
    public IReserveRepository _ReserveRepository { get; set; }
    [Inject]
    public ISnackbar Snackbar { get; set; }
    [Inject]
    public IDialogService DialogService { get; set; }
    #endregion

    #region Properties
    public List<ReserveDto> listReserve { get; set; } = new List<ReserveDto>();
    public IEnumerable<ReserveDto> enumReserve = new List<ReserveDto>();
    public IEnumerable<ReserveDto> pagedData = new List<ReserveDto>();
    public MultipartFormDataContent MultipartFormData = new MultipartFormDataContent();
    public MudTable<ReserveDto>? ReserveTable;
    public int totalItems;

    #endregion

    #region ServerReload
    public async Task<TableData<ReserveDto>> ServerReload(TableState state)
    {
        if (EditedReserve.Id != 0)
        {
            var result = await _ReserveRepository.UpdateReserve(EditedReserve);
            if (result.Response)
            {
                StateHasChanged();
                EditedReserve.Id = 0;
            }
        }

        var resultQuery = await _ReserveRepository.GetAllReserves();

        if (resultQuery.Success)
        {
            var res = resultQuery.Response.ToArray();
            enumReserve = res.AsEnumerable();
            this.StateHasChanged();
        }

        pagedData = enumReserve.ToArray();

        return new TableData<ReserveDto>() { TotalItems = 0, Items = pagedData };
    }


    #endregion
}

/// <summary>
/// Add
/// </summary>
public partial class ListReserveBase : ComponentBase
{
    public async Task OpenAddDialogAdd()
    {
        DialogOptions closeOnEscapeKey = new DialogOptions()
        {
            CloseOnEscapeKey = true,
            MaxWidth = MaxWidth.Medium,
            FullWidth = true,
        };

        var result = await DialogService.Show<FormDialogReserve>("افزودن رزرو ", closeOnEscapeKey).Result;

        if (!result.Canceled)
        {
            StateHasChanged();
            await ReserveTable.ReloadServerData();
        }
    }
}

/// <summary>
/// Edit
/// </summary>
public partial class ListReserveBase : ComponentBase
{
    /// <summary>
    /// باز شدن مدال برای ویرایش
    /// </summary>
    /// <returns></returns>
    public async Task OpenAddDialogEdit()
    {
        var parameters = new DialogParameters<FormDialogReserve>();

        var Id = selectedItems.Select(x => x.Id).FirstOrDefault();
        var NationalCode = selectedItems.Select(x => x.NationalCode).FirstOrDefault();
        var Mobile = selectedItems.Select(x => x.Mobile).FirstOrDefault();
        var FirstName = selectedItems.Select(x => x.FirstName).FirstOrDefault();
        var LastName = selectedItems.Select(x => x.LastName).FirstOrDefault();
        var TimesReserveId = selectedItems.Select(x => x.TimesReserveId).FirstOrDefault();
        var UserId = selectedItems.Select(x => x.UserId).FirstOrDefault();


        parameters.Add(x => x.Id, Id);
        parameters.Add(x => x.NationalCode, NationalCode);
        parameters.Add(x => x.Mobile, Mobile);
        parameters.Add(x => x.FirstName, FirstName);
        parameters.Add(x => x.LastName, LastName);
        parameters.Add(x => x.TimesReserveId, TimesReserveId);
        parameters.Add(x => x.UserId, UserId);


        DialogOptions closeOnEscapeKey = new DialogOptions()
        {
            CloseOnEscapeKey = true,
            MaxWidth = MaxWidth.Medium,
            FullWidth = true,
        };

        var result = await DialogService.Show<FormDialogReserve>("ویرایش  رزرو", parameters, closeOnEscapeKey).Result;

        if (!result.Canceled)
        {
            StateHasChanged();
            await ReserveTable.ReloadServerData();
        }
    }
}

/// <summary>
/// Row Edit
/// </summary>
public partial class ListReserveBase : ComponentBase
{
    #region Row Edit
    public ReserveDto ReserveBeforeEdit { get; set; } = new ReserveDto();
    public ReserveDto EditedReserve { get; set; } = new ReserveDto();

    /// <summary>
    /// هنگام کلیک روی دکمه ویرایش سطری
    /// </summary>
    /// <param name="Reserve"></param>
    public void BackupItem(object Reserve)
    {
        ReserveBeforeEdit = new()
        {
            Id = ((ReserveDto)Reserve).Id,
            FirstName = ((ReserveDto)Reserve).FirstName,
            LastName = ((ReserveDto)Reserve).LastName,
            Mobile = ((ReserveDto)Reserve).Mobile,
            NationalCode = ((ReserveDto)Reserve).NationalCode,
            TimesReserveId = ((ReserveDto)Reserve).TimesReserveId,
            UserId = ((ReserveDto)Reserve).UserId,
        };
    }

    /// <summary>
    /// ذخیره اطلاعات در هنگام ویرایش سطری
    /// </summary>
    /// <param name="Reserve"></param>
    public void ItemHasBeenCommitted(object Reserve)
    {
        var res = (ReserveDto)Reserve;
        EditedReserve = res;
        ReserveTable.ReloadServerData();
    }

    /// <summary>
    /// کنسل کردن  اطلاعات در هنگام ویرایش سطری
    /// </summary>
    /// <param name="Reserve"></param>
    public void ResetItemToOriginalValues(object Reserve)
    {
        ((ReserveDto)Reserve).Id = ReserveBeforeEdit.Id;
        ((ReserveDto)Reserve).FirstName = ReserveBeforeEdit.FirstName;
        ((ReserveDto)Reserve).LastName = ReserveBeforeEdit.LastName;
        ((ReserveDto)Reserve).Mobile = ReserveBeforeEdit.Mobile;
        ((ReserveDto)Reserve).NationalCode = ReserveBeforeEdit.NationalCode;
        ((ReserveDto)Reserve).TimesReserveId = ReserveBeforeEdit.TimesReserveId;
        ((ReserveDto)Reserve).UserId = ReserveBeforeEdit.UserId;
    }
    #endregion
}

/// <summary>
/// Selected Row
/// </summary>
public partial class ListReserveBase : ComponentBase
{
    #region Selected Row
    public HashSet<ReserveDto> selectedItems = new HashSet<ReserveDto>();
    public bool _selectOnRowClick = true;
    public ReserveDto _selectedItem = new ReserveDto();
    public void OnRowClick(TableRowClickEventArgs<ReserveDto> args)
    {
        _selectedItem = args.Item;
    }
    #endregion
}

/// <summary>
/// Delete Message Box
/// </summary>
public partial class ListReserveBase : ComponentBase
{
    public MudMessageBox? mbox { get; set; }

    public async void OnButtonClicked()
    {
        bool? result = await mbox.Show();
        StateHasChanged();
    }
    public async Task DeleteReserves()
    {
        var ids = selectedItems.Select(x => x.Id);
        await _ReserveRepository.DeleteReservesByIds(ids);
        await ReserveTable.ReloadServerData();
    }

}