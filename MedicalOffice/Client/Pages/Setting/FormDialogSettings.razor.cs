using Blazored.TextEditor;
using MedicalOffice.Client.Repositories.Inteface;
using MedicalOffice.Client.Services;
using MedicalOffice.Shared.DTO;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using MudBlazor;

namespace MedicalOffice.Client.Pages.Setting;



public partial class FormDialogSettingsBase : ComponentBase
{
    #region Inject 
    [Inject]
    public ISettingsRepository _SettingsRepository { get; set; }

    [CascadingParameter]
    public MudDialogInstance MudDialog { get; set; }

    [Inject]
    public IFileUpload fileUpload { get; set; }
    #endregion

    #region Parameter
    [Parameter]
    public long Id { get; set; }
    [Parameter]
    public string SiteName { get; set; }
    [Parameter]
    public string? ShortDescription { get; set; }
    [Parameter]
    public string? Logo { get; set; }
    [Parameter]
    public IBrowserFile? LogoFile { get; set; }
    [Parameter]
    public string? Instagram { get; set; }
    [Parameter]
    public string? Telegram { get; set; }
    [Parameter]
    public string? Whatsapp { get; set; }


    #endregion

    #region Fields
    public MultipartFormDataContent MultipartFormData = new MultipartFormDataContent();

    public MudTextField<string>? multilineReference;
    public string? SettingsName { get; set; }

    public SettingsDto Settings = new SettingsDto();

    #endregion

    protected override void OnInitialized()
    {
        if (Id != 0)
        {
            Settings.Id = Id;
            Settings.ShortDescription = ShortDescription;
            Settings.SiteName = SiteName;
            Settings.Whatsapp = Whatsapp;
            Settings.Instagram = Instagram;
            Settings.Logo = Logo;
        }
    }

}

/// <summary>
/// Upload Image
/// </summary>
public partial class FormDialogSettingsBase : ComponentBase
{
    public void FileHandleValueChangedLogo(IList<IBrowserFile> _files_logo)
    {
        LogoFile = _files_logo.FirstOrDefault();
        var result = fileUpload.AddImage(LogoFile).Result;
        MultipartFormData.Add(content: result, name: "\"LogoFile\"", fileName: LogoFile.Name);
        this.StateHasChanged();
    }
}



/// <summary>
/// OnValidSubmit
/// </summary>
public partial class FormDialogSettingsBase : ComponentBase
{
    public bool success;
    public void Submit() => MudDialog.Close(DialogResult.Ok(true));
    public void Cancel() => MudDialog.Cancel();
    public async Task OnValidSubmit(EditContext context)
    {
        success = true;

        MultipartFormData.Add(new StringContent(Settings.Id.ToString()), "Id");
        MultipartFormData.Add(new StringContent(Settings.SiteName.ToString()), "SiteName");

        if (Settings?.ShortDescription != null)
        {
            MultipartFormData.Add(new StringContent(Settings.ShortDescription.ToString()), "ShortDescription");
        }

        if (Settings?.Whatsapp != null)
        {
            MultipartFormData.Add(new StringContent(Settings.Whatsapp.ToString()), "Whatsapp");
        }

        if (Settings?.Telegram != null)
        {
            MultipartFormData.Add(new StringContent(Settings.Telegram.ToString()), "Telegram");
        }

        if (Settings?.Instagram != null)
        {
            MultipartFormData.Add(new StringContent(Settings.Instagram.ToString()), "Instagram");
        }

        if (Id != 0)
        {
            await _SettingsRepository.UpdateSettings(MultipartFormData);
        }

        else
        {
            await _SettingsRepository.CreateSettings(MultipartFormData);
        }

        StateHasChanged();
        Submit();
    }
}
