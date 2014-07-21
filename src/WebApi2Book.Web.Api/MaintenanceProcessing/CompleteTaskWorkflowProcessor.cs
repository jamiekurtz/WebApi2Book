using WebApi2Book.Common;
using WebApi2Book.Common.TypeMapping;
using WebApi2Book.Data.Exceptions;
using WebApi2Book.Data.QueryProcessors;
using WebApi2Book.Web.Api.Models;

namespace WebApi2Book.Web.Api.MaintenanceProcessing
{
    public class CompleteTaskWorkflowProcessor : ICompleteTaskWorkflowProcessor
    {
        private readonly IAutoMapper _autoMapper;
        private readonly IDateTime _dateTime;
        private readonly ITaskByIdQueryProcessor _taskByIdQueryProcessor;
        private readonly IUpdateTaskStatusQueryProcessor _updateTaskStatusQueryProcessor;

        public CompleteTaskWorkflowProcessor(ITaskByIdQueryProcessor taskByIdQueryProcessor,
            IUpdateTaskStatusQueryProcessor updateTaskStatusQueryProcessor, IAutoMapper autoMapper,
            IDateTime dateTime)
        {
            _taskByIdQueryProcessor = taskByIdQueryProcessor;
            _updateTaskStatusQueryProcessor = updateTaskStatusQueryProcessor;
            _autoMapper = autoMapper;
            _dateTime = dateTime;
        }

        public Task CompleteTask(long taskId)
        {
            var taskEntity = _taskByIdQueryProcessor.GetTask(taskId);
            if (taskEntity == null)
            {
                throw new RootObjectNotFoundException("Task not found");
            }
            // Simulate some workflow logic...
            if (taskEntity.Status.Name != "In Progress")
            {
                throw new BusinessRuleViolationException(
                    "Incorrect task status. Expected status of 'In Progress'.");
            }
            taskEntity.CompletedDate = _dateTime.UtcNow;
            _updateTaskStatusQueryProcessor.UpdateTaskStatus(taskEntity, "Completed");
            var task = _autoMapper.Map<Task>(taskEntity);
            return task;
        }
    }
}