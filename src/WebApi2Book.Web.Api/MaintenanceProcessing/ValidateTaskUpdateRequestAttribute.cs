// ValidateTaskUpdateRequestAttribute.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using log4net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WebApi2Book.Common.Logging;
using WebApi2Book.Web.Api.Models;
using WebApi2Book.Web.Common;

namespace WebApi2Book.Web.Api.MaintenanceProcessing
{
    /// <summary>
    ///     Validates the Task update request.
    /// </summary>
    /// <remarks>
    ///     This is a json-specific implementation. Content-neutral implementation is left as
    ///     an exercise for the reader.
    /// </remarks>
    public class ValidateTaskUpdateRequestAttribute : ActionFilterAttribute
    {
        private readonly ILog _log;

        public ValidateTaskUpdateRequestAttribute()
            : this(WebContainerManager.Get<ILogManager>())
        {
        }

        public ValidateTaskUpdateRequestAttribute(ILogManager logManager)
        {
            _log = logManager.GetLog(typeof (ValidateTaskUpdateRequestAttribute));
        }

        public override bool AllowMultiple
        {
            get { return false; }
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var taskId = (long) actionContext.ActionArguments[ActionParameterNames.TaskId];
            var taskFragment =
                (JObject) actionContext.ActionArguments[ActionParameterNames.TaskFragment];
            _log.DebugFormat("{0} = {1}", ActionParameterNames.TaskFragment, taskFragment);

            if (taskFragment == null)
            {
                const string errorMessage = "Malformed or null request.";
                _log.Debug(errorMessage);
                actionContext.Response = actionContext.Request.CreateErrorResponse(
                    HttpStatusCode.BadRequest, errorMessage);
                return;
            }

            try
            {
                var task = taskFragment.ToObject<Task>();
                if (task.TaskId.HasValue && task.TaskId != taskId)
                {
                    const string errorMessage = "Task ids do not match.";
                    _log.Debug(errorMessage);
                    actionContext.Response = actionContext.Request.CreateErrorResponse(
                        HttpStatusCode.BadRequest, errorMessage);
                }
            }
            catch (JsonException ex)
            {
                _log.Debug(ex.Message);
                actionContext.Response = actionContext.Request.CreateErrorResponse(
                    HttpStatusCode.BadRequest, ex.Message);
            }
        }

        public static class ActionParameterNames
        {
            public const string TaskFragment = "updatedTask";
            public const string TaskId = "id";
        }
    }
}