using MedicalOffice.Shared.DTO;
using MedicalOffice.Shared.Entities;
using MedicalOffice.Shared.Helper;

namespace MedicalOffice.Ui.Repositories.Inteface;

public interface ITimesRepository
{
    Task<ResponseData<IEnumerable<TimesReserve>>> GetTimesReserve();
    Task<ResponseData<TimesReserveDto>> GetTimesReserveById(long Id);
    Task<ResponseData<IEnumerable<TimesReserveDto>>> GetTimesReserveByDayId(long id);
}
