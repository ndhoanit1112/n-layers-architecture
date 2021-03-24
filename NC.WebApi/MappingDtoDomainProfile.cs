using AutoMapper;
using NC.Business.Models.Account;
using NC.WebApi.DTOs.Models.Account;
using NC.WebApi.DTOs.Results.Account;

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
