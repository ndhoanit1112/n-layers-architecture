using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
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

        private readonly IConfiguration _config;

        public UserService(NCContext context, UserManager<ApplicationUser> userManager, IConfiguration config)
        {
            _context = context;
            _userManager = userManager;
            _config = config;
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

        //private string GenerateJSONWebToken(UserModel userInfo)
        //{
        //    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        //    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        //    var claims = new[] {
        //        new Claim(JwtRegisteredClaimNames.Sub, userInfo.Username),
        //        new Claim(JwtRegisteredClaimNames.Email, userInfo.EmailAddress),
        //        new Claim("DateOfJoing", userInfo.DateOfJoing.ToString("yyyy-MM-dd")),
        //        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        //    };

        //    var token = new JwtSecurityToken(_config["Jwt:Issuer"],
        //        _config["Jwt:Issuer"],
        //        claims,
        //        expires: DateTime.Now.AddMinutes(120),
        //        signingCredentials: credentials);

        //    return new JwtSecurityTokenHandler().WriteToken(token);
        //}
    }
}
