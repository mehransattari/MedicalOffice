using MedicalOffice.Shared.Entities;
using MedicalOffice.Ui.Repositories.Inteface;
using Microsoft.AspNetCore.Components;

namespace MedicalOffice.Ui.Pages;

public class ContactUsBase:ComponentBase
{
    [Inject]
    public IContactUsRepository contactusRepository { get; set; }
    public ContactUs ContactUs { get; set; } = new ContactUs();
    protected override async Task OnInitializedAsync()

    {
        var res = await contactusRepository.GetContactUs();

        if (res.Success)
        {
            ContactUs = res?.Response?.FirstOrDefault();
        }
    }
}
