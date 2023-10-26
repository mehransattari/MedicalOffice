using MedicalOffice.Shared.DTO;
using MedicalOffice.Shared.Entities;
using MedicalOffice.Shared.Helper;

namespace MedicalOffice.Client.Repositories.Inteface;

public interface IProvidingServiceRepository
{
    Task<ResponseData<bool>> CreateProvidingService(MultipartFormDataContent model);
    Task<ResponseData<bool>> DeleteProvidingServiceByIds(IEnumerable<long> ids);
    Task<ResponseData<IEnumerable<ProvidingService>>> GetProvidingService();
    Task<ResponseData<ProvidingServiceDto>> GetProvidingServiceById(long Id);
    Task<ResponseData<bool>> UpdateProvidingService(MultipartFormDataContent model);
}
