// IAddTaskMaintenanceProcessorV2.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using WebApi2Book.Web.Api.Models;

namespace WebApi2Book.Web.Api.MaintenanceProcessing
{
    /// <summary>
    ///     Processor strategy used to create a new task.
    /// </summary>
    /// <remarks>
    ///     Normally this would be separated from the V1 processors... in a separate
    ///     assembly, or at least in a separate namespece. We're focusing on
    ///     Web API, so to keep this contrived example simple we'll just use
    ///     a class name that indicates the intent.
    /// </remarks>
    public interface IAddTaskMaintenanceProcessorV2
    {
        Task AddTask(NewTaskV2 newTask);
    }
}