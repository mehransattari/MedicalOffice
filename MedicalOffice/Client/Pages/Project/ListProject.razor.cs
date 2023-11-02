using MedicalOffice.Client.Repositories.Inteface;
using MedicalOffice.Shared.DTO;
using MedicalOffice.Shared.Helper.Mapper;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace MedicalOffice.Client.Pages.Project;


public partial class ListProjectBase : ComponentBase
{

    #region Inject
    [Inject]
    public IProjectRepository _projectRepository { get; set; }
    [Inject]
    public ISnackbar Snackbar { get; set; }
    [Inject]
    public IDialogService DialogService { get; set; }
    #endregion

    #region Properties
    public List<ProjectDto> listProject { get; set; } = new List<ProjectDto>();
    public IEnumerable<ProjectDto> enumProject = new List<ProjectDto>();
    public IEnumerable<ProjectDto> pagedData = new List<ProjectDto>();
    public MultipartFormDataContent MultipartFormData = new MultipartFormDataContent();
    public MudTable<ProjectDto>? ProjectTable;

    #endregion

    #region ServerReload
    public async Task<TableData<ProjectDto>> ServerReload(TableState state)
    {
        if (EditedProject.Id != 0)
        {
            MultipartFormData.Add(new StringContent(EditedProject.Title), "Title");
            MultipartFormData.Add(new StringContent(EditedProject.Desc), "Desc");
            MultipartFormData.Add(new StringContent(EditedProject.Id.ToString()), "Id");
            MultipartFormData.Add(new StringContent(EditedProject.ImageUrl), "ImageUrl");

            var result = await _projectRepository.UpdateProject(MultipartFormData);
            if (result.Response)
            {
                StateHasChanged();
                EditedProject.Id = 0;
            }
        }

        var resultQuery = await _projectRepository.GetProject();

        if (resultQuery.Success)
        {
            var res = resultQuery.Response.ToArray();
            enumProject = res.Mapper();
            this.StateHasChanged();
        }

        pagedData = enumProject.ToArray();

        return new TableData<ProjectDto>() { TotalItems = 0, Items = pagedData };
    }


    #endregion
}

/// <summary>
/// Add
/// </summary>
public partial class ListProjectBase : ComponentBase
{
    public async Task OpenAddDialogAdd()
    {
        DialogOptions closeOnEscapeKey = new DialogOptions()
        {
            CloseOnEscapeKey = true,
            MaxWidth = MaxWidth.Medium,
            FullWidth = true,
        };

        var result = await DialogService.Show<FormDialogProject>("افزودن پروژه ", closeOnEscapeKey).Result;

        if (!result.Canceled)
        {
            StateHasChanged();
            await ProjectTable.ReloadServerData();
        }
    }
}

/// <summary>
/// Edit
/// </summary>
public partial class ListProjectBase : ComponentBase
{
    /// <summary>
    /// باز شدن مدال برای ویرایش
    /// </summary>
    /// <returns></returns>
    public async Task OpenAddDialogEdit()
    {
        var parameters = new DialogParameters<FormDialogProject>();

        var Id = selectedItems.Select(x => x.Id).FirstOrDefault();
        var Title = selectedItems.Select(x => x.Title).FirstOrDefault();
        var Desc = selectedItems.Select(x => x.Desc).FirstOrDefault();
        var ImageUrl = selectedItems.Select(x => x.ImageUrl).FirstOrDefault();
  

        parameters.Add(x => x.Id, Id);
        parameters.Add(x => x.Title, Title);
        parameters.Add(x => x.Desc, Desc);
        parameters.Add(x => x.ImageUrl, ImageUrl);


        DialogOptions closeOnEscapeKey = new DialogOptions()
        {
            CloseOnEscapeKey = true,
            MaxWidth = MaxWidth.Medium,
            FullWidth = true,
        };

        var result = await DialogService.Show<FormDialogProject>("ویرایش  پروژه", parameters, closeOnEscapeKey).Result;

        if (!result.Canceled)
        {
            StateHasChanged();
            await ProjectTable.ReloadServerData();
        }
    }
}

/// <summary>
/// Row Edit
/// </summary>
public partial class ListProjectBase : ComponentBase
{
    #region Row Edit
    public ProjectDto ProjectBeforeEdit { get; set; } = new ProjectDto();
    public ProjectDto EditedProject { get; set; } = new ProjectDto();

    /// <summary>
    /// هنگام کلیک روی دکمه ویرایش سطری
    /// </summary>
    /// <param name="Project"></param>
    public void BackupItem(object Project)
    {
        ProjectBeforeEdit = new()
        {
            Id = ((ProjectDto)Project).Id,
            Title = ((ProjectDto)Project).Title,
            Image = ((ProjectDto)Project).Image,
            Desc = ((ProjectDto)Project).Desc,
        };
    }

    /// <summary>
    /// ذخیره اطلاعات در هنگام ویرایش سطری
    /// </summary>
    /// <param name="Project"></param>
    public void ItemHasBeenCommitted(object Project)
    {
        var res = (ProjectDto)Project;
        EditedProject = res;
        ProjectTable.ReloadServerData();
    }

    /// <summary>
    /// کنسل کردن  اطلاعات در هنگام ویرایش سطری
    /// </summary>
    /// <param name="Project"></param>
    public void ResetItemToOriginalValues(object Project)
    {
        ((ProjectDto)Project).Id = ProjectBeforeEdit.Id;
        ((ProjectDto)Project).Title = ProjectBeforeEdit.Title;
        ((ProjectDto)Project).Image = ProjectBeforeEdit.Image;
        ((ProjectDto)Project).Desc = ProjectBeforeEdit.Desc;     
    }
    #endregion
}

/// <summary>
/// Selected Row
/// </summary>
public partial class ListProjectBase : ComponentBase
{
    #region Selected Row
    public HashSet<ProjectDto> selectedItems = new HashSet<ProjectDto>();
    public bool _selectOnRowClick = true;
    public ProjectDto _selectedItem = new ProjectDto();
    public void OnRowClick(TableRowClickEventArgs<ProjectDto> args)
    {
        _selectedItem = args.Item;
    }
    #endregion
}

/// <summary>
/// Delete Message Box
/// </summary>
public partial class ListProjectBase : ComponentBase
{
    public MudMessageBox? mbox { get; set; }

    public async void OnButtonClicked()
    {
        bool? result = await mbox.Show();
        StateHasChanged();
    }
    public async Task DeleteProjects()
    {
        var ids = selectedItems.Select(x => x.Id);
        await _projectRepository.DeleteProjectByIds(ids);
        await ProjectTable.ReloadServerData();
    }

}