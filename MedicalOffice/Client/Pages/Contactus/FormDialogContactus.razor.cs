using MedicalOffice.Client.Repositories.Inteface;
using MedicalOffice.Shared.DTO;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using System.ComponentModel.DataAnnotations;

namespace MedicalOffice.Client.Pages.Roles;

public class FormDialogContactUsBase : ComponentBase
{
    #region Inject CascadingParameter
    [Inject]
    public IContactUsRepository _contactUsRepository { get; set; }

    [CascadingParameter]
    public MudDialogInstance MudDialog { get; set; }
    #endregion

    #region Parameter
    [Parameter]
    public long Id { get; set; }
    [Parameter]
    public string Title { get; set; }
    [Parameter]
    public string? Image { get; set; }
    [Parameter]
    public string? Text { get; set; }
    [Parameter]
    [MaxLength(20)]
    public string? PhoneNumber { get; set; }
    [Parameter]
    [MaxLength(11)]
    public string? Mobile { get; set; }
    [Parameter]
    [MaxLength(400)]
    public string? Address1 { get; set; }
    [Parameter]
    [MaxLength(400)]
    public string? Address2 { get; set; }
    [Parameter]
    public string? Map { get; set; }

    #endregion

    #region Fields
    public string? title { get; set; }

    public ContactUsDto ContactUsDto = new ContactUsDto();

    public List<ContactUsDto> ContactUs { get; set; } = new List<ContactUsDto>();

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
            await _contactUsRepository.UpdateContactUs(ContactUsDto);
        }
        else
        {
            await _contactUsRepository.CreateContactUs(ContactUsDto);
        }
        StateHasChanged();
        Submit();
    }
    protected override async Task OnInitializedAsync()
    {

        var result = await _contactUsRepository.GetContactUs();
        if (result.Success)
        {
            ContactUs = result.Response.ToList();
        }
        if (Id != 0)
        {
            ContactUsDto.Id = Id;
            ContactUsDto.Mobile = Mobile;
            ContactUsDto.PhoneNumber = PhoneNumber;
            ContactUsDto.Address1 = Address1;
            ContactUsDto.Address2 = Address2;
            ContactUsDto.Text = Text;
            ContactUsDto.Map = Map;
            ContactUsDto.Image = Image;

        }
    }
    #endregion
}
