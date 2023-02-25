namespace Services.ResourceManager.AccessLayer
{
    public interface IStorageManager
    {
        Task<byte[]> GetFileAsync(string blobPath);
        Task UploadFileAsync(string filePath, string blobPath);
        public Task DeleteFileAsync(string blobPath);
    }
}