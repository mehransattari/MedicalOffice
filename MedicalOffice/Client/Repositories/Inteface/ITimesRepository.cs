using MedicalOffice.Shared.DTO;
using MedicalOffice.Shared.Entities;
using MedicalOffice.Shared.Helper;

namespace MedicalOffice.Client.Repositories.Inteface;

public interface ITimesRepository
{
    Task<ResponseData<bool>> CreateTimesReserve(MultipartFormDataContent model);
    Task<ResponseData<bool>> DeleteTimesReserveByIds(IEnumerable<long> ids);
    Task<ResponseData<IEnumerable<TimesReserve>>> GetTimesReserve();
    Task<ResponseData<TimesReserveDto>> GetTimesReserveById(long Id);
    Task<ResponseData<bool>> UpdateTimesReserve(MultipartFormDataContent model);
    Task<ResponseData<IEnumerable<TimesReserveDto>>> GetTimesReserveByDayId(long id);
    Task<ResponseData<bool>> CheckDuplicateTimesReserve(TimesReserveDto timesReserveDto);
}
