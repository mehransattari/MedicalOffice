using MedicalOffice.Shared.DTO;
using MedicalOffice.Shared.Helper;
using MedicalOffice.Ui.Repositories.Inteface;
using MedicalOffice.Ui.Services.Interface;

namespace MedicalOffice.Ui.Repositories;

public class RoleRepository : IRoleRepository
{
    #region Constructor
    private readonly IHttpService _http;
    private readonly string _URL = "api/roles";
    public RoleRepository(IHttpService http)
    {
        _http = http;
    }
    #endregion

    #region Methods
    public async Task<ResponseData<bool>> CreateRole(RoleDto Role)
    {
        var result = await _http.PostAsync<RoleDto, bool>($"{_URL}/createRole", Role);
        return result;
    }

    public async Task<ResponseData<bool>> DeleteRole(RoleDto Role)
    {
        var result = await _http.DeleteAsync<RoleDto, bool>($"{_URL}/deleteRole", Role);
        return result;
    }

    public async Task<ResponseData<bool>> DeleteRolesByIds(IEnumerable<long> ids)
    {
        var result = await _http.DeleteAsync<IEnumerable<long>, bool>($"{_URL}/deleteRoles", ids);
        return result;
    }

    public async Task<ResponseData<List<RoleDto>>> GetAllRoles(int skip = 0, int take = 5)
    {
        var result = await _http.Get<List<RoleDto>>($"{_URL}/roles/{skip}/{take}");
        return result;
    }

    public async Task<ResponseData<IEnumerable<RoleDto>>> GetAllRoles(string search)
    {
        var result = await _http.Get<IEnumerable<RoleDto>>($"{_URL}/roles/{search}");
        return result;
    }

    public async Task<ResponseData<int>> GetAllRolesCount()
    {
        var result = await _http.Get<int>($"{_URL}/countRoles");
        return result;
    }

    public async Task<ResponseData<RoleDto>> GetRoleById(long Id)
    {
        var result = await _http.PostAsync<long, RoleDto>($"{_URL}/getRoleById", Id);
        return result;
    }

    public async Task<ResponseData<bool>> UpdateRole(RoleDto Role)
    {
        var result = await _http.PutAsync<RoleDto, bool>($"{_URL}/updateRole", Role);
        return result;
    }
    #endregion

}
