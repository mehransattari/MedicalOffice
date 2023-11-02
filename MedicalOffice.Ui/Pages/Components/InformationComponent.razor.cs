using MedicalOffice.Shared.Entities;
using MedicalOffice.Ui.Repositories.Inteface;
using Microsoft.AspNetCore.Components;

namespace MedicalOffice.Ui.Pages.Components;

public class InformationComponentBase : ComponentBase
{
    [Inject]
    public IAboutUsRepository aboutUsRepository { get; set; }
    public AboutUs aboutUs { get; set; } = new AboutUs();
    protected override async Task OnInitializedAsync()
    {
        var res = await aboutUsRepository.GetAboutUs();

        if (res.Success)
        {
            aboutUs = res?.Response?.FirstOrDefault();
        }
    }
}
