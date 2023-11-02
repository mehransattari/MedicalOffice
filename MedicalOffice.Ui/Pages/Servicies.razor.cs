using MedicalOffice.Shared.Entities;
using MedicalOffice.Ui.Repositories.Inteface;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace MedicalOffice.Ui.Pages;

public class Serviciesbase:ComponentBase
{
    [Inject]
    public IJSRuntime _jSRuntime { get; set; }

    [Inject]
    public IProvidingServiceRepository _serviceRepository { get; set; }

    public IEnumerable<ProvidingService> ProvidingServices { get; set; } = new List<ProvidingService>();



    protected override async Task OnInitializedAsync()
    {
        var result = await _serviceRepository.GetProvidingService();

        if (result.Success)
        {
            ProvidingServices = result.Response.ToList();
        }

    }
}
