using Aplication.Enums;
using Azure.Storage.Blobs;

namespace Aplication.Interfaces
{
    public interface IBlobServices
    {
        BlobContainerClient GetContainer(string containerName, string key);
        Task<string> UploadBlobAsync(Stream file, AzureBlobTypes BlobTypes, string key);
    }
}
