using MedicalOffice.Client.Repositories.Inteface;
using MedicalOffice.Shared.DTO;
using MedicalOffice.Shared.Helper.Mapper;
using Microsoft.AspNetCore.Components;
using MudBlazor;
namespace MedicalOffice.Client.Pages.Contactus;


public partial class ListContactUsBase : ComponentBase
{

    #region Inject
    [Inject]
    public IContactUsRepository _ContactUsRepository { get; set; }
    [Inject]
    public ISnackbar Snackbar { get; set; }
    [Inject]
    public IDialogService DialogService { get; set; }
    #endregion

    #region Properties
    public List<ContactUsDto> listContactUs { get; set; } = new List<ContactUsDto>();
    public IEnumerable<ContactUsDto> enumContactUs = new List<ContactUsDto>();
    public IEnumerable<ContactUsDto> pagedData = new List<ContactUsDto>();
    public MultipartFormDataContent MultipartFormData = new MultipartFormDataContent();
    public MudTable<ContactUsDto>? ContactUsTable;
    public bool disableButtonAdd { get; set; } = false;
   
    #endregion

    #region ServerReload
    public async Task<TableData<ContactUsDto>> ServerReload(TableState state)
    {
        if (EditedContactUs.Id != 0)
        {
            MultipartFormData.Add(new StringContent(EditedContactUs.Title), "Title");
            MultipartFormData.Add(new StringContent(EditedContactUs.Text), "Text");
            MultipartFormData.Add(new StringContent(EditedContactUs.Id.ToString()), "Id");
            MultipartFormData.Add(new StringContent(EditedContactUs.ImageUrl), "ImageUrl");
            MultipartFormData.Add(new StringContent(EditedContactUs.PhoneNumber), "PhoneNumber");
            MultipartFormData.Add(new StringContent(EditedContactUs.Mobile), "Mobile");
            MultipartFormData.Add(new StringContent(EditedContactUs.Address1), "Address1");
            MultipartFormData.Add(new StringContent(EditedContactUs.Address2), "Address2");

            var result = await _ContactUsRepository.UpdateContactUs(MultipartFormData);
            if (result.Response)
            {
                StateHasChanged();
                EditedContactUs.Id = 0;
            }
        }

        var resultQuery = await _ContactUsRepository.GetContactUs();

        if (resultQuery.Success)
        {
            var res = resultQuery.Response.ToArray();
            enumContactUs = res.Mapper();
            disableButtonAdd = enumContactUs.Count() > 0 ? true : false;
            this.StateHasChanged();
        }

        pagedData = enumContactUs.ToArray();

        return new TableData<ContactUsDto>() { TotalItems = 0, Items = pagedData };
    }
 
    
    #endregion
}

/// <summary>
/// Add
/// </summary>
public partial class ListContactUsBase : ComponentBase
{
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
}

/// <summary>
/// Edit
/// </summary>
public partial class ListContactUsBase : ComponentBase
{
    /// <summary>
    /// باز شدن مدال برای ویرایش
    /// </summary>
    /// <returns></returns>
    public async Task OpenAddDialogEdit()
    {
        var parameters = new DialogParameters<FormDialogContactUs>();

        var Id = selectedItems.Select(x => x.Id).FirstOrDefault();
        var Title = selectedItems.Select(x => x.Title).FirstOrDefault();
        var Text = selectedItems.Select(x => x.Text).FirstOrDefault();
        var ImageUrl = selectedItems.Select(x => x.ImageUrl).FirstOrDefault();
        var Address2 = selectedItems.Select(x => x.Address2).FirstOrDefault();
        var Address1 = selectedItems.Select(x => x.Address1).FirstOrDefault();
        var Map = selectedItems.Select(x => x.Map).FirstOrDefault();
        var PhoneNumber = selectedItems.Select(x => x.PhoneNumber).FirstOrDefault();
        var Mobile = selectedItems.Select(x => x.Mobile).FirstOrDefault();


        parameters.Add(x => x.Id, Id);
        parameters.Add(x => x.Title, Title);
        parameters.Add(x => x.Text, Text);
        parameters.Add(x => x.ImageUrl, ImageUrl);
        parameters.Add(x => x.Address2, Address2);
        parameters.Add(x => x.Address1, Address1);
        parameters.Add(x => x.Map, Map);
        parameters.Add(x => x.PhoneNumber, PhoneNumber);
        parameters.Add(x => x.Mobile, Mobile);


        DialogOptions closeOnEscapeKey = new DialogOptions()
        {
            CloseOnEscapeKey = true,
            MaxWidth = MaxWidth.Medium,
            FullWidth = true,
        };

        var result = await DialogService.Show<FormDialogContactUs>("ویرایش تماس با ما", parameters, closeOnEscapeKey).Result;

        if (!result.Canceled)
        {
            StateHasChanged();
            await ContactUsTable.ReloadServerData();
        }
    }
}

