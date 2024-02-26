using MedicalOffice.Shared.Helper;
using MedicalOffice.Ui.Services.Interface;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace MedicalOffice.Ui.Services;

public class HttpService : IHttpService
{
    #region Constructor
    private readonly HttpClient _http;
    public HttpService(HttpClient http)
    {
        _http = http;
        _http.DefaultRequestHeaders.CacheControl=new System.Net.Http.Headers.CacheControlHeaderValue { NoCache = true };
        _http.DefaultRequestHeaders.Pragma.Add(new NameValueHeaderValue("no-cache"));

    }
    JsonSerializerOptions defaultJsonSerializerOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        ReferenceHandler = ReferenceHandler.Preserve,
    };
    #endregion

    #region Methods
    public async Task<ResponseData<object>> PostAsync<T>(string url, T data)
    {
        var dataSerialize = JsonSerializer.Serialize(data);
        var content = new StringContent(dataSerialize, Encoding.UTF8, "application/json");
        var response = await _http.PostAsync(url, content);

        return new ResponseData<object>(null, response.IsSuccessStatusCode, response);
    }
    public ResponseData<TResponse> Post<T, TResponse>(string url, T data)
    {
        var dataJson = JsonSerializer.Serialize(data);
        var stringContent = new StringContent(dataJson, Encoding.UTF8, "application/json");
        var response = _http.PostAsync(url, stringContent).Result;
        if (response.IsSuccessStatusCode)
        {
            var responseDeserialized = Deserialize<TResponse>(response, defaultJsonSerializerOptions).Result;
            return new ResponseData<TResponse>(responseDeserialized, true, response);
        }
        else
        {
            return new ResponseData<TResponse>(default, false, response);
        }
    }
    public async Task<ResponseData<TResponse>> PostAsync<T, TResponse>(string url, T data)
    {
        var dataJson = JsonSerializer.Serialize(data);
        var stringContent = new StringContent(dataJson, Encoding.UTF8, "application/json");
        var response = await _http.PostAsync(url, stringContent);
        if (response.IsSuccessStatusCode)
        {
            var responseDeserialized = await Deserialize<TResponse>(response, defaultJsonSerializerOptions);
            return new ResponseData<TResponse>(responseDeserialized, true, response);
        }
        else
        {
            return new ResponseData<TResponse>(default, false, response);
        }
    }
    public async Task<ResponseData<TResponse>> PostAsync<TResponse>(string url, MultipartFormDataContent data)
    {

        var response = await _http.PostAsync(url, data);
        if (response.IsSuccessStatusCode)
        {
            var responseDeserialized = await Deserialize<TResponse>(response, defaultJsonSerializerOptions);
            return new ResponseData<TResponse>(responseDeserialized, true, response);
        }
        else
        {
            return new ResponseData<TResponse>(default, false, response);
        }
    }
    public async Task<ResponseData<TResponse>> PutAsync<TResponse>(string url, MultipartFormDataContent data)
    {

        var response = await _http.PutAsync(url, data);
        if (response.IsSuccessStatusCode)
        {
            var responseDeserialized = await Deserialize<TResponse>(response, defaultJsonSerializerOptions);
            return new ResponseData<TResponse>(responseDeserialized, true, response);
        }
        else
        {
            return new ResponseData<TResponse>(default, false, response);
        }
    }

    public ResponseData<TResponse> Put<T, TResponse>(string url, T data)
    {
        var dataJson = JsonSerializer.Serialize(data);
        var stringContent = new StringContent(dataJson, Encoding.UTF8, "application/json");
        var response = _http.PutAsync(url, stringContent).Result;
        if (response.IsSuccessStatusCode)
        {
            var responseDeserialized = Deserialize<TResponse>(response, defaultJsonSerializerOptions).Result;
            return new ResponseData<TResponse>(responseDeserialized, true, response);
        }
        else
        {
            return new ResponseData<TResponse>(default, false, response);
        }
    }
    public async Task<ResponseData<TResponse>> PutAsync<T, TResponse>(string url, T data)
    {
        var dataJson = JsonSerializer.Serialize(data);
        var stringContent = new StringContent(dataJson, Encoding.UTF8, "application/json");
        var response = await _http.PutAsync(url, stringContent);
        if (response.IsSuccessStatusCode)
        {
            var responseDeserialized = await Deserialize<TResponse>(response, defaultJsonSerializerOptions);
            return new ResponseData<TResponse>(responseDeserialized, true, response);
        }
        else
        {
            return new ResponseData<TResponse>(default, false, response);
        }
    }


    public ResponseData<TResponse> Delete<T, TResponse>(string url, T data)
    {
        var dataJson = JsonSerializer.Serialize(data);
        var response = _http.DeleteAsync(url).Result;
        if (response.IsSuccessStatusCode)
        {
            var responseDeserialized = Deserialize<TResponse>(response, defaultJsonSerializerOptions).Result;
            return new ResponseData<TResponse>(responseDeserialized, true, response);
        }
        else
        {
            return new ResponseData<TResponse>(default, false, response);
        }
    }
    public async Task<ResponseData<TResponse>> DeleteAsync<T, TResponse>(string url, T data)
    {
        var dataSerialize = JsonSerializer.Serialize(data);
        var content = new StringContent(dataSerialize, Encoding.UTF8, "application/json");
        var response = await _http.PostAsync(url, content);

        if (response.IsSuccessStatusCode)
        {
            var responseDeserialized = await Deserialize<TResponse>(response, defaultJsonSerializerOptions);
            return new ResponseData<TResponse>(responseDeserialized, true, response);
        }
        else
        {
            return new ResponseData<TResponse>(default, false, response);
        }
    }


    private async Task<T> Deserialize<T>(HttpResponseMessage httpResponse, JsonSerializerOptions options)
    {
        var responseString = await httpResponse.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<T>(responseString, options);
    }

    public async Task<ResponseData<T>> Get<T>(string url)
    {
        try
        {
            using (var responseHTTP = await _http.GetAsync(url))
            {
                responseHTTP.EnsureSuccessStatusCode();
                Console.WriteLine($"response: {responseHTTP.Content}");

                var response = await Deserialize<T>(responseHTTP, defaultJsonSerializerOptions);



                return new ResponseData<T>(response, true, responseHTTP);
            }
        }
        catch (HttpRequestException ex)
        {
            // اینجا می‌توانید خطاهای مربوط به درخواست HTTP را مدیریت کنید
            Console.WriteLine($"An HTTP request exception occurred: {ex.Message}");
            return new ResponseData<T>(default, false, null);
        }
        catch (JsonException ex)
        {
            // اینجا می‌توانید خطاهای مربوط به Deserialize کردن JSON را مدیریت کنید
            Console.WriteLine($"A JSON parsing exception occurred: {ex.Message}");
            return new ResponseData<T>(default, false, null);
        }
        catch (Exception ex)
        {
            // اینجا می‌توانید خطاهای دیگر را مدیریت کنید
            Console.WriteLine($"An unexpected exception occurred: {ex.Message}");
            throw; // یا ممکن است خطاهای غیرمنتظره را دوباره پرتاب کنید
        }
    }

    #endregion
}

