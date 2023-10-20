using Blazored.TextEditor;
using MedicalOffice.Client.Repositories.Inteface;
using MedicalOffice.Client.Services;
using MedicalOffice.Shared.DTO;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;

namespace MedicalOffice.Client.Pages.Contactus;

public partial class FormDialogContactUsBase : ComponentBase
{
    #region Inject 
    [Inject]
    public IContactUsRepository _ContactUsRepository { get; set; }

    [CascadingParameter]
    public MudDialogInstance MudDialog { get; set; }

    [Inject]
    public IFileUpload fileUpload { get; set; }
    #endregion

    #region Parameter
    [Parameter]
    public long Id { get; set; }
    [Parameter]
    public string Title { get; set; }
    [Parameter]
    public IBrowserFile? Image { get; set; }
    [Parameter]
    public string? ImageUrl { get; set; }
    [Parameter]
    public string? Text { get; set; }
    [Parameter]
    public string? PhoneNumber { get; set; }
    [Parameter]
    public string? Mobile { get; set; }
    [Parameter]
    public string? Address1 { get; set; }
    [Parameter]
    public string? Address2 { get; set; }
    [Parameter]
    public string? Map { get; set; }

    #endregion

    #region Fields
    public MultipartFormDataContent MultipartFormData = new MultipartFormDataContent();

    public MudTextField<string>? multilineReference;
    public string? ContactUsName { get; set; }

    public ContactUsDto ContactUs = new ContactUsDto();

    #endregion

    protected override void OnInitialized()
    {
        if (Id != 0)
        {
            ContactUs.Id = Id;
            ContactUs.Title = Title;
            ContactUs.Text = Text;
            ContactUs.ImageUrl = ImageUrl;
            ContactUs.Address1 = Address1;
            ContactUs.Address2 = Address2;
            ContactUs.Mobile = Mobile;
            ContactUs.PhoneNumber = PhoneNumber;

        }
    }

}

/// <summary>
/// Upload Image
/// </summary>
public partial class FormDialogContactUsBase : ComponentBase
{
    public void FileHandleValueChanged(IList<IBrowserFile> _files)
    {
        Image = _files.FirstOrDefault();
        var result = fileUpload.AddImage(Image).Result;
        MultipartFormData.Add(content: result, name: "\"Image\"", fileName: Image.Name);
        this.StateHasChanged();
    }
}

/// <summary>
/// TextEditor
/// </summary>
public partial class FormDialogContactUsBase : ComponentBase
{
    public BlazoredTextEditor richEditor = default!;
    public string toolbar = """"...markup here..."""";
    public string body = """"...markup here..."""";

    public void TextHandleValueChanged(string text)
    {
        ContactUs.Text = text;
    }
}

/// <summary>
/// OnValidSubmit
/// </summary>
public partial class FormDialogContactUsBase : ComponentBase
{
    public bool success;
    public void Submit() => MudDialog.Close(DialogResult.Ok(true));
    public void Cancel() => MudDialog.Cancel();
    public async Task OnValidSubmit(EditContext context)
    {
        success = true;

        MultipartFormData.Add(new StringContent(ContactUs.Title), "Title");
        MultipartFormData.Add(new StringContent(content: ContactUs.Text), "Text");
        MultipartFormData.Add(new StringContent(ContactUs.Id.ToString()), "Id");
        MultipartFormData.Add(new StringContent(ContactUs.PhoneNumber), "PhoneNumber");
        MultipartFormData.Add(new StringContent(ContactUs.Mobile), "Mobile");
        MultipartFormData.Add(new StringContent(ContactUs.Address1), "Address1");
        MultipartFormData.Add(new StringContent(ContactUs.Address2), "Address2");

        if (Image == null && !string.IsNullOrEmpty(ContactUs.ImageUrl))
        {
            MultipartFormData.Add(new StringContent(ContactUs.ImageUrl.ToString()), "ImageUrl");
        }

        if (Id != 0)
        {
            await _ContactUsRepository.UpdateContactUs(MultipartFormData);
        }

        else
        {
            await _ContactUsRepository.CreateContactUs(MultipartFormData);
        }

        StateHasChanged();
        Submit();
    }
}
