using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NC.Business.IServices;
using NC.Business.Models.User;
using NC.Common.CustomExceptions;
using NC.WebApi.Controllers.Base;
using NC.WebApi.DTOs.Models.User;
using NC.WebApi.DTOs.Results.User;

namespace NC.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class UserController : ApiBaseController
    {
        private readonly IUserService _userService;

        private readonly ICloudStorageService _storageService;

        public UserController(IUserService userService, ICloudStorageService storageService, IMapper mapper)
            : base(mapper)
        {
            _userService = userService;
            _storageService = storageService;
        }

        // GET: api/<UserController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value12345678", "value2" };
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        //[Authorize]
        public string Get(int id)
        {
            if (id < 2)
            {
                throw new BusinessException("Two more than one!");
            }

            return "value";
        }

        // GET api/<UserController>/5
        [HttpGet]
        [Route("checkusername")]
        public IActionResult CheckUserName(string username)
        {
            var result = _userService.CheckUsernameExisted(username);

            return Ok(result);
        }

        // POST api/<UserController>
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] string username)
        {
            var result = await _userService.CreateUser(username, $"{username}@gmail.com", "Hello123#");

            return Ok(result);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromForm] LoginModelDTO model)
        {
            var result = await _userService.Authenticate(_mapper.Map<LoginModel>(model));

            return Ok(_mapper.Map<LoginResultDTO>(result));
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] string value)
        {
            return Ok(new { id, value });
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok(id);
        }

        [HttpPost]
        [Route("uploadfile")]
        [Authorize]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            var result = await _storageService.UploadFileAsync(file, file.FileName);

            return Ok(result);
        }

        [HttpPost]
        [Route("deletefile")]
        [Authorize]
        public async Task<IActionResult> DeleteFile([FromForm] string fileName)
        {
            await _storageService.DeleteFileAsync(fileName);

            return Ok(true);
        }
    }
}
