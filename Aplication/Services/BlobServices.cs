using Aplication.Enums;
using Aplication.Interfaces;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace Aplication.Services
{
    public class BlobServices : IBlobServices
    {
        public BlobContainerClient GetContainer(string containerName, string key)
        {
            BlobContainerClient container = new BlobContainerClient(key, containerName);
            return container;
        }

        public async Task<string> UploadBlobAsync(Stream file, AzureBlobTypes BlobTypes, string key)
        {
            if (file.Length == 0) return null;
            try
            {
                string containerName = Enum.GetName(typeof(AzureBlobTypes), BlobTypes).ToLower();
                BlobContainerClient container = GetContainer(containerName, key);

                string FileName = Guid.NewGuid().ToString();
                BlobClient newBlobClient = container.GetBlobClient(FileName);

                await newBlobClient.UploadAsync(file);
                return FileName;

            }catch(Exception ex)
            {
                throw new Exception("Error al subir el blob", ex);
            }
        }
    }
}
