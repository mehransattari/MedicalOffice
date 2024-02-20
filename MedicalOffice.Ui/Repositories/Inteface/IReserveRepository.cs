using MedicalOffice.Shared.DTO;
using MedicalOffice.Shared.Entities;
using MedicalOffice.Shared.Helper;

namespace MedicalOffice.Ui.Repositories.Inteface;

public interface IReserveRepository
{
    Task<ResponseData<bool>> AddReserve(ReserveDto reserveDto);
    Task<ResponseData<TimesReserve>> ShowDateAndTimeByTimeReserveId(long timeReserveId);


}
