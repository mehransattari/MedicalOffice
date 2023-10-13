using MedicalOffice.Shared.DTO;
using MedicalOffice.Shared.Helper;

namespace MedicalOffice.Client.Repositories.Inteface;

public interface IRoleRepository
{
    Task<ResponseData<bool>> CreateRole(RoleDto Role);
    Task<ResponseData<bool>> DeleteRole(RoleDto Role);
    Task<ResponseData<bool>> DeleteRolesByIds(IEnumerable<long> ids);
    Task<ResponseData<List<RoleDto>>> GetAllRoles(int skip = 0, int take = 5);
    Task<ResponseData<IEnumerable<RoleDto>>> GetAllRoles(string search);
    Task<ResponseData<int>> GetAllRolesCount();
    Task<ResponseData<RoleDto>> GetRoleById(long Id);
    Task<ResponseData<bool>> UpdateRole(RoleDto Role);
}