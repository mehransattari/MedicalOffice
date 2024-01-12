using MedicalOffice.Shared.Entities;
using MedicalOffice.Ui.Repositories.Inteface;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace MedicalOffice.Ui.Pages.Components;

public class WorksComponentBase: ComponentBase
{
    [Inject]
    public IJSRuntime _jSRuntime { get; set; }

    [Inject]
    public IProjectRepository _projectRepository { get; set; }

    public IEnumerable<Project> Projects { get; set; } = new List<Project>();

    [Parameter]
    public bool IsComponentLoading { get; set; }
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await _jSRuntime.InvokeVoidAsync("gallery_Slider", Projects.Count());
    }

    protected override async Task OnInitializedAsync()
    {
        var result = await _projectRepository.GetProject();

        if (result.Success)
        {
            Projects = result.Response.ToList();
        }
        IsComponentLoading = false;
    }
}
