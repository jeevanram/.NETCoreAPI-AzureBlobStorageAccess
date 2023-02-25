using Microsoft.Extensions.Options;
using Stowage;
using Services.ResourceManager.DTO.Configurations;

namespace Services.ResourceManager.AccessLayer.Azure
{
    public class StorageManager : IStorageManager
    {
        private readonly IFileStorage _blobStorage;
        private readonly BlobStorageOptions _blobStorageConfig;

        public StorageManager(IOptions<BlobStorageOptions> blobStorageOptions)
        {
            _blobStorageConfig = blobStorageOptions.Value;
            _blobStorage = Files.Of.AzureBlobStorage(_blobStorageConfig.Account, _blobStorageConfig.Key, _blobStorageConfig.ContainerName);
        }

        public async Task<byte[]> GetFileAsync(string filePath)
        {
            if (await _blobStorage.Exists(new IOPath(filePath)))
            {
                using MemoryStream ms = new();
                var readStream = await _blobStorage.OpenRead(new IOPath(filePath));
                await readStream.CopyToAsync(ms);
                readStream.Dispose();
                byte[] byteArray = ms.ToArray();
                return byteArray;
            }

            return Array.Empty<byte>();
        }



        public async Task UploadFileAsync(string filePath, string blobPath)
        {
            using var stream = File.OpenRead(filePath);
            var writeStream =  await _blobStorage.OpenWrite(new IOPath(blobPath));
            await stream.CopyToAsync(writeStream);
            writeStream.Dispose();
        }

        public async Task DeleteFileAsync(string filePath)
        {
            await _blobStorage.Rm(filePath);
        }
    }
}