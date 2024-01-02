
using MedicalOffice.Shared.DTO;
using MedicalOffice.Shared.Entities;
using MedicalOffice.Shared.Helper;
using MedicalOffice.Ui.Repositories.Inteface;
using MedicalOffice.Ui.Services.Interface;

namespace MedicalOffice.Ui.Repositories;

public class TimesRepository : ITimesRepository
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

    #endregion
}
