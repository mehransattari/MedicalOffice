using MedicalOffice.Client.Repositories.Inteface;
using MedicalOffice.Shared.DTO;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using MedicalOffice.Shared.Helper.Mapper;

namespace MedicalOffice.Client.Pages.Slider;

public partial class ListSliderBase : ComponentBase
{

    #region Inject
    [Inject]
    public ISliderRepository _sliderRepository { get; set; }
    [Inject]
    public ISnackbar Snackbar { get; set; }
    [Inject]
    public IDialogService DialogService { get; set; }
    #endregion

    #region Properties
    public List<SliderDto> listSlider { get; set; } = new List<SliderDto>();
    public IEnumerable<SliderDto> enumSlider = new List<SliderDto>();
    public IEnumerable<SliderDto> pagedData = new List<SliderDto>();
    public MultipartFormDataContent MultipartFormData = new MultipartFormDataContent();
    public MudTable<SliderDto>? SliderTable;
    public SliderDto EditedSlider { get; set; } = new SliderDto();

    #endregion

    #region ServerReload
    public async Task<TableData<SliderDto>> ServerReload(TableState state)
    {
        if (EditedSlider.Id != 0)
        {
            MultipartFormData.Add(new StringContent(EditedSlider?.Title?.ToString()), "Title");
            MultipartFormData.Add(new StringContent(EditedSlider.Id.ToString()), "Id");

            if(EditedSlider?.Desc!=null)
            {
                MultipartFormData.Add(new StringContent(EditedSlider?.Desc?.ToString()), "Desc");
            }

            if (EditedSlider?.ImageUrl != null)
            {
                MultipartFormData.Add(new StringContent(EditedSlider?.ImageUrl?.ToString()), "ImageUrl");
            }

            var result = await _sliderRepository.UpdateSlider(MultipartFormData);
            if (result.Response)
            {
                StateHasChanged();
                EditedSlider.Id = 0;
            }
        }

        var resultQuery = await _sliderRepository.GetSliders();

        if (resultQuery.Success)
        {
            var res = resultQuery.Response.ToArray();
            enumSlider = res.Mapper();
            this.StateHasChanged();
        }

        pagedData = enumSlider.ToArray();

        return new TableData<SliderDto>() { TotalItems = 0, Items = pagedData };
    }
    #endregion
}

/// <summary>
/// Add
/// </summary>
public partial class ListSliderBase : ComponentBase
{
    public async Task OpenAddDialogAdd()
    {
        DialogOptions closeOnEscapeKey = new DialogOptions()
        {
            CloseOnEscapeKey = true,
            MaxWidth = MaxWidth.Medium,
            FullWidth = true,
        };

        var result = await DialogService.Show<FormDialogSlider>("افزودن اسلایدر", closeOnEscapeKey).Result;

        if (!result.Canceled)
        {
            StateHasChanged();
            await SliderTable.ReloadServerData();
        }
    }
}

/// <summary>
/// Edit
/// </summary>
public partial class ListSliderBase : ComponentBase
{
    /// <summary>
    /// باز شدن مدال برای ویرایش
    /// </summary>
    /// <returns></returns>
    public async Task OpenAddDialogEdit()
    {
        var parameters = new DialogParameters<FormDialogSlider>();
        var Id = selectedItems.Select(x => x.Id).FirstOrDefault();
        var Title = selectedItems.Select(x => x.Title).FirstOrDefault();
        var ImageUrl = selectedItems.Select(x => x.ImageUrl).FirstOrDefault();
        var Desc = selectedItems.Select(x => x.Desc).FirstOrDefault();


        parameters.Add(x => x.Id, Id);
        parameters.Add(x => x.Title, Title);
        parameters.Add(x => x.ImageUrl, ImageUrl);
        parameters.Add(x => x.Desc, Desc);


        DialogOptions closeOnEscapeKey = new DialogOptions()
        {
            CloseOnEscapeKey = true,
            MaxWidth = MaxWidth.Medium,
            FullWidth = true,
        };

        var result = await DialogService.Show<FormDialogSlider>("ویرایش اسلایدر ", parameters, closeOnEscapeKey).Result;

        if (!result.Canceled)
        {
            StateHasChanged();
            await SliderTable.ReloadServerData();
        }
    }
}

/// <summary>
/// Row Edit
/// </summary>
public partial class ListSliderBase : ComponentBase
{
    #region Row Edit
    public SliderDto SliderBeforeEdit { get; set; } = new SliderDto(); 

    /// <summary>
    /// هنگام کلیک روی دکمه ویرایش سطری
    /// </summary>
    /// <param name="Slider"></param>
    public void BackupItem(object Slider)
    {
        SliderBeforeEdit = new()
        {
            Id = ((SliderDto)Slider).Id,
            Title = ((SliderDto)Slider).Title,
            Image = ((SliderDto)Slider).Image,
            Desc = ((SliderDto)Slider).Desc,
        };
    }

    /// <summary>
    /// ذخیره اطلاعات در هنگام ویرایش سطری
    /// </summary>
    /// <param name="Slider"></param>
    public void ItemHasBeenCommitted(object Slider)
    {
        var res = (SliderDto)Slider;
        EditedSlider = res;
        SliderTable.ReloadServerData();
    }

    /// <summary>
    /// کنسل کردن  اطلاعات در هنگام ویرایش سطری
    /// </summary>
    /// <param name="Slider"></param>
    public void ResetItemToOriginalValues(object Slider)
    {
        ((SliderDto)Slider).Id = SliderBeforeEdit.Id;
        ((SliderDto)Slider).Title = SliderBeforeEdit.Title;
        ((SliderDto)Slider).Image = SliderBeforeEdit.Image;
        ((SliderDto)Slider).Desc = SliderBeforeEdit.Desc;

    }
    #endregion
}

/// <summary>
/// Selected Row
/// </summary>
public partial class ListSliderBase : ComponentBase
{
    #region Selected Row
    public HashSet<SliderDto> selectedItems = new HashSet<SliderDto>();
    public bool _selectOnRowClick = true;
    public SliderDto _selectedItem = new SliderDto();
    public void OnRowClick(TableRowClickEventArgs<SliderDto> args)
    {
        _selectedItem = args.Item;
    }
    #endregion
}

/// <summary>
/// Delete Message Box
/// </summary>
public partial class ListSliderBase : ComponentBase
{
    public MudMessageBox? mbox { get; set; }

    public async void OnButtonClicked()
    {
        bool? result = await mbox.Show();
        StateHasChanged();
    }
    public async Task DeleteSliders()
    {
        var ids = selectedItems.Select(x => x.Id);
        await _sliderRepository.DeleteSliderByIds(ids);
        await SliderTable.ReloadServerData();
    }

}