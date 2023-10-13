

namespace MedicalOffice.Server.Helpers;

public class FileHelper
{
    private readonly IHttpContextAccessor _httpContext;
    private readonly IWebHostEnvironment _env;

    public FileHelper(IHttpContextAccessor httpContext , IWebHostEnvironment env)
    {
        _httpContext = httpContext;
        _env = env;
    }

    public async Task<string> SaveFile(byte[] content , string extension , string folderName)
    {
        // huny1234trvtc987.jpg
        var fileName = $"{Guid.NewGuid()}.{ extension }";

        //http://google.com/Images => folderName
        string folder = Path.Combine(_env.WebRootPath, folderName);

        //http://google.com/Images/huny1234trvtc987.jpg
        string savePathAddress = Path.Combine(folder , fileName);

        await System.IO.File.WriteAllBytesAsync(savePathAddress, content);

        var currentUrl = $"{_httpContext.HttpContext.Request.Scheme}://{_httpContext.HttpContext.Request.Host}/{folderName}/{fileName}";

        return currentUrl;
    }

    public async Task<bool> DeleteFile(string fileName , string folderName)
    {
        var myFile = Path.GetFileName(fileName);
        string fileDirectory = Path.Combine(_env.WebRootPath, folderName, myFile);
        if (File.Exists(fileDirectory))
        {
            File.Delete(fileDirectory);
        }

        return await Task.FromResult(true);
    }

    public async Task<string> EditFile(byte[] content , string extension, string folderName ,  string oldFileName)
    {
        if (!string.IsNullOrEmpty(oldFileName))
        {
            await DeleteFile(oldFileName , folderName);
        }

        return await SaveFile(content , extension , folderName);
    }
}
