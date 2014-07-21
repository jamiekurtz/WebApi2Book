// TasksControllerDependencyBlockMock.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using Moq;
using WebApi2Book.Data.QueryProcessors;
using WebApi2Book.Web.Api.Controllers.V1;
using WebApi2Book.Web.Api.InquiryProcessing;
using WebApi2Book.Web.Api.MaintenanceProcessing;

namespace WebApi2Book.Web.Api.Tests.Controllers.V1
{
    public class TasksControllerDependencyBlockMock : Mock<ITasksControllerDependencyBlock>
    {
        private Mock<IAddTaskMaintenanceProcessor> _addTaskMaintenanceProcessorMock;
        private Mock<IAllTasksInquiryProcessor> _allTasksInquiryProcessorMock;
        private Mock<IDeleteTaskQueryProcessor> _deleteTaskQueryProcessorMock;
        private Mock<IPagedDataRequestFactory> _pagedDataRequestFactoryMock;
        private Mock<ITaskByIdInquiryProcessor> _taskByIdInquiryProcessorMock;
        private Mock<IUpdateTaskMaintenanceProcessor> _updateTaskMaintenanceProcessorMock;

        public TasksControllerDependencyBlockMock()
        {
            Setup(x => x.AddTaskMaintenanceProcessor).Returns(AddTaskMaintenanceProcessorMock.Object);
            Setup(x => x.TaskByIdInquiryProcessor).Returns(TaskByIdInquiryProcessorMock.Object);
            Setup(x => x.UpdateTaskMaintenanceProcessor).Returns(UpdateTaskMaintenanceProcessorMock.Object);
            Setup(x => x.PagedDataRequestFactory).Returns(PagedDataRequestFactoryMock.Object);
            Setup(x => x.AllTasksInquiryProcessor).Returns(AllTasksInquiryProcessorMock.Object);
            Setup(x => x.DeleteTaskQueryProcessor).Returns(DeleteTaskQueryProcessorMock.Object);
        }

        public Mock<IAddTaskMaintenanceProcessor> AddTaskMaintenanceProcessorMock
        {
            get
            {
                return _addTaskMaintenanceProcessorMock ??
                       (_addTaskMaintenanceProcessorMock = new Mock<IAddTaskMaintenanceProcessor>());
            }
            set { _addTaskMaintenanceProcessorMock = value; }
        }

        public Mock<ITaskByIdInquiryProcessor> TaskByIdInquiryProcessorMock
        {
            get
            {
                return _taskByIdInquiryProcessorMock ??
                       (_taskByIdInquiryProcessorMock = new Mock<ITaskByIdInquiryProcessor>());
            }
            set { _taskByIdInquiryProcessorMock = value; }
        }

        public Mock<IUpdateTaskMaintenanceProcessor> UpdateTaskMaintenanceProcessorMock
        {
            get
            {
                return _updateTaskMaintenanceProcessorMock ??
                       (_updateTaskMaintenanceProcessorMock = new Mock<IUpdateTaskMaintenanceProcessor>());
            }
            set { _updateTaskMaintenanceProcessorMock = value; }
        }

        public Mock<IPagedDataRequestFactory> PagedDataRequestFactoryMock
        {
            get
            {
                return _pagedDataRequestFactoryMock ??
                       (_pagedDataRequestFactoryMock = new Mock<IPagedDataRequestFactory>());
            }
            set { _pagedDataRequestFactoryMock = value; }
        }

        public Mock<IAllTasksInquiryProcessor> AllTasksInquiryProcessorMock
        {
            get
            {
                return _allTasksInquiryProcessorMock ??
                       (_allTasksInquiryProcessorMock = new Mock<IAllTasksInquiryProcessor>());
            }
            set { _allTasksInquiryProcessorMock = value; }
        }

        public Mock<IDeleteTaskQueryProcessor> DeleteTaskQueryProcessorMock
        {
            get
            {
                return _deleteTaskQueryProcessorMock ??
                       (_deleteTaskQueryProcessorMock = new Mock<IDeleteTaskQueryProcessor>());
            }
            set { _deleteTaskQueryProcessorMock = value; }
        }
    }
}