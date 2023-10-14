using MedicalOffice.Client.Repositories.Inteface;
using MedicalOffice.Shared.DTO;
using MedicalOffice.Shared.Entities;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using MedicalOffice.Shared.Helper.Mapper;
using MedicalOffice.Client.Pages.Contactus;

namespace MedicalOffice.Client.Pages.ContactUses;

public class ListContactUsBase : ComponentBase
{
    #region ======= Fields ================

    #region Inject
    [Inject]
    public IContactUsRepository _ContactUsRepository { get; set; }
    [Inject]
    public ISnackbar Snackbar { get; set; }
    [Inject]
    public IDialogService DialogService { get; set; }
    #endregion

    #region Properties
    public List<ContactUs> ContactUss { get; set; } = new List<ContactUs>();

    public IEnumerable<ContactUsDto> ContactUses = new List<ContactUsDto>();
    public ContactUs EditedContactUs { get; set; } = new ContactUs();
    public ContactUsDto ContactUsBeforeEdit { get; set; } = new ContactUsDto();


    #endregion

    #region Settings
    public TableApplyButtonPosition applyButtonPosition = TableApplyButtonPosition.End;

    public TableEditButtonPosition editButtonPosition = TableEditButtonPosition.End;

    public TableEditTrigger editTrigger = TableEditTrigger.EditButton;

    #endregion

    #region Selected Row
    public HashSet<ContactUsDto> selectedItems = new HashSet<ContactUsDto>();

    public bool _selectOnRowClick = true;

    public ContactUsDto _selectedItem = new ContactUsDto();
    #endregion

    public IEnumerable<ContactUsDto> pagedData;

    public MudTable<ContactUsDto> ContactUsTable;

    #endregion

    #region=======  Methods ==============
    public async Task<TableData<ContactUsDto>> ServerReload(TableState state)
    {
         if (EditedContactUs.Id != 0)
        {
            var ContactUsDto = EditedContactUs.Mapper();
            var result = await _ContactUsRepository.UpdateContactUs(ContactUsDto);
            if (result.Response)
            {
                StateHasChanged();
                EditedContactUs.Id = 0;
            }
        }
        var resultQuery = await _ContactUsRepository.GetContactUs();

        if (resultQuery.Success)
        {
            ContactUses = resultQuery.Response.ToArray();
        }

        pagedData = ContactUses.ToArray();
        return new TableData<ContactUsDto>() { TotalItems = 0, Items = pagedData };
    }



    #region Row Edit
    /// <summary>
    /// هنگام کلیک روی دکمه ویرایش سطری
    /// </summary>
    /// <param name="ContactUs"></param>
    public void BackupItem(object ContactUs)
    {
        ContactUsBeforeEdit = new()
        {
            Id = ((ContactUsDto)ContactUs).Id,
            Mobile = ((ContactUsDto)ContactUs).Mobile,
            PhoneNumber = ((ContactUsDto)ContactUs).PhoneNumber,
            Address1 = ((ContactUsDto)ContactUs).Address1,
            Address2 = ((ContactUsDto)ContactUs).Address2,
            Text = ((ContactUsDto)ContactUs).Text,
            Title = ((ContactUsDto)ContactUs).Title,
            Map = ((ContactUsDto)ContactUs).Map,
            Image = ((ContactUsDto)ContactUs).Image,

        };
    }

    /// <summary>
    /// ذخیره اطلاعات در هنگام ویرایش سطری
    /// </summary>
    /// <param name="ContactUs"></param>
    public void ItemHasBeenCommitted(object ContactUs)
    {
        var res = (ContactUsDto)ContactUs;
        EditedContactUs = res.Mapper();
        ContactUsTable.ReloadServerData();
    }

    /// <summary>
    /// کنسل کردن  اطلاعات در هنگام ویرایش سطری
    /// </summary>
    /// <param name="ContactUs"></param>
    public void ResetItemToOriginalValues(object ContactUs)
    {
        ((ContactUsDto)ContactUs).Id = ContactUsBeforeEdit.Id;
        ((ContactUsDto)ContactUs).Mobile = ContactUsBeforeEdit.Mobile;
        ((ContactUsDto)ContactUs).PhoneNumber = ContactUsBeforeEdit.PhoneNumber;
        ((ContactUsDto)ContactUs).Address1 = ContactUsBeforeEdit.Address1;
        ((ContactUsDto)ContactUs).Address2 = ContactUsBeforeEdit.Address2;
        ((ContactUsDto)ContactUs).Text = ContactUsBeforeEdit.Text;
        ((ContactUsDto)ContactUs).Title = ContactUsBeforeEdit.Title;
        ((ContactUsDto)ContactUs).Map = ContactUsBeforeEdit.Map;
        ((ContactUsDto)ContactUs).Image = ContactUsBeforeEdit.Image;

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

    public async Task DeleteContactUses()
    {
        var ids = selectedItems.Select(x => x.Id);
        await _ContactUsRepository.DeleteContactUsByIds(ids);
        await ContactUsTable.ReloadServerData();
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

        var result = await DialogService.Show<FormDialogContactUs>("افزودن تماس با ما", closeOnEscapeKey).Result;

        if (!result.Canceled)
        {
            StateHasChanged();
            await ContactUsTable.ReloadServerData();
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
        var parameters = new DialogParameters<FormDialogContactUs>();
        var Id = selectedItems.Select(x => x.Id).FirstOrDefault();
        var Title = selectedItems.Select(x => x.Title).FirstOrDefault();
        var Address1 = selectedItems.Select(x => x.Address1).FirstOrDefault();
        var Address2 = selectedItems.Select(x => x.Address2).FirstOrDefault();
        var Mobile = selectedItems.Select(x => x.Mobile).FirstOrDefault();
        var PhoneNumber = selectedItems.Select(x => x.PhoneNumber).FirstOrDefault();
        var Image = selectedItems.Select(x => x.Image).FirstOrDefault();
        var Map = selectedItems.Select(x => x.Map).FirstOrDefault();


        parameters.Add(x => x.Id, Id);
        parameters.Add(x => x.Address1, Address1);
        parameters.Add(x => x.Address2, Address2);
        parameters.Add(x => x.Title, Title);
        parameters.Add(x => x.Mobile, Mobile);
        parameters.Add(x => x.PhoneNumber, PhoneNumber);
        parameters.Add(x => x.Image, Image);
        parameters.Add(x => x.Map, Map);


        DialogOptions closeOnEscapeKey = new DialogOptions()
        {
            CloseOnEscapeKey = true,
            MaxWidth = MaxWidth.Medium,
            FullWidth = true,
        };

        var result = await DialogService.Show<FormDialogContactUs>("ویرایش نقش", parameters, closeOnEscapeKey).Result;

        if (!result.Canceled)
        {
            StateHasChanged();
            await ContactUsTable.ReloadServerData();
        }
    }
    #endregion
    public void OnRowClick(TableRowClickEventArgs<ContactUsDto> args)
    {
        _selectedItem = args.Item;
    }

    #endregion

 
}