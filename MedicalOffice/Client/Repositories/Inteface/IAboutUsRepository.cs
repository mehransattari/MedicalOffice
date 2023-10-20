using MedicalOffice.Shared.DTO;
using MedicalOffice.Shared.Entities;
using MedicalOffice.Shared.Helper;

namespace MedicalOffice.Client.Repositories.Inteface;

public interface IAboutUsRepository
{
    Task<ResponseData<bool>> CreateAboutUs(MultipartFormDataContent model);
    Task<ResponseData<bool>> DeleteAboutUsByIds(IEnumerable<long> ids);
    Task<ResponseData<IEnumerable<AboutUs>>> GetAboutUs();
    Task<ResponseData<AboutUsDto>> GetAboutUsById(long Id);
    Task<ResponseData<bool>> UpdateAboutUs(MultipartFormDataContent model);
}
