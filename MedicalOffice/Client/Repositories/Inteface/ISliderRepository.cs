using MedicalOffice.Shared.DTO;
using MedicalOffice.Shared.Entities;
using MedicalOffice.Shared.Helper;

namespace MedicalOffice.Client.Repositories.Inteface;

public interface ISliderRepository
{
    Task<ResponseData<bool>> CreateSlider(MultipartFormDataContent model);
    Task<ResponseData<bool>> DeleteSliderByIds(IEnumerable<long> ids);
    Task<ResponseData<IEnumerable<Slider>>> GetSliders();
    Task<ResponseData<SliderDto>> GetSliderById(long Id);
    Task<ResponseData<bool>> UpdateSlider(MultipartFormDataContent model);
}
