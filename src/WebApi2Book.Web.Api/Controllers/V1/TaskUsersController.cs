// TaskUsersController.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System.Collections.Generic;
using System.Web.Http;
using WebApi2Book.Common;
using WebApi2Book.Web.Api.InquiryProcessing;
using WebApi2Book.Web.Api.MaintenanceProcessing;
using WebApi2Book.Web.Api.Models;
using WebApi2Book.Web.Common;
using WebApi2Book.Web.Common.Routing;

namespace WebApi2Book.Web.Api.Controllers.V1
{
    [ApiVersion1RoutePrefix("tasks")]
    [UnitOfWorkActionFilter]
    [Authorize(Roles = Constants.RoleNames.SeniorWorker)]
    public class TaskUsersController : ApiController
    {
        private readonly ITaskUsersInquiryProcessor _taskUsersInquiryProcessor;
        private readonly ITaskUsersMaintenanceProcessor _taskUsersMaintenanceProcessor;

        public TaskUsersController(ITaskUsersInquiryProcessor taskUsersInquiryProcessor,
            ITaskUsersMaintenanceProcessor taskUsersMaintenanceProcessor)
        {
            _taskUsersInquiryProcessor = taskUsersInquiryProcessor;
            _taskUsersMaintenanceProcessor = taskUsersMaintenanceProcessor;
        }

        [Route("{taskId:long}/users", Name = "GetTaskUsersRoute")]
        public TaskUsersInquiryResponse GetTaskUsers(long taskId)
        {
            var users = _taskUsersInquiryProcessor.GetTaskUsers(taskId);
            return users;
        }

        [Route("{taskId:long}/users", Name = "ReplaceTaskUsersRoute")]
        [HttpPut]
        public Task ReplaceTaskUsers(long taskId, [FromBody] IEnumerable<long> userIds)
        {
            var task = _taskUsersMaintenanceProcessor.ReplaceTaskUsers(taskId, userIds);
            return task;
        }

        [Route("{taskId:long}/users", Name = "DeleteTaskUsersRoute")]
        [HttpDelete]
        public Task DeleteTaskUsers(long taskId)
        {
            var task = _taskUsersMaintenanceProcessor.DeleteTaskUsers(taskId);
            return task;
        }

        [Route("{taskId:long}/users/{userId:long}", Name = "AddTaskUserRoute")]
        [HttpPut]
        public Task AddTaskUser(long taskId, long userId)
        {
            var task = _taskUsersMaintenanceProcessor.AddTaskUser(taskId, userId);
            return task;
        }

        [Route("{taskId:long}/users/{userId:long}", Name = "DeleteTaskUserRoute")]
        [HttpDelete]
        public Task DeleteTaskUser(long taskId, long userId)
        {
            var task = _taskUsersMaintenanceProcessor.DeleteTaskUser(taskId, userId);
            return task;
        }
    }
}