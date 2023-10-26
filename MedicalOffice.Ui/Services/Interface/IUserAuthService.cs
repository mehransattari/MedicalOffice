using MedicalOffice.Shared.Helper;

namespace MedicalOffice.Ui.Services.Interface;

public interface IUserAuthService
{
    Task Login(TokenData tokenData);
    Task Logout();
    Task<bool> CheckToken();
    Task CleanUp();
}