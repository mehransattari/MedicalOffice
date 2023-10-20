using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;
namespace MedicalOffice.Client.Upload;

public class FileUploadBase : ComponentBase
{
  
    [Parameter]
    public Action<IList<IBrowserFile>> OnValueChanged { get; set; }


    public IList<IBrowserFile> files = new List<IBrowserFile>();
    public void UploadFiles(IBrowserFile file)
    {
        files.Add(file);
        OnValueChanged?.Invoke(files);
    }
}
