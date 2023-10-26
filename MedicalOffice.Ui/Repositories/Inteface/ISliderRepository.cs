using MedicalOffice.Shared.DTO;
using MedicalOffice.Shared.Entities;
using MedicalOffice.Shared.Helper;

namespace MedicalOffice.Ui.Repositories.Inteface;

public interface ISliderRepository
{

    Task<ResponseData<IEnumerable<Slider>>> GetSliders();
  
}
