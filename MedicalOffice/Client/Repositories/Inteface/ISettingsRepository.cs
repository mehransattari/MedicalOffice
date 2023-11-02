using MedicalOffice.Shared.DTO;
using MedicalOffice.Shared.Entities;
using MedicalOffice.Shared.Helper;

namespace MedicalOffice.Client.Repositories.Inteface;

public interface ISettingsRepository
{
    Task<ResponseData<bool>> CreateSettings(MultipartFormDataContent model);
    Task<ResponseData<bool>> DeleteSettingsByIds(IEnumerable<long> ids);
    Task<ResponseData<IEnumerable<Settings>>> GetSettings();
    Task<ResponseData<SettingsDto>> GetSettingsById(long Id);
    Task<ResponseData<bool>> UpdateSettings(MultipartFormDataContent model);
}
