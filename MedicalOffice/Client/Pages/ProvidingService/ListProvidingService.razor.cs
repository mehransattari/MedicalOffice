using MedicalOffice.Client.Repositories.Inteface;
using MedicalOffice.Shared.DTO;
using MedicalOffice.Shared.Helper.Mapper;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace MedicalOffice.Client.Pages.ProvidingService;

public partial class ListProvidingServiceBase : ComponentBase
{

    #region Inject
    [Inject]
    public IProvidingServiceRepository _ProvidingServiceRepository { get; set; }
    [Inject]
    public ISnackbar Snackbar { get; set; }
    [Inject]
    public IDialogService DialogService { get; set; }
    #endregion

    #region Properties
    public List<ProvidingServiceDto> listProvidingService { get; set; } = new List<ProvidingServiceDto>();
    public IEnumerable<ProvidingServiceDto> enumProvidingService = new List<ProvidingServiceDto>();
    public IEnumerable<ProvidingServiceDto> pagedData = new List<ProvidingServiceDto>();
    public MultipartFormDataContent MultipartFormData = new MultipartFormDataContent();
    public MudTable<ProvidingServiceDto>? ProvidingServiceTable;
    public bool disableButtonAdd { get; set; } = false;

    #endregion

    #region ServerReload
    public async Task<TableData<ProvidingServiceDto>> ServerReload(TableState state)
    {
        if (EditedProvidingService.Id != 0)
        {
            MultipartFormData.Add(new StringContent(EditedProvidingService.Title), "Title");
            MultipartFormData.Add(new StringContent(EditedProvidingService.Desc), "Desc");
            MultipartFormData.Add(new StringContent(EditedProvidingService.Id.ToString()), "Id");
            MultipartFormData.Add(new StringContent(EditedProvidingService.ImageUrl), "ImageUrl");
            MultipartFormData.Add(new StringContent(EditedProvidingService.ShortDesc), "ShortDesc");


            var result = await _ProvidingServiceRepository.UpdateProvidingService(MultipartFormData);
            if (result.Response)
            {
                StateHasChanged();
                EditedProvidingService.Id = 0;
            }
        }

        var resultQuery = await _ProvidingServiceRepository.GetProvidingService();

        if (resultQuery.Success)
        {
            var res = resultQuery.Response.ToArray();
            enumProvidingService = res.Mapper();
            disableButtonAdd = enumProvidingService.Count() > 0 ? true : false;
            this.StateHasChanged();
        }

        pagedData = enumProvidingService.ToArray();

        return new TableData<ProvidingServiceDto>() { TotalItems = 0, Items = pagedData };
    }


    #endregion
}

/// <summary>
/// Add
/// </summary>
public partial class ListProvidingServiceBase : ComponentBase
{
    public async Task OpenAddDialogAdd()
    {
        DialogOptions closeOnEscapeKey = new DialogOptions()
        {
            CloseOnEscapeKey = true,
            MaxWidth = MaxWidth.Medium,
            FullWidth = true,
        };

        var result = await DialogService.Show<FormDialogProvidingService>("افزودن خدمت", closeOnEscapeKey).Result;

        if (!result.Canceled)
        {
            StateHasChanged();
            await ProvidingServiceTable.ReloadServerData();
        }
    }
}

/// <summary>
/// Edit
/// </summary>
public partial class ListProvidingServiceBase : ComponentBase
{
    /// <summary>
    /// باز شدن مدال برای ویرایش
    /// </summary>
    /// <returns></returns>
    public async Task OpenAddDialogEdit()
    {
        var parameters = new DialogParameters<FormDialogProvidingService>();

        var Id = selectedItems.Select(x => x.Id).FirstOrDefault();
        var Title = selectedItems.Select(x => x.Title).FirstOrDefault();
        var Desc = selectedItems.Select(x => x.Desc).FirstOrDefault();
        var ImageUrl = selectedItems.Select(x => x.ImageUrl).FirstOrDefault();
        var ShortDesc = selectedItems.Select(x => x.ShortDesc).FirstOrDefault();



        parameters.Add(x => x.Id, Id);
        parameters.Add(x => x.Title, Title);
        parameters.Add(x => x.Desc, Desc);
        parameters.Add(x => x.ImageUrl, ImageUrl);
        parameters.Add(x => x.ShortDesc, ShortDesc);



        DialogOptions closeOnEscapeKey = new DialogOptions()
        {
            CloseOnEscapeKey = true,
            MaxWidth = MaxWidth.Medium,
            FullWidth = true,
        };

        var result = await DialogService.Show<FormDialogProvidingService>("ویرایش خدمت", parameters, closeOnEscapeKey).Result;

        if (!result.Canceled)
        {
            StateHasChanged();
            await ProvidingServiceTable.ReloadServerData();
        }
    }
}

