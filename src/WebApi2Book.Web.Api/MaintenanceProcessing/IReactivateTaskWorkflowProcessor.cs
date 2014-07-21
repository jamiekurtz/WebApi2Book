using WebApi2Book.Web.Api.Models;
namespace WebApi2Book.Web.Api.MaintenanceProcessing
{
    public interface IReactivateTaskWorkflowProcessor
    {
        Task ReactivateTask(long taskId);
    }
}