using Microsoft.AspNetCore.Identity;
using NC.Business.IServices;
using NC.Infrastructure;
using NC.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NC.Business.Servives
{
    public class UserService : IUserService
    {
        private readonly NCContext _context;

        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(NCContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<bool> CreateUser(string username, string email, string password)
        {
            var newUser = new ApplicationUser
            {
                Email = email,
                UserName = username,
                FirstName = "Hoan",
                LastName = "Nguyễn Đình",
            };

            var result = await _userManager.CreateAsync(newUser, password);
            return result.Succeeded;
        }
    }
}
