using WebApi2Book.Web.Api.InquiryProcessing;
using WebApi2Book.Web.Api.MaintenanceProcessing;

namespace WebApi2Book.Web.Api.Controllers.V1
{
    public class TasksControllerDependencyBlock : ITasksControllerDependencyBlock
    {
        public TasksControllerDependencyBlock(
            IAddTaskMaintenanceProcessor addTaskMaintenanceProcessor,
            ITaskByIdInquiryProcessor taskByIdInquiryProcessor,
            IUpdateTaskMaintenanceProcessor updateTaskMaintenanceProcessor,
            IPagedDataRequestFactory pagedDataRequestFactory,
            IAllTasksInquiryProcessor allTasksInquiryProcessor)
        {
            AddTaskMaintenanceProcessor = addTaskMaintenanceProcessor;
            TaskByIdInquiryProcessor = taskByIdInquiryProcessor;
            UpdateTaskMaintenanceProcessor = updateTaskMaintenanceProcessor;
            PagedDataRequestFactory = pagedDataRequestFactory;
            AllTasksInquiryProcessor = allTasksInquiryProcessor;
        }

        public IAddTaskMaintenanceProcessor AddTaskMaintenanceProcessor { get; private set; }
        public ITaskByIdInquiryProcessor TaskByIdInquiryProcessor { get; private set; }
        public IUpdateTaskMaintenanceProcessor UpdateTaskMaintenanceProcessor { get; private set; }
        public IPagedDataRequestFactory PagedDataRequestFactory { get; private set; }
        public IAllTasksInquiryProcessor AllTasksInquiryProcessor { get; private set; }
    }
}