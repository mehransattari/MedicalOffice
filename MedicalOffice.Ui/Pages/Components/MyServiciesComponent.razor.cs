using MedicalOffice.Shared.Entities;
using MedicalOffice.Ui.Repositories.Inteface;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace MedicalOffice.Ui.Pages.Components;

public class MyServiciesComponentBase : ComponentBase
{
    [Inject]
    public IJSRuntime _jSRuntime { get; set; }

    [Inject]
    public IProvidingServiceRepository _serviceRepository { get; set; }

    public IEnumerable<ProvidingService> ProvidingServices { get; set; } = new List<ProvidingService>();

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await _jSRuntime.InvokeVoidAsync("our_service_slider", ProvidingServices.Count());
    }

    protected override async Task OnInitializedAsync()
    {
        var result = await _serviceRepository.GetProvidingService();

        if (result.Success)
        {
            ProvidingServices =  result.Response.ToList();
        }

    }

}
