using Blazored.TextEditor;
using MedicalOffice.Client.Repositories.Inteface;
using MedicalOffice.Client.Services;
using MedicalOffice.Shared.DTO;
using MedicalOffice.Shared.Entities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;


namespace MedicalOffice.Client.Pages.AboutUss;

public partial class FormDialogAboutUsBase : ComponentBase
{
    #region Inject 
    [Inject]
    public IAboutUsRepository _aboutUsRepository { get; set; }

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

    #endregion

    #region Fields
    public MultipartFormDataContent MultipartFormData = new MultipartFormDataContent();

    public MudTextField<string>? multilineReference;
    public string? AboutUsName { get; set; }

    public AboutUsDto AboutUs = new AboutUsDto();

    #endregion

    protected override void OnInitialized()
    {
        if (Id != 0)
        {
            AboutUs.Id = Id;
            AboutUs.Title = Title;
            AboutUs.Text = Text;
            AboutUs.ImageUrl = ImageUrl;
        }
    }

}

/// <summary>
/// Upload Image
/// </summary>
public partial class FormDialogAboutUsBase : ComponentBase
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
public partial class FormDialogAboutUsBase : ComponentBase
{
    public BlazoredTextEditor richEditor = default!;
    public string toolbar = """"...markup here..."""";
    public string body = """"...markup here..."""";

    public void TextHandleValueChanged(string text)
    {
        AboutUs.Text = text;
    }
}

/// <summary>
/// OnValidSubmit
/// </summary>
public partial class FormDialogAboutUsBase : ComponentBase
{
    public bool success;
    public void Submit() => MudDialog.Close(DialogResult.Ok(true));
    public void Cancel() => MudDialog.Cancel();
    public async Task OnValidSubmit(EditContext context)
    {
        success = true;

        MultipartFormData.Add(new StringContent(AboutUs.Id.ToString()), "Id");
        MultipartFormData.Add(new StringContent(AboutUs.Text.ToString()), "Text");
        MultipartFormData.Add(new StringContent(AboutUs.Title.ToString()), "Title");

        if (Image == null && !string.IsNullOrEmpty(AboutUs.ImageUrl))
        {
            MultipartFormData.Add(new StringContent(AboutUs.ImageUrl.ToString()), "ImageUrl");
        }

        if (Id != 0)
        {
            await _aboutUsRepository.UpdateAboutUs(MultipartFormData);
        }

        else
        {
            await _aboutUsRepository.CreateAboutUs(MultipartFormData);
        }

        StateHasChanged();
        Submit();
    }
}
