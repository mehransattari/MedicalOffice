using MedicalOffice.Client.Repositories.Inteface;
using MedicalOffice.Client.Services;
using MedicalOffice.Shared.DTO;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using System.ComponentModel.DataAnnotations;

namespace MedicalOffice.Client.Pages.Slider;

public partial class FormDialogSliderBase : ComponentBase
{
    #region Inject 
    [Inject]
    public ISliderRepository _sliderrepository { get; set; }

    [CascadingParameter]
    public required MudDialogInstance MudDialog { get; set; }

    [Inject]
    public required IFileUpload fileUpload { get; set; }
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

    [Parameter]
    public string? Property1 { get; set; }
    [Parameter]
    public string? ShortDesc1 { get; set; }

    [Parameter]
    public string? Property2 { get; set; }

    [Parameter]
    public string? ShortDesc2 { get; set; }

    [Parameter]
    public string? Property3 { get; set; }

    [Parameter]
    public string? ShortDesc3 { get; set; }

    #endregion

    #region Fields
    public MultipartFormDataContent MultipartFormData = new MultipartFormDataContent();

    public MudTextField<string>? multilineReference;
    public string? SliderName { get; set; }

    public SliderDto Slider = new();

    #endregion

    protected override void OnInitialized()
    {
        if (Id != 0)
        {
            Slider.Id = Id;
            Slider.Title = Title;
            Slider.ImageUrl = ImageUrl;
            Slider.Desc = Desc;
            Slider.ShortDesc1 = ShortDesc1;
            Slider.ShortDesc2 = ShortDesc2;
            Slider.ShortDesc3 = ShortDesc3;
            Slider.Property1 = Property1;
            Slider.Property2 = Property2;
            Slider.Property3 = Property3;

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
        if (Slider.Desc != null)
        {
            MultipartFormData.Add(new StringContent(Slider.Desc.ToString()), "Desc");
        }

        if (Slider.ShortDesc1 != null)
        {
            MultipartFormData.Add(new StringContent(Slider.ShortDesc1.ToString()), "ShortDesc1");
        }

        if (Slider.ShortDesc2 != null)
        {
            MultipartFormData.Add(new StringContent(Slider.ShortDesc2.ToString()), "ShortDesc2");
        }

        if (Slider.ShortDesc3 != null)
        {
            MultipartFormData.Add(new StringContent(Slider.ShortDesc3.ToString()), "ShortDesc3");
        }

        if (Slider.Property1 != null)
        {
            MultipartFormData.Add(new StringContent(Slider.Property1.ToString()), "Property1");
        }

        if (Slider.Property2 != null)
        {
            MultipartFormData.Add(new StringContent(Slider.Property2.ToString()), "Property2");
        }

        if (Slider.Property3 != null)
        {
            MultipartFormData.Add(new StringContent(Slider.Property3.ToString()), "Property3");
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
