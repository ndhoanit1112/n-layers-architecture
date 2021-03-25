using NC.Business.IServices.Base;
using System.Threading.Tasks;

namespace NC.Business.IServices
{
    public interface IEmailService : IBaseService
    {
        Task SendEmailAsync(string toEmail, string subject, string message, string[] bcc = null);
    }
}
