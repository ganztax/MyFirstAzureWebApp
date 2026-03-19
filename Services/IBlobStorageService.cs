namespace MyFirstAzureWebApp.Services;

public interface IBlobStorageService
{
    Task<string> UploadFileToBlobAsync(string fileName, string contentType, Stream fileStream);
    Task<bool> DeleteFileToBlobAsync(string fileName);
}
