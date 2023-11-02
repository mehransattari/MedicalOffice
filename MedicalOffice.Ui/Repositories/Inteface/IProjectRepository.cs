using MedicalOffice.Shared.Entities;
using MedicalOffice.Shared.Helper;

namespace MedicalOffice.Ui.Repositories.Inteface;

public interface IProjectRepository
{
 
    Task<ResponseData<IEnumerable<Project>>> GetProject();
    Task<ResponseData<Project>> GetProjectById(long Id);
 
}