/// <summary>
/// Row Edit
/// </summary>
public partial class ListContactUsBase : ComponentBase
{
    #region Row Edit
    public ContactUsDto ContactUsBeforeEdit { get; set; } = new ContactUsDto();
    public ContactUsDto EditedContactUs { get; set; } = new ContactUsDto();

    /// <summary>
    /// هنگام کلیک روی دکمه ویرایش سطری
    /// </summary>
    /// <param name="ContactUs"></param>
    public void BackupItem(object ContactUs)
    {
        ContactUsBeforeEdit = new()
        {
            Id = ((ContactUsDto)ContactUs).Id,
            Title = ((ContactUsDto)ContactUs).Title,
            Image = ((ContactUsDto)ContactUs).Image,
            Mobile = ((ContactUsDto)ContactUs).Mobile,
            PhoneNumber = ((ContactUsDto)ContactUs).PhoneNumber,

        };
    }

    /// <summary>
    /// ذخیره اطلاعات در هنگام ویرایش سطری
    /// </summary>
    /// <param name="ContactUs"></param>
    public void ItemHasBeenCommitted(object ContactUs)
    {
        var res = (ContactUsDto)ContactUs;
        EditedContactUs = res;
        ContactUsTable.ReloadServerData();
    }

    /// <summary>
    /// کنسل کردن  اطلاعات در هنگام ویرایش سطری
    /// </summary>
    /// <param name="ContactUs"></param>
    public void ResetItemToOriginalValues(object ContactUs)
    {
        ((ContactUsDto)ContactUs).Id = ContactUsBeforeEdit.Id;
        ((ContactUsDto)ContactUs).Title = ContactUsBeforeEdit.Title;
        ((ContactUsDto)ContactUs).Image = ContactUsBeforeEdit.Image;
        ((ContactUsDto)ContactUs).Text = ContactUsBeforeEdit.Text;
        ((ContactUsDto)ContactUs).Mobile = ContactUsBeforeEdit.Mobile;
        ((ContactUsDto)ContactUs).PhoneNumber = ContactUsBeforeEdit.PhoneNumber;

    }
    #endregion
}

/// <summary>
/// Selected Row
/// </summary>
public partial class ListContactUsBase : ComponentBase
{
    #region Selected Row
    public HashSet<ContactUsDto> selectedItems = new HashSet<ContactUsDto>();
    public bool _selectOnRowClick = true;
    public ContactUsDto _selectedItem = new ContactUsDto();
    public void OnRowClick(TableRowClickEventArgs<ContactUsDto> args)
    {
        _selectedItem = args.Item;
    }
    #endregion
}

/// <summary>
/// Delete Message Box
/// </summary>
public partial class ListContactUsBase : ComponentBase
{
    public MudMessageBox? mbox { get; set; }

    public async void OnButtonClicked()
    {
        bool? result = await mbox.Show();
        StateHasChanged();
    }
    public async Task DeleteContactUss()
    {
        var ids = selectedItems.Select(x => x.Id);
        await _ContactUsRepository.DeleteContactUsByIds(ids);
        await ContactUsTable.ReloadServerData();
    }

}