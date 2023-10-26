using MedicalOffice.Shared.DTO;
using MedicalOffice.Shared.Entities;
using MedicalOffice.Shared.Helper;
using MedicalOffice.Ui.Repositories.Inteface;
using MedicalOffice.Ui.Services.Interface;

namespace MedicalOffice.Ui.Repositories;

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

    
    public async Task<ResponseData<IEnumerable<Slider>>> GetSliders()
    {
        var result = await _http.Get<IEnumerable<Slider>>($"{_URL}");
        return result;
    }


}
