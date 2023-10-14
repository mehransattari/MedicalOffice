using MedicalOffice.Client.Repositories.Inteface;
using MedicalOffice.Shared.DTO;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using MedicalOffice.Client.Pages.AboutUs;

namespace MedicalOffice.Client.Pages.AboutUss;

public class ListAboutUsBase : ComponentBase
{
    #region ======= Fields ================

    #region Inject
    [Inject]
    public IAboutUsRepository _aboutUsRepository { get; set; }
    [Inject]
    public ISnackbar Snackbar { get; set; }
    [Inject]
    public IDialogService DialogService { get; set; }
    #endregion

    #region Properties
    public List<AboutUsDto> AboutUss { get; set; } = new List<AboutUsDto>();

    public IEnumerable<AboutUsDto> AboutUses = new List<AboutUsDto>();

    public AboutUsDto EditedAboutUs { get; set; } = new AboutUsDto();

    public AboutUsDto AboutUsBeforeEdit { get; set; } = new AboutUsDto();

    #endregion

    #region Settings
    public TableApplyButtonPosition applyButtonPosition = TableApplyButtonPosition.End;

    public TableEditButtonPosition editButtonPosition = TableEditButtonPosition.End;

    public TableEditTrigger editTrigger = TableEditTrigger.EditButton;

    #endregion

    #region Selected Row
    public HashSet<AboutUsDto> selectedItems = new HashSet<AboutUsDto>();

    public bool _selectOnRowClick = true;

    public AboutUsDto _selectedItem = new AboutUsDto();
    #endregion

    public IEnumerable<AboutUsDto> pagedData;

    public MudTable<AboutUsDto> AboutUsTable;

    #endregion

    #region=======  Methods ==============
    public async Task<TableData<AboutUsDto>> ServerReload(TableState state)
    {
         if (EditedAboutUs.Id != 0)
        {
            var result = await _aboutUsRepository.UpdateAboutUs(EditedAboutUs);
            if (result.Response)
            {
                StateHasChanged();
                EditedAboutUs.Id = 0;
            }
        }


        var resultQuery = await _aboutUsRepository.GetAboutUs();

        if (resultQuery.Success)
        {
            AboutUses = resultQuery.Response.ToArray();
        }

        pagedData = AboutUses.ToArray();
        return new TableData<AboutUsDto>() { TotalItems = 0, Items = pagedData };
    }

  

    #region Row Edit
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

    #region Delete Message Box
    public MudMessageBox mbox { get; set; }

    string state = "Message box hasn't been opened yet";

    public async void OnButtonClicked()
    {
        bool? result = await mbox.Show();

        state = result == null ? "Canceled" : "Deleted!";
        StateHasChanged();
    }

    public async Task DeleteAboutUss()
    {
        var ids = selectedItems.Select(x => x.Id);
        await _aboutUsRepository.DeleteAboutUsByIds(ids);
        await AboutUsTable.ReloadServerData();
    }
    #endregion

    #region Add Dialog
    /// <summary>
    /// باز شدن مدال برای افزودن
    /// </summary>
    /// <returns></returns>
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
    #endregion

    #region Edit Dialog
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
        var Image = selectedItems.Select(x => x.Image).FirstOrDefault();


        parameters.Add(x => x.Id, Id);
        parameters.Add(x => x.Text, Text);
        parameters.Add(x => x.Text, Text);
        parameters.Add(x => x.Image, Image);


        DialogOptions closeOnEscapeKey = new DialogOptions()
        {
            CloseOnEscapeKey = true,
            MaxWidth = MaxWidth.Medium,
            FullWidth = true,
        };

        var result = await DialogService.Show<FormDialogAboutUs>("ویرایش نقش", parameters, closeOnEscapeKey).Result;

        if (!result.Canceled)
        {
            StateHasChanged();
            await AboutUsTable.ReloadServerData();
        }
    }
    #endregion
    public void OnRowClick(TableRowClickEventArgs<AboutUsDto> args)
    {
        _selectedItem = args.Item;
    }

    #endregion


}