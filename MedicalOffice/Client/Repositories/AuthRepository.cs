

using MedicalOffice.Client.Repositories.Inteface;
using MedicalOffice.Client.Services.Interface;
using MedicalOffice.Shared.Helper;

namespace MedicalOffice.Client.Repositories;

public class AuthRepository : IAuthRepository
{
    #region Constructor
    private readonly IHttpService _httpService;
    private readonly string authUrl = "api/auth";
    public AuthRepository(IHttpService http)
    {
        _httpService = http;
    }
    #endregion

    #region Methods
    public async Task<TokenData> Login(UserData userData)
    {
        var response = await _httpService.PostAsync<UserData, TokenData>($"{authUrl}/login", userData);
        if (!response.Success)
        {
            throw new ApplicationException(await response.GetBody());
        }

        return response.Response;
    }
    #endregion
}


