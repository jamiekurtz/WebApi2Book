using WebApi2Book.Web.Api.Models;
namespace WebApi2Book.Web.Api.MaintenanceProcessing
{
    public interface IUpdateTaskMaintenanceProcessor
    {
        Task UpdateTask(long taskId, object taskFragment);
    }
}