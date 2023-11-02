using MedicalOffice.Shared.DTO;
using MedicalOffice.Shared.Entities;
using MedicalOffice.Shared.Helper;
using MedicalOffice.Ui.Repositories.Inteface;
using MedicalOffice.Ui.Services.Interface;

namespace MedicalOffice.Ui.Repositories;

public class SettingsRepository : ISettingsRepository
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

    public async Task<ResponseData<SettingsDto>> GetLogo()
    {
        var result = await _http.Get<SettingsDto>($"{_URL}/getlogo");
        return result;
    }

    
    #endregion
}
