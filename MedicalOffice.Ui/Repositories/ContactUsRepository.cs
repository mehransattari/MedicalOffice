using MedicalOffice.Shared.DTO;
using MedicalOffice.Shared.Entities;
using MedicalOffice.Shared.Helper;
using MedicalOffice.Ui.Repositories.Inteface;
using MedicalOffice.Ui.Services.Interface;

namespace MedicalOffice.Ui.Repositories;

public class ContactUsRepository : IContactUsRepository
{
    #region Constructor
    private readonly IHttpService _http;
    private readonly string _URL = "api/contactus";
    public ContactUsRepository(IHttpService http)
    {
        _http = http;
    }
    #endregion


    #region Methods
    public async Task<ResponseData<bool>> CreateContactUs(MultipartFormDataContent model)
    {
        var result = await _http.PostAsync<bool>($"{_URL}", model);
        return result;
    }

    public async Task<ResponseData<bool>> UpdateContactUs(MultipartFormDataContent model)
    {
        var result = await _http.PutAsync<bool>($"{_URL}", model);
        return result;
    }

    public async Task<ResponseData<bool>> DeleteContactUsByIds(IEnumerable<long> ids)
    {
        var result = await _http.DeleteAsync<IEnumerable<long>, bool>($"{_URL}/deleteContactUs", ids);
        return result;
    }
    public async Task<ResponseData<IEnumerable<ContactUs>>> GetContactUs()
    {
        var result = await _http.Get<IEnumerable<ContactUs>>($"{_URL}");
        return result;
    }

    public async Task<ResponseData<ContactUsDto>> GetContactUsById(long Id)
    {
        var result = await _http.PostAsync<long, ContactUsDto>($"{_URL}", Id);
        return result;
    }


    #endregion
}
