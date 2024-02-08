using MedicalOffice.Shared.DTO;
using MedicalOffice.Shared.Helper;
using MedicalOffice.Ui.Services.Interface;

namespace MedicalOffice.Ui.Services;

public class SmsService: ISmsService
{
    #region Constructor
    private readonly IHttpService _http;
    private readonly string _URL = "api/smsmessage";
    public SmsService( IHttpService http)
    {
        _http = http;
    }
    #endregion

    public async Task<ResponseData<bool>> SendSmsAsync(SmsDto model)
    {
        var result = await _http.PostAsync<SmsDto, bool>($"{_URL}", model);
        return result;
    }



}
