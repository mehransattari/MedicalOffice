using MedicalOffice.Shared.DTO;
using MedicalOffice.Shared.Helper;

namespace MedicalOffice.Client.Repositories.Inteface;

public interface IReserveRepository
{
    Task<ResponseData<bool>> CreateReserve(ReserveDto Reserve);
    Task<ResponseData<bool>> DeleteReserve(ReserveDto Reserve);
    Task<ResponseData<bool>> DeleteReservesByIds(IEnumerable<long> ids);
    Task<ResponseData<List<ReserveDto>>> GetAllReserves(int skip, int take, string search);
    Task<ResponseData<IEnumerable<ReserveDto>>> GetAllReserves(string search);
    Task<ResponseData<int>> GetAllReservesCount();
    Task<ResponseData<int>> GetAllReservesCount(string search);

    Task<ResponseData<ReserveDto>> GetReserveById(long Id);
    Task<ResponseData<bool>> UpdateReserve(ReserveDto Reserve);
}
