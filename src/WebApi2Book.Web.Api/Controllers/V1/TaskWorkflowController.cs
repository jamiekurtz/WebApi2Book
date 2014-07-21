// TaskWorkflowController.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System.Web.Http;
using WebApi2Book.Common;
using WebApi2Book.Web.Api.MaintenanceProcessing;
using WebApi2Book.Web.Api.Models;
using WebApi2Book.Web.Common;
using WebApi2Book.Web.Common.Routing;
using WebApi2Book.Web.Common.Security;

namespace WebApi2Book.Web.Api.Controllers.V1
{
    [ApiVersion1RoutePrefix("")]
    [UnitOfWorkActionFilter]
    [Authorize(Roles = Constants.RoleNames.SeniorWorker)]
    public class TaskWorkflowController : ApiController
    {
        private readonly ICompleteTaskWorkflowProcessor _completeTaskWorkflowProcessor;
        private readonly IReactivateTaskWorkflowProcessor _reactivateTaskWorkflowProcessor;
        private readonly IStartTaskWorkflowProcessor _startTaskWorkflowProcessor;

        public TaskWorkflowController(IStartTaskWorkflowProcessor startTaskWorkflowProcessor,
            ICompleteTaskWorkflowProcessor completeTaskWorkflowProcessor,
            IReactivateTaskWorkflowProcessor reactivateTaskWorkflowProcessor)
        {
            _startTaskWorkflowProcessor = startTaskWorkflowProcessor;
            _completeTaskWorkflowProcessor = completeTaskWorkflowProcessor;
            _reactivateTaskWorkflowProcessor = reactivateTaskWorkflowProcessor;
        }

        [HttpPost]
        [Route("tasks/{taskId:long}/activations", Name = "StartTaskRoute")]
        public Task StartTask(long taskId)
        {
            var task = _startTaskWorkflowProcessor.StartTask(taskId);
            return task;
        }

        [HttpPost]
        [Route("tasks/{taskId:long}/completions", Name = "CompleteTaskRoute")]
        public Task CompleteTask(long taskId)
        {
            var task = _completeTaskWorkflowProcessor.CompleteTask(taskId);
            return task;
        }

        [HttpPost]
        [UserAudit]
        [Route("tasks/{taskId:long}/reactivations", Name = "ReactivateTaskRoute")]
        public Task ReactivateTask(long taskId)
        {
            var task = _reactivateTaskWorkflowProcessor.ReactivateTask(taskId);
            return task;
        }
    }
}