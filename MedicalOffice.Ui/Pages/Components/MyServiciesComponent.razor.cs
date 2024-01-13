using MedicalOffice.Shared.Entities;
using MedicalOffice.Ui.Repositories.Inteface;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace MedicalOffice.Ui.Pages.Components;

public class MyServiciesComponentBase : ComponentBase
{
    #region Inject
    [Inject]
    public IJSRuntime _jSRuntime { get; set; }

    [Inject]
    public IProvidingServiceRepository _serviceRepository { get; set; }
    #endregion

    #region Fields
    public IEnumerable<ProvidingService> ProvidingServices { get; set; } = new List<ProvidingService>();

    [Parameter]
    public EventCallback<bool> IsComponentLoading { get; set; }
    #endregion


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
            await IsComponentLoading.InvokeAsync(false);
        }
     
    }

}
