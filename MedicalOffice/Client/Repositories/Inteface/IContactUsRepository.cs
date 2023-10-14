using MedicalOffice.Shared.DTO;
using MedicalOffice.Shared.Helper;

namespace MedicalOffice.Client.Repositories.Inteface;

public interface IContactUsRepository
{
    Task<ResponseData<bool>> CreateContactUs(ContactUsDto model);
    Task<ResponseData<bool>> DeleteContactUsByIds(IEnumerable<long> ids);
    Task<ResponseData<IEnumerable<ContactUsDto>>> GetContactUs();
    Task<ResponseData<ContactUsDto>> GetContactUsById(long Id);
    Task<ResponseData<bool>> UpdateContactUs(ContactUsDto model);
}
