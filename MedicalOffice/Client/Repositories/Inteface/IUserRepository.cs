using MedicalOffice.Shared.DTO;
using MedicalOffice.Shared.Entities;
using MedicalOffice.Shared.Helper;

namespace MedicalOffice.Client.Repositories.Inteface;

public interface IUserRepository
{
    Task<ResponseData<bool>> CreateUser(UserDto user);
    Task<ResponseData<bool>> RegisterUser(RegisterDTO user);
    Task<ResponseData<bool>> DeleteUser(UserDto user);
    Task<ResponseData<bool>> DeleteUsersByIds(IEnumerable<long> ids);
    Task<ResponseData<List<UserDto>>> GetAllUsers(int skip = 0, int take = 5);
    Task<ResponseData<IEnumerable<UserDto>>> GetAllUsers(string search);
    Task<ResponseData<int>> GetAllUsersCount();
    Task<ResponseData<UserDto>> GetUserById(long Id);
    Task<ResponseData<List<Role>>> Roles();
    Task<ResponseData<bool>> UpdateUser(UserDto user);
}
