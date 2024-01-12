using MedicalOffice.Shared.DTO;
using MedicalOffice.Shared.Entities;
using MedicalOffice.Ui.Pages.Dialogs;
using MedicalOffice.Ui.Repositories.Inteface;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;

namespace MedicalOffice.Ui.Pages.Components;



public class SliderComponentBase : ComponentBase
{
    [Inject]
    public required IJSRuntime _jSRuntime { get; set; }

    [Inject]
    public required ISliderRepository _sliderRepository { get; set; }

    [Inject]
    public required IDialogService DialogService { get; set; }

    [Parameter]
    public bool IsComponentLoading { get; set; }

    public Slider Slider { get; set; } = new Slider();

    protected override async Task OnInitializedAsync()
    {
        var result = await _sliderRepository.GetSliders();

        if (result.Success)
        {
            Slider = result?.Response?.FirstOrDefault();
        }
        IsComponentLoading = false;
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await _jSRuntime.InvokeVoidAsync("gallery_Slider");
    }

    public void ShowReserveDate()
    {
        var options = new DialogOptions { CloseOnEscapeKey = true };
        DialogService.Show<FormDialogShowReserve>("Simple Dialog", options);
    }
}
