using MedicalOffice.Shared.DTO;
using MedicalOffice.Shared.Entities;
using MedicalOffice.Shared.Helper;
using MedicalOffice.Ui.Repositories.Inteface;
using MedicalOffice.Ui.Services.Interface;

namespace MedicalOffice.Ui.Repositories;

public class UserRepository : IUserRepository
{
    #region Constructor
    private readonly IHttpService _http;
    private readonly string _URL = "api/users";
    public UserRepository(IHttpService http)
    {
        _http = http;
    }
    #endregion

    #region Methods
    public async Task<ResponseData<List<Role>>> Roles()
    {
        var result = await _http.Get<List<Role>>($"{_URL}/roles");

        return result;
    }

    public async Task<ResponseData<bool>> CreateUser(UserDto user)
    {
        var result = await _http.PostAsync<UserDto, bool>($"{_URL}/createUser", user);

        return result;
    }
    public async Task<ResponseData<int>> GetAllUsersCount()
    {
        var result = await _http.Get<int>($"{_URL}/countUsers");
        return result;
    }
    public async Task<ResponseData<List<UserDto>>> GetAllUsers(int skip = 0, int take = 5)
    {
        var result = await _http.Get<List<UserDto>>($"{_URL}/userList/{skip}/{take}");
        return result;
    }
    public async Task<ResponseData<IEnumerable<UserDto>>> GetAllUsers(string search)
    {
        var result = await _http.Get<IEnumerable<UserDto>>($"{_URL}/userList/{search}");
        return result;
    }
    public async Task<ResponseData<UserDto>> GetUserById(long Id)
    {
        var result = await _http.PostAsync<long, UserDto>($"{_URL}/getUserById", Id);

        return result;
    }

    public async Task<ResponseData<bool>> UpdateUser(UserDto user)
    {
        var result = await _http.PostAsync<UserDto, bool>($"{_URL}/updateUser", user);

        return result;
    }

    public async Task<ResponseData<bool>> DeleteUser(UserDto user)
    {
        var result = await _http.PostAsync<UserDto, bool>($"{_URL}/deleteUser", user);

        return result;
    }
    public async Task<ResponseData<bool>> DeleteUsersByIds(IEnumerable<long> ids)
    {
        var result = await _http.PostAsync<IEnumerable<long>, bool>($"{_URL}/deleteUsers", ids);

        return result;
    }

    public async Task<ResponseData<bool>> RegisterUser(RegisterDTO user)
    {
        var result = await _http.PostAsync<RegisterDTO, bool>($"{_URL}/registerUser", user);

        return result;
    }


    public async Task<ResponseData<bool>> AddReserve(ReserveDto reserveDto)
    {
        var result = await _http.PostAsync<ReserveDto, bool>($"{_URL}/ReserveUser", reserveDto);

        return result;
    }
    #endregion
}
