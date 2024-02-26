
using MedicalOffice.Shared.DTO;
using MedicalOffice.Shared.Entities;
using MedicalOffice.Shared.Helper;
using MedicalOffice.Ui.Repositories.Inteface;
using MedicalOffice.Ui.Services.Interface;

namespace MedicalOffice.Ui.Repositories;

public class ReserveRepository : IReserveRepository
{
    #region Constructor
    private readonly IHttpService _http;
    private readonly string _URL = "api/Reserves";
    public ReserveRepository(IHttpService http)
    {
        _http = http;
    }
    #endregion

    #region Methods
    public async Task<ResponseData<bool>> AddReserve(ReserveDto reserveDto)
    {
        var result = await _http.PostAsync<ReserveDto, bool>($"{_URL}/ReserveUser", reserveDto);

        return result;
    }
    public async  Task<ResponseData<TimesReserve>> ShowDateAndTimeByTimeReserveId(long timeReserveId)
    {
        var result = await _http.Get<TimesReserve>($"{_URL}/showDateAndTimeByTimeReserveId/{timeReserveId}");

        return result;
    }
    public async Task<ResponseData<bool>> CheckDuplicateReservation(string nationalCode, long timesReserveId)
    {
        var result = await _http.Get<bool>($"{_URL}/checkDuplicateReservation/{nationalCode}/{timesReserveId}");

        return result;
    }

    
    #endregion
}
