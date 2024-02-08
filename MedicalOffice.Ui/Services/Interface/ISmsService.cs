using MedicalOffice.Shared.DTO;
using MedicalOffice.Shared.Helper;

namespace MedicalOffice.Ui.Services.Interface;

public interface ISmsService
{
    Task<ResponseData<bool>> SendSmsAsync(SmsDto model);
}
