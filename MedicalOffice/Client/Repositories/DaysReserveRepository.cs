using MedicalOffice.Client.Repositories.Inteface;
using MedicalOffice.Client.Services.Interface;
using MedicalOffice.Shared.DTO;
using MedicalOffice.Shared.Entities;
using MedicalOffice.Shared.Helper;

namespace MedicalOffice.Client.Repositories;

public class DaysReserveRepository : IDaysReserveRepository
{
    #region Constructor
    private readonly IHttpService _http;
    private readonly string _URL = "api/DaysReserve";
    public DaysReserveRepository( IHttpService http)
    {
        _http = http;
    }

    #endregion

    #region Methods
    public async Task<ResponseData<bool>> CreateDaysReserve(MultipartFormDataContent model)
    {
        var result = await _http.PostAsync<bool>($"{_URL}", model);
        return result;
    }

    public async Task<ResponseData<bool>> UpdateDaysReserve(MultipartFormDataContent model)
    {
        var result = await _http.PutAsync<bool>($"{_URL}", model);
        return result;
    }

    public async Task<ResponseData<bool>> DeleteDaysReserveByIds(IEnumerable<long> ids)
    {
        var result = await _http.DeleteAsync<IEnumerable<long>, bool>($"{_URL}/deleteDaysReserve", ids);
        return result;
    }
    public async Task<ResponseData<IEnumerable<DaysReserve>>> GetDaysReserve()
    {
        var result = await _http.Get<IEnumerable<DaysReserve>>($"{_URL}");
        return result;
    }

    public async Task<ResponseData<DaysReserveDto>> GetDaysReserveById(long Id)
    {
        var result = await _http.Get<DaysReserveDto>($"{_URL}/{Id}");
        return result;
    }


    #endregion
}
