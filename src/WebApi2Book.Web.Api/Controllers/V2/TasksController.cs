// TasksController.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System.Net.Http;
using System.Web.Http;
using WebApi2Book.Web.Api.MaintenanceProcessing;
using WebApi2Book.Web.Api.Models;
using WebApi2Book.Web.Common;
using WebApi2Book.Web.Common.Validation;

namespace WebApi2Book.Web.Api.Controllers.V2
{
    [RoutePrefix("api/{apiVersion:apiVersionConstraint(v2)}/tasks")]
    [UnitOfWorkActionFilter]
    public class TasksController : ApiController
    {
        private readonly IAddTaskMaintenanceProcessorV2 _addTaskMaintenanceProcessor;

        public TasksController(IAddTaskMaintenanceProcessorV2 addTaskMaintenanceProcessor)
        {
            _addTaskMaintenanceProcessor = addTaskMaintenanceProcessor;
        }

        [Route("", Name = "AddTaskRouteV2")]
        [HttpPost]
        [ValidateModel]
        public IHttpActionResult AddTask(HttpRequestMessage requestMessage, NewTaskV2 newTask)
        {
            var task = _addTaskMaintenanceProcessor.AddTask(newTask);

            var result = new TaskCreatedActionResult(requestMessage, task);

            return result;
        }
    }
}