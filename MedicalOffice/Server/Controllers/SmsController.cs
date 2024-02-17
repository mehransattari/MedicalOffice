using MedicalOffice.Shared.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MedicalOffice.Server.Controllers;

[Route("api/smsmessage")]
[ApiController]
public class SmsController : Controller
{

    [HttpPost]
    public async Task<bool> SmsSender( [FromBody]SmsDto model)
    {
        try
        {
          return await SendSmsAsync(model);
        }
        catch (Exception)
        {
            return false;
        }
    }

    private async Task<bool> SendSmsAsync(SmsDto smsDto)
    {
        try
        {

           var Username = "psms8625";
           var Password = "otnyTY16^9U^";
           var Domain = "0098";
           var From = "30005090503446";
            //شماره های فعال شده: 30005090503446 - 30005090503446 - 3000164545 -
            HttpClient httpClient = new HttpClient();
            var connectString = $"http://www.0098sms.com/sendsmslink.aspx?FROM=" +
                 From + "&TO=" + smsDto.To + "&TEXT=" + smsDto.Text + 
                "&USERNAME=" + Username + "&PASSWORD=" + Password + "&DOMAIN=" + Domain;

            var httpResponse = await httpClient.GetAsync(connectString);

            if (httpResponse.StatusCode == HttpStatusCode.OK)
                return true;
            else
                return false;
        }
        catch
        {
            return false;
        }
    }

}
