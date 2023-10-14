using Blazored.TextEditor;
using MedicalOffice.Client.Repositories.Inteface;
using MedicalOffice.Shared.DTO;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using MudBlazor;


namespace MedicalOffice.Client.Pages.AboutUss;

public class FormDialogAboutUsBase : ComponentBase
{
    #region Inject CascadingParameter
    [Inject]
    public IAboutUsRepository _aboutUsRepository { get; set; }

    [CascadingParameter]
    public MudDialogInstance MudDialog { get; set; }
    #endregion

    #region Parameter
    [Parameter]
    public long Id { get; set; }
    [Parameter]
    public string Title { get; set; }
    [Parameter]
    public IFormFile? Image { get; set; }
    [Parameter]   
    public string? Text { get; set; }

    #endregion

    #region Fields
    public MudTextField<string> multilineReference;

    public string? AboutUsName { get; set; }

    public AboutUsDto AboutUs = new AboutUsDto();

    public List<AboutUsDto> AboutUss { get; set; } = new List<AboutUsDto>();

    public void Submit() => MudDialog.Close(DialogResult.Ok(true));

    public void Cancel() => MudDialog.Cancel();

    public bool success;

    public string[] errors = { };
    #endregion

    #region Methods
  

    public async Task OnValidSubmit(EditContext context)
    {
        success = true;
        if (Id != 0)
        {
            await _aboutUsRepository.UpdateAboutUs(AboutUs);
        }
        else
        {
            await _aboutUsRepository.CreateAboutUs(AboutUs);
        }
        StateHasChanged();
        Submit();
    }
    protected override async Task OnInitializedAsync()
    {

        var result = await _aboutUsRepository.GetAboutUs();
        if (result.Success)
        {
            AboutUss = result.Response.ToList();
        }
        if (Id != 0)
        {
            AboutUs.Id = Id;
            AboutUs.Title = Title;
            AboutUs.Text = Text;
            AboutUs.Image = Image;

        }

        TextEditor();
    }
    #endregion

    #region Upload Image

    public void HandleValueChanged(IList<IBrowserFile> _files)
    {
        Console.WriteLine(_files.FirstOrDefault().Name);
    }
    #endregion

    #region TextEditor
    public BlazoredTextEditor richEditor = default!;
    public string toolbar = """"...markup here..."""";
    public string body = """"...markup here..."""";

    public  void TextEditor()
    {
        toolbar = """"
            <select class="ql-header">
                <option selected=""></option>
                <option value="1"></option>
                <option value="2"></option>
                <option value="3"></option>
                <option value="4"></option>
                <option value="5"></option>
            </select>
            <span class="ql-formats">
                <button class="ql-bold"></button>
                <button class="ql-italic"></button>
                <button class="ql-underline"></button>
                <button class="ql-strike"></button>
            </span>
            <span class="ql-formats">
                <select class="ql-color"></select>
                <select class="ql-background"></select>
            </span>
            <span class="ql-formats">
                <button class="ql-list" value="ordered"></button>
                <button class="ql-list" value="bullet"></button>
            </span>
            <span class="ql-formats">
                <button class="ql-link"></button>
            </span>
            """";

        body = """"
            <h4>This Toolbar works with HTML</h4>
            <a href="https://BlazorHelpWebsite.com">BlazorHelpWebsite.com</a>
            """";
    }
    #endregion
}
