using MedicalOffice.Shared.Entities;
using MedicalOffice.Shared.Helper;

namespace MedicalOffice.Client.Repositories.Inteface;

public interface IProjectRepository
{
    Task<ResponseData<bool>> CreateProject(MultipartFormDataContent model);
    Task<ResponseData<bool>> DeleteProjectByIds(IEnumerable<long> ids);
    Task<ResponseData<IEnumerable<Project>>> GetProject();
    Task<ResponseData<Project>> GetProjectById(long Id);
    Task<ResponseData<bool>> UpdateProject(MultipartFormDataContent model);
}
