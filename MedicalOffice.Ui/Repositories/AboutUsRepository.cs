using MedicalOffice.Shared.DTO;
using MedicalOffice.Shared.Entities;
using MedicalOffice.Shared.Helper;
using MedicalOffice.Ui.Repositories.Inteface;
using MedicalOffice.Ui.Services.Interface;

namespace MedicalOffice.Ui.Repositories;

public class AboutUsRepository : IAboutUsRepository
{
    #region Constructor
    private readonly IHttpService _http;
    private readonly string _URL = "api/aboutus";
    private readonly HttpClient _httpClient;
    public AboutUsRepository(HttpClient httpClient, IHttpService http)
    {
        _httpClient = httpClient;
        _http = http;

    }

    #endregion

    #region Methods
    public async Task<ResponseData<bool>> CreateAboutUs(MultipartFormDataContent model)
    {
        var result = await _http.PostAsync<bool>($"{_URL}", model);
        return result;
    }

    public async Task<ResponseData<bool>> UpdateAboutUs(MultipartFormDataContent model)
    {
        var result = await _http.PutAsync<bool>($"{_URL}", model);
        return result;
    }

    public async Task<ResponseData<bool>> DeleteAboutUsByIds(IEnumerable<long> ids)
    {
        var result = await _http.DeleteAsync<IEnumerable<long>, bool>($"{_URL}/deleteAboutUs", ids);
        return result;
    }
    public async Task<ResponseData<IEnumerable<AboutUs>>> GetAboutUs()
    {
        var result = await _http.Get<IEnumerable<AboutUs>>($"{_URL}");
        return result;
    }

    public async Task<ResponseData<AboutUsDto>> GetAboutUsById(long Id)
    {
        var result = await _http.PostAsync<long, AboutUsDto>($"{_URL}", Id);
        return result;
    }


    #endregion

}
