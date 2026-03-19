using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Logging;

namespace MyFirstAzureWebApp.Services;

public class BlobStorageService : IBlobStorageService
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<BlobStorageService> _logger;
    string blobStorageConnectionString = string.Empty;
    private const string blobContainerName = "root";
    public BlobStorageService(IConfiguration configuration, ILogger<BlobStorageService> logger)
    {
        _configuration = configuration;
        _logger = logger;
        blobStorageConnectionString = _configuration.GetConnectionString("AzureStorageAccount")!;
    }
    public async Task<string> UploadFileToBlobAsync(string fileName, string contentType, Stream fileStream)
    {
        try
        {
            var container = new BlobContainerClient(blobStorageConnectionString, blobContainerName);
            var createResponse = container.CreateIfNotExistsAsync();
            if (createResponse.Result != null && createResponse.Result.GetRawResponse().Status == 201)
                await container.SetAccessPolicyAsync(PublicAccessType.Blob);
            var blobClient = container.GetBlobClient(fileName);
            await blobClient.DeleteIfExistsAsync(DeleteSnapshotsOption.IncludeSnapshots);
            await blobClient.UploadAsync(fileStream, new BlobHttpHeaders { ContentType = contentType });
            return fileName;
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex.ToString());
            throw;
        }
    }
    public async Task<bool> DeleteFileToBlobAsync(string fileName)
    {
        try
        {
            var container = new BlobContainerClient(blobStorageConnectionString, blobContainerName);
            var response = await container.CreateIfNotExistsAsync();
            if (response != null && response.GetRawResponse().Status == 201)
                await container.SetAccessPolicyAsync(PublicAccessType.Blob);
            var blobClient = container.GetBlobClient(fileName);
            await blobClient.DeleteIfExistsAsync(DeleteSnapshotsOption.IncludeSnapshots);
            
            return true;
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex.ToString());
            throw;
        }
    }

}
