using NC.Business.IServices.Base;
using NC.Business.Models.User;
using System.Threading.Tasks;

namespace NC.Business.IServices
{
    public interface IUserService : IBaseService
    {
        Task<bool> CreateUser(string username, string email, string password);
        Task<LoginResult> Authenticate(LoginModel loginModel);
        bool CheckUsernameExisted(string username);
        Task<string> RefreshToken(RefreshTokenModel model);
    }
}
