using MedicalOffice.Shared.Helper;

namespace MedicalOffice.Client.Services.Interface;

public interface IHttpService
{
    ResponseData<TResponse> Post<T, TResponse>(string url, T data);
    Task<ResponseData<object>> PostAsync<T>(string url, T data);
    Task<ResponseData<TResponse>> PostAsync<T, TResponse>(string url, T data);
    ResponseData<TResponse> Put<T, TResponse>(string url, T data);
    Task<ResponseData<TResponse>> PutAsync<T, TResponse>(string url, T data);
    ResponseData<TResponse> Delete<T, TResponse>(string url, T data);
    Task<ResponseData<TResponse>> DeleteAsync<T, TResponse>(string url, T data);
    Task<ResponseData<T>> Get<T>(string url);

    Task<ResponseData<TResponse>> PostAsync<TResponse>(string url, MultipartFormDataContent data);
    Task<ResponseData<TResponse>> PutAsync<TResponse>(string url, MultipartFormDataContent data);

}