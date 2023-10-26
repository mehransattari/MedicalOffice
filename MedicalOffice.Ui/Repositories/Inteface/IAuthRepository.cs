using MedicalOffice.Shared.Helper;

namespace MedicalOffice.Ui.Repositories.Inteface;

public interface IAuthRepository
{
    Task<TokenData> Login(UserData userData);
}