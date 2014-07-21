using System.Collections.Generic;
using WebApi2Book.Common.TypeMapping;
using WebApi2Book.Data.QueryProcessors;
using WebApi2Book.Web.Api.Models;

namespace WebApi2Book.Web.Api.MaintenanceProcessing
{
    public class TaskUsersMaintenanceProcessor : ITaskUsersMaintenanceProcessor
    {
        private readonly IAutoMapper _autoMapper;
        private readonly IUpdateTaskQueryProcessor _queryProcessor;

        public TaskUsersMaintenanceProcessor(IUpdateTaskQueryProcessor queryProcessor,
            IAutoMapper autoMapper)
        {
            _queryProcessor = queryProcessor;
            _autoMapper = autoMapper;
        }

        public Task ReplaceTaskUsers(long taskId, IEnumerable<long> userIds)
        {
            var taskEntity = _queryProcessor.ReplaceTaskUsers(taskId, userIds);
            return CreateTaskResponse(taskEntity);
        }

        public Task DeleteTaskUsers(long taskId)
        {
            var taskEntity = _queryProcessor.DeleteTaskUsers(taskId);
            return CreateTaskResponse(taskEntity);
        }

        public Task AddTaskUser(long taskId, long userId)
        {
            var taskEntity = _queryProcessor.AddTaskUser(taskId, userId);
            return CreateTaskResponse(taskEntity);
        }

        public Task DeleteTaskUser(long taskId, long userId)
        {
            var taskEntity = _queryProcessor.DeleteTaskUser(taskId, userId);
            return CreateTaskResponse(taskEntity);
        }

        public virtual Task CreateTaskResponse(Data.Entities.Task taskEntity)
        {
            var task = _autoMapper.Map<Task>(taskEntity);
            return task;
        }
    }
}