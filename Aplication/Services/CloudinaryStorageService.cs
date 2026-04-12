using Aplication.Enums;
using Aplication.Interfaces;
using Aplication.Options;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;

namespace Aplication.Services
{
    public class CloudinaryStorageService : IFileStorageService
    {
        private readonly Cloudinary _cloudinary;

        public CloudinaryStorageService(IOptions<CloudinaryStorageOptions> options)
        {
            var cloudinaryUrl = options.Value?.Url;

            if (string.IsNullOrWhiteSpace(cloudinaryUrl))
            {
                throw new InvalidOperationException("Cloudinary URL is not configured.");
            }

            var uri = new Uri(cloudinaryUrl);
            var userInfo = uri.UserInfo.Split(':', 2);

            if (userInfo.Length != 2)
            {
                throw new InvalidOperationException("Cloudinary URL is not valid.");
            }

            var account = new Account(uri.Host, userInfo[0], userInfo[1]);
            _cloudinary = new Cloudinary(account);
            _cloudinary.Api.Secure = true;
        }

        public async Task<string?> UploadFileAsync(Stream file, StorageContainers container, string fileName)
        {
            if (file == null || file.Length == 0)
            {
                return null;
            }

            try
            {
                var containerName = Enum.GetName(typeof(StorageContainers), container)?.ToLowerInvariant();
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(fileName, file),
                    Folder = containerName,
                    PublicId = fileName,
                    UseFilename = false,
                    UniqueFilename = false
                };

                var uploadResult = await _cloudinary.UploadAsync(uploadParams);

                if (uploadResult.Error != null)
                {
                    throw new Exception(uploadResult.Error.Message);
                }

                return uploadResult.SecureUrl?.ToString() ?? uploadResult.Url?.ToString() ?? uploadResult.PublicId;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al subir el archivo a Cloudinary", ex);
            }
        }

        public async Task DeleteFileAsync(StorageContainers container, string fileReference)
        {
            if (string.IsNullOrWhiteSpace(fileReference))
            {
                return;
            }

            try
            {
                var publicId = ExtractPublicId(fileReference);
                var deleteParams = new DeletionParams(publicId)
                {
                    ResourceType = ResourceType.Image
                };

                var deletionResult = await _cloudinary.DestroyAsync(deleteParams);

                if (deletionResult.Error != null)
                {
                    throw new Exception(deletionResult.Error.Message);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar el archivo en Cloudinary", ex);
            }
        }

        private static string ExtractPublicId(string fileReference)
        {
            if (!Uri.TryCreate(fileReference, UriKind.Absolute, out var uri))
            {
                return fileReference;
            }

            var path = uri.AbsolutePath;
            var uploadIndex = path.IndexOf("/upload/", StringComparison.OrdinalIgnoreCase);

            if (uploadIndex < 0)
            {
                return fileReference;
            }

            var uploadedPath = path[(uploadIndex + "/upload/".Length)..];
            var pathSegments = uploadedPath.Split('/', StringSplitOptions.RemoveEmptyEntries);

            if (pathSegments.Length == 0)
            {
                return fileReference;
            }

            var publicIdSegments = pathSegments
                .SkipWhile(segment => segment.Length > 1 && segment.StartsWith('v') && long.TryParse(segment[1..], out _))
                .ToArray();

            if (publicIdSegments.Length == 0)
            {
                return fileReference;
            }

            var lastSegment = publicIdSegments[^1];
            var lastDotIndex = lastSegment.LastIndexOf('.');

            if (lastDotIndex > 0)
            {
                publicIdSegments[^1] = lastSegment[..lastDotIndex];
            }

            return string.Join("/", publicIdSegments);
        }
    }
}