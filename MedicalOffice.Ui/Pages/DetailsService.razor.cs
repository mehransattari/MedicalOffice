using MedicalOffice.Shared.DTO;
using MedicalOffice.Shared.Entities;
using MedicalOffice.Ui.Repositories.Inteface;
using Microsoft.AspNetCore.Components;

namespace MedicalOffice.Ui.Pages;

public class DetailsServiceBase:ComponentBase
{
    [Inject]
    public NavigationManager NavigationManager { get; set; }

    [Inject]
    public IProvidingServiceRepository _serviceRepository { get; set; }


    [Parameter]
    public long id { get; set; }

    public ProvidingService ProvidingService { get; set; } = new ProvidingService();

    protected override async Task OnInitializedAsync()
    {
        if (id != 0)
        {
            var result = await _serviceRepository.GetProvidingServiceById(id);

            if (result.Success)
            {
                ProvidingService = result.Response;
            }
        }
    }
}
