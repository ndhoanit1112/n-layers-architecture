using AutoMapper;
using Microsoft.AspNetCore.Mvc;

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
    }
}
