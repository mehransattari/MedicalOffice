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
    public IJSRuntime _jSRuntime { get; set; }

    [Inject]
    public ISliderRepository _sliderRepository { get; set; }

    [Inject]
    public IDialogService DialogService { get; set; }

    public Slider Slider { get; set; } = new Slider();

    protected override async Task OnInitializedAsync()
    {
        var result = await _sliderRepository.GetSliders();

        if (result.Success)
        {
            Slider = result?.Response?.FirstOrDefault();
        }
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await _jSRuntime.InvokeVoidAsync("gallery_Slider");
    }

    public async Task ShowReserveDate()
    {
        var parameters = new DialogParameters<FormDialogShowReserveBase>();
        DialogOptions closeOnEscapeKey = new DialogOptions()
        {
            CloseOnEscapeKey = true,
            MaxWidth = MaxWidth.ExtraExtraLarge,
            FullWidth = true,
        };

        var result = await DialogService
                           .Show<FormDialogShowReserve>("روزهای رزرو", parameters, closeOnEscapeKey)
                           .Result;

        if (!result.Canceled)
        {
            StateHasChanged();          
        }
    }
}
