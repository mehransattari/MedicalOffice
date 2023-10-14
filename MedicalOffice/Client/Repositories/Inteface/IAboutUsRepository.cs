using MedicalOffice.Shared.DTO;
using MedicalOffice.Shared.Helper;

namespace MedicalOffice.Client.Repositories.Inteface;

public interface IAboutUsRepository
{
    Task<ResponseData<bool>> CreateAboutUs(AboutUsDto model);
    Task<ResponseData<bool>> DeleteAboutUsByIds(IEnumerable<long> ids);
    Task<ResponseData<IEnumerable<AboutUsDto>>> GetAboutUs();
    Task<ResponseData<AboutUsDto>> GetAboutUsById(long Id);
    Task<ResponseData<bool>> UpdateAboutUs(AboutUsDto model);
}
