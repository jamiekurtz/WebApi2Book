// NewTaskV2ToTaskEntityAutoMapperTypeConfigurator.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using AutoMapper;
using WebApi2Book.Common.TypeMapping;
using WebApi2Book.Web.Api.Models;
using Task = WebApi2Book.Data.Entities.Task;

namespace WebApi2Book.Web.Api.AutoMappingConfiguration
{
    public class NewTaskV2ToTaskEntityAutoMapperTypeConfigurator : IAutoMapperTypeConfigurator
    {
        public void Configure()
        {
            Mapper.CreateMap<NewTaskV2, Task>()
                .ForMember(opt => opt.Version, x => x.Ignore())
                .ForMember(opt => opt.CreatedBy, x => x.Ignore())
                .ForMember(opt => opt.TaskId, x => x.Ignore())
                .ForMember(opt => opt.CreatedDate, x => x.Ignore())
                .ForMember(opt => opt.CompletedDate, x => x.Ignore())
                .ForMember(opt => opt.Status, x => x.Ignore())
                .ForMember(opt => opt.Users,
                    x => x.ResolveUsing(newTask => newTask.Assignee == null ? null : new[] {newTask.Assignee}));
        }
    }
}