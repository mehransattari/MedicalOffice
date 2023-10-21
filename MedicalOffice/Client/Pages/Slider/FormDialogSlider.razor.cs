using MedicalOffice.Client.Repositories.Inteface;
using MedicalOffice.Client.Services;
using MedicalOffice.Shared.DTO;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;

namespace MedicalOffice.Client.Pages.Slider;

public partial class FormDialogSliderBase : ComponentBase
{
    #region Inject 
    [Inject]
    public ISliderRepository _sliderrepository { get; set; }

    [CascadingParameter]
    public MudDialogInstance MudDialog { get; set; }

    [Inject]
    public IFileUpload fileUpload { get; set; }
    #endregion

    #region Parameter
    [Parameter]
    public long Id { get; set; }
    [Parameter]
    public string Title { get; set; }
    [Parameter]
    public IBrowserFile? Image { get; set; }
    [Parameter]
    public string? ImageUrl { get; set; }
    [Parameter]
    public string? Desc { get; set; }

    #endregion

    #region Fields
    public MultipartFormDataContent MultipartFormData = new MultipartFormDataContent();

    public MudTextField<string>? multilineReference;
    public string? SliderName { get; set; }

    public SliderDto Slider = new SliderDto();

    #endregion

    protected override void OnInitialized()
    {
        if (Id != 0)
        {
            Slider.Id = Id;
            Slider.Title = Title;
            Slider.ImageUrl = ImageUrl;
            Slider.Desc = Desc;

        }
    }

}

/// <summary>
/// Upload Image
/// </summary>
public partial class FormDialogSliderBase : ComponentBase
{
    public void FileHandleValueChanged(IList<IBrowserFile> _files)
    {
        Image = _files.FirstOrDefault();
        var result = fileUpload.AddImage(Image).Result;
        MultipartFormData.Add(content: result, name: "\"Image\"", fileName: Image.Name);
        this.StateHasChanged();
    }
}

/// <summary>
/// OnValidSubmit
/// </summary>
public partial class FormDialogSliderBase : ComponentBase
{
    public bool success;
    public void Submit() => MudDialog.Close(DialogResult.Ok(true));
    public void Cancel() => MudDialog.Cancel();
    public async Task OnValidSubmit(EditContext context)
    {
        success = true;

        MultipartFormData.Add(new StringContent(Slider.Id.ToString()), "Id");
        MultipartFormData.Add(new StringContent(Slider.Title.ToString()), "Title");
        if(Slider.Desc!=null)
        {
            MultipartFormData.Add(new StringContent(Slider.Desc.ToString()), "Desc");
        }

        if (Image == null && !string.IsNullOrEmpty(Slider.ImageUrl))
        {
            MultipartFormData.Add(new StringContent(Slider.ImageUrl.ToString()), "ImageUrl");
        }

        if (Id != 0)
        {
            await _sliderrepository.UpdateSlider(MultipartFormData);
        }
        else
        {
            await _sliderrepository.CreateSlider(MultipartFormData);
        }

        StateHasChanged();
        Submit();
    }
}
