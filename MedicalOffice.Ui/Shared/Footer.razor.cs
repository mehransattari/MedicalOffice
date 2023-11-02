using MedicalOffice.Shared.Entities;
using MedicalOffice.Ui.Repositories.Inteface;
using Microsoft.AspNetCore.Components;

namespace MedicalOffice.Ui.Shared;

public class FooterBase:ComponentBase
{
    [Inject]
    public IContactUsRepository contactUsRepository { get; set; }

    [Inject]
    public ISettingsRepository settingsRepository { get; set; }

    public ContactUs ContactUs { get; set; } = new ContactUs();

    public Settings Settings { get; set; } = new Settings();

    protected override async Task OnInitializedAsync()
    {
        var resContactUs = await contactUsRepository.GetContactUs();
        var resSettings = await settingsRepository.GetSettings();

        if (resContactUs.Success)
        {
            ContactUs = resContactUs?.Response?.FirstOrDefault();
        }

        if (resSettings.Success)
        {
            Settings = resSettings?.Response?.FirstOrDefault();
        }
    }
}
