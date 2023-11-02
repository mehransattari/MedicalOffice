using MedicalOffice.Shared.DTO;
using MedicalOffice.Shared.Entities;
using MedicalOffice.Shared.Helper;

namespace MedicalOffice.Ui.Repositories.Inteface;

public interface ISettingsRepository
{
    Task<ResponseData<IEnumerable<Settings>>> GetSettings();
    Task<ResponseData<SettingsDto>> GetSettingsById(long Id);
    Task<ResponseData<SettingsDto>> GetLogo();
}
