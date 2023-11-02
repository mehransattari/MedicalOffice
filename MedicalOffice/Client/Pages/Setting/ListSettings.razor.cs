using MedicalOffice.Client.Repositories.Inteface;
using MedicalOffice.Shared.DTO;
using MedicalOffice.Shared.Helper.Mapper;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace MedicalOffice.Client.Pages.Setting;


public partial class ListSettingsBase : ComponentBase
{

    #region Inject
    [Inject]
    public ISettingsRepository _SettingsRepository { get; set; }
    [Inject]
    public ISnackbar Snackbar { get; set; }
    [Inject]
    public IDialogService DialogService { get; set; }
    #endregion

    #region Properties
    public List<SettingsDto> listSettings { get; set; } = new List<SettingsDto>();
    public IEnumerable<SettingsDto> enumSettings = new List<SettingsDto>();
    public IEnumerable<SettingsDto> pagedData = new List<SettingsDto>();
    public MultipartFormDataContent MultipartFormData = new MultipartFormDataContent();
    public MudTable<SettingsDto>? SettingsTable;
    public bool disableButtonAdd { get; set; } = false;

    #endregion

    #region ServerReload
    public async Task<TableData<SettingsDto>> ServerReload(TableState state)
    {
        if (EditedSettings.Id != 0)
        {
            MultipartFormData.Add(new StringContent(EditedSettings.ShortDescription), "ShortDescription");
            MultipartFormData.Add(new StringContent(EditedSettings.SiteName), "SiteName");
            MultipartFormData.Add(new StringContent(EditedSettings.Id.ToString()), "Id");
            MultipartFormData.Add(new StringContent(EditedSettings.Logo), "Logo");
            MultipartFormData.Add(new StringContent(EditedSettings.Whatsapp), "Whatsapp");
            MultipartFormData.Add(new StringContent(EditedSettings.Telegram), "Telegram");
            MultipartFormData.Add(new StringContent(EditedSettings.Instagram), "Instagram");


            var result = await _SettingsRepository.UpdateSettings(MultipartFormData);
            if (result.Response)
            {
                StateHasChanged();
                EditedSettings.Id = 0;
            }
        }

        var resultQuery = await _SettingsRepository.GetSettings();

        if (resultQuery.Success)
        {
            var res = resultQuery.Response.ToArray();
            enumSettings = res.Mapper();
            disableButtonAdd = enumSettings.Count() > 0 ? true : false;
            this.StateHasChanged();
        }

        pagedData = enumSettings.ToArray();

        return new TableData<SettingsDto>() { TotalItems = 0, Items = pagedData };
    }


    #endregion
}

/// <summary>
/// Add
/// </summary>
public partial class ListSettingsBase : ComponentBase
{
    public async Task OpenAddDialogAdd()
    {
        DialogOptions closeOnEscapeKey = new DialogOptions()
        {
            CloseOnEscapeKey = true,
            MaxWidth = MaxWidth.Medium,
            FullWidth = true,
        };

        var result = await DialogService.Show<FormDialogSettings>("افزودن خدمت", closeOnEscapeKey).Result;

        if (!result.Canceled)
        {
            StateHasChanged();
            await SettingsTable.ReloadServerData();
        }
    }
}

