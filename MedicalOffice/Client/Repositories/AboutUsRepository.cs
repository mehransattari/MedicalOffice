using MedicalOffice.Client.Repositories.Inteface;
using MedicalOffice.Client.Services.Interface;
using MedicalOffice.Shared.DTO;
using MedicalOffice.Shared.Helper;

namespace MedicalOffice.Client.Repositories;

public class AboutUsRepository : IAboutUsRepository
{
    #region Constructor
    private readonly IHttpService _http;
    private readonly string _URL = "api/aboutus";
    public AboutUsRepository(IHttpService http)
    {
        _http = http;
    }
    #endregion

    #region Methods
    public async Task<ResponseData<bool>> CreateAboutUs(AboutUsDto model)
    {
        var result = await _http.PostAsync<AboutUsDto, bool>($"{_URL}/createAboutUs", model);
        return result;
    }
      
    public async Task<ResponseData<bool>> DeleteAboutUsByIds(IEnumerable<long> ids)
    {
        var result = await _http.DeleteAsync<IEnumerable<long>, bool>($"{_URL}/deleteAboutUs", ids);
        return result;
    }
    public async Task<ResponseData<IEnumerable<AboutUsDto>>> GetAboutUs()
    {
        var result = await _http.Get<IEnumerable<AboutUsDto>>($"{_URL}");
        return result;
    }

    public async Task<ResponseData<AboutUsDto>> GetAboutUsById(long Id)
    {
        var result = await _http.PostAsync<long, AboutUsDto>($"{_URL}/getAboutUsById", Id);
        return result;
    }

    public async Task<ResponseData<bool>> UpdateAboutUs(AboutUsDto model)
    {
        var result = await _http.PutAsync<AboutUsDto, bool>($"{_URL}/updateAboutUs", model);
        return result;
    }
    #endregion

}