/// <summary>
/// Row Edit
/// </summary>
public partial class ListProvidingServiceBase : ComponentBase
{
    #region Row Edit
    public ProvidingServiceDto ProvidingServiceBeforeEdit { get; set; } = new ProvidingServiceDto();
    public ProvidingServiceDto EditedProvidingService { get; set; } = new ProvidingServiceDto();

    /// <summary>
    /// هنگام کلیک روی دکمه ویرایش سطری
    /// </summary>
    /// <param name="ProvidingService"></param>
    public void BackupItem(object ProvidingService)
    {
        ProvidingServiceBeforeEdit = new()
        {
            Id = ((ProvidingServiceDto)ProvidingService).Id,
            Title = ((ProvidingServiceDto)ProvidingService).Title,
            Image = ((ProvidingServiceDto)ProvidingService).Image,
            Desc = ((ProvidingServiceDto)ProvidingService).Desc,
            ShortDesc = ((ProvidingServiceDto)ProvidingService).ShortDesc,
        };
    }

    /// <summary>
    /// ذخیره اطلاعات در هنگام ویرایش سطری
    /// </summary>
    /// <param name="ProvidingService"></param>
    public void ItemHasBeenCommitted(object ProvidingService)
    {
        var res = (ProvidingServiceDto)ProvidingService;
        EditedProvidingService = res;
        ProvidingServiceTable.ReloadServerData();
    }

    /// <summary>
    /// کنسل کردن  اطلاعات در هنگام ویرایش سطری
    /// </summary>
    /// <param name="ProvidingService"></param>
    public void ResetItemToOriginalValues(object ProvidingService)
    {
        ((ProvidingServiceDto)ProvidingService).Id = ProvidingServiceBeforeEdit.Id;
        ((ProvidingServiceDto)ProvidingService).Title = ProvidingServiceBeforeEdit.Title;
        ((ProvidingServiceDto)ProvidingService).Image = ProvidingServiceBeforeEdit.Image;
        ((ProvidingServiceDto)ProvidingService).ShortDesc = ProvidingServiceBeforeEdit.ShortDesc;
        ((ProvidingServiceDto)ProvidingService).Desc = ProvidingServiceBeforeEdit.Desc;
    }
    #endregion
}

/// <summary>
/// Selected Row
/// </summary>
public partial class ListProvidingServiceBase : ComponentBase
{
    #region Selected Row
    public HashSet<ProvidingServiceDto> selectedItems = new HashSet<ProvidingServiceDto>();
    public bool _selectOnRowClick = true;
    public ProvidingServiceDto _selectedItem = new ProvidingServiceDto();
    public void OnRowClick(TableRowClickEventArgs<ProvidingServiceDto> args)
    {
        _selectedItem = args.Item;
    }
    #endregion
}

/// <summary>
/// Delete Message Box
/// </summary>
public partial class ListProvidingServiceBase : ComponentBase
{
    public MudMessageBox? mbox { get; set; }

    public async void OnButtonClicked()
    {
        bool? result = await mbox.Show();
        StateHasChanged();
    }
    public async Task DeleteProvidingServices()
    {
        var ids = selectedItems.Select(x => x.Id);
        await _ProvidingServiceRepository.DeleteProvidingServiceByIds(ids);
        await ProvidingServiceTable.ReloadServerData();
    }

}