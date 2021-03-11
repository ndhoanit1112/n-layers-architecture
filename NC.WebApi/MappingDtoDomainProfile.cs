﻿using AutoMapper;
using NC.Business.Models.User;
using NC.WebApi.DTOs.Models;
using NC.WebApi.DTOs.Results;

namespace NC.WebApi
{
    public class MappingDtoDomainProfile : Profile
    {
        public MappingDtoDomainProfile()
        {
            //Mapping DTO (model) to Domain Model
            CreateMap<LoginModelDTO, LoginModel>();

            //Mapping Domain Model to DTO (result)
            CreateMap<LoginResult, LoginResultDTO>()
                .ForMember(d => d.Status, opts => opts.MapFrom(s => (int)s.Status));
        }
    }
}