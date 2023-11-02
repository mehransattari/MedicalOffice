using MedicalOffice.Client.Repositories.Inteface;
using MedicalOffice.Client.Services.Interface;
using MedicalOffice.Shared.Entities;
using MedicalOffice.Shared.Helper;

namespace MedicalOffice.Client.Repositories;

public class ProjectRepository : IProjectRepository
{
    #region Constructor
    private readonly IHttpService _http;
    private readonly string _URL = "api/project";
    private readonly HttpClient _httpClient;
    public ProjectRepository(HttpClient httpClient, IHttpService http)
    {
        _httpClient = httpClient;
        _http = http;

    }

    #endregion

    #region Methods
    public async Task<ResponseData<bool>> CreateProject(MultipartFormDataContent model)
    {
        var result = await _http.PostAsync<bool>($"{_URL}", model);
        return result;
    }

    public async Task<ResponseData<bool>> UpdateProject(MultipartFormDataContent model)
    {
        var result = await _http.PutAsync<bool>($"{_URL}", model);
        return result;
    }

    public async Task<ResponseData<bool>> DeleteProjectByIds(IEnumerable<long> ids)
    {
        var result = await _http.DeleteAsync<IEnumerable<long>, bool>($"{_URL}/deleteProject", ids);
        return result;
    }
    public async Task<ResponseData<IEnumerable<Project>>> GetProject()
    {
        var result = await _http.Get<IEnumerable<Project>>($"{_URL}");
        return result;
    }

  

    public async Task<ResponseData<Project>> GetProjectById(long Id)
    {
        var result = await _http.Get<Project>($"{_URL}/{Id}");
        return result;
    }




    #endregion
}
