using MedicalOffice.Shared.Entities;
using MedicalOffice.Shared.Helper;
using MedicalOffice.Ui.Repositories.Inteface;
using MedicalOffice.Ui.Services.Interface;

namespace MedicalOffice.Ui.Repositories;

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
