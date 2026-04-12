using Aplication.Enums;

namespace Aplication.Interfaces
{
    public interface IFileStorageService
    {
        Task<string?> UploadFileAsync(Stream file, StorageContainers container, string fileName);
        Task DeleteFileAsync(StorageContainers container, string fileReference);
    }
}