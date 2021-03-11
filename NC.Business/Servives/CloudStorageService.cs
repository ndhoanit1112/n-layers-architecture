using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore.Http;
using NC.Business.IServices;
using NC.Common;
using System.IO;
using System.Threading.Tasks;

namespace NC.Business.Servives
{
    public class CloudStorageService : ICloudStorageService
    {
        private readonly GoogleCredential _googleCredential;

        private readonly StorageClient _storageClient;

        private readonly string _bucketName;

        public CloudStorageService()
        {
            _googleCredential = GoogleCredential.FromFile(GlobalSettings.GetGoogleCredentialFile());
            _storageClient = StorageClient.Create(_googleCredential);
            _bucketName = GlobalSettings.GetGoogleCloudStorageBucket();
        }

        public async Task<string> UploadFileAsync(IFormFile file, string fileNameForStorage)
        {
            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            var dataObject = await _storageClient.UploadObjectAsync(_bucketName, fileNameForStorage, null, memoryStream);

            return dataObject.MediaLink;
        }

        public async Task DeleteFileAsync(string fileNameForStorage)
        {
            await _storageClient.DeleteObjectAsync(_bucketName, fileNameForStorage);
        }
    }
}
