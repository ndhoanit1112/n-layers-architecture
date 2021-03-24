using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using NC.Business.IServices;
using NC.Business.Models.Account;
using NC.Common;
using NC.Common.Enums;
using NC.WebApi.Controllers.Base;
using NC.WebApi.DTOs.Models.Account;
using NC.WebApi.DTOs.Results.Account;
using System.Threading.Tasks;

namespace NC.WebApi.Controllers.Admin
{
    [Authorize(Roles = Constants.SystemAdminRole)]
    [Route("api/admin/[controller]")]
    public class AccountController : ApiBaseController
    {
        private readonly IAccountService _accountService;

        private readonly ICloudStorageService _storageService;

        public AccountController(IAccountService accountService, ICloudStorageService storageService, IMapper mapper)
            : base(mapper)
        {
            _accountService = accountService;
            _storageService = storageService;
        }

        // GET api/<UserController>/5
        [HttpGet]
        [Route("checkusername")]
        public IActionResult CheckUserName(string username)
        {
            var result = _accountService.CheckUsernameExisted(username);

            return Ok(Success(result));
        }

        // POST api/<UserController>
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] string username)
        {
            var result = await _accountService.CreateUser(username, $"{username}@gmail.com", "Hello123#");

            return Ok(Success(result));
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        public async Task<IActionResult> Login([FromForm] LoginModelDTO model)
        {
            var loginModel = _mapper.Map<LoginModel>(model);
            loginModel.UserAgent = Request.Headers[HeaderNames.UserAgent].ToString();

            var result = await _accountService.Authenticate(loginModel);

            if (result.Status == LoginStatus.Failed)
            {
                return Unauthorized(Failed((int)ResponseCode.Failed, result.Message));
            }

            if (result.Status == LoginStatus.UserLocked || result.Status == LoginStatus.UserExpired)
            {
                return Forbid(Failed((int)ResponseCode.Failed, result.Message));
            }

            return Ok(Success(_mapper.Map<LoginResultDTO>(result)));
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("refreshtoken")]
        public async Task<IActionResult> RefreshToken([FromForm] RefreshTokenModelDTO model)
        {
            var refreshTokenModel = _mapper.Map<RefreshTokenModel>(model);
            refreshTokenModel.UserAgent = Request.Headers[HeaderNames.UserAgent].ToString();

            var result = await _accountService.RefreshToken(refreshTokenModel);

            if (result == null)
            {
                return Unauthorized(Failed());
            }

            return Ok(Success(new RefreshTokenResultDTO(result)));
        }

        [HttpPost]
        [Route("uploadfile")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            var result = await _storageService.UploadFileAsync(file, file.FileName);

            return Ok(Success(result));
        }

        [HttpPost]
        [Route("deletefile")]
        public async Task<IActionResult> DeleteFile([FromForm] string fileName)
        {
            await _storageService.DeleteFileAsync(fileName);

            return Ok(Success(true));
        }
    }
}
