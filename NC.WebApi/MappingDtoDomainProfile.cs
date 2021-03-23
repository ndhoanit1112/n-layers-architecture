using AutoMapper;
using NC.Business.Models.User;
using NC.WebApi.DTOs.Models.User;
using NC.WebApi.DTOs.Results.User;

namespace NC.WebApi
{
    public class MappingDtoDomainProfile : Profile
    {
        public MappingDtoDomainProfile()
        {
            //Mapping DTO (model) to Domain Model
            CreateMap<LoginModelDTO, LoginModel>();
            CreateMap<RefreshTokenModelDTO, RefreshTokenModel>();

            //Mapping Domain Model to DTO (result)
            CreateMap<LoginResult, LoginResultDTO>();
        }
    }
}
