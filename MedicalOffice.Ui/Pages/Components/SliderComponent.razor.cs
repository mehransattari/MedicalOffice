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

    public void ShowReserveDate()
    {
        //var parameters = new DialogParameters<FormDialogShowReserveBase>();
        //DialogOptions closeOnEscapeKey = new DialogOptions()
        //{
        //    CloseOnEscapeKey = true,
        //    MaxWidth = MaxWidth.ExtraExtraLarge,
        //    FullWidth = true,
        //};

        //var result = await DialogService
        //                   .Show<FormDialogShowReserve>("روزهای رزرو", parameters, closeOnEscapeKey)
        //                   .Result;

        var options = new DialogOptions { CloseOnEscapeKey = true };
        DialogService.Show<FormDialogShowReserve>("Simple Dialog", options);

        //if (!result.Canceled)
        //{
        //    StateHasChanged();          
        //}
    }
}
