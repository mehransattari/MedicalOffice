using MedicalOffice.Shared.Helper;

namespace MedicalOffice.Client.Services.Interface;

public interface IUserAuthService
{
    Task Login(TokenData tokenData);
    Task Logout();
    Task<bool> CheckToken();
    Task CleanUp();
}