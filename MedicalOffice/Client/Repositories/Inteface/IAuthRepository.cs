using MedicalOffice.Shared.Helper;

namespace MedicalOffice.Client.Repositories.Inteface;

public interface IAuthRepository
{
    Task<TokenData> Login(UserData userData);
}