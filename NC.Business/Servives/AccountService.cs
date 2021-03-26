using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using NC.Business.IServices;
using NC.Business.Models.Account;
using NC.Business.Servives.Base;
using NC.Common;
using NC.Common.Enums;
using NC.Common.Helpers;
using NC.Infrastructure;
using NC.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace NC.Business.Servives
{
    public class AccountService : BaseService, IAccountService
    {
        private readonly NCContext _context;

        private readonly UserManager<ApplicationUser> _userManager;

        private readonly SignInManager<ApplicationUser> _signInManager;

        private readonly IEmailService _emailService;

        public AccountService(NCContext context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IEmailService emailService)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
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

            if (result.Succeeded)
            {
                await _emailService.SendEmailAsync(email, "User created successfully", "Thank you for using our service!");
            }

            return result.Succeeded;
        }

        public async Task<LoginResult> Authenticate(LoginModel loginModel)
        {
            var user = await _userManager.FindByNameAsync(loginModel.Username);

            if (user == null)
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

                var refreshToken = GenerateRefreshToken();
                var now = DateTime.Now;

                var newRefreshTokenDb = new RefreshToken
                {
                    TokenHashed = HashHelper.HashStringMD5(refreshToken),
                    Expires = now.AddDays(7),
                    UserAgent = loginModel.UserAgent,
                    InsertDate = now,
                    InsertUserId = user.Id,
                };

                _context.RefreshTokens.Add(newRefreshTokenDb);
                _context.SaveChanges();

                var loginResult = new LoginResult(LoginStatus.Success)
                {
                    AccessToken = GenerateAccessToken(userInfo),
                    RefreshToken = refreshToken,
                };

                return loginResult;
            }

            return new LoginResult(LoginStatus.Failed, "Invalid username or password!");
        }

        public async Task<string> RefreshToken(RefreshTokenModel model)
        {
            var hashedToken = HashHelper.HashStringMD5(model.RefreshToken);
            var refreshToken = _context.RefreshTokens.SingleOrDefault(t => t.InsertUserId == model.UserId && t.TokenHashed == hashedToken && t.UserAgent == model.UserAgent);
            if (refreshToken == null)
                return null;

            if (DateTime.Now > refreshToken.Expires)
            {
                _context.RefreshTokens.Remove(refreshToken);
                _context.SaveChanges();

                return null;
            }

            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null)
                return null;

            var userInfo = new UserInfo
            {
                Id = user.Id,
                Username = user.UserName,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Roles = (await _userManager.GetRolesAsync(user)).ToArray(),
            };

            return GenerateAccessToken(userInfo);
        }

        private string GenerateAccessToken(UserInfo userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(GlobalSettings.GetSecret()));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>() {
                new Claim(JwtRegisteredClaimNames.NameId, userInfo.Id),
                new Claim(Constants.ClaimTypeFullName, $"{userInfo.FirstName} {userInfo.LastName}"),
            };

            foreach (var role in userInfo.Roles)
            {
                claims.Add(new Claim(Constants.ClaimTypeRole, role));
            }

            var token = new JwtSecurityToken(GlobalSettings.GetIssuer(),
                GlobalSettings.GetAudience(),
                claims,
                expires: DateTime.Now.AddHours(12),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string GenerateRefreshToken()
        {
            using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            var randomBytes = new byte[64];
            rngCryptoServiceProvider.GetBytes(randomBytes);

            return Convert.ToBase64String(randomBytes);
        }

        public bool CheckUsernameExisted(string username)
        {
            return _context.Users.Any(u => u.UserName == username);
        }
    }
}
