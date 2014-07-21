// StartTaskWorkflowProcessor.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using WebApi2Book.Common;
using WebApi2Book.Common.TypeMapping;
using WebApi2Book.Data.Exceptions;
using WebApi2Book.Data.QueryProcessors;
using WebApi2Book.Web.Api.LinkServices;
using WebApi2Book.Web.Api.Models;

namespace WebApi2Book.Web.Api.MaintenanceProcessing
{
    public class StartTaskWorkflowProcessor : IStartTaskWorkflowProcessor
    {
        private readonly IAutoMapper _autoMapper;
        private readonly IDateTime _dateTime;
        private readonly ITaskByIdQueryProcessor _taskByIdQueryProcessor;
        private readonly ITaskLinkService _taskLinkService;
        private readonly IUpdateTaskStatusQueryProcessor _updateTaskStatusQueryProcessor;

        public StartTaskWorkflowProcessor(ITaskByIdQueryProcessor taskByIdQueryProcessor,
            IUpdateTaskStatusQueryProcessor updateTaskStatusQueryProcessor, IAutoMapper autoMapper,
            ITaskLinkService taskLinkService, IDateTime dateTime)
        {
            _taskByIdQueryProcessor = taskByIdQueryProcessor;
            _updateTaskStatusQueryProcessor = updateTaskStatusQueryProcessor;
            _autoMapper = autoMapper;
            _taskLinkService = taskLinkService;
            _dateTime = dateTime;
        }

        public Task StartTask(long taskId)
        {
            var taskEntity = _taskByIdQueryProcessor.GetTask(taskId);
            if (taskEntity == null)
            {
                throw new RootObjectNotFoundException("Task not found");
            }

            // Simulate some workflow logic...
            if (taskEntity.Status.Name != "Not Started")
            {
                throw new BusinessRuleViolationException("Incorrect task status. Expected status of 'Not Started'.");
            }

            taskEntity.StartDate = _dateTime.UtcNow;
            _updateTaskStatusQueryProcessor.UpdateTaskStatus(taskEntity, "In Progress");

            var task = _autoMapper.Map<Task>(taskEntity);

            _taskLinkService.AddLinks(task);

            return task;
        }
    }
}