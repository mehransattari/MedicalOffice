using MedicalOffice.Shared.DTO;
using MedicalOffice.Shared.Entities;
using MedicalOffice.Shared.Helper;
using MedicalOffice.Ui.Repositories.Inteface;
using MedicalOffice.Ui.Services.Interface;

namespace MedicalOffice.Ui.Repositories;

public class ProvidingServiceRepository : IProvidingServiceRepository
{
    #region Constructor
    private readonly IHttpService _http;
    private readonly string _URL = "api/ProvidingService";
    private readonly HttpClient _httpClient;
    public ProvidingServiceRepository(HttpClient httpClient, IHttpService http)
    {
        _httpClient = httpClient;
        _http = http;

    }

    #endregion

    #region Methods
    public async Task<ResponseData<bool>> CreateProvidingService(MultipartFormDataContent model)
    {
        var result = await _http.PostAsync<bool>($"{_URL}", model);
        return result;
    }

    public async Task<ResponseData<bool>> UpdateProvidingService(MultipartFormDataContent model)
    {
        var result = await _http.PutAsync<bool>($"{_URL}", model);
        return result;
    }

    public async Task<ResponseData<bool>> DeleteProvidingServiceByIds(IEnumerable<long> ids)
    {
        var result = await _http.DeleteAsync<IEnumerable<long>, bool>($"{_URL}/deleteProvidingService", ids);
        return result;
    }
    public async Task<ResponseData<IEnumerable<ProvidingService>>> GetProvidingService()
    {
        var result = await _http.Get<IEnumerable<ProvidingService>>($"{_URL}");
        return result;
    }

    public async Task<ResponseData<ProvidingService>> GetProvidingServiceById(long Id)
    {
        var result = await _http.Get<ProvidingService>($"{_URL}/{Id}");
        return result;
    }


    #endregion
}
