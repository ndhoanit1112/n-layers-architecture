﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NC.Business.IServices;
using NC.Business.Models.User;
using NC.WebApi.Controllers.Base;
using NC.WebApi.DTOs.Models;
using NC.WebApi.DTOs.Results;

namespace NC.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class UserController : ApiBaseController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService, IMapper mapper)
            : base(mapper)
        {
            _userService = userService;
        }

        // GET: api/<UserController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1234567", "value2" };
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        [Authorize]
        public string Get(int id)
        {
            return "value";
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
    }
}
