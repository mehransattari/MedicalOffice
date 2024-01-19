using MedicalOffice.Client.Repositories.Inteface;
using MedicalOffice.Client.Services.Interface;
using MedicalOffice.Shared.DTO;
using MedicalOffice.Shared.Helper;

namespace MedicalOffice.Client.Repositories;

public class ReserveRepository: IReserveRepository
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
    public async Task<ResponseData<bool>> CreateReserve(ReserveDto Reserve)
    {
        var result = await _http.PostAsync<ReserveDto, bool>($"{_URL}/createReserve", Reserve);
        return result;
    }

    public async Task<ResponseData<bool>> DeleteReserve(ReserveDto Reserve)
    {
        var result = await _http.DeleteAsync<ReserveDto, bool>($"{_URL}/deleteReserve", Reserve);
        return result;
    }

    public async Task<ResponseData<bool>> DeleteReservesByIds(IEnumerable<long> ids)
    {
        var result = await _http.DeleteAsync<IEnumerable<long>, bool>($"{_URL}/deleteReserves", ids);
        return result;
    }

    public async Task<ResponseData<List<ReserveDto>>> GetAllReserves(int skip = 0, int take = 5)
    {
        var result = await _http.Get<List<ReserveDto>>($"{_URL}/Reserves/{skip}/{take}");
        return result;
    }

    public async Task<ResponseData<IEnumerable<ReserveDto>>> GetAllReserves(string search)
    {
        var result = await _http.Get<IEnumerable<ReserveDto>>($"{_URL}/Reserves/{search}");
        return result;
    }

    public async Task<ResponseData<int>> GetAllReservesCount()
    {
        var result = await _http.Get<int>($"{_URL}/countReserves");
        return result;
    }

    public async Task<ResponseData<ReserveDto>> GetReserveById(long Id)
    {
        var result = await _http.PostAsync<long, ReserveDto>($"{_URL}/getReserveById", Id);
        return result;
    }

    public async Task<ResponseData<bool>> UpdateReserve(ReserveDto Reserve)
    {
        var result = await _http.PutAsync<ReserveDto, bool>($"{_URL}/updateReserve", Reserve);
        return result;
    }
    #endregion
}
