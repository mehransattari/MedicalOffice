using MedicalOffice.Client.Repositories.Inteface;
using MedicalOffice.Client.Services.Interface;
using MedicalOffice.Shared.DTO;
using MedicalOffice.Shared.Entities;
using MedicalOffice.Shared.Helper;

namespace MedicalOffice.Client.Repositories;

public class TimesRepository: ITimesRepository
{
    #region Constructor
    private readonly IHttpService _http;
    private readonly string _URL = "api/timesreserve";
    public TimesRepository(IHttpService http)
    {
        _http = http;
    }

    #endregion

    #region Methods
    public async Task<ResponseData<bool>> CreateTimesReserve(MultipartFormDataContent model)
    {
        var result = await _http.PostAsync<bool>($"{_URL}", model);
        return result;
    }

    public async Task<ResponseData<bool>> UpdateTimesReserve(MultipartFormDataContent model)
    {
        var result = await _http.PutAsync<bool>($"{_URL}", model);
        return result;
    }

    public async Task<ResponseData<bool>> DeleteTimesReserveByIds(IEnumerable<long> ids)
    {
        var result = await _http.DeleteAsync<IEnumerable<long>, bool>($"{_URL}/deleteTimesReserve", ids);
        return result;
    }

    public async Task<ResponseData<IEnumerable<TimesReserve>>> GetTimesReserve()
    {
        var result = await _http.Get<IEnumerable<TimesReserve>>($"{_URL}");
        return result;
    }

    public async Task<ResponseData<TimesReserveDto>> GetTimesReserveById(long Id)
    {
        var result = await _http.Get<TimesReserveDto>($"{_URL}/{Id}");
        return result;
    }

    public async Task<ResponseData<IEnumerable<TimesReserveDto>>> GetTimesReserveByDayId(long id)
    {
        var result = await _http.Get<IEnumerable<TimesReserveDto>>($"{_URL}/getbyday/{id}");
        return result;
    }

    public async Task<ResponseData<bool>> CheckDuplicateTimesReserve(TimesReserveDto timesReserveDto)
    {
        var result = await _http.PostAsync<TimesReserveDto, bool>($"{_URL}/checkDuplicateTimesReserve", timesReserveDto);
        return result;
    }
    #endregion
}
