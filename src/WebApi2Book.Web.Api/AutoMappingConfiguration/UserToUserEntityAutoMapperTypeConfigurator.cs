// UserToUserEntityAutoMapperTypeConfigurator.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using AutoMapper;
using WebApi2Book.Common.TypeMapping;
using WebApi2Book.Web.Api.Models;

namespace WebApi2Book.Web.Api.AutoMappingConfiguration
{
    public class UserToUserEntityAutoMapperTypeConfigurator : IAutoMapperTypeConfigurator
    {
        public void Configure()
        {
            Mapper.CreateMap<User, Data.Entities.User>()
                .ForMember(opt => opt.Version, x => x.Ignore())
                .ForMember(opt => opt.Tasks, x => x.Ignore());
        }
    }
}