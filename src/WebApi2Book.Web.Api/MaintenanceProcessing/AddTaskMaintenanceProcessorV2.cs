// AddTaskMaintenanceProcessorV2.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using WebApi2Book.Common.TypeMapping;
using WebApi2Book.Data.QueryProcessors;
using WebApi2Book.Web.Api.LinkServices;
using WebApi2Book.Web.Api.Models;

namespace WebApi2Book.Web.Api.MaintenanceProcessing
{
    public class AddTaskMaintenanceProcessorV2 : IAddTaskMaintenanceProcessorV2
    {
        private readonly IAutoMapper _autoMapper;
        private readonly IAddTaskQueryProcessor _queryProcessor;
        private readonly ITaskLinkService _taskLinkService;

        public AddTaskMaintenanceProcessorV2(IAddTaskQueryProcessor queryProcessor, IAutoMapper autoMapper,
            ITaskLinkService taskLinkService)
        {
            _queryProcessor = queryProcessor;
            _autoMapper = autoMapper;
            _taskLinkService = taskLinkService;
        }

        public Task AddTask(NewTaskV2 newTask)
        {
            var taskEntity = _autoMapper.Map<Data.Entities.Task>(newTask);

            _queryProcessor.AddTask(taskEntity);

            var task = _autoMapper.Map<Task>(taskEntity);

            _taskLinkService.AddLinks(task);

            return task;
        }
    }
}