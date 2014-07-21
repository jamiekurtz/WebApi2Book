using WebApi2Book.Web.Api.Models;
namespace WebApi2Book.Web.Api.MaintenanceProcessing
{
    public interface ICompleteTaskWorkflowProcessor
    {
        Task CompleteTask(long taskId);
    }
}