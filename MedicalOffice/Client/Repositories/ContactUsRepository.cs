using MedicalOffice.Client.Repositories.Inteface;
using MedicalOffice.Client.Services.Interface;
using MedicalOffice.Shared.DTO;
using MedicalOffice.Shared.Helper;

namespace MedicalOffice.Client.Repositories;

public class ContactUsRepository : IContactUsRepository
{
    #region Constructor
    private readonly IHttpService _http;
    private readonly string _URL = "api/roles";
    public ContactUsRepository(IHttpService http)
    {
        _http = http;
    }
    #endregion

    #region Methods


    public async Task<ResponseData<bool>> CreateContactUs(ContactUsDto model)
    {
        var result = await _http.PostAsync<ContactUsDto, bool>($"{_URL}/createContactUs", model);
        return result;
    }

    public async Task<ResponseData<bool>> DeleteContactUsByIds(IEnumerable<long> ids)
    {
        var result = await _http.DeleteAsync<IEnumerable<long>, bool>($"{_URL}/deleteContactUs", ids);
        return result;
    }

    public async Task<ResponseData<IEnumerable<ContactUsDto>>> GetContactUs()
    {
        var result = await _http.Get<IEnumerable<ContactUsDto>>($"{_URL}/ContactUs");
        return result;
    }

    public async Task<ResponseData<ContactUsDto>> GetContactUsById(long Id)
    {
        var result = await _http.PostAsync<long, ContactUsDto>($"{_URL}/getContactUsById", Id);
        return result;
    }

    public async Task<ResponseData<bool>> UpdateContactUs(ContactUsDto model)
    {
        var result = await _http.PutAsync<ContactUsDto, bool>($"{_URL}/updateContactUs", model);
        return result;
    }

    #endregion
}
