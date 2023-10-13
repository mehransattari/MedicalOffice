

namespace MedicalOffice.Shared.Helper;

public class ResponseData<T>
{
    public ResponseData(T response, bool success, HttpResponseMessage httpResponseMessage)
    {
        Success = success;
        Response = response;
        HttpResponse = httpResponseMessage;
    }

    public bool Success { get; set; }
    public T Response { get; set; }
    public HttpResponseMessage HttpResponse { get; set; }

    public async Task<string> GetBody()
    {
        return await HttpResponse.Content.ReadAsStringAsync();
    }

}