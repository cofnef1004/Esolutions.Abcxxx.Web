﻿using AutoMapper;
using ES.QLBongDa.Authorization.Users;
using ES.QLBongDa.Dto;

namespace ES.QLBongDa.Startup
{
    public static class CustomDtoMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<User, UserDto>()
                .ForMember(dto => dto.Roles, options => options.Ignore())
                .ForMember(dto => dto.OrganizationUnits, options => options.Ignore());
        }
    }
}