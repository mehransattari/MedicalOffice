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

           var Username = "zsms4731";
           var Password = "83328265";
           var Domain = "0098";
           var From = "300026592018";

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
