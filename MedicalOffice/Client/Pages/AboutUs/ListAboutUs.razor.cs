using MedicalOffice.Client.Repositories.Inteface;
using MedicalOffice.Shared.DTO;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using MedicalOffice.Client.Pages.AboutUs;
using MedicalOffice.Shared.Helper.Mapper;
namespace MedicalOffice.Client.Pages.AboutUss;

public partial class ListAboutUsBase : ComponentBase
{

    #region Inject
    [Inject]
    public IAboutUsRepository _aboutUsRepository { get; set; }
    [Inject]
    public ISnackbar Snackbar { get; set; }
    [Inject]
    public IDialogService DialogService { get; set; }
    #endregion

    #region Properties
    public List<AboutUsDto> listAboutUs { get; set; } = new List<AboutUsDto>();
    public IEnumerable<AboutUsDto> enumAboutUs = new List<AboutUsDto>();
    public IEnumerable<AboutUsDto> pagedData = new List<AboutUsDto>();
    public MultipartFormDataContent MultipartFormData = new MultipartFormDataContent();
    public MudTable<AboutUsDto>? AboutUsTable;

    public bool disableButtonAdd { get; set; } = false;

    #endregion

    #region ServerReload
    public async Task<TableData<AboutUsDto>> ServerReload(TableState state)
    {
        if (EditedAboutUs.Id != 0)
        {
            MultipartFormData.Add(new StringContent(EditedAboutUs.Title.ToString()), "Title");
            if (EditedAboutUs?.Text != null)
            {
                MultipartFormData.Add(new StringContent(EditedAboutUs.Text.ToString()), "Text");
            }
            MultipartFormData.Add(new StringContent(EditedAboutUs.Id.ToString()), "Id");
            if (EditedAboutUs?.ImageUrl != null)
            {
                MultipartFormData.Add(new StringContent(EditedAboutUs.ImageUrl.ToString()), "ImageUrl");
            }

            var result = await _aboutUsRepository.UpdateAboutUs(MultipartFormData);
            if (result.Response)
            {
                StateHasChanged();
                EditedAboutUs.Id = 0;
            }
        }

        var resultQuery = await _aboutUsRepository.GetAboutUs();

        if (resultQuery.Success)
        {
            var res = resultQuery.Response.ToArray();
            enumAboutUs = res.Mapper();
            disableButtonAdd = enumAboutUs.Count() > 0 ? true : false;
            this.StateHasChanged();
        }

        pagedData = enumAboutUs.ToArray();

        return new TableData<AboutUsDto>() { TotalItems = 0, Items = pagedData };
    }
    #endregion
}

/// <summary>
/// Add
/// </summary>
public partial class ListAboutUsBase : ComponentBase
{  
    public async Task OpenAddDialogAdd()
    {
        DialogOptions closeOnEscapeKey = new DialogOptions()
        {
            CloseOnEscapeKey = true,
            MaxWidth = MaxWidth.Medium,
            FullWidth = true,
        };

        var result = await DialogService.Show<FormDialogAboutUs>("افزودن درباره ما", closeOnEscapeKey).Result;

        if (!result.Canceled)
        {
            StateHasChanged();
            await AboutUsTable.ReloadServerData();
        }
    }
}

/// <summary>
/// Edit
/// </summary>
public partial class ListAboutUsBase : ComponentBase
{
    /// <summary>
    /// باز شدن مدال برای ویرایش
    /// </summary>
    /// <returns></returns>
    public async Task OpenAddDialogEdit()
    {
        var parameters = new DialogParameters<FormDialogAboutUs>();
        var Id = selectedItems.Select(x => x.Id).FirstOrDefault();
        var Title = selectedItems.Select(x => x.Title).FirstOrDefault();
        var Text = selectedItems.Select(x => x.Text).FirstOrDefault();
        var ImageUrl = selectedItems.Select(x => x.ImageUrl).FirstOrDefault();


        parameters.Add(x => x.Id, Id);
        parameters.Add(x => x.Title, Title);
        parameters.Add(x => x.Text, Text);
        parameters.Add(x => x.ImageUrl, ImageUrl);


        DialogOptions closeOnEscapeKey = new DialogOptions()
        {
            CloseOnEscapeKey = true,
            MaxWidth = MaxWidth.Medium,
            FullWidth = true,
        };

        var result = await DialogService.Show<FormDialogAboutUs>("ویرایش درباره ما", parameters, closeOnEscapeKey).Result;

        if (!result.Canceled)
        {
            StateHasChanged();
            await AboutUsTable.ReloadServerData();
        }
    }
}

/// <summary>
/// Row Edit
/// </summary>
public partial class ListAboutUsBase : ComponentBase
{
    #region Row Edit
    public AboutUsDto AboutUsBeforeEdit { get; set; } = new AboutUsDto();
    public AboutUsDto EditedAboutUs { get; set; } = new AboutUsDto();

    /// <summary>
    /// هنگام کلیک روی دکمه ویرایش سطری
    /// </summary>
    /// <param name="AboutUs"></param>
    public void BackupItem(object AboutUs)
    {
        AboutUsBeforeEdit = new()
        {
            Id = ((AboutUsDto)AboutUs).Id,
            Title = ((AboutUsDto)AboutUs).Title,
            Text = ((AboutUsDto)AboutUs).Text,
            Image = ((AboutUsDto)AboutUs).Image,
        };
    }

    /// <summary>
    /// ذخیره اطلاعات در هنگام ویرایش سطری
    /// </summary>
    /// <param name="AboutUs"></param>
    public void ItemHasBeenCommitted(object AboutUs)
    {
        var res = (AboutUsDto)AboutUs;
        EditedAboutUs = res;
        AboutUsTable.ReloadServerData();
    }

    /// <summary>
    /// کنسل کردن  اطلاعات در هنگام ویرایش سطری
    /// </summary>
    /// <param name="AboutUs"></param>
    public void ResetItemToOriginalValues(object AboutUs)
    {
        ((AboutUsDto)AboutUs).Id = AboutUsBeforeEdit.Id;
        ((AboutUsDto)AboutUs).Title = AboutUsBeforeEdit.Title;
        ((AboutUsDto)AboutUs).Image = AboutUsBeforeEdit.Image;
        ((AboutUsDto)AboutUs).Text = AboutUsBeforeEdit.Text;

    }
    #endregion
}

/// <summary>
/// Selected Row
/// </summary>
public partial class ListAboutUsBase : ComponentBase
{
    #region Selected Row
    public HashSet<AboutUsDto> selectedItems = new HashSet<AboutUsDto>();
    public bool _selectOnRowClick = true;
    public AboutUsDto _selectedItem = new AboutUsDto();
    public void OnRowClick(TableRowClickEventArgs<AboutUsDto> args)
    {
        _selectedItem = args.Item;
    }
    #endregion
}

/// <summary>
/// Delete Message Box
/// </summary>
public partial class ListAboutUsBase : ComponentBase
{
    public MudMessageBox? mbox { get; set; }

    public async void OnButtonClicked()
    {
        bool? result = await mbox.Show();
        StateHasChanged();
    }
    public async Task DeleteAboutUss()
    {
        var ids = selectedItems.Select(x => x.Id);
        await _aboutUsRepository.DeleteAboutUsByIds(ids);
        await AboutUsTable.ReloadServerData();
    }

}