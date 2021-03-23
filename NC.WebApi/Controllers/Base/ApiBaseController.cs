using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NC.Common.Enums;
using NC.WebApi.DTOs.Results;
using System.Net;

namespace NC.WebApi.Controllers.Base
{
    [ApiController]
    public class ApiBaseController : ControllerBase
    {
        protected IMapper _mapper { get; set; }

        public ApiBaseController(IMapper mapper)
        {
            _mapper = mapper;
        }

        protected static ApiResponse Success()
        {
            return new ApiResponse((int)ResponseCode.Success, null);
        }

        protected static ApiResponse<T> Success<T>(T result)
        {
            return new ApiResponse<T>((int)ResponseCode.Success, null, result);
        }

        protected static ApiResponse Failed()
        {
            return new ApiResponse((int)ResponseCode.Failed, null);
        }

        protected static ApiResponse Failed(int errorCode, string message)
        {
            return new ApiResponse(errorCode, message);
        }

        protected static ApiResponse<T> Failed<T>(T result)
        {
            return new ApiResponse<T>((int)ResponseCode.Failed, null, result);
        }

        protected static ApiResponse<T> Failed<T>(int errorCode, string message, T result)
        {
            return new ApiResponse<T>(errorCode, message, result);
        }

        protected ObjectResult Forbid(object obj)
        {
            return StatusCode((int)HttpStatusCode.Forbidden, obj);
        }
    }
}
