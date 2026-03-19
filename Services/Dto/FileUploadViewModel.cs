namespace MyFirstAzureWebApp.Services.Dto;

public class FileUploadViewModel
{
    public string FileName { get; set; } = string.Empty;
    public string ContentType { get; set; } = string.Empty;
    public string FileStorageUrl { get; set; } = string.Empty;
    public Stream FileStream { get; set; } = Stream.Null;
}