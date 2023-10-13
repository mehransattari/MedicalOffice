using MedicalOffice.Client.Repositories.Inteface;
using MedicalOffice.Shared.DTO;
using MedicalOffice.Shared.Entities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using System.ComponentModel.DataAnnotations;

namespace MedicalOffice.Client.Pages.Roles;

public class FormDialogRoleBase : ComponentBase
{
    #region Inject CascadingParameter
    [Inject]
    public IRoleRepository _RoleRepository { get; set; }

    [CascadingParameter]
    public MudDialogInstance MudDialog { get; set; }
    #endregion

    #region Parameter
    [Parameter]
    public long Id { get; set; }
    [Parameter]
    public string FaCaption { get; set; }
    [Parameter]
    public string EnCaption { get; set; }
    [Parameter]
    public int Number { get; set; }

    #endregion

    #region Fields
    public string? RoleName { get; set; }

    public RoleDto Role = new RoleDto();

    public List<RoleDto> roles { get; set; } = new List<RoleDto>();

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
            await _RoleRepository.UpdateRole(Role);
        }
        else
        {
            await _RoleRepository.CreateRole(Role);
        }
        StateHasChanged();
        Submit();
    }
    protected override async Task OnInitializedAsync()
    {

        var resultRoles = await _RoleRepository.GetAllRoles();
        if (resultRoles.Success)
        {
            roles = resultRoles.Response;
        }
        if (Id != 0)
        {
            Role.Id = Id;
            Role.FaCaption = FaCaption;
            Role.EnCaption = EnCaption;
          
        }     
    }
    #endregion
}
