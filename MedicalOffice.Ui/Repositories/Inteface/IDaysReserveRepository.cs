using MedicalOffice.Shared.DTO;
using MedicalOffice.Shared.Entities;
using MedicalOffice.Shared.Helper;

namespace MedicalOffice.Ui.Repositories.Inteface;

public interface IDaysReserveRepository
{

    Task<ResponseData<IEnumerable<DaysReserve>>> GetDaysReserve();
    Task<ResponseData<DaysReserveDto>> GetDaysReserveById(long Id);
    Task<ResponseData<List<DaysReserve>>> GetTimesDayReserve();

}
