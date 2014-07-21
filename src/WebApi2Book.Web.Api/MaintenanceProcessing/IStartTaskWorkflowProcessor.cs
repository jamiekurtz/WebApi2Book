using WebApi2Book.Web.Api.Models;
namespace WebApi2Book.Web.Api.MaintenanceProcessing
{
    public interface IStartTaskWorkflowProcessor
    {
        Task StartTask(long taskId);
    }
}