/// <summary>
/// Edit
/// </summary>
public partial class ListSettingsBase : ComponentBase
{
    /// <summary>
    /// باز شدن مدال برای ویرایش
    /// </summary>
    /// <returns></returns>
    public async Task OpenAddDialogEdit()
    {
        var parameters = new DialogParameters<FormDialogSettings>();

        var Id = selectedItems.Select(x => x.Id).FirstOrDefault();
        var ShortDescription = selectedItems.Select(x => x.ShortDescription).FirstOrDefault();
        var Whatsapp = selectedItems.Select(x => x.Whatsapp).FirstOrDefault();
        var Telegram = selectedItems.Select(x => x.Telegram).FirstOrDefault();
        var Instagram = selectedItems.Select(x => x.Instagram).FirstOrDefault();
        var SiteName = selectedItems.Select(x => x.SiteName).FirstOrDefault();
        var Logo = selectedItems.Select(x => x.Logo).FirstOrDefault();



        parameters.Add(x => x.Id, Id);
        parameters.Add(x => x.SiteName, SiteName);
        parameters.Add(x => x.Instagram, Instagram);
        parameters.Add(x => x.Telegram, Telegram);
        parameters.Add(x => x.Whatsapp, Whatsapp);
        parameters.Add(x => x.ShortDescription, ShortDescription);
        parameters.Add(x => x.Logo, Logo);



        DialogOptions closeOnEscapeKey = new DialogOptions()
        {
            CloseOnEscapeKey = true,
            MaxWidth = MaxWidth.Medium,
            FullWidth = true,
        };

        var result = await DialogService.Show<FormDialogSettings>("ویرایش تنظیمات", parameters, closeOnEscapeKey).Result;

        if (!result.Canceled)
        {
            StateHasChanged();
            await SettingsTable.ReloadServerData();
        }
    }
}

/// <summary>
/// Row Edit
/// </summary>
public partial class ListSettingsBase : ComponentBase
{
    #region Row Edit
    public SettingsDto SettingsBeforeEdit { get; set; } = new SettingsDto();
    public SettingsDto EditedSettings { get; set; } = new SettingsDto();

    /// <summary>
    /// هنگام کلیک روی دکمه ویرایش سطری
    /// </summary>
    /// <param name="Settings"></param>
    public void BackupItem(object Settings)
    {
        SettingsBeforeEdit = new()
        {
            Id = ((SettingsDto)Settings).Id,
            ShortDescription = ((SettingsDto)Settings).ShortDescription,
            SiteName = ((SettingsDto)Settings).SiteName,
            Instagram = ((SettingsDto)Settings).Instagram,
            Telegram = ((SettingsDto)Settings).Telegram,
            Whatsapp = ((SettingsDto)Settings).Whatsapp,
            Logo = ((SettingsDto)Settings).Logo,
        };
    }

    /// <summary>
    /// ذخیره اطلاعات در هنگام ویرایش سطری
    /// </summary>
    /// <param name="Settings"></param>
    public void ItemHasBeenCommitted(object Settings)
    {
        var res = (SettingsDto)Settings;
        EditedSettings = res;
        SettingsTable.ReloadServerData();
    }

    /// <summary>
    /// کنسل کردن  اطلاعات در هنگام ویرایش سطری
    /// </summary>
    /// <param name="Settings"></param>
    public void ResetItemToOriginalValues(object Settings)
    {
        ((SettingsDto)Settings).Id = SettingsBeforeEdit.Id;
        ((SettingsDto)Settings).ShortDescription = SettingsBeforeEdit.ShortDescription;
        ((SettingsDto)Settings).SiteName = SettingsBeforeEdit.SiteName;
        ((SettingsDto)Settings).Instagram = SettingsBeforeEdit.Instagram;
        ((SettingsDto)Settings).Telegram = SettingsBeforeEdit.Telegram;
        ((SettingsDto)Settings).Whatsapp = SettingsBeforeEdit.Whatsapp;
        ((SettingsDto)Settings).Logo = SettingsBeforeEdit.Logo;

    }
    #endregion
}

/// <summary>
/// Selected Row
/// </summary>
public partial class ListSettingsBase : ComponentBase
{
    #region Selected Row
    public HashSet<SettingsDto> selectedItems = new HashSet<SettingsDto>();
    public bool _selectOnRowClick = true;
    public SettingsDto _selectedItem = new SettingsDto();
    public void OnRowClick(TableRowClickEventArgs<SettingsDto> args)
    {
        _selectedItem = args.Item;
    }
    #endregion
}

/// <summary>
/// Delete Message Box
/// </summary>
public partial class ListSettingsBase : ComponentBase
{
    public MudMessageBox? mbox { get; set; }

    public async void OnButtonClicked()
    {
        bool? result = await mbox.Show();
        StateHasChanged();
    }
    public async Task DeleteSettingss()
    {
        var ids = selectedItems.Select(x => x.Id);
        await _SettingsRepository.DeleteSettingsByIds(ids);
        await SettingsTable.ReloadServerData();
    }

}