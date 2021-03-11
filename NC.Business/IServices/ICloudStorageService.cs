using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace NC.Business.IServices
{
    public interface ICloudStorageService
    {
        Task<string> UploadFileAsync(IFormFile file, string fileNameForStorage);

        Task DeleteFileAsync(string fileNameForStorage);
    }
}
