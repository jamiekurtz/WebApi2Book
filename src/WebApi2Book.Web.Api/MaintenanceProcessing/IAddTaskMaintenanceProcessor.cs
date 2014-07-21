using WebApi2Book.Web.Api.Models;

namespace WebApi2Book.Web.Api.MaintenanceProcessing
{
    public interface IAddTaskMaintenanceProcessor
    {
        Task AddTask(NewTask newTask);
    }
}