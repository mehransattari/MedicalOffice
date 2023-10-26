using Blazored.TextEditor;
using MedicalOffice.Client.Repositories.Inteface;
using MedicalOffice.Client.Services;
using MedicalOffice.Shared.DTO;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;

namespace MedicalOffice.Client.Pages.ProvidingService;



public partial class FormDialogProvidingServiceBase : ComponentBase
{
    #region Inject 
    [Inject]
    public IProvidingServiceRepository _ProvidingServiceRepository { get; set; }

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
    public string? Desc { get; set; }
    [Parameter]
    public string? ShortDesc { get; set; }


    #endregion

    #region Fields
    public MultipartFormDataContent MultipartFormData = new MultipartFormDataContent();

    public MudTextField<string>? multilineReference;
    public string? ProvidingServiceName { get; set; }

    public ProvidingServiceDto ProvidingService = new ProvidingServiceDto();

    #endregion

    protected override void OnInitialized()
    {
        if (Id != 0)
        {
            ProvidingService.Id = Id;
            ProvidingService.Title = Title;
            ProvidingService.Desc = Desc;
            ProvidingService.ImageUrl = ImageUrl;
            ProvidingService.ShortDesc = ShortDesc;
        }
    }

}

/// <summary>
/// Upload Image
/// </summary>
public partial class FormDialogProvidingServiceBase : ComponentBase
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
public partial class FormDialogProvidingServiceBase : ComponentBase
{
    public BlazoredTextEditor richEditor = default!;
    public string toolbar = """"...markup here..."""";
    public string body = """"...markup here..."""";

    public void TextHandleValueChanged(string text)
    {
        ProvidingService.Desc = text;
    }
}

/// <summary>
/// OnValidSubmit
/// </summary>
public partial class FormDialogProvidingServiceBase : ComponentBase
{
    public bool success;
    public void Submit() => MudDialog.Close(DialogResult.Ok(true));
    public void Cancel() => MudDialog.Cancel();
    public async Task OnValidSubmit(EditContext context)
    {
        success = true;

        MultipartFormData.Add(new StringContent(ProvidingService.Title), "Title");

        if(!string.IsNullOrEmpty(ProvidingService.Desc))
        {
            MultipartFormData.Add(new StringContent(content: ProvidingService.Desc), "Desc");
        }

        MultipartFormData.Add(new StringContent(ProvidingService.Id.ToString()), "Id");

        if (!string.IsNullOrEmpty(ProvidingService.ShortDesc))
        {
            MultipartFormData.Add(new StringContent(ProvidingService.ShortDesc), "ShortDesc");
        }

        if (Image == null && !string.IsNullOrEmpty(ProvidingService.ImageUrl))
        {
            MultipartFormData.Add(new StringContent(ProvidingService.ImageUrl.ToString()), "ImageUrl");
        }

        if (Id != 0)
        {
            await _ProvidingServiceRepository.UpdateProvidingService(MultipartFormData);
        }

        else
        {
            await _ProvidingServiceRepository.CreateProvidingService(MultipartFormData);
        }

        StateHasChanged();
        Submit();
    }
}