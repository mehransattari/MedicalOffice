using MedicalOffice.Shared.Entities;
using MedicalOffice.Ui.Repositories.Inteface;
using MedicalOffice.Ui.Services.Interface;
using Microsoft.AspNetCore.Components;
using System.Diagnostics.Contracts;

namespace MedicalOffice.Ui.Shared;

public class HeaderBase : ComponentBase
{
    [Inject]
    public IContactUsRepository contactUsRepository { get; set; }

    [Inject]
    public ISettingsRepository settingsRepository { get; set; }

    [Inject]
    public IUserAuthService userAuthService { get; set; }

    [Inject]
    public NavigationManager navigationManager { get; set; }

    [Inject]
    public IConfiguration _config { get; set; }


    public string Logo { get; set; } = string.Empty;

    public ContactUs ContactUs { get; set; } = new ContactUs();

    public string adminUrl { get; set; }

    protected override async Task OnInitializedAsync()
    {
        adminUrl = _config["typeSite:adminsite"];

        var resSettings = await settingsRepository.GetLogo();

        if (resSettings.Success)
        {
            Logo = resSettings?.Response.Logo;
        }

        var resContactUs = await contactUsRepository.GetContactUs();

        if (resContactUs.Success)
        {
            ContactUs = resContactUs?.Response?.FirstOrDefault();
        }

    }
    public async Task LogOut()
    {
       await userAuthService.Logout();
        navigationManager.NavigateTo("/login");
    }
}
