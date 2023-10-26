using MedicalOffice.Shared.DTO;
using MedicalOffice.Shared.Entities;
using MedicalOffice.Shared.Helper;

namespace MedicalOffice.Ui.Repositories.Inteface;

public interface IContactUsRepository
{
    Task<ResponseData<bool>> CreateContactUs(MultipartFormDataContent model);
    Task<ResponseData<bool>> DeleteContactUsByIds(IEnumerable<long> ids);
    Task<ResponseData<IEnumerable<ContactUs>>> GetContactUs();
    Task<ResponseData<ContactUsDto>> GetContactUsById(long Id);
    Task<ResponseData<bool>> UpdateContactUs(MultipartFormDataContent model);
}
