using MedicalOffice.Client.Repositories.Inteface;
using MedicalOffice.Shared.DTO;
using MedicalOffice.Shared.Entities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
namespace MedicalOffice.Client.Pages.Users;

public class FormDialogUserBase : ComponentBase
{
    #region Inject CascadingParameter
    [Inject]
    public IUserRepository _userRepository { get; set; }

    [CascadingParameter]
    public MudDialogInstance MudDialog { get; set; }
    #endregion

    #region Parameter
    [Parameter]
    public long Id { get; set; }
    [Parameter]
    public string? FirstName { get; set; }
    [Parameter]
    public string? LastName { get; set; }
    [Parameter]
    public string Mobile { get; set; }
    [Parameter]
    public int Number { get; set; }
    [Parameter]
    public long RoleId { get; set; }
    #endregion

    #region Fields
    public string? RoleName { get; set; }

    public UserDto User = new UserDto();

    public List<Role> roles { get; set; } = new List<Role>();

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
            await _userRepository.UpdateUser(User);
        }
        else
        {
            await _userRepository.CreateUser(User);
        }
        StateHasChanged();
        Submit();
    }
    protected override async Task OnInitializedAsync()
    {

        var resultRoles = await _userRepository.Roles();
        if (resultRoles.Success)
        {
            roles = resultRoles.Response;
        }
        if (Id != 0)
        {
            User.Id = Id;
            User.LastName = LastName;
            User.FirstName = FirstName;
            User.Mobile = Mobile;
            User.RoleId = RoleId;
        }
        if (RoleId == 0)
        {
            RoleId = 1;
        }
        var role = roles.FirstOrDefault(x => x.Id == RoleId);
        RoleName = role.FaCaption;
        roles = roles.Where(x => x.Id != role.Id).ToList();
    }
    #endregion
}
