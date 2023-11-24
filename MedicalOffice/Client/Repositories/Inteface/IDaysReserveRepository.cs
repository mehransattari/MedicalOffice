using MedicalOffice.Shared.DTO;
using MedicalOffice.Shared.Entities;
using MedicalOffice.Shared.Helper;

namespace MedicalOffice.Client.Repositories.Inteface;

public interface IDaysReserveRepository
{
    Task<ResponseData<bool>> CreateDaysReserve(MultipartFormDataContent model);
    Task<ResponseData<bool>> DeleteDaysReserveByIds(IEnumerable<long> ids);
    Task<ResponseData<IEnumerable<DaysReserve>>> GetDaysReserve();
    Task<ResponseData<DaysReserveDto>> GetDaysReserveById(long Id);
    Task<ResponseData<bool>> UpdateDaysReserve(MultipartFormDataContent model);
}
