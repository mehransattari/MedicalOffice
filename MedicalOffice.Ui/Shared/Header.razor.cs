using MedicalOffice.Shared.Entities;
using MedicalOffice.Ui.Repositories.Inteface;
using Microsoft.AspNetCore.Components;

namespace MedicalOffice.Ui.Shared;

public class HeaderBase : ComponentBase
{
    [Inject]
    public IContactUsRepository contactUsRepository { get; set; }

    [Inject]
    public ISettingsRepository settingsRepository { get; set; }

    public string Logo { get; set; } = string.Empty;

    public ContactUs ContactUs { get; set; } = new ContactUs();


    protected override async Task OnInitializedAsync()
    {
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
}
