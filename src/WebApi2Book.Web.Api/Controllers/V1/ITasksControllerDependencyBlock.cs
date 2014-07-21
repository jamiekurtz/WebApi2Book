using WebApi2Book.Web.Api.InquiryProcessing;
using WebApi2Book.Web.Api.MaintenanceProcessing;
namespace WebApi2Book.Web.Api.Controllers.V1
{
    public interface ITasksControllerDependencyBlock
    {
        IAddTaskMaintenanceProcessor AddTaskMaintenanceProcessor { get; }
        ITaskByIdInquiryProcessor TaskByIdInquiryProcessor { get; }
        IUpdateTaskMaintenanceProcessor UpdateTaskMaintenanceProcessor { get; }
        IPagedDataRequestFactory PagedDataRequestFactory { get; }
        IAllTasksInquiryProcessor AllTasksInquiryProcessor { get; }
    }
}