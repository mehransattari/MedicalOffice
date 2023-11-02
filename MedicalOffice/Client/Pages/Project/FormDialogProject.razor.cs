using Blazored.TextEditor;
using MedicalOffice.Client.Repositories.Inteface;
using MedicalOffice.Client.Services;
using MedicalOffice.Shared.DTO;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;

namespace MedicalOffice.Client.Pages.Project;


public partial class FormDialogProjectBase : ComponentBase
{
    #region Inject 
    [Inject]
    public IProjectRepository _projectRepository { get; set; }

    [CascadingParameter]
    public MudDialogInstance MudDialog { get; set; }

    [Inject]
    public IFileUpload fileUpload { get; set; }
    #endregion

    #region Parameter
    [Parameter]
    public long Id { get; set; }
    [Parameter]
    public string? Title { get; set; }
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
    public string? ProjectName { get; set; }

    public ProjectDto Project = new ProjectDto();

    #endregion

    protected override void OnInitialized()
    {
        if (Id != 0)
        {
            Project.Id = Id;
            Project.Title = Title;
            Project.Desc = Desc;
            Project.ImageUrl = ImageUrl;
        }
    }

}

/// <summary>
/// Upload Image
/// </summary>
public partial class FormDialogProjectBase : ComponentBase
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
public partial class FormDialogProjectBase : ComponentBase
{
    public bool success;
    public void Submit() => MudDialog.Close(DialogResult.Ok(true));
    public void Cancel() => MudDialog.Cancel();
    public async Task OnValidSubmit(EditContext context)
    {
        success = true;

        MultipartFormData.Add(new StringContent(Project.Title), "Title");
        MultipartFormData.Add(new StringContent(content: Project.Desc), "Desc");
        MultipartFormData.Add(new StringContent(Project.Id.ToString()), "Id");  

        if (Image == null && !string.IsNullOrEmpty(Project.ImageUrl))
        {
            MultipartFormData.Add(new StringContent(Project.ImageUrl.ToString()), "ImageUrl");
        }

        if (Id != 0)
        {
            await _projectRepository.UpdateProject(MultipartFormData);
        }

        else
        {
            await _projectRepository.CreateProject(MultipartFormData);
        }

        StateHasChanged();
        Submit();
    }
}
