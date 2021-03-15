using NC.Business.IServices.Base;
using NC.Business.Models.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NC.Business.IServices
{
    public interface IUserService : IBaseService
    {
        Task<bool> CreateUser(string username, string email, string password);

        Task<LoginResult> Authenticate(LoginModel loginModel);
    }
}
