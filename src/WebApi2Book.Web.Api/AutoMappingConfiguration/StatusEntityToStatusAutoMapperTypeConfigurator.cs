using AutoMapper;
using WebApi2Book.Common.TypeMapping;
using WebApi2Book.Data.Entities;
namespace WebApi2Book.Web.Api.AutoMappingConfiguration
{
    public class StatusEntityToStatusAutoMapperTypeConfigurator : IAutoMapperTypeConfigurator
    {
        public void Configure()
        {
            Mapper.CreateMap<Status, Models.Status>();
        }
    }
}