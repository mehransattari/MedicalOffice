
using MedicalOffice.Client.Services.Interface;
using MedicalOffice.Shared.Helper;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace MedicalOffice.Client.Services;

public class HttpService : IHttpService
{
    #region Constructor
    private readonly HttpClient _http;
    public HttpService(HttpClient http)
    {
        _http = http;
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
        var response =  _http.PostAsync(url, stringContent).Result;
        if (response.IsSuccessStatusCode)
        {
            var responseDeserialized =  Deserialize<TResponse>(response, defaultJsonSerializerOptions).Result;
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
        var responseHTTP = await _http.GetAsync(url);

        if (responseHTTP.IsSuccessStatusCode)
        {
            var response = await Deserialize<T>(responseHTTP, defaultJsonSerializerOptions);
            return new ResponseData<T>(response, true, responseHTTP);
        }
        else
        {
            return new ResponseData<T>(default, false, responseHTTP);
        }
    }
    #endregion
}

