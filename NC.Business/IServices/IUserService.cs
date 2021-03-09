using NC.BusinessModel.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NC.Business.IServices
{
    public interface IUserService
    {
        Task<bool> CreateUser(string username, string email, string password);

        Task<LoginResult> Authenticate(LoginModel loginModel);
    }
}
