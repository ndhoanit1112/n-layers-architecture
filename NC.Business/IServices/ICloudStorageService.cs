using Microsoft.AspNetCore.Http;
using NC.Business.IServices.Base;
using System.Threading.Tasks;

namespace NC.Business.IServices
{
    public interface ICloudStorageService : IBaseService
    {
        Task<string> UploadFileAsync(IFormFile file, string fileNameForStorage);

        Task DeleteFileAsync(string fileNameForStorage);
    }
}
