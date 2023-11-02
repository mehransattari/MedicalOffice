using MedicalOffice.Client.Repositories.Inteface;
using MedicalOffice.Client.Services.Interface;
using MedicalOffice.Shared.DTO;
using MedicalOffice.Shared.Entities;
using MedicalOffice.Shared.Helper;

namespace MedicalOffice.Client.Repositories;

public class SettingsRepository: ISettingsRepository
{
    #region Constructor

    private readonly IHttpService _http;
    private readonly string _URL = "api/settings";
    private readonly HttpClient _httpClient;
    public SettingsRepository(HttpClient httpClient, IHttpService http)
    {
        _httpClient = httpClient;
        _http = http;
    }

    #endregion

    #region Methods
    public async Task<ResponseData<bool>> CreateSettings(MultipartFormDataContent model)
    {
        var result = await _http.PostAsync<bool>($"{_URL}", model);
        return result;
    }

    public async Task<ResponseData<bool>> UpdateSettings(MultipartFormDataContent model)
    {
        var result = await _http.PutAsync<bool>($"{_URL}", model);
        return result;
    }

    public async Task<ResponseData<bool>> DeleteSettingsByIds(IEnumerable<long> ids)
    {
        var result = await _http.DeleteAsync<IEnumerable<long>, bool>($"{_URL}/deleteSettings", ids);
        return result;
    }
    public async Task<ResponseData<IEnumerable<Settings>>> GetSettings()
    {
        var result = await _http.Get<IEnumerable<Settings>>($"{_URL}");
        return result;
    }

    public async Task<ResponseData<SettingsDto>> GetSettingsById(long Id)
    {
        var result = await _http.Get<SettingsDto>($"{_URL}/{Id}");
        return result;
    }


    #endregion
}
