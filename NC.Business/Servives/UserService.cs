using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NC.Business.IServices;
using NC.BusinessModel.User;
using NC.Common;
using NC.Common.Enums;
using NC.Infrastructure;
using NC.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NC.Business.Servives
{
    public class UserService : IUserService
    {
        private readonly NCContext _context;

        private readonly UserManager<ApplicationUser> _userManager;

        private readonly SignInManager<ApplicationUser> _signInManager;

        private readonly RoleManager<ApplicationRole> _roleManager;

        public UserService(NCContext context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<ApplicationRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
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

            await _userManager.AddToRoleAsync(newUser, Constants.SystemAdminRole);
            return result.Succeeded;
        }

        public async Task<LoginResult> Authenticate(LoginModel loginModel)
        {
            var user = await _userManager.FindByNameAsync(loginModel.Username);

            if(user == null)
            {
                return new LoginResult(LoginStatus.Failed, "Invalid username or password!");
            }

            var signInResult = await _signInManager.PasswordSignInAsync(loginModel.Username, loginModel.Password, false, false);

            //...other login results(locked,...)

            if (signInResult.Succeeded)
            {
                var userInfo = new UserInfo
                {
                    Id = user.Id,
                    Username = user.UserName,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Roles = (await _userManager.GetRolesAsync(user)).ToArray(),
                };

                var loginResult = new LoginResult(LoginStatus.Success)
                {
                    AccessToken = GenerateJSONWebToken(userInfo)
                };

                return loginResult;
            }

            return new LoginResult(LoginStatus.Failed, "Invalid username or password!");
        }

        private string GenerateJSONWebToken(UserInfo userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(GlobalSettings.GetSecret()));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>() {
                new Claim(ClaimTypes.NameIdentifier, userInfo.Id),
                new Claim(ClaimTypes.GivenName, userInfo.FirstName),
                new Claim(ClaimTypes.Surname, userInfo.LastName),
                new Claim(ClaimTypes.Email, userInfo.Email),
            };

            foreach(var role in userInfo.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var token = new JwtSecurityToken(GlobalSettings.GetIssuer(),
                GlobalSettings.GetAudience(),
                claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
