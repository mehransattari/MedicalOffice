using MedicalOffice.Client.Repositories.Inteface;
using MedicalOffice.Client.Services.Interface;
using MedicalOffice.Shared.DTO;
using MedicalOffice.Shared.Entities;
using MedicalOffice.Shared.Helper;

namespace MedicalOffice.Client.Repositories;

public class SliderRepository : ISliderRepository
{
    #region Constructor
    private readonly IHttpService _http;
    private readonly string _URL = "api/slider";
    private readonly HttpClient _httpClient;
    public SliderRepository(HttpClient httpClient, IHttpService http)
    {
        _httpClient = httpClient;
        _http = http;

    }

    #endregion

    public async Task<ResponseData<bool>> CreateSlider(MultipartFormDataContent model)
    {
        var result = await _http.PostAsync<bool>($"{_URL}", model);
        return result;
    }

    public async Task<ResponseData<bool>> UpdateSlider(MultipartFormDataContent model)
    {
        var result = await _http.PutAsync<bool>($"{_URL}", model);
        return result;
    }

    public async Task<ResponseData<bool>> DeleteSliderByIds(IEnumerable<long> ids)
    {
        var result = await _http.DeleteAsync<IEnumerable<long>, bool>($"{_URL}/deleteSlider", ids);
        return result;
    }

    public async Task<ResponseData<SliderDto>> GetSliderById(long Id)
    {
        var result = await _http.PostAsync<long, SliderDto>($"{_URL}", Id);
        return result; 
    }

    public async Task<ResponseData<IEnumerable<Slider>>> GetSliders()
    {
        var result = await _http.Get<IEnumerable<Slider>>($"{_URL}");
        return result;
    }

  
}
