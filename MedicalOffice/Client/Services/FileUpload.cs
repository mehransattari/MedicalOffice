using Microsoft.AspNetCore.Components.Forms;
using System.Net.Http.Headers;

namespace MedicalOffice.Client.Services;
public interface IFileUpload
{
    Task<StreamContent> AddImage(IBrowserFile file);
}
public class FileUpload : IFileUpload
{
    public async Task<StreamContent> AddImage(IBrowserFile file)
    {
        var fileContent =
          new StreamContent(file.OpenReadStream());
        fileContent.Headers.ContentType =
                new MediaTypeHeaderValue(file.ContentType);
        return fileContent;
    }


}