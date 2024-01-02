
using MedicalOffice.Shared.DTO;
using MedicalOffice.Shared.Entities;
using MedicalOffice.Shared.Helper;
using MedicalOffice.Ui.Repositories.Inteface;
using MedicalOffice.Ui.Services.Interface;

namespace MedicalOffice.Ui.Repositories;

public class DaysReserveRepository : IDaysReserveRepository
{
    #region Constructor
    private readonly IHttpService _http;
    private readonly string _URL = "api/DaysReserve";
    public DaysReserveRepository(IHttpService http)
    {
        _http = http;
    }

    #endregion

    #region Methods

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
